using System;

public class CuentaBancaria {

    private int saldo;

    public CuentaBancaria (){

        saldo = 0;
    }

    public void depositar(int saldo_a_depositar){

        if (saldo_a_depositar <= 0){
            Console.WriteLine( "error: El saldo a ingresar es negativo o 0");
            return;
        }
        saldo = saldo + saldo_a_depositar;
    }

    public virtual bool extraer(int saldo_a_extraer){
        saldo = saldo - saldo_a_extraer;
        return true;
    }

    
    public void mostrarSaldo(){

        Console.WriteLine ($"El saldo actual es {saldo}");
    }

    public int Devolversaldo(){
        return saldo;
    }

}