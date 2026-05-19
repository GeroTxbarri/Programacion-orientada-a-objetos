namespace Jugadores{
    public class JugadorProfesional:IJugador{
        public int tiempo {get;set;}
       
        public bool Cansado(){

            return (tiempo>=40);
            
        }
        public bool Correr(int mins){
            if(Cansado()){
                Console.WriteLine("el jugador esta cansado");
                return false;
            }
            if (tiempo + mins >= 40){

                Console.WriteLine("el jugador se canso");
                tiempo = 40;
                return false;
            }
            if ( tiempo + mins <40){
                Console.WriteLine("el jugador no se canso");
                tiempo = tiempo + mins;
                return true;
            }
            return false;
            
        }
        public void Descansar(int mins){
            tiempo = tiempo - mins;

        }
        

    }


}