namespace vehiculos
{
    public class Camion : Ivehiculo
    {
        private int VelocidadMaxima = 30; 
        private int _posicion = 0;

        public void Mover(int tiempo)
        {
            _posicion += VelocidadMaxima * tiempo;
        }

        public int Posicion()
        {
            return _posicion;
        }

        public void ReiniciarPosicion()
        {
            _posicion = 0;
        }
    }
}