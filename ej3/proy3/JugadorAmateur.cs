namespace Jugadores{
    public class JugadorAmateur:IJugador{

        public int tiempo {get;set;}
        
        

        public bool Cansado(){

            return (tiempo>=20);
            
        }
        public bool Correr(int mins){
            if(Cansado()){
                Console.WriteLine("el jugador esta cansado");
                return false;
            }
            if (tiempo + mins >= 20){

                Console.WriteLine("el jugador se canso");
                tiempo = 20;
                return false;
            }
            if ( tiempo + mins <20){
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