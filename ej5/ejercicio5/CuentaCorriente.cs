using System;

public class CuentaCorriente : CuentaBancaria{

    private int limite ;

    public CuentaCorriente(int limite){
        this.limite = limite;
    }

    public override bool extraer (int saldo_a_extraer){

        int saldo = Devolversaldo();

        if (saldo - saldo_a_extraer <limite){
            Console.WriteLine ( "Error: no se permite saldo menor que el limite");
            return false;
        }
        else{
            return base.extraer(saldo_a_extraer);
        }

    }
        

}