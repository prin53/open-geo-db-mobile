using System;
using Moq;
using MvvmCross.Test.Core;
using NUnit.Framework;
using OpenGeoDB.Models;
using OpenGeoDB.Services.Location;
using OpenGeoDB.Utilities;

namespace OpenGeoDB.Test.Unit.Utilities
{
    [TestFixture]
    public class ZipCodeComparerTests : MvxIoCSupportingTest
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
            Assert.DoesNotThrow(() => new ZipCodeComparer(
                Mock.Of<ILocationService>(),
                new ZipCodeModel()
            ));
        }

        [Test]
        public void Constructor_ThrowsArgumentNullException()
        {
            // arrange act assert
            Assert.Throws<ArgumentNullException>(() => new ZipCodeComparer(null, null));
        }

        #endregion

        #region Compare

        [Test]
        public void Compare_CallsLocationService()
        {
            // arrange
            var zipCode = new ZipCodeModel
            {
                Latitude = 1,
                Longitude = 1
            };

            var zipCode1 = new ZipCodeModel
            {
                Latitude = 2,
                Longitude = 2
            };

            var zipCode2 = new ZipCodeModel
            {
                Latitude = 3,
                Longitude = 3
            };

            var locationServiceMock = new Mock<ILocationService>(MockBehavior.Loose);

            var comparer = new ZipCodeComparer(
                locationServiceMock.Object,
                zipCode
            );

            // act
            var result = comparer.Compare(zipCode1, zipCode2);

            // assert
            locationServiceMock.Verify(mock => mock.GetDistance(
                It.Is<LocationModel>(item => item.Latitude == 1 && item.Longitude == 1),
                It.Is<LocationModel>(item => item.Latitude == 2 && item.Longitude == 2)
            ), Times.Once());

            locationServiceMock.Verify(mock => mock.GetDistance(
                It.Is<LocationModel>(item => item.Latitude == 1 && item.Longitude == 1),
                It.Is<LocationModel>(item => item.Latitude == 3 && item.Longitude == 3)
            ), Times.Once());
        }

        [Test]
        public void Compare_ReturnsCorrectData_Equals()
        {
            // arrange
            var zipCode = new ZipCodeModel
            {
                Latitude = 1,
                Longitude = 1
            };

            var zipCode1 = new ZipCodeModel
            {
                Latitude = 2,
                Longitude = 2
            };

            var zipCode2 = new ZipCodeModel
            {
                Latitude = 3,
                Longitude = 3
            };

            var locationServiceMock = new Mock<ILocationService>(MockBehavior.Loose);

            var comparer = new ZipCodeComparer(
                locationServiceMock.Object,
                zipCode
            );

            // act
            var result = comparer.Compare(zipCode1, zipCode2);

            // assert
            Assert.AreEqual(0, result);
        }

        [Test]
        public void Compare_ReturnsCorrectData_Greater()
        {
            // arrange
            var zipCode = new ZipCodeModel
            {
                Latitude = 1,
                Longitude = 1
            };

            var zipCode1 = new ZipCodeModel
            {
                Latitude = 2,
                Longitude = 2
            };

            var zipCode2 = new ZipCodeModel
            {
                Latitude = 3,
                Longitude = 3
            };

            var locationServiceMock = new Mock<ILocationService>(MockBehavior.Loose);
            locationServiceMock.Setup(mock => mock.GetDistance(
                It.Is<LocationModel>(item => item.Latitude == 1 && item.Longitude == 1),
                It.Is<LocationModel>(item => item.Latitude == 2 && item.Longitude == 2)
            )).Returns(10);

            locationServiceMock.Setup(mock => mock.GetDistance(
                It.Is<LocationModel>(item => item.Latitude == 1 && item.Longitude == 1),
                It.Is<LocationModel>(item => item.Latitude == 3 && item.Longitude == 3)
            )).Returns(5);

            var comparer = new ZipCodeComparer(
                locationServiceMock.Object,
                zipCode
            );

            // act
            var result = comparer.Compare(zipCode1, zipCode2);

            // assert
            Assert.Less(0, result);
        }

        [Test]
        public void Compare_ReturnsCorrectData_Less()
        {
            // arrange
            var zipCode = new ZipCodeModel
            {
                Latitude = 1,
                Longitude = 1
            };

            var zipCode1 = new ZipCodeModel
            {
                Latitude = 2,
                Longitude = 2
            };

            var zipCode2 = new ZipCodeModel
            {
                Latitude = 3,
                Longitude = 3
            };

            var locationServiceMock = new Mock<ILocationService>(MockBehavior.Loose);
            locationServiceMock.Setup(mock => mock.GetDistance(
                It.Is<LocationModel>(item => item.Latitude == 1 && item.Longitude == 1),
                It.Is<LocationModel>(item => item.Latitude == 2 && item.Longitude == 2)
            )).Returns(5);

            locationServiceMock.Setup(mock => mock.GetDistance(
                It.Is<LocationModel>(item => item.Latitude == 1 && item.Longitude == 1),
                It.Is<LocationModel>(item => item.Latitude == 3 && item.Longitude == 3)
            )).Returns(10);

            var comparer = new ZipCodeComparer(
                locationServiceMock.Object,
                zipCode
            );

            // act
            var result = comparer.Compare(zipCode1, zipCode2);

            // assert
            Assert.Greater(0, result);
        }

        #endregion
    }
}
