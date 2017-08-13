using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Moq;
using MvvmCross.Test.Core;
using NUnit.Framework;
using OpenGeoDB.Models;
using OpenGeoDB.Repositories.ZipCodes;
using OpenGeoDB.Services.Data;
using OpenGeoDB.Services.Location;

namespace OpenGeoDB.Test.Unit.Repositories.ZipCodes
{
    [TestFixture]
    public class ZipCodesRepositoryTests : MvxIoCSupportingTest
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
            Assert.DoesNotThrow(() => new ZipCodesRepository(
                Mock.Of<IDataStore>(),
                Mock.Of<ILocationService>()
            ));
        }

        [Test]
        public void Constructor_ThrowsArgumentNullException()
        {
            // arrange act assert
            Assert.Throws<ArgumentNullException>(() => new ZipCodesRepository(null, null));
        }

        #endregion

        #region Clear

        [Test]
        public void Clear_CallsDataStore()
        {
            // arrange
            var dataStoreMock = new Mock<IDataStore>(MockBehavior.Loose);

            var locationServiceMock = new Mock<ILocationService>(MockBehavior.Loose);

            var repository = new ZipCodesRepository(
                dataStoreMock.Object,
                locationServiceMock.Object
            );

            // act
            repository.Clear();

            // assert
            dataStoreMock.Verify(mock => mock.ClearAll<ZipCodeModel>(), Times.Once());
        }

        #endregion

        #region GetAll

        [Test]
        public void GetAll_CallsDataStore()
        {
            // arrange
            var models = new[]
            {
                new ZipCodeModel
                {
                    Id = 1,
                    Zip = 1
                },
                new ZipCodeModel
                {
                    Id = 3,
                    Zip = 3
                },
                new ZipCodeModel
                {
                    Id = 2,
                    Zip = 2
                }
            };

            var dataStoreMock = new Mock<IDataStore>(MockBehavior.Loose);
            dataStoreMock.Setup(mock => mock.GetAll<ZipCodeModel>())
                         .Returns(models.AsQueryable());

            var locationServiceMock = new Mock<ILocationService>(MockBehavior.Loose);

            var repository = new ZipCodesRepository(
                dataStoreMock.Object,
                locationServiceMock.Object
            );

            // act
            var items = repository.GetAll().ToList();

            // assert
            dataStoreMock.Verify(mock => mock.GetAll<ZipCodeModel>(), Times.Once());
        }

        [Test]
        public void GetAll_ReturnsCorrectData()
        {
            // arrange
            var models = new[]
            {
                new ZipCodeModel
                {
                    Id = 1,
                    Zip = 1
                },
                new ZipCodeModel
                {
                    Id = 3,
                    Zip = 3
                },
                new ZipCodeModel
                {
                    Id = 2,
                    Zip = 2
                }
            };

            var dataStoreMock = new Mock<IDataStore>(MockBehavior.Loose);
            dataStoreMock.Setup(mock => mock.GetAll<ZipCodeModel>())
                         .Returns(models.AsQueryable());

            var locationServiceMock = new Mock<ILocationService>(MockBehavior.Loose);

            var repository = new ZipCodesRepository(
                dataStoreMock.Object,
                locationServiceMock.Object
            );

            // act
            var items = repository.GetAll().ToList();

            // assert
            Assert.AreEqual(3, items.Count);
            Assert.AreEqual(1, items[0].Id);
            Assert.AreEqual(2, items[2].Id);
            Assert.AreEqual(3, items[1].Id);
        }

        #endregion

        #region AddAll

        [Test]
        public void AddAll_CallsDataStore()
        {
            // arrange
            var dataStoreMock = new Mock<IDataStore>(MockBehavior.Loose);

            var locationServiceMock = new Mock<ILocationService>(MockBehavior.Loose);

            var repository = new ZipCodesRepository(
                dataStoreMock.Object,
                locationServiceMock.Object
            );

            // act
            repository.AddAll(new[]
            {
                new ZipCodeModel
                {
                    Id = 1,
                    Zip = 1
                }
            });

            // assert
            dataStoreMock.Verify(mock => mock.InsertAll(It.Is<IEnumerable<ZipCodeModel>>(item => item.First().Id == 1)), Times.Once());
        }

        #endregion

        #region GetById

        [Test]
        public void GetById_CallsDataStore()
        {
            // arrange
            var model = new ZipCodeModel
            {
                Id = 1,
                Zip = 1
            };

            var dataStoreMock = new Mock<IDataStore>(MockBehavior.Loose);
            dataStoreMock.Setup(mock => mock.GetById<ZipCodeModel, int>(It.IsAny<int>()))
                         .Returns(model);

            var locationServiceMock = new Mock<ILocationService>(MockBehavior.Loose);

            var repository = new ZipCodesRepository(
                dataStoreMock.Object,
                locationServiceMock.Object
            );

            // act
            repository.GetById(1);

            // assert
            dataStoreMock.Verify(mock => mock.GetById<ZipCodeModel, int>(It.Is<int>(item => item == 1)), Times.Once());
        }

        [Test]
        public void GetById_ReturnsCorrectData()
        {
            // arrange
            var model = new ZipCodeModel
            {
                Id = 1,
                Zip = 1
            };

            var dataStoreMock = new Mock<IDataStore>(MockBehavior.Loose);
            dataStoreMock.Setup(mock => mock.GetById<ZipCodeModel, int>(It.IsAny<int>()))
                         .Returns(model);

            var locationServiceMock = new Mock<ILocationService>(MockBehavior.Loose);

            var repository = new ZipCodesRepository(
                dataStoreMock.Object,
                locationServiceMock.Object
            );

            // act
            var item = repository.GetById(1);

            // assert
            Assert.AreEqual(1, item.Id);
        }

        #endregion

        #region GetZipCodesCount

        [Test]
        public void GetZipCodesCount_CallsDataStore()
        {
            var dataStoreMock = new Mock<IDataStore>(MockBehavior.Loose);

            var locationServiceMock = new Mock<ILocationService>(MockBehavior.Loose);

            var repository = new ZipCodesRepository(
                dataStoreMock.Object,
                locationServiceMock.Object
            );

            // act
            var result = repository.GetZipCodesCount(0);

            // assert
            dataStoreMock.Verify(mock => mock.Count(It.IsAny<Expression<Func<ZipCodeModel, bool>>>()), Times.Once());
        }

        [Test]
        public void GetZipCodesCount_ReturnsCorrectData()
        {
            // arrange
            var dataStoreMock = new Mock<IDataStore>(MockBehavior.Loose);
            dataStoreMock.Setup(mock => mock.Count(It.IsAny<Expression<Func<ZipCodeModel, bool>>>()))
                         .Returns(3);

            var locationServiceMock = new Mock<ILocationService>(MockBehavior.Loose);

            var repository = new ZipCodesRepository(
                dataStoreMock.Object,
                locationServiceMock.Object
            );

            // act
            var result = repository.GetZipCodesCount(0);

            // assert
            Assert.AreEqual(3, result);
        }

        #endregion

        #region GetFirstForCity

        [Test]
        public void GetFirstForCity_CallsDataStore()
        {
            var dataStoreMock = new Mock<IDataStore>(MockBehavior.Loose);

            var locationServiceMock = new Mock<ILocationService>(MockBehavior.Loose);

            var repository = new ZipCodesRepository(
                dataStoreMock.Object,
                locationServiceMock.Object
            );

            // act
            var result = repository.GetFirstForCity(0);

            // assert
            dataStoreMock.Verify(mock => mock.FirstOrDefault(It.IsAny<Expression<Func<ZipCodeModel, bool>>>()), Times.Once());
        }

        [Test]
        public void GetFirstForCity_ReturnsCorrectData()
        {
            // arrange
            var dataStoreMock = new Mock<IDataStore>(MockBehavior.Loose);
            dataStoreMock.Setup(mock => mock.FirstOrDefault(It.IsAny<Expression<Func<ZipCodeModel, bool>>>()))
                         .Returns(new ZipCodeModel { Id = 1 });

            var locationServiceMock = new Mock<ILocationService>(MockBehavior.Loose);

            var repository = new ZipCodesRepository(
                dataStoreMock.Object,
                locationServiceMock.Object
            );

            // act
            var result = repository.GetFirstForCity(0);

            // assert
            Assert.AreEqual(1, result.Id);
        }

        #endregion

        #region GetAllForCity

        [Test]
        public void GetAllForCity_CallsDataStore()
        {
            // arrange
            var dataStoreMock = new Mock<IDataStore>(MockBehavior.Loose);

            var locationServiceMock = new Mock<ILocationService>(MockBehavior.Loose);

            var repository = new ZipCodesRepository(
                dataStoreMock.Object,
                locationServiceMock.Object
            );

            // act
            var items = repository.GetAll().ToList();

            // assert
            dataStoreMock.Verify(mock => mock.GetAll<ZipCodeModel>(), Times.Once());
        }

        [Test]
        public void GetAllForCity_ReturnsCorrectData()
        {
            // arrange
            var models = new[]
            {
                new ZipCodeModel
                {
                    Id = 1,
                    Zip = 1,
                    CityId = 1
                },
                new ZipCodeModel
                {
                    Id = 3,
                    Zip = 3,
                    CityId = 0
                },
                new ZipCodeModel
                {
                    Id = 2,
                    Zip = 2,
                    CityId = 1
                }
            };

            var dataStoreMock = new Mock<IDataStore>(MockBehavior.Loose);
            dataStoreMock.Setup(mock => mock.GetAll<ZipCodeModel>())
                         .Returns(models.AsQueryable());

            var locationServiceMock = new Mock<ILocationService>(MockBehavior.Loose);

            var repository = new ZipCodesRepository(
                dataStoreMock.Object,
                locationServiceMock.Object
            );

            // act
            var items = repository.GetAllForCity(1).ToList();

            // assert
            Assert.AreEqual(2, items.Count);
        }

        #endregion

        #region GetNearby

        [Test]
        public void GetNearby_CallsDataStore()
        {
            // arrange
            var dataStoreMock = new Mock<IDataStore>(MockBehavior.Loose);
            dataStoreMock.Setup(mock => mock.GetById<ZipCodeModel, int>(It.IsAny<int>()))
                         .Returns(new ZipCodeModel());

            var locationServiceMock = new Mock<ILocationService>(MockBehavior.Loose);

            var repository = new ZipCodesRepository(
                dataStoreMock.Object,
                locationServiceMock.Object
            );

            // act
            var items = repository.GetNearby(0, 1).ToList();

            // assert
            dataStoreMock.Verify(mock => mock.GetAll<ZipCodeModel>(), Times.Once());
        }

        [Test]
        public void GetNearby_ReturnsCorrectData()
        {
            // arrange
            var models = new[]
            {
                new ZipCodeModel
                {
                    Id = 1,
                    Zip = 1,
                    Latitude = 1,
                    Longitude = 1
                },
                new ZipCodeModel
                {
                    Id = 4,
                    Zip = 4,
                    Latitude = 4,
                    Longitude = 4
                },
                new ZipCodeModel
                {
                    Id = 3,
                    Zip = 3,
                    Latitude = 3,
                    Longitude = 3
                },
                new ZipCodeModel
                {
                    Id = 2,
                    Zip = 2,
                    Latitude = 2,
                    Longitude = 2
                }
            };

            var dataStoreMock = new Mock<IDataStore>(MockBehavior.Loose);
            dataStoreMock.Setup(mock => mock.GetById<ZipCodeModel, int>(It.IsAny<int>()))
                         .Returns<int>(item => models.FirstOrDefault(i => i.Id == item));
            dataStoreMock.Setup(mock => mock.GetAll<ZipCodeModel>())
                         .Returns(models.AsQueryable());

            var locationServiceMock = new Mock<ILocationService>(MockBehavior.Loose);
            locationServiceMock.Setup(mock => mock.GetDistance(It.IsAny<LocationModel>(), It.IsAny<LocationModel>()))
                               .Returns<LocationModel, LocationModel>((item1, item2) => Math.Sqrt(Math.Pow(item2.Latitude - item1.Latitude, 2) + Math.Pow(item2.Longitude - item1.Latitude, 2)));

            var repository = new ZipCodesRepository(
                dataStoreMock.Object,
                locationServiceMock.Object
            );

            // act
            var items = repository.GetNearby(2, 2).ToList();

            // assert
            Assert.AreEqual(2, items.Count);
        }

        #endregion
    }
}
