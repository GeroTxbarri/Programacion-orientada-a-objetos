using System;

    public class Program{

        static void Main () {
            CajaDeAhorro ahorro = new CajaDeAhorro();
            ahorro.depositar(1000);
            ahorro.extraer(400);
            ahorro.extraer(800); // debe rechazarse
            ahorro.mostrarSaldo(); // debe mostrar 600
            CuentaCorriente corriente = new CuentaCorriente(500); // descubierto:500    
            corriente.depositar(200);
            corriente.extraer(600); // queda en -400, es valido
            corriente.extraer(200); // supera el descubierto, debe rechazarse
            corriente.mostrarSaldo(); // debe mostrar -400

            Banco banco = new Banco();
            CajaDeAhorro ahorro2 = new CajaDeAhorro();
            CuentaCorriente corriente2 = new CuentaCorriente(500);
            banco.agregarCuenta(ahorro2);
            banco.agregarCuenta(corriente2);
            ahorro.depositar(1000);
            banco.transferir(ahorro2, corriente2, 300); // debe funcionar
            banco.transferir(ahorro2, corriente2, 900); // debe rechazarse, saldo insuficiente

        }


    }

