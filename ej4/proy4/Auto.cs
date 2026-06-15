namespace vehiculos
{
    public class Auto : Ivehiculo
    {
        private int _velocidadMaxima;
        private int _posicion = 0;

    
        public Auto(int velocidad = 40)
        {
            _velocidadMaxima = velocidad;
        }

        public void Mover(int tiempo)
        {
            _posicion += _velocidadMaxima * tiempo;
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
