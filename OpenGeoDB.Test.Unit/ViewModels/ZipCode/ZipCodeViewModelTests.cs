using System;
using System.Linq;
using System.Threading.Tasks;
using Moq;
using MvvmCross.Core.Navigation;
using MvvmCross.Core.ViewModels;
using MvvmCross.Test.Core;
using NUnit.Framework;
using OpenGeoDB.Models;
using OpenGeoDB.Repositories.Cities;
using OpenGeoDB.Repositories.ZipCodes;
using OpenGeoDB.Services.Alert;
using OpenGeoDB.Test.Unit.Helpers;
using OpenGeoDB.ViewModels.ZipCode;

namespace OpenGeoDB.Test.Unit.ViewModels.ZipCode
{
    [TestFixture]
    public class ZipCodeViewModelTests : MvxIoCSupportingTest
    {
        [SetUp]
        public void SetupTest()
        {
            Setup();

            SetInvariantCulture();
        }

        #region Constructors

        [Test]
        public void Constructor_DoesNotThrowException()
        {
            // arrange act assert
            Assert.DoesNotThrow(() => new ZipCodeViewModel(
                Mock.Of<IMvxNavigationService>(),
                Mock.Of<IAlertService>(),
                Mock.Of<IZipCodesRepository>(),
                Mock.Of<ICitiesRepository>()
            ));
        }

        [Test]
        public void Constructor_ThrowsArgumentNullException()
        {
            // arrange act assert
            Assert.Throws<ArgumentNullException>(() => new ZipCodeViewModel(null, null, null, null));
        }

        #endregion

        #region Load Command

        [Test]
        public async Task LoadCommand_CallsZipCodesRepository_LoadZipCode()
        {
            // arrange
            var navigationsServiceMock = new Mock<IMvxNavigationService>(MockBehavior.Loose);

            var alertServiceMock = MockHelpers.CreateAlertService();

            var zipCodesRepositoryMock = new Mock<IZipCodesRepository>(MockBehavior.Loose);

            var citiesRepositoryMock = new Mock<ICitiesRepository>(MockBehavior.Loose);

            var viewModel = new ZipCodeViewModel(
                navigationsServiceMock.Object,
                alertServiceMock.Object,
                zipCodesRepositoryMock.Object,
                citiesRepositoryMock.Object
            );

            // act
            await viewModel.Initialize(new ZipCodeViewModel.Parameter(3));
            await viewModel.LoadCommand.ExecuteAsync();

            // assert
            zipCodesRepositoryMock.Verify(mock => mock.GetById(
                It.Is<int>(item => item == 3)
            ), Times.Once());
        }

        [Test]
        public async Task LoadCommand_CallsZipCodesRepository_SetZipCodeData()
        {
            // arrange
            var zipCode = new ZipCodeModel
            {
                Id = 3,
                CityId = 2,
                Zip = 123,
                Latitude = 10,
                Longitude = 20
            };

            var city = new CityModel
            {
                Id = 2,
                Name = "test"
            };

            var navigationsServiceMock = new Mock<IMvxNavigationService>(MockBehavior.Loose);

            var alertServiceMock = MockHelpers.CreateAlertService();

            var zipCodesRepositoryMock = new Mock<IZipCodesRepository>(MockBehavior.Loose);
            zipCodesRepositoryMock.Setup(mock => mock.GetById(It.IsAny<int>()))
                                  .Returns(zipCode);

            var citiesRepositoryMock = new Mock<ICitiesRepository>(MockBehavior.Loose);
            citiesRepositoryMock.Setup(mock => mock.GetById(It.IsAny<int>()))
                         .Returns(city);

            var viewModel = new ZipCodeViewModel(
                navigationsServiceMock.Object,
                alertServiceMock.Object,
                zipCodesRepositoryMock.Object,
                citiesRepositoryMock.Object
            );

            // act
            await viewModel.Initialize(new ZipCodeViewModel.Parameter(3));
            await viewModel.LoadCommand.ExecuteAsync();

            // assert
            citiesRepositoryMock.Verify(mock => mock.GetById(
                It.Is<int>(item => item == 2)
            ), Times.Once());
        }

