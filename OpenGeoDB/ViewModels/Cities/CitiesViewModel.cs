using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MvvmCross.Core.Navigation;
using MvvmCross.Core.ViewModels;
using MvvmCross.Platform;
using OpenGeoDB.Models;
using OpenGeoDB.Repositories.Cities;
using OpenGeoDB.Repositories.ZipCodes;
using OpenGeoDB.Services.Alert;
using OpenGeoDB.ViewModels.ZipCode;
using OpenGeoDB.ViewModels.ZipCodes;

namespace OpenGeoDB.ViewModels.Cities
{
    public class CitiesViewModel : ViewModelBase
    {
        private const string Tag = nameof(CitiesViewModel);

        public CitiesViewModel(
            IMvxNavigationService navigationService,
            IAlertService alertService,
            ICitiesRepository citiesRepository,
            IZipCodesRepository zipCodesRepository
        ) : base(navigationService, alertService)
        {
            CitiesRepository = citiesRepository ?? throw new ArgumentNullException(nameof(citiesRepository));
            ZipCodesRepository = zipCodesRepository ?? throw new ArgumentNullException(nameof(zipCodesRepository));
        }

        protected ICitiesRepository CitiesRepository { get; }

        protected IZipCodesRepository ZipCodesRepository { get; }

        #region Items

        private MvxObservableCollection<IGrouping<string, CityItemViewModel>> _items;

        public MvxObservableCollection<IGrouping<string, CityItemViewModel>> Items
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

        private IMvxAsyncCommand<CityItemViewModel> _selectCommand;

        public IMvxAsyncCommand<CityItemViewModel> SelectCommand => _selectCommand ?? (_selectCommand = new MvxAsyncCommand<CityItemViewModel>(DoSelectCommand));

        private async Task DoSelectCommand(CityItemViewModel cityItemViewModel)
        {
            var zipCodesCount = ZipCodesRepository.GetZipCodesCount(cityItemViewModel.Model.Id);

            if (zipCodesCount > 1)
            {
                await NavigateToZipCodes(cityItemViewModel.Model);
            }
            else
            {
                await LoadAndNavigateToZipCode(cityItemViewModel);
            }
        }

        private Task NavigateToZipCodes(CityModel city)
        {
            return NavigationService.Navigate<ZipCodesViewModel, ZipCodesViewModel.Parameter>(
                new ZipCodesViewModel.Parameter(city.Id)
            );
        }

        private async Task LoadAndNavigateToZipCode(CityItemViewModel cityItemViewModel)
        {
            var zipCode = ZipCodesRepository.GetFirstForCity(cityItemViewModel.Model.Id);

            if (zipCode == null)
            {
                await AlertService.ErrorAsync("No zip codes found for city");

                return;
            }

            await NavigateToZipCode(zipCode);
        }

        private Task NavigateToZipCode(ZipCodeModel zipCode)
        {
            return NavigationService.Navigate<ZipCodeViewModel, ZipCodeViewModel.Parameter>(
                new ZipCodeViewModel.Parameter(zipCode.Id)
            );
        }

        #endregion

        private IEnumerable<IGrouping<string, CityItemViewModel>> GroupItems(IEnumerable<CityItemViewModel> items)
        {
            return items.GroupBy(GetGroupKey);
        }

        private string GetGroupKey(CityItemViewModel cityItemViewModel)
        {
            return string.IsNullOrEmpty(cityItemViewModel.Name) ? string.Empty : cityItemViewModel.Name[0].ToString();
        }

        protected override async Task Load()
        {
            await base.Load();

            try
            {
                var cities = CitiesRepository.GetAll();

                var items = cities.Select(CityItemViewModel.Create);

                var groupedItems = GroupItems(items);

                Items = new MvxObservableCollection<IGrouping<string, CityItemViewModel>>(groupedItems);
            }
            catch (Exception exception)
            {
                Mvx.TaggedError(Tag, exception.ToString());

                await AlertService.ErrorAsync(exception.Message);
            }
        }
    }
}
