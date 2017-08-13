using System;
using System.Threading.Tasks;
using MvvmCross.Core.Navigation;
using MvvmCross.Platform;
using OpenGeoDB.Services.Alert;
using OpenGeoDB.Services.Update;
using OpenGeoDB.ViewModels.Cities;

namespace OpenGeoDB.ViewModels.Update
{
    public class UpdateViewModel : ViewModelBase
    {
        private const string Tag = nameof(UpdateViewModel);

        public UpdateViewModel(
            IMvxNavigationService navigationService,
            IAlertService alertService,
            IUpdateService updateService
        ) : base(navigationService, alertService)
        {
            UpdateService = updateService ?? throw new ArgumentNullException(nameof(updateService));
        }

        protected IUpdateService UpdateService { get; }

        protected override async Task Load()
        {
            await base.Load();

            try
            {
                await UpdateService.UpdateAsync();
            }
            catch (Exception exception)
            {
                Mvx.TaggedError(Tag, exception.ToString());

                await AlertService.ErrorAsync(exception.Message);
            }
            finally
            {
                await NavigationService.Navigate<CitiesViewModel>();
            }
        }
    }
}
