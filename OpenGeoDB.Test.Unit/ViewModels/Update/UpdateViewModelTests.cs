using System;
using System.Threading;
using System.Threading.Tasks;
using Moq;
using MvvmCross.Core.Navigation;
using MvvmCross.Core.ViewModels;
using MvvmCross.Test.Core;
using NUnit.Framework;
using OpenGeoDB.Services.Alert;
using OpenGeoDB.Services.Update;
using OpenGeoDB.Test.Unit.Helpers;
using OpenGeoDB.ViewModels.Cities;
using OpenGeoDB.ViewModels.Update;

namespace OpenGeoDB.Test.Unit.ViewModels.Update
{
    [TestFixture]
    public class UpdateViewModelTests : MvxIoCSupportingTest
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
            Assert.DoesNotThrow(() => new UpdateViewModel(
                Mock.Of<IMvxNavigationService>(),
                Mock.Of<IAlertService>(),
                Mock.Of<IUpdateService>()
            ));
        }

        [Test]
        public void Constructor_ThrowsArgumentNullException()
        {
            // arrange act assert
            Assert.Throws<ArgumentNullException>(() => new UpdateViewModel(null, null, null));
        }

        #endregion

        #region Load Command

        [Test]
        public async Task LoadCommand_CallsUpdateService()
        {
            // arrange
            var navigationsServiceMock = new Mock<IMvxNavigationService>(MockBehavior.Loose);

            var alertServiceMock = MockHelpers.CreateAlertService();

            var updateServiceMock = new Mock<IUpdateService>(MockBehavior.Loose);

            var viewModel = new UpdateViewModel(
                navigationsServiceMock.Object,
                alertServiceMock.Object,
                updateServiceMock.Object
            );

            // act
            await viewModel.LoadCommand.ExecuteAsync();

            // assert
            updateServiceMock.Verify(mock => mock.UpdateAsync(
                It.IsAny<CancellationToken>()
            ), Times.Once());
        }

        [Test]
        public async Task LoadCommand_NotCallsAlertService_UpdateSuccessful()
        {
            // arrange
            var navigationsServiceMock = new Mock<IMvxNavigationService>(MockBehavior.Loose);

            var alertServiceMock = MockHelpers.CreateAlertService();

            var updateServiceMock = new Mock<IUpdateService>(MockBehavior.Loose);

            var viewModel = new UpdateViewModel(
                navigationsServiceMock.Object,
                alertServiceMock.Object,
                updateServiceMock.Object
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
        public async Task LoadCommand_CallsAlertService_UpdateFailure()
        {
            // arrange
            var navigationsServiceMock = new Mock<IMvxNavigationService>(MockBehavior.Loose);

            var alertServiceMock = MockHelpers.CreateAlertService();

            var updateServiceMock = new Mock<IUpdateService>(MockBehavior.Strict);

            var viewModel = new UpdateViewModel(
                navigationsServiceMock.Object,
                alertServiceMock.Object,
                updateServiceMock.Object
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

        [Test]
        public async Task LoadCommand_NavigatesToCorrectViewModel_UpdateFailure()
        {
            // arrange
            var navigationsServiceMock = new Mock<IMvxNavigationService>(MockBehavior.Loose);

            var alertServiceMock = MockHelpers.CreateAlertService();

            var updateServiceMock = new Mock<IUpdateService>(MockBehavior.Strict);

            var viewModel = new UpdateViewModel(
                navigationsServiceMock.Object,
                alertServiceMock.Object,
                updateServiceMock.Object
            );

            // act
            await viewModel.LoadCommand.ExecuteAsync();

            // assert
            navigationsServiceMock.Verify(mock => mock.Navigate<CitiesViewModel>(
                It.IsAny<IMvxBundle>()
            ), Times.Once());
        }

        [Test]
        public async Task LoadCommand_NavigatesToCorrectViewModel_UpdateSuccessful()
        {
            // arrange
            var navigationsServiceMock = new Mock<IMvxNavigationService>(MockBehavior.Loose);

            var alertServiceMock = MockHelpers.CreateAlertService();

            var updateServiceMock = new Mock<IUpdateService>(MockBehavior.Loose);

            var viewModel = new UpdateViewModel(
                navigationsServiceMock.Object,
                alertServiceMock.Object,
                updateServiceMock.Object
            );

            // act
            await viewModel.LoadCommand.ExecuteAsync();

            // assert
            navigationsServiceMock.Verify(mock => mock.Navigate<CitiesViewModel>(
                It.IsAny<IMvxBundle>()
            ), Times.Once());
        }

        #endregion
    }
}