        [Test]
        public async Task LoadCommand_CallsCityRepository_LoadCityData()
        {
            // arrange
            var zipCode = new ZipCodeModel
            {
                Id = 3,
                CityId = 2,
                Zip = 123,
                Latitude = 10,
                Longitude = 20
            };

            var city = new CityModel
            {
                Id = 2,
                Name = "test"
            };

            var navigationsServiceMock = new Mock<IMvxNavigationService>(MockBehavior.Loose);

            var alertServiceMock = MockHelpers.CreateAlertService();

            var zipCodesRepositoryMock = new Mock<IZipCodesRepository>(MockBehavior.Loose);
            zipCodesRepositoryMock.Setup(mock => mock.GetById(It.IsAny<int>()))
                                  .Returns(zipCode);

            var citiesRepositoryMock = new Mock<ICitiesRepository>(MockBehavior.Loose);
            citiesRepositoryMock.Setup(mock => mock.GetById(It.IsAny<int>()))
                         .Returns(city);

            var viewModel = new ZipCodeViewModel(
                navigationsServiceMock.Object,
                alertServiceMock.Object,
                zipCodesRepositoryMock.Object,
                citiesRepositoryMock.Object
            );

            // act
            await viewModel.Initialize(new ZipCodeViewModel.Parameter(3));
            await viewModel.LoadCommand.ExecuteAsync();

            // assert
            Assert.AreEqual(city.Name, viewModel.City);
        }

        [Test]
        public async Task LoadCommand_CallsCityRepository_SetCityData()
        {
            // arrange
            var zipCode = new ZipCodeModel
            {
                Id = 3,
                CityId = 2,
                Zip = 123,
                Latitude = 10,
                Longitude = 20
            };

            var city = new CityModel
            {
                Id = 2,
                Name = "test"
            };

            var navigationsServiceMock = new Mock<IMvxNavigationService>(MockBehavior.Loose);

            var alertServiceMock = MockHelpers.CreateAlertService();

            var zipCodesRepositoryMock = new Mock<IZipCodesRepository>(MockBehavior.Loose);
            zipCodesRepositoryMock.Setup(mock => mock.GetById(It.IsAny<int>()))
                                  .Returns(zipCode);

            var citiesRepositoryMock = new Mock<ICitiesRepository>(MockBehavior.Loose);
            citiesRepositoryMock.Setup(mock => mock.GetById(It.IsAny<int>()))
                         .Returns(city);

            var viewModel = new ZipCodeViewModel(
                navigationsServiceMock.Object,
                alertServiceMock.Object,
                zipCodesRepositoryMock.Object,
                citiesRepositoryMock.Object
            );

            // act
            await viewModel.Initialize(new ZipCodeViewModel.Parameter(3));
            await viewModel.LoadCommand.ExecuteAsync();

            // assert
            Assert.AreEqual(city.Name, viewModel.City);
        }

        [Test]
        public async Task LoadCommand_CallsZipCodesRepository_LoadNearbyData()
        {
            // arrange
            var zipCode = new ZipCodeModel
            {
                Id = 3,
                CityId = 2,
                Zip = 123,
                Latitude = 10,
                Longitude = 20
            };

            var city = new CityModel
            {
                Id = 2,
                Name = "test"
            };

            var navigationsServiceMock = new Mock<IMvxNavigationService>(MockBehavior.Loose);

            var alertServiceMock = MockHelpers.CreateAlertService();

            var zipCodesRepositoryMock = new Mock<IZipCodesRepository>(MockBehavior.Loose);
            zipCodesRepositoryMock.Setup(mock => mock.GetById(It.IsAny<int>()))
                                  .Returns(zipCode);
            zipCodesRepositoryMock.Setup(mock => mock.GetNearby(It.IsAny<int>(), It.IsAny<int>()))
                                  .Returns(new[] { zipCode });

            var citiesRepositoryMock = new Mock<ICitiesRepository>(MockBehavior.Loose);
            citiesRepositoryMock.Setup(mock => mock.GetById(It.IsAny<int>()))
                         .Returns(city);

            var viewModel = new ZipCodeViewModel(
                navigationsServiceMock.Object,
                alertServiceMock.Object,
                zipCodesRepositoryMock.Object,
                citiesRepositoryMock.Object
            );

            // act
            await viewModel.Initialize(new ZipCodeViewModel.Parameter(3));
            await viewModel.LoadCommand.ExecuteAsync();

            // assert
            zipCodesRepositoryMock.Verify(mock => mock.GetNearby(
                It.Is<int>(item => item == 3),
                It.Is<int>(item => item == Configuration.NearbyZipCodesCount)
            ), Times.Once());
        }

