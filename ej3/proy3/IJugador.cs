namespace Jugadores{
    public interface IJugador{

        public int tiempo {get;set;}
        public bool Cansado();
        public bool Correr(int minutos);
        public void Descansar(int minutos);
    }
}