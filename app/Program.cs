using System;
using Db4objects.Db4o;
using Db4objects.Db4o.Query;

namespace app{
    class Program{
        static void ListResult(IObjectSet result){
                Console.WriteLine(result.Count);
                foreach (object item in result){
                    Console.WriteLine(item);
                }
        }
        static void RetriveAllZawodniks(IObjectContainer db){
                IObjectSet result = db.QueryByExample(typeof(Zawodnik));
                ListResult(result);
        }
                // ListResult(result);}

        static void Main(){
            File.Delete("baza");
            // var zawodnik1 = new Zawodnik("angel", 2, 2000);
            // Console.WriteLine(zawodnik1.ToString());
            using(IObjectContainer db = Db4oEmbedded.OpenFile("baza")){
                var zawodnik1 = new Zawodnik("Kowalski", 1984, 100);
                var zawodnik2 = new Zawodnik("Nowak", 1988, 97);
                var zawodnik3 = new Zawodnik("Grabowski", 1982, 100);
                // var zawodnik_template = new Zawodnik(null,0,100);

                db.Store(zawodnik1);
                db.Store(zawodnik2);
                db.Store(zawodnik3);

                // QUERY BY EXAMPLE
                var zawodnik_template = new Zawodnik(null,0,100);
                IObjectSet result = db.QueryByExample(zawodnik_template);
                ListResult(result);

                var zawodnik_Kowalski = new Zawodnik("Kowalski",0,0);
                IObjectSet result_filter = db.QueryByExample(zawodnik_Kowalski);        
                // tu powinno być foreach
                var found = (Zawodnik)result_filter.Next();
                found.AddPoints(10);
                db.Store(found);
                RetriveAllZawodniks(db);

                Console.WriteLine('\n');
                var zawodnik4 = new Zawodnik("Andrzejewski",1989,98);
                db.Store(zawodnik4);
                RetriveAllZawodniks(db);

                Console.WriteLine('\n');
                
                db.Commit();

                db.Close();
                
                // Console.WriteLine("Stored {0}", zawodnik1.ToString());

                // Console.WriteLine(zawodnik1.ToString());
            }

            using(IObjectContainer db = Db4oEmbedded.OpenFile("baza")){
                // NATIVE QUERY
                IList <Zawodnik> zawodnicy = db.Query <Zawodnik> (delegate(Zawodnik zawodnik){
                    return (zawodnik.ilosc_punktow > 100 
                            || zawodnik.rok_urodzenia > 1984 );
                    });
                Console.WriteLine(zawodnicy.Count());    
                foreach (object item in zawodnicy){
                    Console.WriteLine(item);
                }

                Console.WriteLine("\nSODA");
                IQuery query = db.Query();
                query.Constrain(typeof(Zawodnik));
                IObjectSet result = query.Execute();
                ListResult(result);
                
                query.Descend("nazwisko").Constrain("Kowalski");
                IObjectSet result1 = query.Execute();
                ListResult(result1);

                Console.WriteLine("\n13");
                IQuery query99 = db.Query();
                query99.Constrain(typeof(Zawodnik));
                query99.Descend("ilosc_punktow").Constrain(99).Greater();
                IObjectSet result99 = query99.Execute();
                ListResult(result99);




                db.Commit();
                db.Close();
            


            }
            
            // var zawodnik1 = new Zawodnik("angel", 2, 2000);
            // Console.WriteLine(zawodnik1.ToString());
        }
    }
}