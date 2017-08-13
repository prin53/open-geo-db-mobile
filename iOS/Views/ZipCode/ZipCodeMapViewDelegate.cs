using MapKit;

namespace OpenGeoDB.iOS.Views.ZipCode
{
    public class ZipCodeMapViewDelegate : MKMapViewDelegate
    {
        private const string AnnotationIdentifier = "Annotation";

        public override MKAnnotationView GetViewForAnnotation(MKMapView mapView, IMKAnnotation annotation)
        {
            var annotationView = mapView.DequeueReusableAnnotation(AnnotationIdentifier) as MKPinAnnotationView;

            if (annotationView == null)
            {
                annotationView = new MKPinAnnotationView(annotation, AnnotationIdentifier);
            }

            var zipCodeAnnotation = annotation as ZipCodeAnnotation;

            if (zipCodeAnnotation != null)
            {
                annotationView.PinTintColor = zipCodeAnnotation.Color;
            }

            return annotationView;
        }
    }
}
