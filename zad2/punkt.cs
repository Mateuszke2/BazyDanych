using System;
namespace zad2{ 

    public class Punkt{
        
        public float x;
        public float y;
        
        // konstruktor
        public Punkt(float x, float y){
                this.x = x;
                this.y = y;
        }

        public override string ToString(){
                return $"X: {x}, Y: {y}";
        }

        public void Przesun(float przesuniecie_X, float przesuniecie_Y){
                this.x = this.x + przesuniecie_X;
                this.y = this.y + przesuniecie_Y;
        }
        

    }
}   