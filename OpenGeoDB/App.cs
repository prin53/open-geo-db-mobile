using MvvmCross.Core.ViewModels;
using MvvmCross.Localization;
using MvvmCross.Platform;
using MvvmCross.Plugins.ResxLocalization;
using OpenGeoDB.Repositories.Cities;
using OpenGeoDB.Repositories.Update;
using OpenGeoDB.Repositories.ZipCodes;
using OpenGeoDB.Resources;
using OpenGeoDB.Services.Data;
using OpenGeoDB.Services.EmbeddedResource;
using OpenGeoDB.Services.Update;

namespace OpenGeoDB
{
    public class App : MvxApplication
    {
        private void RegisterTypes()
        {
            Mvx.RegisterSingleton<IMvxTextProvider>(new MvxResxTextProvider(Strings.ResourceManager));
            Mvx.LazyConstructAndRegisterSingleton<IEmbeddedResourceLoader, EmbeddedResourceLoader>();
            Mvx.LazyConstructAndRegisterSingleton<IDataProvider, CsvDataProvider>();
            Mvx.LazyConstructAndRegisterSingleton<IDataStore, SQLiteDataStore>();
            Mvx.LazyConstructAndRegisterSingleton<IUpdateRepository, UpdateRepository>();
            Mvx.LazyConstructAndRegisterSingleton<ICitiesRepository, CitiesRepository>();
            Mvx.LazyConstructAndRegisterSingleton<IZipCodesRepository, ZipCodesRepository>();
            Mvx.LazyConstructAndRegisterSingleton<IUpdateService, UpdateService>();
            Mvx.LazyConstructAndRegisterSingleton<IMvxAppStart, AppStart>();
        }

        public override void Initialize()
        {
            base.Initialize();

            RegisterTypes();
        }
    }
}
