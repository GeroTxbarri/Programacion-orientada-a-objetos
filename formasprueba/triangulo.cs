using System;

namespace Formas {
    
    public class Triangulo:interfaz{
        public int lado1;
        public int lado2;
        public int lado3;
        public int altura;

        public Triangulo ( int base1,int base2,int base4){
            base1T= base1;
            base2T = base2;
            base3T = base3;
            alturaT = altura;
        }
        public float calcularArea(){
            return (lado1T * alturaT )/2;

        }
        public float calcularPerimetro(){
            return base1T + base2T + base3T;
        }


    }

}