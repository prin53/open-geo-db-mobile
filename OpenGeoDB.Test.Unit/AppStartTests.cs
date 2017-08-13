using System;
using Moq;
using MvvmCross.Core.Navigation;
using MvvmCross.Core.ViewModels;
using MvvmCross.Test.Core;
using NUnit.Framework;
using OpenGeoDB.Services.Update;
using OpenGeoDB.ViewModels.Cities;
using OpenGeoDB.ViewModels.Update;

namespace OpenGeoDB.Test.Unit
{
    [TestFixture]
    public class AppStartTests : MvxIoCSupportingTest
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
            Assert.DoesNotThrow(() => new AppStart(
                Mock.Of<IMvxNavigationService>(),
                Mock.Of<IUpdateService>()
            ));
        }

        [Test]
        public void Constructor_ThrowsArgumentNullException()
        {
            // arrange act assert
            Assert.Throws<ArgumentNullException>(() => new AppStart(null, null));
        }

        #endregion

        #region Start

        [Test]
        public void Start_NavigatesToCorrectViewModel_NotOutdated()
        {
            // arrange
            var navigationsServiceMock = new Mock<IMvxNavigationService>(MockBehavior.Loose);

            var updateServiceMock = new Mock<IUpdateService>();
            updateServiceMock.Setup(mock => mock.Outdated).Returns(false);

            var appStart = new AppStart(
                navigationsServiceMock.Object,
                updateServiceMock.Object
            );

            // act
            appStart.Start();

            // assert
            navigationsServiceMock.Verify(mock => mock.Navigate<CitiesViewModel>(
                It.IsAny<IMvxBundle>()
            ), Times.Once());
        }

        [Test]
        public void Start_NavigatesToCorrectViewModel_Outdated()
        {
            // arrange
            var navigationsServiceMock = new Mock<IMvxNavigationService>(MockBehavior.Loose);
      
            var updateServiceMock = new Mock<IUpdateService>();
            updateServiceMock.Setup(mock => mock.Outdated).Returns(true);

            var appStart = new AppStart(
                navigationsServiceMock.Object,
                updateServiceMock.Object
            );

            // act
            appStart.Start();

            // assert
            navigationsServiceMock.Verify(mock => mock.Navigate<UpdateViewModel>(
                It.IsAny<IMvxBundle>()
            ), Times.Once());
        }

        #endregion
    }
}
