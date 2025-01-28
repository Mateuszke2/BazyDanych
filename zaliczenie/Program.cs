// GWIZDAK 252945, GRUPA A
using System;
using Db4objects.Db4o;
using Db4objects.Db4o.Query;
using System.Text;
namespace zaliczenie{

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
        public void Move(float przesuniecie_X, float przesuniecie_Y){
                this.x = this.x + przesuniecie_X;
                this.y = this.y + przesuniecie_Y;
        }
    }

    public class Luk:Figura{
        float promien;
        float kat;
        public Luk(float promien, float kat, Punkt punktOdniesienia) : base(typeof(Luk), punktOdniesienia){
            this.promien = promien;
            this.kat = kat;
        }
        public override string Draw(){
            string x = base.Draw();
            return x + ($"Promien = {this.promien}, Kat = {this.kat}");

        }
    }
    public class Prostokat:Figura{
        float dlugosc1;
        float dlugosc2;

        public Prostokat(float dlugosc1, float dlugosc2, Punkt punktOdniesienia) : base(typeof(Prostokat), punktOdniesienia){
        this.dlugosc1 = dlugosc1;
        this.dlugosc2 = dlugosc2;
        }
        public override string Draw(){
            string x = base.Draw();
            return x + ($"Dlugosc1 = {this.dlugosc1}, Dlugosc2 = {this.dlugosc2}");
        }
    }
    public class Figura{
        // public string nazwa;
        public Type typ_figury;
        public Punkt punkt_odniesienia;
        
        public Figura(Type typ_figury, Punkt punkt_odniesienia){
            this.typ_figury = typ_figury;
            this.punkt_odniesienia = punkt_odniesienia;
        }
        public virtual string Draw()
        {
            return ($"Typ Figury: {typ_figury.Name}, Punkt Odniesienia: {punkt_odniesienia}");
        }
        
    }

    public class Rysunek{
        public string Nazwa;
        public List<Figura> Figury;

        public Rysunek(string nazwa)
        {
            Nazwa = nazwa;
            Figury = new List<Figura>();
        }

        public void DodajFigure(Figura figura)
        {
            Figury.Add(figura);
        }

        public void UsunLuk(){
            Figura figuraDoUsuniecia = Figury.FirstOrDefault(f => f.typ_figury == typeof(Luk));

            if (figuraDoUsuniecia != null)
            {
                Figury.Remove(figuraDoUsuniecia);
                
            }
             else
        {
            Console.WriteLine("Nie znaleziono figury typu Luk w rysunku.");
        }
            
        }

        public void PrzesunWszystkie(float X, float Y){
            foreach(Figura fig in this.Figury){
                fig.punkt_odniesienia.Move(X,Y);
            }
        }
        public override string ToString()
        {
            // Console.WriteLine($"Rysunek: {Nazwa}");
            StringBuilder sb = new StringBuilder();
            sb.Append($"Rysunek: {Nazwa}");
            foreach (var figura in Figury){
                sb.Append(figura.Draw());
            }
            return sb.ToString();

        }
        // public override string ToString(){
        //     WyswietlFigury();
        // }
    }


    class Program
    {
        static void ListResult(IObjectSet result){
                Console.WriteLine(result.Count);
                foreach (object item in result){
                    Console.WriteLine(item);
                }
        }

        static void ListAllRysunek(IObjectContainer db){
                IObjectSet result = db.QueryByExample(typeof(Rysunek));
                ListResult(result);
        }

        static void Main(string[] args)
        {   
            // Type typ = typeof(Figura);
            // Console.WriteLine(typ);
            File.Delete("baza");
            using(IObjectContainer db = Db4oEmbedded.OpenFile("baza")){
                Console.WriteLine("Start1");
                Rysunek uran = new Rysunek("uran");
                Prostokat p1 = new Prostokat(9,12,new Punkt(5,8));
                Luk l1 = new Luk(6,55,new Punkt(8,11));
                uran.DodajFigure(p1);
                uran.DodajFigure(l1);
                db.Store(uran);
                db.Commit();
                db.Close();
                Console.WriteLine("Koniec1\n");
            }
            using(IObjectContainer db = Db4oEmbedded.OpenFile("baza")){
                Console.WriteLine("Start2");
                ListAllRysunek(db);
                Rysunek jowisz = new Rysunek("jowisz");
                Luk l2 = new Luk(8,120,new Punkt(2,4));
                jowisz.DodajFigure(l2);
                db.Store(jowisz);
                db.Commit();
                db.Close();
                Console.WriteLine("Koniec2\n");
            }
            using(IObjectContainer db = Db4oEmbedded.OpenFile("baza")){
                Console.WriteLine("Start3");
                ListAllRysunek(db);
                var rys = new Rysunek("uran");
                IObjectSet result = db.QueryByExample(rys);
                foreach(Rysunek rysunek in result){
                    rysunek.PrzesunWszystkie(5,6);
                    db.Ext().Store(rysunek, int.MaxValue);
                }
                
                db.Close();
                Console.WriteLine("Koniec3\n");
            }
            using(IObjectContainer db = Db4oEmbedded.OpenFile("baza")){
                Console.WriteLine("Start4");
                ListAllRysunek(db);
                var rys = new Rysunek("jowisz");
                IObjectSet result = db.QueryByExample(rys);
                Prostokat p3 = new Prostokat(5,4,new Punkt(3,1));
                foreach(Rysunek rysunek in result){
                    rysunek.DodajFigure(p3);
                    db.Ext().Store(rysunek, int.MaxValue);
                }
                db.Close();
                Console.WriteLine("Koniec4\n");
            }
            using(IObjectContainer db = Db4oEmbedded.OpenFile("baza")){
                Console.WriteLine("Start5");
                ListAllRysunek(db);
                var rys = new Rysunek("uran");
                IObjectSet result = db.QueryByExample(rys);
                foreach(Rysunek rysunek in result){
                    rysunek.UsunLuk();
                    db.Ext().Store(rysunek, int.MaxValue);
                }
                db.Close();
                Console.WriteLine("Koniec5\n");
            }
            using(IObjectContainer db = Db4oEmbedded.OpenFile("baza")){
                Console.WriteLine("Start6");
                ListAllRysunek(db);
                db.Close();
                Console.WriteLine("Koniec6\n");
            }
        }


    }
}
