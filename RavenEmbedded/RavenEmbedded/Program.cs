using System;
using System.Linq;
using Raven.Client.Documents;
using Raven.Client.Documents.Indexes;
using Raven.Client.Documents.Indexes.Spatial;
using Raven.Embedded;

namespace RavenEmbedded
{
    internal static class Program
    {
        private static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            
            EmbeddedServer.Instance.StartServer();
            EmbeddedServer.Instance.OpenStudioInBrowser();
            
            using (var store = EmbeddedServer.Instance.GetDocumentStore("Embedded"))
            {
                IndexCreation.CreateIndexes(
                    typeof(EmployeesByFirstAndLastName).Assembly,
                    store);

                IndexCreation.CreateIndexes(
                    typeof(EventsByCoordinates).Assembly,
                    store);

                using (var s = store.OpenSession())
                {
                    /*
                    s.Store(new Employee{FirstName = "Edu", LastName = "Edu"});
                    s.Store(new Employee{FirstName = "Edu", LastName = "Bob"});
                    s.Store(new Employee{FirstName = "Bob", LastName = "Edu"});
                    s.Store(new Employee{FirstName = "Bob", LastName = "Bob"});

                    s.SaveChanges();
                    
                    var results = s
                        .Query<Employee, EmployeesByFirstAndLastName>()
                        .Where(x => x.FirstName == "Bob" || x.LastName == "Bob")
                        .ToList();
                    */

#if false
                    s.Store(new Event{Name = "E", Latitude = 0.1, Longitude = 0.1,});
                    s.Store(new Event{Name = "D", Latitude = 0.11, Longitude = 0.11,});

                    s.SaveChanges();
#endif

                    /*
                    var results = s
                        .Query<Event, EventsByNameAndCoordinates>()
                        .Where(x => x.Name.Any())
                        //.Where(x => x.Latitude > 0 || x.Latitude > 0) exception
                        .ToList();
                    */
                    /*
                    var results = s
                        .Query<Event>()
                        .Spatial(
                            factory => factory.Point(x => x.Latitude, x => x.Longitude),
                            criteria => criteria.WithinRadius(0.002, 0.1, 0.1))
                        .ToList();
                    */
                    var results = s
                        .Query<Event, EventsByCoordinates>()
                        .Spatial(
                            "Coordinates",
                            criteria => criteria.WithinRadius(0.01, 0.1, 0.1, SpatialUnits.Kilometers))
                        .ToList();
                }
                
                Console.WriteLine("Press ENTER to quit this program (and close RavenDB)");
                Console.ReadLine();
            }
        }
    }
}
