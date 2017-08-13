using System;
using System.Linq;
using System.Threading.Tasks;
using MvvmCross.Core.Navigation;
using MvvmCross.Core.ViewModels;
using MvvmCross.Platform;
using OpenGeoDB.Repositories.ZipCodes;
using OpenGeoDB.Services.Alert;
using OpenGeoDB.ViewModels.ZipCode;

namespace OpenGeoDB.ViewModels.ZipCodes
{
    public class ZipCodesViewModel : ViewModelBase<ZipCodesViewModel.Parameter>
    {
        private const string Tag = nameof(ZipCodesViewModel);

        public ZipCodesViewModel(
            IMvxNavigationService navigationService,
            IAlertService alertService,
            IZipCodesRepository zipCodesRepository
        ) : base(navigationService, alertService)
        {
            ZipCodesRepository = zipCodesRepository ?? throw new ArgumentNullException(nameof(zipCodesRepository));
        }

        protected int CityId { get; private set; }

        protected IZipCodesRepository ZipCodesRepository { get; }

        #region Items

        private MvxObservableCollection<ZipCodeItemViewModel> _items;

        public MvxObservableCollection<ZipCodeItemViewModel> Items
        {
            get => _items;
            set
            {
                _items = value;
                RaisePropertyChanged(() => Items);
            }
        }

        #endregion

        #region Select Command

        private IMvxAsyncCommand<ZipCodeItemViewModel> _selectCommand;

        public IMvxAsyncCommand<ZipCodeItemViewModel> SelectCommand => _selectCommand ?? (_selectCommand = new MvxAsyncCommand<ZipCodeItemViewModel>(DoSelectCommand));

        private Task DoSelectCommand(ZipCodeItemViewModel zipCodeItemViewModel)
        {
            return NavigationService.Navigate<ZipCodeViewModel, ZipCodeViewModel.Parameter>(
                new ZipCodeViewModel.Parameter(zipCodeItemViewModel.Model.Id)
            );
        }

        #endregion

        protected override async Task Load()
        {
            await base.Load();

            try
            {
                var items = ZipCodesRepository.GetAllForCity(CityId);

                Items = new MvxObservableCollection<ZipCodeItemViewModel>(items.Select(ZipCodeItemViewModel.Create));
            }
            catch (Exception exception)
            {
                Mvx.TaggedError(Tag, exception.ToString());

                await AlertService.ErrorAsync(exception.Message);
            }
        }

        public override Task Initialize(Parameter parameter)
        {
            if (parameter == null)
            {
                throw new ArgumentNullException(nameof(parameter));
            }

            CityId = parameter.CityId;

            return Task.FromResult(parameter);
        }

        public class Parameter
        {
            public int CityId { get; }

            public Parameter(int cityId)
            {
                CityId = cityId;
            }
        }
    }
}
