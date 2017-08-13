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
using OpenGeoDB.ViewModels.Cities;
using OpenGeoDB.ViewModels.ZipCode;
using OpenGeoDB.ViewModels.ZipCodes;

namespace OpenGeoDB.Test.Unit.ViewModels.Cities
{
    [TestFixture]
    public class CitiesViewModelTests : MvxIoCSupportingTest
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
            Assert.DoesNotThrow(() => new CitiesViewModel(
                Mock.Of<IMvxNavigationService>(),
                Mock.Of<IAlertService>(),
                Mock.Of<ICitiesRepository>(),
                Mock.Of<IZipCodesRepository>()
            ));
        }

        [Test]
        public void Constructor_ThrowsArgumentNullException()
        {
            // arrange act assert
            Assert.Throws<ArgumentNullException>(() => new CitiesViewModel(null, null, null, null));
        }

        #endregion

        #region Load Command

        [Test]
        public async Task LoadCommand_CallsCitiesRepository()
        {
            // arrange
            var navigationsServiceMock = new Mock<IMvxNavigationService>(MockBehavior.Loose);

            var alertServiceMock = MockHelpers.CreateAlertService();

            var citiesRepositoryMock = new Mock<ICitiesRepository>(MockBehavior.Loose);

            var zipCodesRepositoryMock = new Mock<IZipCodesRepository>(MockBehavior.Loose);

            var viewModel = new CitiesViewModel(
                navigationsServiceMock.Object,
                alertServiceMock.Object,
                citiesRepositoryMock.Object,
                zipCodesRepositoryMock.Object
            );

            // act
            await viewModel.LoadCommand.ExecuteAsync();

            // assert
            citiesRepositoryMock.Verify(mock => mock.GetAll(), Times.Once());
        }

        [Test]
        public async Task LoadCommand_SetsItemsCorrectly()
        {
            // arrange
            var navigationsServiceMock = new Mock<IMvxNavigationService>(MockBehavior.Loose);

            var alertServiceMock = MockHelpers.CreateAlertService();

            var citiesRepositoryMock = new Mock<ICitiesRepository>(MockBehavior.Loose);
            citiesRepositoryMock.Setup(mock => mock.GetAll())
                                .Returns(new[] { new CityModel { Id = 1 } });

            var zipCodesRepositoryMock = new Mock<IZipCodesRepository>(MockBehavior.Loose);

            var viewModel = new CitiesViewModel(
                navigationsServiceMock.Object,
                alertServiceMock.Object,
                citiesRepositoryMock.Object,
                zipCodesRepositoryMock.Object
            );

            // act
            await viewModel.LoadCommand.ExecuteAsync();

            // assert
            Assert.AreEqual(1, viewModel.Items.Count);
        }

        [Test]
        public async Task LoadCommand_SetsGroupsCorrectly()
        {
            // arrange
            var cities = new[]
            {
                new CityModel
                {
                    Id = 1,
                    Name = "ACity"
                },
                new CityModel
                {
                    Id = 1,
                    Name = "ACity2"
                },
                new CityModel
                {
                    Id = 1,
                    Name = "CCity2"
                }
            };

            var navigationsServiceMock = new Mock<IMvxNavigationService>(MockBehavior.Loose);

            var alertServiceMock = MockHelpers.CreateAlertService();

            var citiesRepositoryMock = new Mock<ICitiesRepository>(MockBehavior.Loose);
            citiesRepositoryMock.Setup(mock => mock.GetAll())
                                .Returns(cities);

            var zipCodesRepositoryMock = new Mock<IZipCodesRepository>(MockBehavior.Loose);

            var viewModel = new CitiesViewModel(
                navigationsServiceMock.Object,
                alertServiceMock.Object,
                citiesRepositoryMock.Object,
                zipCodesRepositoryMock.Object
            );

            // act
            await viewModel.LoadCommand.ExecuteAsync();

            // assert
            Assert.AreEqual(2, viewModel.Items.Count);
        }

        [Test]
        public async Task LoadCommand_SetsGroupsInCorrectOrder()
        {
            // arrange
            var cities = new[]
            {
                new CityModel
                {
                    Id = 1,
                    Name = "ACity"
                },
                new CityModel
                {
                    Id = 2,
                    Name = "ACity2"
                },
                new CityModel
                {
                    Id = 3,
                    Name = "CCity2"
                }
            };

            var navigationsServiceMock = new Mock<IMvxNavigationService>(MockBehavior.Loose);

            var alertServiceMock = MockHelpers.CreateAlertService();

            var citiesRepositoryMock = new Mock<ICitiesRepository>(MockBehavior.Loose);
            citiesRepositoryMock.Setup(mock => mock.GetAll())
                                .Returns(cities);

            var zipCodesRepositoryMock = new Mock<IZipCodesRepository>(MockBehavior.Loose);

            var viewModel = new CitiesViewModel(
                navigationsServiceMock.Object,
                alertServiceMock.Object,
                citiesRepositoryMock.Object,
                zipCodesRepositoryMock.Object
            );

            // act
            await viewModel.LoadCommand.ExecuteAsync();

            // assert
            Assert.AreEqual(1, viewModel.Items.First().First().Model.Id);
            Assert.AreEqual(2, viewModel.Items.First().Last().Model.Id);
            Assert.AreEqual(3, viewModel.Items.Last().First().Model.Id);
        }

        [Test]
        public async Task LoadCommand_NotCallsAlertService_LoadSuccessful()
        {
            // arrange
            var navigationsServiceMock = new Mock<IMvxNavigationService>(MockBehavior.Loose);

            var alertServiceMock = MockHelpers.CreateAlertService();

            var citiesRepositoryMock = new Mock<ICitiesRepository>(MockBehavior.Loose);

            var zipCodesRepositoryMock = new Mock<IZipCodesRepository>(MockBehavior.Loose);

            var viewModel = new CitiesViewModel(
                navigationsServiceMock.Object,
                alertServiceMock.Object,
                citiesRepositoryMock.Object,
                zipCodesRepositoryMock.Object
            );

            // act
            await viewModel.LoadCommand.ExecuteAsync();

            // assert
            alertServiceMock.Verify(mock => mock.Alert(
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<Action>()
            ), Times.Never());
        }

        [Test]
        public async Task LoadCommand_CallsAlertService_LoadFailure()
        {
            // arrange
            var navigationsServiceMock = new Mock<IMvxNavigationService>(MockBehavior.Loose);

            var alertServiceMock = MockHelpers.CreateAlertService();

            var citiesRepositoryMock = new Mock<ICitiesRepository>(MockBehavior.Strict);

            var zipCodesRepositoryMock = new Mock<IZipCodesRepository>(MockBehavior.Loose);

            var viewModel = new CitiesViewModel(
                navigationsServiceMock.Object,
                alertServiceMock.Object,
                citiesRepositoryMock.Object,
                zipCodesRepositoryMock.Object
            );

            // act
            await viewModel.LoadCommand.ExecuteAsync();

            // assert
            alertServiceMock.Verify(mock => mock.Alert(
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<Action>()
            ), Times.Once());
        }

        #endregion

        #region Select Command

        [Test]
        public async Task SelectCommand_NavigatesToCorrectViewModelWithCorrectData_OneZipCode()
        {
            // arrange
            var navigationsServiceMock = new Mock<IMvxNavigationService>(MockBehavior.Loose);

            var alertServiceMock = MockHelpers.CreateAlertService();

            var citiesRepositoryMock = new Mock<ICitiesRepository>(MockBehavior.Loose);
            citiesRepositoryMock.Setup(mock => mock.GetAll())
                                .Returns(new[] { new CityModel() });

            var zipCodesRepositoryMock = new Mock<IZipCodesRepository>(MockBehavior.Loose);
            zipCodesRepositoryMock.Setup(mock => mock.GetZipCodesCount(It.IsAny<int>()))
                                  .Returns(1);
            zipCodesRepositoryMock.Setup(mock => mock.GetFirstForCity(It.IsAny<int>()))
                                  .Returns(new ZipCodeModel { Id = 1 });

            var viewModel = new CitiesViewModel(
                navigationsServiceMock.Object,
                alertServiceMock.Object,
                citiesRepositoryMock.Object,
                zipCodesRepositoryMock.Object
            );

            // act
            await viewModel.LoadCommand.ExecuteAsync();
            await viewModel.SelectCommand.ExecuteAsync(viewModel.Items.First().First());

            // assert
            navigationsServiceMock.Verify(mock => mock.Navigate<ZipCodeViewModel, ZipCodeViewModel.Parameter>(
                It.Is<ZipCodeViewModel.Parameter>(item => item.ZipCodeId == 1),
                It.IsAny<IMvxBundle>()
            ), Times.Once());
        }

        [Test]
        public async Task SelectCommand_NavigatesToCorrectViewModelWithCorrectData_ManyZipCodes()
        {
            // arrange
            var navigationsServiceMock = new Mock<IMvxNavigationService>(MockBehavior.Loose);

            var alertServiceMock = MockHelpers.CreateAlertService();

            var citiesRepositoryMock = new Mock<ICitiesRepository>(MockBehavior.Loose);
            citiesRepositoryMock.Setup(mock => mock.GetAll())
                                .Returns(new[] { new CityModel { Id = 1 } });

            var zipCodesRepositoryMock = new Mock<IZipCodesRepository>(MockBehavior.Loose);
            zipCodesRepositoryMock.Setup(mock => mock.GetZipCodesCount(It.IsAny<int>()))
                                  .Returns(2);
            zipCodesRepositoryMock.Setup(mock => mock.GetFirstForCity(It.IsAny<int>()))
                                  .Returns(new ZipCodeModel());

            var viewModel = new CitiesViewModel(
                navigationsServiceMock.Object,
                alertServiceMock.Object,
                citiesRepositoryMock.Object,
                zipCodesRepositoryMock.Object
            );

            // act
            await viewModel.LoadCommand.ExecuteAsync();
            await viewModel.SelectCommand.ExecuteAsync(viewModel.Items.First().First());

            // assert
            navigationsServiceMock.Verify(mock => mock.Navigate<ZipCodesViewModel, ZipCodesViewModel.Parameter>(
                It.Is<ZipCodesViewModel.Parameter>(item => item.CityId == 1),
                It.IsAny<IMvxBundle>()
            ), Times.Once());
        }

        [Test]
        public async Task SelectCommand_NpNavigation_OneZipCode_ZipCodeNotFound()
        {
            // arrange
            var navigationsServiceMock = new Mock<IMvxNavigationService>(MockBehavior.Loose);

            var alertServiceMock = MockHelpers.CreateAlertService();

            var citiesRepositoryMock = new Mock<ICitiesRepository>(MockBehavior.Loose);
            citiesRepositoryMock.Setup(mock => mock.GetAll())
                                .Returns(new[] { new CityModel { Id = 1 } });

            var zipCodesRepositoryMock = new Mock<IZipCodesRepository>(MockBehavior.Loose);
            zipCodesRepositoryMock.Setup(mock => mock.GetZipCodesCount(It.IsAny<int>()))
                                  .Returns(1);
            zipCodesRepositoryMock.Setup(mock => mock.GetFirstForCity(It.IsAny<int>()))
                                  .Returns<ZipCodeModel>(null);

            var viewModel = new CitiesViewModel(
                navigationsServiceMock.Object,
                alertServiceMock.Object,
                citiesRepositoryMock.Object,
                zipCodesRepositoryMock.Object
            );

            // act
            await viewModel.LoadCommand.ExecuteAsync();
            await viewModel.SelectCommand.ExecuteAsync(viewModel.Items.First().First());

            // assert
            navigationsServiceMock.Verify(mock => mock.Navigate<ZipCodesViewModel, ZipCodesViewModel.Parameter>(
                It.Is<ZipCodesViewModel.Parameter>(item => item.CityId == 1),
                It.IsAny<IMvxBundle>()
            ), Times.Never());
        }

        [Test]
        public async Task SelectCommand_CallsAlertService_OneZipCode_ZipCodeNotFound()
        {
            // arrange
            var navigationsServiceMock = new Mock<IMvxNavigationService>(MockBehavior.Loose);

            var alertServiceMock = MockHelpers.CreateAlertService();

            var citiesRepositoryMock = new Mock<ICitiesRepository>(MockBehavior.Loose);
            citiesRepositoryMock.Setup(mock => mock.GetAll())
                                .Returns(new[] { new CityModel { Id = 1 } });

            var zipCodesRepositoryMock = new Mock<IZipCodesRepository>(MockBehavior.Loose);
            zipCodesRepositoryMock.Setup(mock => mock.GetZipCodesCount(It.IsAny<int>()))
                                  .Returns(1);
            zipCodesRepositoryMock.Setup(mock => mock.GetFirstForCity(It.IsAny<int>()))
                                  .Returns<ZipCodeModel>(null);

            var viewModel = new CitiesViewModel(
                navigationsServiceMock.Object,
                alertServiceMock.Object,
                citiesRepositoryMock.Object,
                zipCodesRepositoryMock.Object
            );

            // act
            await viewModel.LoadCommand.ExecuteAsync();
            await viewModel.SelectCommand.ExecuteAsync(viewModel.Items.First().First());

            // assert
            alertServiceMock.Verify(mock => mock.Alert(
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<Action>()
            ), Times.Once());
        }

        #endregion
    }
}
