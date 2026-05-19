using System;

namespace Formas {
    
    public class Circulo:interfaz{
        public int radio;
    

        public Circulo ( int radio){
            radioCirc = radio;
        }
        public float calcularArea(){
            return 3.14 * radioCirc * radioCirc ;
        }
        public float calcularPerimetro(){
            return 3.14*2*radioCirc;
        }


    }

}