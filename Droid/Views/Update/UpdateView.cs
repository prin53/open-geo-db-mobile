using Android.App;
using Android.Content.PM;
using MvvmCross.Droid.Views;

namespace OpenGeoDB.Droid.Views.Update
{
    [Activity(
        Theme = "@style/OpenGeoDB.Theme.NoActionBar",
        Label = nameof(UpdateView),
        NoHistory = true,
        ScreenOrientation = ScreenOrientation.Portrait
    )]
    public class UpdateView : MvxActivity
    {
        protected override void OnCreate(Android.OS.Bundle bundle)
        {
            base.OnCreate(bundle);

            SetContentView(Resource.Layout.ViewUpdate);
        }
    }
}
