using System;

public class Cronometro{

    private int minutos;
    private  int segundos;

    public Cronometro(int segundos_inicial, int minutos_inicial){
        minutos =minutos_inicial;
        segundos = segundos_inicial;
    }
    public void mostrarTiempo(){
        Console.WriteLine( $"el tiempo del cronometro es {minutos}minutos , y {segundos} segundos");
    }
    public void reiniciar_cronometro(){
        minutos=0;
        segundos=0;
    }
    public void incrementar_tiempo(){
        if(segundos == 59){
            segundos =0;
            minutos++;
        }
        else {
            segundos++;
        }
    }

}