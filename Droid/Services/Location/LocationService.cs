using System.Linq;
using OpenGeoDB.Models;
using OpenGeoDB.Services.Location;
using AndroidLocation = Android.Locations.Location;

namespace OpenGeoDB.Droid.Services.Location
{
    public class LocationService : ILocationService
    {
        public double GetDistance(LocationModel first, LocationModel second)
        {
            var results = new float[4];

            AndroidLocation.DistanceBetween(
                first.Latitude, 
                first.Longitude, 
                second.Latitude, 
                second.Longitude, 
                results
            );

            return results.First();
        }
    }
}
