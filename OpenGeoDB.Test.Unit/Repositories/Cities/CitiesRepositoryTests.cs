using System;
using System.Collections.Generic;
using System.Linq;
using Moq;
using MvvmCross.Test.Core;
using NUnit.Framework;
using OpenGeoDB.Models;
using OpenGeoDB.Repositories.Cities;
using OpenGeoDB.Services.Data;

namespace Repositories.Cities
{
    [TestFixture]
    public class CitiesRepositoryTests : MvxIoCSupportingTest
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
            Assert.DoesNotThrow(() => new CitiesRepository(
                Mock.Of<IDataStore>()
            ));
        }

        [Test]
        public void Constructor_ThrowsArgumentNullException()
        {
            // arrange act assert
            Assert.Throws<ArgumentNullException>(() => new CitiesRepository(null));
        }

        #endregion

        #region Clear

        [Test]
        public void Clear_CallsDataStore()
        {
            // arrange
            var dataStoreMock = new Mock<IDataStore>(MockBehavior.Loose);

            var repository = new CitiesRepository(
                dataStoreMock.Object
            );

            // act
            repository.Clear();

            // assert
            dataStoreMock.Verify(mock => mock.ClearAll<CityModel>(), Times.Once());
        }

        #endregion

        #region GetAll

        [Test]
        public void GetAll_CallsDataStore()
        {
            // arrange
            var models = new[]
            {
                new CityModel
                {
                    Id = 1,
                    Name = "1"
                },
                new CityModel
                {
                    Id = 3,
                    Name = "3"
                },
                new CityModel
                {
                    Id = 2,
                    Name = "2"
                }
            };

            var dataStoreMock = new Mock<IDataStore>(MockBehavior.Loose);
            dataStoreMock.Setup(mock => mock.GetAll<CityModel>())
                         .Returns(models.AsQueryable());

            var repository = new CitiesRepository(
                dataStoreMock.Object
            );

            // act
            var items = repository.GetAll().ToList();

            // assert
            dataStoreMock.Verify(mock => mock.GetAll<CityModel>(), Times.Once());
        }

        [Test]
        public void GetAll_ReturnsCorrectData()
        {
            // arrange
            var models = new[]
            {
                new CityModel
                {
                    Id = 1,
                    Name = "1"
                },
                new CityModel
                {
                    Id = 3,
                    Name = "3"
                },
                new CityModel
                {
                    Id = 2,
                    Name = "2"
                }
            };

            var dataStoreMock = new Mock<IDataStore>(MockBehavior.Loose);
            dataStoreMock.Setup(mock => mock.GetAll<CityModel>())
                         .Returns(models.AsQueryable());

            var repository = new CitiesRepository(
                dataStoreMock.Object
            );

            // act
            var items = repository.GetAll().ToList();

            // assert
            Assert.AreEqual(3, items.Count);
            Assert.AreEqual(1, items[0].Id);
            Assert.AreEqual(2, items[1].Id);
            Assert.AreEqual(3, items[2].Id);
        }

        #endregion

        #region AddAll

        [Test]
        public void AddAll_CallsDataStore()
        {
            // arrange
            var dataStoreMock = new Mock<IDataStore>(MockBehavior.Loose);

            var repository = new CitiesRepository(
                dataStoreMock.Object
            );

            // act
            repository.AddAll(new[]
            {
                new CityModel
                {
                    Id = 1,
                    Name = "1"
                }
            });

            // assert
            dataStoreMock.Verify(mock => mock.InsertAll(It.Is<IEnumerable<CityModel>>(item => item.First().Id == 1)), Times.Once());
        }

        #endregion

        #region GetById

        [Test]
        public void GetById_CallsDataStore()
        {
            // arrange
            var model = new CityModel
            {
                Id = 1,
                Name = "1"
            };

            var dataStoreMock = new Mock<IDataStore>(MockBehavior.Loose);
            dataStoreMock.Setup(mock => mock.GetById<CityModel, int>(It.IsAny<int>()))
                         .Returns(model);

            var repository = new CitiesRepository(
                dataStoreMock.Object
            );

            // act
            repository.GetById(1);

            // assert
            dataStoreMock.Verify(mock => mock.GetById<CityModel, int>(It.Is<int>(item => item == 1)), Times.Once());
        }

        [Test]
        public void GetById_ReturnsCorrectData()
        {
            // arrange
            var model = new CityModel
            {
                Id = 1,
                Name = "1"
            };

            var dataStoreMock = new Mock<IDataStore>(MockBehavior.Loose);
            dataStoreMock.Setup(mock => mock.GetById<CityModel, int>(It.IsAny<int>()))
                         .Returns(model);

            var repository = new CitiesRepository(
                dataStoreMock.Object
            );

            // act
            var item = repository.GetById(1);

            // assert
            Assert.AreEqual(1, item.Id);
        }

        #endregion
    }
}