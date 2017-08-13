// WARNING
//
// This file has been generated automatically by Visual Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using Foundation;
using System.CodeDom.Compiler;

namespace OpenGeoDB.iOS.Views.ZipCode
{
	[Register ("MapTableViewCell")]
	partial class MapTableViewCell
	{
		[Outlet]
		MapKit.MKMapView _mapView { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (_mapView != null) {
				_mapView.Dispose ();
				_mapView = null;
			}
		}
	}
}
