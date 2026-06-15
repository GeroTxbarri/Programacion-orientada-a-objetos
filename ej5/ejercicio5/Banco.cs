using System;

public class Banco {

    private List<CuentaBancaria> cuentas;

    public Banco()
    {
        cuentas = new List<CuentaBancaria>();
    }
    public void agregarCuenta (CuentaBancaria cuenta){

        cuentas.Add(cuenta);

    }

    public void transferir (CuentaBancaria origen, CuentaBancaria destino, int monto){

        if (monto <= 0){

            Console.WriteLine( " Error: EL monto a tranferir no es positivo");
            return;
        }

        if( !cuentas.Contains(origen) || !cuentas.Contains(destino)){

            Console.WriteLine("Error : una de las cuentas no estan en el banco");
            return;
        }

        bool  extraccion_exitosa = origen.extraer(monto);

        if (extraccion_exitosa){
            destino.depositar(monto);
            Console.WriteLine ("Tranferencia exitosa");
        }
        else{
            Console.WriteLine("Error en la tranferencia");

        }
    }
}

