using System;
using System.Text;
namespace zad2{ 

    public class Rysunek{
        public string nazwa;
        public List<Punkt> punkty;

        public Rysunek(string nazwa, List<Punkt> punkty){
            this.nazwa = nazwa;
            this.punkty = new List<Punkt>();
            foreach (Punkt punkt in punkty){
                this.punkty.Add(punkt);
            }
        }
        public override string ToString(){
            StringBuilder sb = new StringBuilder();
            sb.Append($"Nazwa: {nazwa}, Punkty: ");
            foreach (Punkt punkt in punkty){
                sb.Append($"|X: {punkt.x}, Y: {punkt.y}| ");
            }
            return sb.ToString();
                
        }
        public void PrzesunWszystkie(float X, float Y){
            foreach(Punkt punkt in this.punkty){
                punkt.Przesun(X,Y);
            }
        }

        public void UsunPunkt(float X, float Y){
            Punkt DoUsuniecia = this.punkty.Find(p => p.x == X && p.y == Y);
            if (DoUsuniecia != null) {
            this.punkty.Remove(DoUsuniecia);
            }
        }

        public void DodajPunkt(float X, float Y){
            Punkt DoDodania = new Punkt(X,Y);
            this.punkty.Add(DoDodania);
        }
    }

}