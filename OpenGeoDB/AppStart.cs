using System;
using MvvmCross.Core.Navigation;
using MvvmCross.Core.ViewModels;
using OpenGeoDB.Services.Update;
using OpenGeoDB.ViewModels.Cities;
using OpenGeoDB.ViewModels.Update;

namespace OpenGeoDB
{
    public class AppStart : IMvxAppStart
    {
        public AppStart(
            IMvxNavigationService navigationService,
            IUpdateService updateService
        )
        {
            NavigationService = navigationService ?? throw new ArgumentNullException(nameof(navigationService));
            UpdateService = updateService ?? throw new ArgumentNullException(nameof(updateService));
        }

        protected IMvxNavigationService NavigationService { get; }

        protected IUpdateService UpdateService { get; }

        public void Start(object hint = null)
        {
            if (UpdateService.Outdated)
            {
                NavigationService.Navigate<UpdateViewModel>().GetAwaiter().GetResult();
            }
            else
            {
                NavigationService.Navigate<CitiesViewModel>().GetAwaiter().GetResult();
            }
        }
    }
}
