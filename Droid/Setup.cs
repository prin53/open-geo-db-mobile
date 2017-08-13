using Android.Content;
using MvvmCross.Core.ViewModels;
using MvvmCross.Droid.Platform;
using MvvmCross.Platform;
using OpenGeoDB.Droid.Services.Alert;
using OpenGeoDB.Droid.Services.Data;
using OpenGeoDB.Droid.Services.Location;
using OpenGeoDB.Services.Alert;
using OpenGeoDB.Services.Data;
using OpenGeoDB.Services.Location;

namespace OpenGeoDB.Droid
{
    public class Setup : MvxAndroidSetup
    {
        public Setup(Context applicationContext) : base(applicationContext)
        {
            /* Required constructor */
        }

        protected override IMvxApplication CreateApp()
        {
            return new App();
        }

        protected override void InitializeFirstChance()
        {
            Mvx.LazyConstructAndRegisterSingleton<IAlertService, AlertService>();
            Mvx.LazyConstructAndRegisterSingleton<ISQLiteConnectionFactory, SQLiteConnectionFactory>();
            Mvx.LazyConstructAndRegisterSingleton<ILocationService, LocationService>();

            base.InitializeFirstChance();
        }
    }
}
