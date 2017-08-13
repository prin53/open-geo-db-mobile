using MvvmCross.Core.ViewModels;
using MvvmCross.iOS.Platform;
using MvvmCross.iOS.Views.Presenters;
using MvvmCross.Platform;
using OpenGeoDB.iOS.Services.Alert;
using OpenGeoDB.iOS.Services.Data;
using OpenGeoDB.iOS.Services.Location;
using OpenGeoDB.Services.Alert;
using OpenGeoDB.Services.Data;
using OpenGeoDB.Services.Location;

namespace OpenGeoDB.iOS
{
    public class Setup : MvxIosSetup
    {
        public Setup(IMvxApplicationDelegate applicationDelegate, IMvxIosViewPresenter presenter)
            : base(applicationDelegate, presenter)
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
