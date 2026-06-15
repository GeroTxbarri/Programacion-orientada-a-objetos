using Clinicamedica;
using Microsoft.EntityFrameworkCore;

namespace ClinicaMedica
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Ingrese el DNI del paciente: ");
            int dni = int.Parse(Console.ReadLine());

            var paciente = context.Pacientes.FirstOrDefault(p => p.Dni == dni);

            if(paciente != null){

                Console.WriteLine($"Paciente encontrado: {paciente.Nombre} {paciente.Apellido}");
                Console.WriteLine($"Teléfono: {paciente.Telefono} | Email: {paciente.Email}");

            
            var context = new ClinicaContext();

            var turnos = context.Turnos
                .Include(t => t.Paciente)
                .Include(t => t.Medico)
                .Include(t => t.Especialidad)
                .Include(t => t.Estado)
                .OrderBy(t => t.Fecha)
                .ThenBy(t => t.Hora)
                .ToList();

            foreach (var t in turnos)
                {

                Console.WriteLine($"{t},{t.Fecha} {t.Hora} | {t.Paciente.Nombre} {t.Paciente.Apellido} | {t.Medico.Nombre} {t.Medico.Apellido} | {t.Especialidad.Nombre} | {t.Estado.Descripcion}");
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
                Console.WriteLine("email : ");
                string email = Console.ReadLine();
                Console.WriteLine(" dia nacimiento: ");
                string nacimiento = Console.ReadLine();
    
                var nuevoPaciente = new Paciente
                {
                    Dni = dni,
                    Nombre = nombre,
                    Apellido = apellido,
                    Email= email,
                    Telefono = telefono,
                    Nacimiento = nacimiento;
                };
                context.Pacientes.Add(nuevoPaciente);
                context.SaveChanges();
                Console.WriteLine("Paciente registrado exitosamente.");
            }
        }
    }
}
