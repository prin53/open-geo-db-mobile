using Android.App;
using Android.OS;
using Android.Support.V7.Widget;
using Android.Views;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Droid.Support.V7.RecyclerView;
using MvvmCross.Droid.Views;
using OpenGeoDB.ViewModels.ZipCodes;

namespace OpenGeoDB.Droid.Views.ZipCodes
{
    [Activity(Label = nameof(ZipCodesView))]
    public class ZipCodesView : MvxActivity
    {
        protected MvxRecyclerView RecyclerView { get; private set; }

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            InitView();

            InitBindings();
        }

        protected void InitView()
        {
            SetContentView(Resource.Layout.ViewZipCodes);

            ActionBar.SetDisplayHomeAsUpEnabled(true);
            ActionBar.SetDisplayShowHomeEnabled(true);
            ActionBar.SetDisplayShowHomeEnabled(false);

            RecyclerView = FindViewById<MvxRecyclerView>(Resource.Id.RecyclerView);

            RecyclerView.AddItemDecoration(new DividerItemDecoration(this, DividerItemDecoration.Vertical));
            RecyclerView.HasFixedSize = true;
        }

        protected void InitBindings()
        {
            var set = this.CreateBindingSet<ZipCodesView, ZipCodesViewModel>();
            set.Bind(this).For(v => v.Title).ToLocalizationId("Title");
            set.Apply();
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