        [Test]
        public async Task LoadCommand_CallsZipCodesRepository_SetNearbyData()
        {
            // arrange
            var zipCode = new ZipCodeModel
            {
                Id = 3,
                CityId = 2,
                Zip = 123,
                Latitude = 10,
                Longitude = 20
            };

            var city = new CityModel
            {
                Id = 2,
                Name = "test"
            };

            var navigationsServiceMock = new Mock<IMvxNavigationService>(MockBehavior.Loose);

            var alertServiceMock = MockHelpers.CreateAlertService();

            var zipCodesRepositoryMock = new Mock<IZipCodesRepository>(MockBehavior.Loose);
            zipCodesRepositoryMock.Setup(mock => mock.GetById(It.IsAny<int>()))
                                  .Returns(zipCode);
            zipCodesRepositoryMock.Setup(mock => mock.GetNearby(It.IsAny<int>(), It.IsAny<int>()))
                                  .Returns(new[] { zipCode });

            var citiesRepositoryMock = new Mock<ICitiesRepository>(MockBehavior.Loose);
            citiesRepositoryMock.Setup(mock => mock.GetById(It.IsAny<int>()))
                         .Returns(city);

            var viewModel = new ZipCodeViewModel(
                navigationsServiceMock.Object,
                alertServiceMock.Object,
                zipCodesRepositoryMock.Object,
                citiesRepositoryMock.Object
            );

            // act
            await viewModel.Initialize(new ZipCodeViewModel.Parameter(3));
            await viewModel.LoadCommand.ExecuteAsync();

            // assert
            Assert.AreEqual(1, viewModel.Nearby.Count);
        }

        #endregion

        #region Select Command

        [Test]
        public async Task SelectCommand_NavigatesToCorrectViewModelWithCorrectData()
        {
            // arrange
            var zipCode = new ZipCodeModel
            {
                Id = 3,
                CityId = 2,
                Zip = 123,
                Latitude = 10,
                Longitude = 20
            };

            var city = new CityModel
            {
                Id = 2,
                Name = "test"
            };

            var navigationsServiceMock = new Mock<IMvxNavigationService>(MockBehavior.Loose);

            var alertServiceMock = MockHelpers.CreateAlertService();

            var zipCodesRepositoryMock = new Mock<IZipCodesRepository>(MockBehavior.Loose);
            zipCodesRepositoryMock.Setup(mock => mock.GetById(It.IsAny<int>()))
                                  .Returns(zipCode);
            zipCodesRepositoryMock.Setup(mock => mock.GetNearby(It.IsAny<int>(), It.IsAny<int>()))
                                  .Returns(new[] { zipCode });

            var citiesRepositoryMock = new Mock<ICitiesRepository>(MockBehavior.Loose);
            citiesRepositoryMock.Setup(mock => mock.GetById(It.IsAny<int>()))
                         .Returns(city);

            var viewModel = new ZipCodeViewModel(
                navigationsServiceMock.Object,
                alertServiceMock.Object,
                zipCodesRepositoryMock.Object,
                citiesRepositoryMock.Object
            );

            // act
            await viewModel.LoadCommand.ExecuteAsync();
            await viewModel.SelectCommand.ExecuteAsync(viewModel.Nearby.First());

            // assert
            navigationsServiceMock.Verify(mock => mock.Navigate<ZipCodeViewModel, ZipCodeViewModel.Parameter>(
                It.Is<ZipCodeViewModel.Parameter>(item => item.ZipCodeId == 3),
                It.IsAny<IMvxBundle>()
            ), Times.Once());
        }

        #endregion
    }
}
