using CoreLocation;
using OpenGeoDB.Models;
using OpenGeoDB.Services.Location;
using OpenGeoDB.iOS.Extensions;

namespace OpenGeoDB.iOS.Services.Location
{
    public class LocationService : ILocationService
    {
        public double GetDistance(LocationModel first, LocationModel second)
        {
            using (var firstLocation = first.ToLocation())
            {
                using (var secondLocation = second.ToLocation())
                {
                    return firstLocation.DistanceFrom(secondLocation);
                }
            }
        }
    }
}
