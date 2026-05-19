using System;

public class CajaDeAhorro : CuentaBancaria{
    public override bool extraer (int saldo_a_extraer){

        int saldo = Devolversaldo();

        if (saldo - saldo_a_extraer <0){
            Console.WriteLine ( "Error: no se permite saldo negativo");
            return false;
        }
        else{
            return base.extraer(saldo_a_extraer);
        }

    }
        

}