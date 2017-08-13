using System;
using CoreLocation;
using OpenGeoDB.Models;

namespace OpenGeoDB.iOS.Extensions
{
    public static class LocationModelExtensions
    {
        public static CLLocationCoordinate2D ToLocationCoordinate2D(this LocationModel location)
        {
            if (location == null)
            {
                throw new ArgumentNullException(nameof(location));
            }

            return new CLLocationCoordinate2D(location.Latitude, location.Longitude);
        }

        public static CLLocation ToLocation(this LocationModel location)
        {
            if (location == null)
            {
                throw new ArgumentNullException(nameof(location));
            }

            return new CLLocation(location.Latitude, location.Longitude);
        }
    }
}
