
using System;

public class Program{
    static void Main (){

        Cronometro cronometro = new Cronometro(0,0);
        for (int i = 0; i < 5000; i++) {
            cronometro.incrementar_tiempo();
        }
        cronometro.mostrarTiempo();

    }
    

}

