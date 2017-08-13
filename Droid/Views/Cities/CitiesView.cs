using System.Linq;
using Android.App;
using Android.OS;
using Android.Support.V7.Widget;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Droid.Support.V7.RecyclerView;
using MvvmCross.Droid.Views;
using OpenGeoDB.Droid.Widgets;
using OpenGeoDB.ViewModels.Cities;
using Android.Graphics.Drawables;
using Android.Graphics;

namespace OpenGeoDB.Droid.Views.Cities
{
    [Activity(Label = nameof(CitiesView))]
    public class CitiesView : MvxActivity
    {
        protected MvxRecyclerView RecyclerView { get; private set; }

        protected FastScrollerLayout FastScrollerLayout { get; private set; }

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            InitView();

            InitBindings();
        }

        protected string GetTitle(int position)
        {
            var item = RecyclerView.Adapter.GetItem(position) as CityItemViewModel;

            if (item == null)
            {
                return null;
            }

            return item.Name.FirstOrDefault().ToString();
        }

        protected void InitView()
        {
            SetContentView(Resource.Layout.ViewCities);

            RecyclerView = FindViewById<MvxRecyclerView>(Resource.Id.RecyclerView);
            FastScrollerLayout = FindViewById<FastScrollerLayout>(Resource.Id.FastScroller);

            RecyclerView.AddItemDecoration(new DividerItemDecoration(this, DividerItemDecoration.Vertical));
            RecyclerView.HasFixedSize = true;

            FastScrollerLayout.RecyclerView = RecyclerView;
            FastScrollerLayout.TitleFactory = GetTitle;
        }

        protected void InitBindings()
        {
            var set = this.CreateBindingSet<CitiesView, CitiesViewModel>();
            set.Bind(this).For(v => v.Title).ToLocalizationId("Title");
            set.Apply();
        }
    }
}
