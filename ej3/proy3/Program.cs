using System;

namespace Jugadores{
    public class Program{
        public static void Main(){
            IJugador jugador = new JugadorAmateur();
            jugador.Correr(30);
            IJugador jugador2 = new JugadorProfesional();
            jugador2.Correr(30);

        }
    }

}
