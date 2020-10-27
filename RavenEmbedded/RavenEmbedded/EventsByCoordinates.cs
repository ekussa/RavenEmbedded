using System.Linq;
using Raven.Client.Documents.Indexes;

namespace RavenEmbedded
{
    public class EventsByCoordinates : AbstractIndexCreationTask<Event>
    {
        public EventsByCoordinates()
        {
            Map = events => from e in events
                select new
                {
                    Coordinates = CreateSpatialField(e.Latitude, e.Longitude)
                };
            
            Spatial("Coordinates", factory => factory.Cartesian.BoundingBoxIndex());
        }
    }
}
