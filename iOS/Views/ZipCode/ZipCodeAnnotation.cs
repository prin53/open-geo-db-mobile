using System;
using CoreLocation;
using MapKit;
using OpenGeoDB.iOS.Extensions;
using OpenGeoDB.Models;
using UIKit;

namespace OpenGeoDB.iOS
{
    public class ZipCodeAnnotation : MKAnnotation
    {
        public ZipCodeAnnotation(LocationModel location, UIColor color)
        {
            Location = location ?? throw new ArgumentNullException(nameof(location));
            Color = color ?? throw new ArgumentNullException(nameof(color));
        }

        public LocationModel Location { get; }

        public UIColor Color { get; }

        public override CLLocationCoordinate2D Coordinate => Location.ToLocationCoordinate2D();
    }
}
