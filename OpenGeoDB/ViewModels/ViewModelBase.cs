using System;
using System.Threading.Tasks;
using MvvmCross.Core.Navigation;
using MvvmCross.Core.ViewModels;
using MvvmCross.Localization;
using OpenGeoDB.Services.Alert;

namespace OpenGeoDB.ViewModels
{
    public class ViewModelBase : MvxViewModel, IMvxLocalizedTextSourceOwner
    {
        public ViewModelBase(
            IMvxNavigationService navigationService,
            IAlertService alertService
        )
        {
            NavigationService = navigationService ?? throw new ArgumentNullException(nameof(navigationService));
            AlertService = alertService ?? throw new ArgumentNullException(nameof(alertService));
        }

        protected IMvxNavigationService NavigationService { get; }

        protected IAlertService AlertService { get; }

        #region Localized Text Source

        private IMvxLanguageBinder _localizedTextSource;

        public IMvxLanguageBinder LocalizedTextSource
        {
            get => _localizedTextSource = _localizedTextSource ?? CreateLanguageBinder();
        }

        protected IMvxLanguageBinder CreateLanguageBinder()
        {
            return new MvxLanguageBinder(string.Empty, GetType().Name);
        }

        #endregion

        #region Load Command

        private IMvxAsyncCommand _loadCommand;

        public IMvxAsyncCommand LoadCommand => _loadCommand ?? (_loadCommand = new MvxAsyncCommand(DoLoadCommand));

        private Task DoLoadCommand()
        {
            return Load();
        }

        #endregion

        protected virtual Task Load()
        {
            return Task.FromResult<object>(null);
        }

        public override void ViewAppearing()
        {
            base.ViewAppearing();

            Task.Run(() => LoadCommand.Execute());
        }
    }
}
