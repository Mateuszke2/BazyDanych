using System;
using Db4objects.Db4o;
using Db4objects.Db4o.Query;

namespace zad2{
    class Program{

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

        static void ListRysunek(IObjectContainer db, string nazwa){
                var rys = new Rysunek(nazwa,[]);
                IObjectSet result = db.QueryByExample(rys);
                ListResult(result);
        }

        static void Main(){
            File.Delete("baza");
            // var zawodnik1 = new Zawodnik("angel", 2, 2000);
            // Console.WriteLine(zawodnik1.ToString());
            using(IObjectContainer db = Db4oEmbedded.OpenFile("baza")){
                Console.WriteLine("Start 1");
                Punkt p1 = new Punkt(0,1);
                Punkt p2 = new Punkt(3,4);
                var rys1 = new Rysunek("rys1",[p1,p2]);
                db.Store(rys1);
                db.Commit();
                ListAllRysunek(db);
                db.Close();
                Console.WriteLine("Koniec 1\n");
            }
            using(IObjectContainer db = Db4oEmbedded.OpenFile("baza")){
                Console.WriteLine("Start 2");
                ListAllRysunek(db);
                // ListRysunek(db, "rys1");
                Punkt p3 = new Punkt(0,0);
                var rys2 = new Rysunek("rys2",[p3]);
                db.Store(rys2);
                db.Commit();
                db.Close();
                Console.WriteLine("Koniec 2\n");
            }
            using(IObjectContainer db = Db4oEmbedded.OpenFile("baza")){
                Console.WriteLine("Start 3");
                ListAllRysunek(db);
                var rys = new Rysunek("rys1",[]);
                IObjectSet result = db.QueryByExample(rys);
                foreach(Rysunek rysunek in result){
                    rysunek.PrzesunWszystkie(2,1);
                    db.Ext().Store(rysunek, int.MaxValue); //!!!!!!!!!!!!!!!!!!
                }
                // db.Commit();
                ListAllRysunek(db);
                db.Close();
                Console.WriteLine("Koniec 3");
            }
            using(IObjectContainer db = Db4oEmbedded.OpenFile("baza")){
                Console.WriteLine("Start 4");
                ListAllRysunek(db);
                var rys = new Rysunek("rys1",[]);
                IObjectSet result = db.QueryByExample(rys);
                foreach(Rysunek rysunek in result){
                    rysunek.UsunPunkt(2,2);
                    db.Ext().Store(rysunek, int.MaxValue);
                }
                
                db.Close();
                Console.WriteLine("Koniec 4");
            }

            using(IObjectContainer db = Db4oEmbedded.OpenFile("baza")){
                Console.WriteLine("Start 5");
                ListAllRysunek(db);
                var rys = new Rysunek("rys2",[]);
                IObjectSet result = db.QueryByExample(rys);
                foreach(Rysunek rysunek in result){
                    rysunek.DodajPunkt(9,10);
                    db.Ext().Store(rysunek, int.MaxValue);
                }
                db.Close();
                Console.WriteLine("Koniec 5");
            }

            using(IObjectContainer db = Db4oEmbedded.OpenFile("baza")){
                Console.WriteLine("Start 6");
                ListAllRysunek(db);
                db.Close();
                Console.WriteLine("Koniec 6");
            }

        }
    }
}