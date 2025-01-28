using System;
namespace app{ 
       class Zawodnik{
            public string nazwisko;
            public int ilosc_punktow;
            public int rok_urodzenia;

            public Zawodnik(string nazwisko, int rok_urodzenia, int ilosc_punktow){
                this.nazwisko = nazwisko;
                this.ilosc_punktow=ilosc_punktow;
                this.rok_urodzenia=rok_urodzenia;
            }

            public override string ToString()
            {
                return $"Zawodnik: {nazwisko}, Punkty: {ilosc_punktow}, Rok urodzenia: {rok_urodzenia}";
            }

            public void AddPoints(int ile_dodac){
                this.ilosc_punktow = this.ilosc_punktow + ile_dodac;
            }
        }
        
    }