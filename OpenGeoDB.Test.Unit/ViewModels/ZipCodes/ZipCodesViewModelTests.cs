using System;
using System.Linq;
using System.Threading.Tasks;
using Moq;
using MvvmCross.Core.Navigation;
using MvvmCross.Core.ViewModels;
using MvvmCross.Test.Core;
using NUnit.Framework;
using OpenGeoDB.Models;
using OpenGeoDB.Repositories.ZipCodes;
using OpenGeoDB.Services.Alert;
using OpenGeoDB.Test.Unit.Helpers;
using OpenGeoDB.ViewModels.ZipCode;
using OpenGeoDB.ViewModels.ZipCodes;

namespace OpenGeoDB.Test.Unit.ViewModels.ZipCodes
{
    [TestFixture]
    public class ZipCodesViewModelTests : MvxIoCSupportingTest
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
            Assert.DoesNotThrow(() => new ZipCodesViewModel(
                Mock.Of<IMvxNavigationService>(),
                Mock.Of<IAlertService>(),
                Mock.Of<IZipCodesRepository>()
            ));
        }

        [Test]
        public void Constructor_ThrowsArgumentNullException()
        {
            // arrange act assert
            Assert.Throws<ArgumentNullException>(() => new ZipCodesViewModel(null, null, null));
        }

        #endregion

        #region Load Command

        [Test]
        public async Task LoadCommand_CallsZipCodesRepositoryWithCorrectData()
        {
            // arrange
            var navigationsServiceMock = new Mock<IMvxNavigationService>(MockBehavior.Loose);

            var alertServiceMock = MockHelpers.CreateAlertService();

            var zipCodesRepositoryMock = new Mock<IZipCodesRepository>(MockBehavior.Loose);

            var viewModel = new ZipCodesViewModel(
                navigationsServiceMock.Object,
                alertServiceMock.Object,
                zipCodesRepositoryMock.Object
            );

            // act
            await viewModel.Initialize(new ZipCodesViewModel.Parameter(3));
            await viewModel.LoadCommand.ExecuteAsync();

            // assert
            zipCodesRepositoryMock.Verify(mock => mock.GetAllForCity(
                It.Is<int>(item => item == 3)
            ), Times.Once());
        }

        [Test]
        public async Task LoadCommand_SetsItemsCorrectly()
        {
            // arrange
            var navigationsServiceMock = new Mock<IMvxNavigationService>(MockBehavior.Loose);

            var alertServiceMock = MockHelpers.CreateAlertService();

            var zipCodesRepositoryMock = new Mock<IZipCodesRepository>(MockBehavior.Loose);
            zipCodesRepositoryMock.Setup(mock => mock.GetAllForCity(It.IsAny<int>()))
                                  .Returns(new[] { new ZipCodeModel { Id = 1 } });


            var viewModel = new ZipCodesViewModel(
                navigationsServiceMock.Object,
                alertServiceMock.Object,
                zipCodesRepositoryMock.Object
            );

            // act
            await viewModel.LoadCommand.ExecuteAsync();

            // assert
            Assert.AreEqual(1, viewModel.Items.Count);
        }

        [Test]
        public async Task LoadCommand_NotCallsAlertService_LoadSuccessful()
        {
            // arrange
            var navigationsServiceMock = new Mock<IMvxNavigationService>(MockBehavior.Loose);

            var alertServiceMock = MockHelpers.CreateAlertService();

            var zipCodesRepositoryMock = new Mock<IZipCodesRepository>(MockBehavior.Loose);

            var viewModel = new ZipCodesViewModel(
                navigationsServiceMock.Object,
                alertServiceMock.Object,
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

            var zipCodesRepositoryMock = new Mock<IZipCodesRepository>(MockBehavior.Strict);

            var viewModel = new ZipCodesViewModel(
                navigationsServiceMock.Object,
                alertServiceMock.Object,
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
        public async Task SelectCommand_NavigatesToCorrectViewModelWithCorrectData()
        {
            // arrange
            var navigationsServiceMock = new Mock<IMvxNavigationService>(MockBehavior.Loose);

            var alertServiceMock = MockHelpers.CreateAlertService();

            var zipCodesRepositoryMock = new Mock<IZipCodesRepository>(MockBehavior.Loose);
            zipCodesRepositoryMock.Setup(mock => mock.GetAllForCity(It.IsAny<int>()))
                                  .Returns(new[] { new ZipCodeModel { Id = 1 } });

            var viewModel = new ZipCodesViewModel(
                navigationsServiceMock.Object,
                alertServiceMock.Object,
                zipCodesRepositoryMock.Object
            );

            // act
            await viewModel.LoadCommand.ExecuteAsync();
            await viewModel.SelectCommand.ExecuteAsync(viewModel.Items.First());

            // assert
            navigationsServiceMock.Verify(mock => mock.Navigate<ZipCodeViewModel, ZipCodeViewModel.Parameter>(
                It.Is<ZipCodeViewModel.Parameter>(item => item.ZipCodeId == 1),
                It.IsAny<IMvxBundle>()
            ), Times.Once());
        }

        #endregion
    }
}
