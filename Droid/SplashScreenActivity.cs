using Android.App;
using Android.Content.PM;
using MvvmCross.Droid.Views;

namespace OpenGeoDB.Droid
{
    [Activity(
        Theme = "@style/OpenGeoDB.Theme.NoActionBar",
        MainLauncher = true,
        Icon = "@mipmap/icon",
        Label = "@string/AppName",
        NoHistory = true,
        ScreenOrientation = ScreenOrientation.Portrait
    )]
    public class SplashScreenActivity : MvxSplashScreenActivity
    {
        public SplashScreenActivity() : base(Resource.Layout.ActivitySplashScreen)
        {
            /* Required constructor */
        }
    }
}
