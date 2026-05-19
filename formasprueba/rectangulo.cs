using System;

namespace Formas {
    
    public class Rectangulo:interfaz{
        public int baseR;
        public int altura;

        public Rectangulo ( int baseR, int altura){
            baseRect = base;
            alturaRect = altura;
        }
        public float calcularArea(){
            return baseRect * alturaRect;
        }
        public float calcularPerimetro(){
            return 2*baseRect + 2* alturaRect;
        }


    }

}