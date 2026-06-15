namespace vehiculos
{
    public class Carrera
    {
        private Ivehiculo _vehiculo1;
        private Ivehiculo _vehiculo2;
        private string _nombreV1;
        private string _nombreV2;

        public Carrera(Ivehiculo vehiculo1, string nombreV1, Ivehiculo vehiculo2, string nombreV2)
        {
            _vehiculo1 = vehiculo1;
            _nombreV1  = nombreV1;
            _vehiculo2 = vehiculo2;
            _nombreV2  = nombreV2;
        }

        public void Competir(int segundos)
        {
            _vehiculo1.ReiniciarPosicion();
            _vehiculo2.ReiniciarPosicion();

            _vehiculo1.Mover(segundos);
            _vehiculo2.Mover(segundos);

            int pos1 = _vehiculo1.Posicion();
            int pos2 = _vehiculo2.Posicion();

            Console.WriteLine($"\nResultado de la carrera de {segundos} segundos");
            Console.WriteLine($"  {_nombreV1}: {pos1} metros");
            Console.WriteLine($"  {_nombreV2}: {pos2} metros");

            if (pos1 > pos2)
                Console.WriteLine($"  Ganador: {_nombreV1}");
            else if (pos2 > pos1)
                Console.WriteLine($"  Ganador: {_nombreV2}");
            else
                Console.WriteLine("empate");
        }
    }
}
