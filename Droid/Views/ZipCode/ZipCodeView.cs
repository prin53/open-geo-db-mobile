using System.Collections.Generic;
using System.Linq;
using Android.App;
using Android.Gms.Maps;
using Android.Gms.Maps.Model;
using Android.OS;
using Android.Support.V7.Widget;
using Android.Views;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Droid.Support.V7.RecyclerView;
using MvvmCross.Droid.Views;
using OpenGeoDB.Models;
using OpenGeoDB.ViewModels.ZipCode;
using OpenGeoDB.ViewModels.ZipCodes;

namespace OpenGeoDB.Droid.Views.ZipCode
{
    [Activity(Label = nameof(ZipCodeView))]
    public class ZipCodeView : MvxActivity, IOnMapReadyCallback
    {
        private ICollection<Marker> _nearbyMarkers = new List<Marker>();
        private IEnumerable<ZipCodeItemViewModel> _nearby;
        private LocationModel _location;

        protected MvxRecyclerView RecyclerView { get; private set; }

        protected GoogleMap GoogleMap { get; private set; }

        protected MapFragment MapFragment => (MapFragment)FragmentManager.FindFragmentById(Resource.Id.MapFragment);

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

                ReloadMarkers();
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

                ReloadMarkers();
            }
        }

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            InitView();
        }

        protected static MarkerOptions CreateMarkerOptions(LocationModel location, bool selected = false)
        {
            var bitmapDescriptorFactory = selected ? BitmapDescriptorFactory.HueRed : BitmapDescriptorFactory.HueBlue;

            return new MarkerOptions()
                .SetPosition(new LatLng(location.Latitude, location.Longitude))
                .SetIcon(BitmapDescriptorFactory.DefaultMarker(bitmapDescriptorFactory));
        }

        protected static void AddNearbyMarkers(IEnumerable<ZipCodeItemViewModel> nearby, GoogleMap googleMap, ICollection<Marker> markers)
        {
            foreach (var zipCode in nearby)
            {
                markers.Add(googleMap.AddMarker(CreateMarkerOptions(zipCode.Location)));
            }
        }

        protected static void AddCurrentMarker(LocationModel location, GoogleMap googleMap, ICollection<Marker> markers)
        {
            markers.Add(googleMap.AddMarker(CreateMarkerOptions(location, true)));
        }

        protected static LatLngBounds GetBounds(IEnumerable<Marker> markers)
        {
            var builder = new LatLngBounds.Builder();

            foreach (var marker in markers)
            {
                builder.Include(marker.Position);
            }

            return builder.Build();
        }

        protected static void SetVisiblePosition(GoogleMap googleMap, IEnumerable<Marker> markers, int padding)
        {
            var bounds = GetBounds(markers);

            var cameraUpdate = CameraUpdateFactory.NewLatLngBounds(bounds, padding);

            googleMap.AnimateCamera(cameraUpdate);
        }

        protected void InitView()
        {
            SetContentView(Resource.Layout.ViewZipCode);

            ActionBar.SetDisplayHomeAsUpEnabled(true);
            ActionBar.SetDisplayShowHomeEnabled(true);
            ActionBar.SetDisplayShowHomeEnabled(false);

            RecyclerView = FindViewById<MvxRecyclerView>(Resource.Id.RecyclerView);

            RecyclerView.AddItemDecoration(new DividerItemDecoration(this, DividerItemDecoration.Vertical));
            RecyclerView.HasFixedSize = true;

            MapFragment.GetMapAsync(this);
        }

        protected void InitBindings()
        {
            var set = this.CreateBindingSet<ZipCodeView, ZipCodeViewModel>();
            set.Bind(this).For(v => v.Title).ToLocalizationId("Title");
            set.Bind(this).For(v => v.Nearby).To(vm => vm.Nearby);
            set.Bind(this).For(v => v.Location).To(vm => vm.Location);
            set.Apply();
        }

        protected void ReloadMarkers()
        {
            GoogleMap.Clear();
            _nearbyMarkers.Clear();

            if (Nearby == null || !Nearby.Any() || Location == null)
            {
                return;
            }

            AddNearbyMarkers(Nearby, GoogleMap, _nearbyMarkers);

            AddCurrentMarker(Location, GoogleMap, _nearbyMarkers);

            SetVisiblePosition(
                GoogleMap,
                _nearbyMarkers,
                (int)Resources.GetDimension(Resource.Dimension.PaddingLarge)
            );
        }

        public void OnMapReady(GoogleMap googleMap)
        {
            GoogleMap = googleMap;

            InitBindings();
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            switch (item.ItemId)
            {
                case Android.Resource.Id.Home:
                    OnBackPressed();
                    return true;
                default:
                    return base.OnOptionsItemSelected(item);
            }
        }
    }
}
