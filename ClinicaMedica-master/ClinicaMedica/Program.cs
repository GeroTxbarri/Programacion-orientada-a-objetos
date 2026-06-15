using Clinicamedica;
using Microsoft.EntityFrameworkCore;

namespace ClinicaMedica
{
    internal class Program
    {
        // Convierte el número de día de semana a su nombre en español
        static string NombreDia(int dia) => dia switch
        {
            1 => "Lunes",
            2 => "Martes",
            3 => "Miércoles",
            4 => "Jueves",
            5 => "Viernes",
            6 => "Sábado",
            7 => "Domingo",
            _ => "Desconocido"
        };

        static void Main(string[] args)
        {
            var context = new ClinicaContext();

            // consigna 1:

            Console.Write("Ingrese el DNI del paciente: ");
            int dni = int.Parse(Console.ReadLine());

            // FirstOrDefault busca el paciente por DNI. Devuelve null si no existe.
            var paciente = context.Pacientes.FirstOrDefault(p => p.Dni == dni);

            if(paciente != null){

                Console.WriteLine($"\nPaciente encontrado: {paciente.Nombre} {paciente.Apellido}");
                Console.WriteLine($"Teléfono: {paciente.Telefono} | Email: {paciente.Email}");

                // Traer sus turnos filtrando por DNI con .Where(t => t.Dni == dni)
                // .Include(...) carga las relaciones (Medico, Especialidad, Estado)
                // para poder acceder a t.Medico.Nombre, t.Especialidad.Nombre, etc.
                var turnos = context.Turnos
                    .Include(t => t.Medico)
                    .Include(t => t.Especialidad)
                    .Include(t => t.Estado)
                    .Where(t => t.Dni == dni)
                    .OrderBy(t => t.Fecha)
                    .ThenBy(t => t.Hora)
                    .ToList();

                if (turnos.Count > 0)
                {
                    Console.WriteLine("\nTurnos registrados:");

                    // Mostramos los turnos numerados [1], [2], etc.
                    for (int i = 0; i < turnos.Count; i++)
                    {
                        var t = turnos[i];
                        Console.WriteLine($"  [{i + 1}] {t.Fecha} {t.Hora} | Dr. {t.Medico.Nombre} {t.Medico.Apellido} | {t.Especialidad.Nombre} | Estado: {t.Estado.Descripcion}");
                    }

                    Console.Write("\n¿Desea cancelar algún turno? (s/n): ");
                    if (Console.ReadLine()?.Trim().ToLower() == "s")
                    {
                        Console.Write("Ingrese el número del turno a cancelar: ");

                        if (int.TryParse(Console.ReadLine(), out int numTurno)
                            && numTurno >= 1 && numTurno <= turnos.Count)
                        {
                            var turnoCancelar = turnos[numTurno - 1];

                            // Buscamos el estado "cancelado" en la tabla estado de la BD
                            var estadoCancelado = context.Estados
                                .FirstOrDefault(e => e.Descripcion.ToLower() == "cancelado");

                            if (estadoCancelado != null)
                            {
                                turnoCancelar.IdEstado = estadoCancelado.IdEstado;
                                context.SaveChanges();
                                Console.WriteLine("Turno cancelado exitosamente.");
                            }
                            else
                            {
                                Console.WriteLine("Error: no se encontró el estado 'cancelado' en la base de datos.");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Número de turno inválido.");
                        }

                        return; // Terminamos el flujo luego de cancelar
                    }
                }
                else
                {
                    Console.WriteLine("El paciente no tiene turnos registrados.");
                }
            }
            else
            {
                Console.WriteLine("Paciente no encontrado. Registrando nuevo paciente...");
                Console.Write("Nombre: ");
                string nombre = Console.ReadLine();
                Console.Write("Apellido: ");
                string apellido = Console.ReadLine();
                Console.Write("Telefono: ");
                string telefono = Console.ReadLine();
                Console.Write("Email: ");
                string email = Console.ReadLine();
                Console.Write("Fecha de nacimiento (YYYY-MM-DD): ");
                string fechaNac = Console.ReadLine();

                // Creamos el objeto Paciente y lo agregamos a la BD
                // FechaNacimiento es el nombre correcto del campo según Paciente.cs
                paciente = new Paciente
                {
                    Dni = dni,
                    Nombre = nombre,
                    Apellido = apellido,
                    Email= email,
                    Telefono = telefono,
                    FechaNacimiento = fechaNac
                };

                context.Pacientes.Add(paciente);
                context.SaveChanges(); // Ejecuta el INSERT en la BD
                Console.WriteLine("Paciente registrado exitosamente.");
            }

            // ══════════════════════════════════════════════════
            // PASO 2 — Seleccionar especialidad
            // ══════════════════════════════════════════════════

            // Traemos TODAS las especialidades de la tabla "especialidad"
            // Equivale a: SELECT * FROM especialidad
            var especialidades = context.Especialidades.ToList();

            if (especialidades.Count == 0)
            {
                Console.WriteLine("No hay especialidades disponibles en el sistema.");
                return;
            }

            Console.WriteLine("\nEspecialidades disponibles:");

            // i+1 porque las listas son base 0 internamente, pero mostramos desde 1 al usuario
            for (int i = 0; i < especialidades.Count; i++)
                Console.WriteLine($"  [{i + 1}] {especialidades[i].Nombre} (duración: {especialidades[i].DuracionTurnoMin} min)");

            Console.Write("Seleccione una especialidad: ");

            // Validamos que el número ingresado sea válido y esté dentro del rango
            if (!int.TryParse(Console.ReadLine(), out int numEsp)
                || numEsp < 1 || numEsp > especialidades.Count)
            {
                Console.WriteLine("Selección inválida.");
                return;
            }

            // numEsp-1 convierte de base-1 (lo que vio el usuario) a base-0 (índice de lista)
            var especialidadElegida = especialidades[numEsp - 1];
            Console.WriteLine($"Especialidad seleccionada: {especialidadElegida.Nombre}");

            // ══════════════════════════════════════════════════
            // PASO 3 — Seleccionar médico
            // ══════════════════════════════════════════════════

            // La relación entre médico y especialidad NO es directa:
            // pasa por la tabla Disponibilidad (Matricula + IdEspecialidad + DiaSemana)
            // Entonces: buscamos todas las disponibilidades de esa especialidad
            // e Include(d => d.Medico) carga el objeto Medico de cada fila
            var disponibilidades = context.Disponibilidades
                .Include(d => d.Medico)
                .Where(d => d.IdEspecialidad == especialidadElegida.IdEspecialidad
                         && d.Medico.Activo == 1)
                .ToList();

            // Un médico puede atender varios días → aparece varias veces en disponibilidades
            // DistinctBy(m => m.Matricula) elimina los duplicados dejando uno por médico
            var medicos = disponibilidades
                .Select(d => d.Medico)
                .DistinctBy(m => m.Matricula)
                .ToList();

            if (medicos.Count == 0)
            {
                Console.WriteLine("No hay médicos disponibles para esa especialidad.");
                return;
            }

            Console.WriteLine($"\nMédicos que atienden {especialidadElegida.Nombre}:");
            for (int i = 0; i < medicos.Count; i++)
                Console.WriteLine($"  [{i + 1}] Dr. {medicos[i].Nombre} {medicos[i].Apellido}");

            Console.Write("Seleccione un médico: ");

            if (!int.TryParse(Console.ReadLine(), out int numMed)
                || numMed < 1 || numMed > medicos.Count)
            {
                Console.WriteLine("Selección inválida.");
                return;
            }

            var medicoElegido = medicos[numMed - 1];
            Console.WriteLine($"Médico seleccionado: Dr. {medicoElegido.Nombre} {medicoElegido.Apellido}");

            // ══════════════════════════════════════════════════
            // PASO 4 — Mostrar disponibilidad e ingresar fecha/hora
            // ══════════════════════════════════════════════════

            // Reutilizamos la lista 'disponibilidades' ya cargada en el Paso 3
            // Solo filtramos por la matrícula del médico elegido — sin nueva consulta a la BD
            var dispMedico = disponibilidades
                .Where(d => d.Matricula == medicoElegido.Matricula)
                .OrderBy(d => d.DiaSemana)
                .ToList();

            Console.WriteLine($"\nDisponibilidad de Dr. {medicoElegido.Nombre} {medicoElegido.Apellido} — {especialidadElegida.Nombre}:");
            foreach (var d in dispMedico)
                Console.WriteLine($"  {NombreDia(d.DiaSemana)}: {d.HoraInicio} a {d.HoraFin}");

            // Pedimos la fecha deseada en formato YYYY-MM-DD
            Console.Write("\nIngrese la fecha del turno (YYYY-MM-DD): ");
            string fecha = Console.ReadLine() ?? "";

            // Pedimos la hora deseada en formato HH:MM
            Console.Write("Ingrese la hora del turno (HH:MM): ");
            string hora = Console.ReadLine() ?? "";

            // ══════════════════════════════════════════════════
            // PASO 5 — Confirmar y registrar el turno
            // ══════════════════════════════════════════════════

            // Mostramos un resumen completo para que el usuario verifique antes de confirmar
            Console.WriteLine("\n══ Resumen del turno ══════════════════════════");
            Console.WriteLine($"  Paciente    : {paciente.Nombre} {paciente.Apellido} (DNI {paciente.Dni})");
            Console.WriteLine($"  Médico      : Dr. {medicoElegido.Nombre} {medicoElegido.Apellido}");
            Console.WriteLine($"  Especialidad: {especialidadElegida.Nombre}");
            Console.WriteLine($"  Fecha       : {fecha}");
            Console.WriteLine($"  Hora        : {hora}");
            Console.WriteLine("═══════════════════════════════════════════════");

            Console.Write("¿Confirma el turno? (s/n): ");
            if (Console.ReadLine()?.Trim().ToLower() != "s")
            {
                Console.WriteLine("Turno no confirmado. Operación cancelada.");
                return;
            }

            // Buscamos el estado "reservado" en la tabla estado de la BD
            // No hardcodeamos el id (ej: 1) porque puede variar según la BD
            var estadoReservado = context.Estados
                .FirstOrDefault(e => e.Descripcion.ToLower() == "reservado");

            if (estadoReservado == null)
            {
                Console.WriteLine("Error: no se encontró el estado 'reservado' en la base de datos.");
                return;
            }

            // Creamos el objeto Turno con todos los datos recolectados en los pasos anteriores
            var nuevoTurno = new Turno
            {
                Dni            = paciente.Dni,
                Matricula      = medicoElegido.Matricula,
                IdEspecialidad = especialidadElegida.IdEspecialidad,
                Fecha          = fecha,
                Hora           = hora,
                IdEstado       = estadoReservado.IdEstado
            };

            // Add() marca el objeto para inserción — SaveChanges() ejecuta el INSERT en la BD
            context.Turnos.Add(nuevoTurno);
            context.SaveChanges();
            Console.WriteLine("\n¡Turno registrado exitosamente! Hasta pronto.");
        }
    }
}
