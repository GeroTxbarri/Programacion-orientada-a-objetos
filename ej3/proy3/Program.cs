using System;

namespace Jugadores{
    public class Program{
        public static void Main(){
            IJugador jugador = new JugadorAmateur();
            jugador.Correr(30);
            jugador.Correr(1);
            jugador.Descansar(5);
            jugador.Correr(4);
            IJugador jugador2 = new JugadorProfesional();
            jugador2.Correr(30);
            jugador2.Correr(9);
            jugador2.Correr(2);
            jugador2.Descansar(2);
            jugador2.Correr(1);

        }
    }

}
