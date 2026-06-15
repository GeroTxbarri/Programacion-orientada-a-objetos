namespace vehiculos{

    public interface Ivehiculo{

        public void Mover(int tiempo);
        public int Posicion();
        public void ReiniciarPosicion();
    }
}