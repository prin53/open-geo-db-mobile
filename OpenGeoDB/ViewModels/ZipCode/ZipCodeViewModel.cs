using System;
using System.Linq;
using System.Threading.Tasks;
using MvvmCross.Core.Navigation;
using MvvmCross.Core.ViewModels;
using MvvmCross.Platform;
using OpenGeoDB.Models;
using OpenGeoDB.Repositories.Cities;
using OpenGeoDB.Repositories.ZipCodes;
using OpenGeoDB.Services.Alert;
using OpenGeoDB.ViewModels.ZipCodes;

namespace OpenGeoDB.ViewModels.ZipCode
{
    public class ZipCodeViewModel : ViewModelBase<ZipCodeViewModel.Parameter>
    {
        private const string Tag = nameof(ZipCodeViewModel);

        public ZipCodeViewModel(
            IMvxNavigationService navigationService,
            IAlertService alertService,
            IZipCodesRepository zipCodesRepository,
            ICitiesRepository citiesRepository
        ) : base(navigationService, alertService)
        {
            ZipCodesRepository = zipCodesRepository ?? throw new ArgumentNullException(nameof(zipCodesRepository));
            CitiesRepository = citiesRepository ?? throw new ArgumentNullException(nameof(citiesRepository));
        }

        protected int ZipCodeId { get; private set; }

        protected IZipCodesRepository ZipCodesRepository { get; }

        protected ICitiesRepository CitiesRepository { get; }

        #region City

        private string _city;

        public string City
        {
            get => _city;
            set
            {
                _city = value;
                RaisePropertyChanged(() => City);
            }
        }

        #endregion

        #region Zip

        private int? _zip;

        public int? Zip
        {
            get => _zip;
            set
            {
                _zip = value;
                RaisePropertyChanged(() => Zip);
            }
        }

        #endregion

        #region Location

        private LocationModel _location;

        public LocationModel Location
        {
            get => _location;
            set
            {
                _location = value;
                RaisePropertyChanged(() => Location);
            }
        }

        #endregion

        #region Nearby

        private MvxObservableCollection<ZipCodeItemViewModel> _nearby;

        public MvxObservableCollection<ZipCodeItemViewModel> Nearby
        {
            get => _nearby;
            set
            {
                _nearby = value;
                RaisePropertyChanged(() => Nearby);
            }
        }

        #endregion

        #region Select Command

        private IMvxAsyncCommand<ZipCodeItemViewModel> _selectCommand;

        public IMvxAsyncCommand<ZipCodeItemViewModel> SelectCommand => _selectCommand ?? (_selectCommand = new MvxAsyncCommand<ZipCodeItemViewModel>(DoSelectCommand));

        private Task DoSelectCommand(ZipCodeItemViewModel zipCodeItemViewModel)
        {
            return NavigationService.Navigate<ZipCodeViewModel, Parameter>(
                new Parameter(zipCodeItemViewModel.Model.Id)
            );
        }

        #endregion

        protected override async Task Load()
        {
            await base.Load();

            try
            {
                var zipCode = ZipCodesRepository.GetById(ZipCodeId);

                if (zipCode == null)
                {
                    await AlertService.ErrorAsync("Zip Code not found");

                    return;
                }

                var city = CitiesRepository.GetById(zipCode.CityId);

                if (city == null)
                {
                    await AlertService.ErrorAsync("City not found");

                    return;
                }

                var items = ZipCodesRepository.GetNearby(zipCode.Id, Configuration.NearbyZipCodesCount);

                City = city.Name;
                Zip = zipCode.Zip;
                Location = zipCode.Location;
                Nearby = new MvxObservableCollection<ZipCodeItemViewModel>(items.Select(ZipCodeItemViewModel.Create));
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

            ZipCodeId = parameter.ZipCodeId;

            return Task.FromResult(parameter);
        }

        public class Parameter
        {
            public int ZipCodeId { get; }

            public Parameter(int zipCodeId)
            {
                ZipCodeId = zipCodeId;
            }
        }
    }
}
