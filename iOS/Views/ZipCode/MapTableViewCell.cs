using System;
using System.Collections.Generic;
using System.Linq;
using MapKit;
using OpenGeoDB.iOS.Visual;
using OpenGeoDB.Models;
using OpenGeoDB.ViewModels.ZipCodes;
using UIKit;

namespace OpenGeoDB.iOS.Views.ZipCode
{
    public partial class MapTableViewCell : UITableViewCell
    {
        private IEnumerable<ZipCodeItemViewModel> _nearby;
        private LocationModel _location;

        public MapTableViewCell(IntPtr handle) : base(handle)
        {
            /* Required constructor */
        }

        public IEnumerable<ZipCodeItemViewModel> Nearby
        {
            get => _nearby;
            set
            {
                if (ReferenceEquals(_nearby, value))
                {
                    return;
                }

                _nearby = value;

                ReloadPins();
            }
        }

        public LocationModel Location
        {
            get => _location;
            set
            {
                if (ReferenceEquals(_location, value))
                {
                    return;
                }

                _location = value;

                ReloadPins();
            }
        }

        protected void ReloadPins()
        {
            _mapView.RemoveAnnotations(_mapView.Annotations);

            if (Nearby == null || !Nearby.Any() || Location == null)
            {
                return;
            }

            AddNearbyPins(Nearby, _mapView);

            AddCurrentPin(Location, _mapView);

            SetVisiblePosition(_mapView);
        }

        protected static void AddNearbyPins(IEnumerable<ZipCodeItemViewModel> nearby, MKMapView mapView)
        {
            var annotations = nearby.Select(item => new ZipCodeAnnotation(item.Location, Palette.Tint))
                                    .ToArray();

            mapView.AddAnnotations(annotations);
        }

        protected static void AddCurrentPin(LocationModel location, MKMapView mapView)
        {
            mapView.AddAnnotation(new ZipCodeAnnotation(location, Palette.PinCurrent));
        }

        protected static void SetVisiblePosition(MKMapView mapView)
        {
            var mapRect = MKMapRect.Null;

            foreach (var annotation in mapView.Annotations)
            {
                var mapPoint = MKMapPoint.FromCoordinate(annotation.Coordinate);
                var mapPointRect = new MKMapRect(mapPoint.X, mapPoint.Y, .1f, .1f);
                mapRect = MKMapRect.Union(mapRect, mapPointRect);
            }

            mapRect = mapView.MapRectThatFits(mapRect, new UIEdgeInsets(
                Dimensions.PaddingLarge,
                Dimensions.PaddingMedium,
                Dimensions.PaddingLarge,
                Dimensions.PaddingMedium
            ));

            mapView.SetVisibleMapRect(mapRect, true);
        }

        public override void AwakeFromNib()
        {
            base.AwakeFromNib();

            SelectionStyle = UITableViewCellSelectionStyle.None;

            _mapView.Delegate = new ZipCodeMapViewDelegate();
        }
    }
}
