using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Moq;
using MvvmCross.Test.Core;
using NUnit.Framework;
using OpenGeoDB.Models;
using OpenGeoDB.Repositories.Cities;
using OpenGeoDB.Repositories.Update;
using OpenGeoDB.Repositories.ZipCodes;
using OpenGeoDB.Services.Data;
using OpenGeoDB.Services.Update;

namespace OpenGeoDB.Test.Unit.Services.Update
{
    [TestFixture]
    public class UpdateServiceTests : MvxIoCSupportingTest
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
            Assert.DoesNotThrow(() => new UpdateService(
                Mock.Of<IDataProvider>(),
                Mock.Of<IUpdateRepository>(),
                Mock.Of<ICitiesRepository>(),
                Mock.Of<IZipCodesRepository>()
            ));
        }

        [Test]
        public void Constructor_ThrowsArgumentNullException()
        {
            // arrange act assert
            Assert.Throws<ArgumentNullException>(() => new UpdateService(null, null, null, null));
        }

        #endregion

        #region Outdated

        [Test]
        public void Outdated_CallsUpdateRepository()
        {
            // arrange
            var dataProviderMock = new Mock<IDataProvider>(MockBehavior.Loose);

            var updateRepositoryMock = new Mock<IUpdateRepository>(MockBehavior.Loose);

            var citiesRepositoryMock = new Mock<ICitiesRepository>(MockBehavior.Loose);

            var zipCodesRepository = new Mock<IZipCodesRepository>(MockBehavior.Loose);

            var service = new UpdateService(
                dataProviderMock.Object,
                updateRepositoryMock.Object,
                citiesRepositoryMock.Object,
                zipCodesRepository.Object
            );

            // act
            var result = service.Outdated;

            // assert
            updateRepositoryMock.Verify(mock => mock.Updated, Times.Once(), "Check if 'Configuration.ShouldForceUpdate' is set to 'false");
        }

        [Test]
        public void Outdated_ReturnsCorrectData_Outdated()
        {
            // arrange
            var dataProviderMock = new Mock<IDataProvider>(MockBehavior.Loose);

            var updateRepositoryMock = new Mock<IUpdateRepository>(MockBehavior.Loose);
            updateRepositoryMock.Setup(mock => mock.Updated)
                                .Returns(DateTime.MinValue);

            var citiesRepositoryMock = new Mock<ICitiesRepository>(MockBehavior.Loose);

            var zipCodesRepository = new Mock<IZipCodesRepository>(MockBehavior.Loose);

            var service = new UpdateService(
                dataProviderMock.Object,
                updateRepositoryMock.Object,
                citiesRepositoryMock.Object,
                zipCodesRepository.Object
            );

            // act
            var result = service.Outdated;

            // assert
            Assert.IsTrue(result);
        }

        [Test]
        public void Outdated_ReturnsCorrectData_NotOutdated()
        {
            // arrange
            var dataProviderMock = new Mock<IDataProvider>(MockBehavior.Loose);

            var updateRepositoryMock = new Mock<IUpdateRepository>(MockBehavior.Loose);
            updateRepositoryMock.Setup(mock => mock.Updated)
                                .Returns(DateTime.MaxValue);

            var citiesRepositoryMock = new Mock<ICitiesRepository>(MockBehavior.Loose);

            var zipCodesRepository = new Mock<IZipCodesRepository>(MockBehavior.Loose);

            var service = new UpdateService(
                dataProviderMock.Object,
                updateRepositoryMock.Object,
                citiesRepositoryMock.Object,
                zipCodesRepository.Object
            );

            // act
            var result = service.Outdated;

            // assert
            Assert.IsFalse(result, "Check if 'Configuration.ShouldForceUpdate' is set to 'false");
        }

        #endregion

        #region Update

        [Test]
        public async Task Update_CallsDataProvider_NoCallWhenNoOutdated()
        {
            // arrange
            var dataProviderMock = new Mock<IDataProvider>(MockBehavior.Loose);

            var updateRepositoryMock = new Mock<IUpdateRepository>(MockBehavior.Loose);
            updateRepositoryMock.Setup(mock => mock.Updated)
                                .Returns(DateTime.MaxValue);

            var citiesRepositoryMock = new Mock<ICitiesRepository>(MockBehavior.Loose);

            var zipCodesRepository = new Mock<IZipCodesRepository>(MockBehavior.Loose);

            var service = new UpdateService(
                dataProviderMock.Object,
                updateRepositoryMock.Object,
                citiesRepositoryMock.Object,
                zipCodesRepository.Object
            );

            // act
            await service.UpdateAsync();

            // assert
            dataProviderMock.Verify(mock => mock.LoadAsync(It.IsAny<CancellationToken>()), Times.Never(), "Check if 'Configuration.ShouldForceUpdate' is set to 'false");
        }

        [Test]
        public async Task Update_CallsDataProvider_CallWhenOutdated()
        {
            // arrange
            var dataProviderMock = new Mock<IDataProvider>(MockBehavior.Loose);

            var updateRepositoryMock = new Mock<IUpdateRepository>(MockBehavior.Loose);
            updateRepositoryMock.Setup(mock => mock.Updated)
                                .Returns(DateTime.MinValue);

            var citiesRepositoryMock = new Mock<ICitiesRepository>(MockBehavior.Loose);

            var zipCodesRepository = new Mock<IZipCodesRepository>(MockBehavior.Loose);

            var service = new UpdateService(
                dataProviderMock.Object,
                updateRepositoryMock.Object,
                citiesRepositoryMock.Object,
                zipCodesRepository.Object
            );

            // act
            await service.UpdateAsync();

            // assert
            dataProviderMock.Verify(mock => mock.LoadAsync(It.IsAny<CancellationToken>()), Times.Once());
        }

        [Test]
        public async Task Update_ClearsCitiesData()
        {
            // arrange
            var dataProviderMock = new Mock<IDataProvider>(MockBehavior.Loose);

            var updateRepositoryMock = new Mock<IUpdateRepository>(MockBehavior.Loose);
            updateRepositoryMock.Setup(mock => mock.Updated)
                                .Returns(DateTime.MinValue);

            var citiesRepositoryMock = new Mock<ICitiesRepository>(MockBehavior.Loose);

            var zipCodesRepository = new Mock<IZipCodesRepository>(MockBehavior.Loose);

            var service = new UpdateService(
                dataProviderMock.Object,
                updateRepositoryMock.Object,
                citiesRepositoryMock.Object,
                zipCodesRepository.Object
            );

            // act
            await service.UpdateAsync();

            // assert
            citiesRepositoryMock.Verify(mock => mock.Clear(), Times.Once());
        }

        [Test]
        public async Task Update_ClearsZipCodesData()
        {
            // arrange
            var dataProviderMock = new Mock<IDataProvider>(MockBehavior.Loose);

            var updateRepositoryMock = new Mock<IUpdateRepository>(MockBehavior.Loose);
            updateRepositoryMock.Setup(mock => mock.Updated)
                                .Returns(DateTime.MinValue);

            var citiesRepositoryMock = new Mock<ICitiesRepository>(MockBehavior.Loose);

            var zipCodesRepository = new Mock<IZipCodesRepository>(MockBehavior.Loose);

            var service = new UpdateService(
                dataProviderMock.Object,
                updateRepositoryMock.Object,
                citiesRepositoryMock.Object,
                zipCodesRepository.Object
            );

            // act
            await service.UpdateAsync();

            // assert
            zipCodesRepository.Verify(mock => mock.Clear(), Times.Once());
        }

        [Test]
        public async Task Update_SavesCorrectData_CitiesCount()
        {
            // arrange
            var models = new RawZipCodeModel[]
            {
                new RawZipCodeModel
                {
                    Id = 1,
                    CityName = "1",
                    Zip = 1,
                    Latitude = 1,
                    Longitude = 1
                },
                new RawZipCodeModel
                {
                    Id = 2,
                    CityName = "2",
                    Zip = 2,
                    Latitude = 2,
                    Longitude = 2
                },
                new RawZipCodeModel
                {
                    Id = 3,
                    CityName = "2",
                    Zip = 3,
                    Latitude = 3,
                    Longitude = 3
                }
            };

            var dataProviderMock = new Mock<IDataProvider>(MockBehavior.Loose);
            dataProviderMock.Setup(mock => mock.LoadAsync(It.IsAny<CancellationToken>()))
                            .ReturnsAsync(models);

            var updateRepositoryMock = new Mock<IUpdateRepository>(MockBehavior.Loose);
            updateRepositoryMock.Setup(mock => mock.Updated)
                                .Returns(DateTime.MinValue);

            var citiesRepositoryMock = new Mock<ICitiesRepository>(MockBehavior.Loose);

            var zipCodesRepository = new Mock<IZipCodesRepository>(MockBehavior.Loose);

            var service = new UpdateService(
                dataProviderMock.Object,
                updateRepositoryMock.Object,
                citiesRepositoryMock.Object,
                zipCodesRepository.Object
            );

            // act
            await service.UpdateAsync();

            // assert
            citiesRepositoryMock.Verify(mock => mock.AddAll(
                It.Is<IEnumerable<CityModel>>(items => items.Count() == 2)
            ), Times.Once());
        }

        [Test]
        public async Task Update_SavesCorrectData_ZipCodesCount()
        {
            // arrange
            var models = new RawZipCodeModel[]
            {
                new RawZipCodeModel
                {
                    Id = 1,
                    CityName = "1",
                    Zip = 1,
                    Latitude = 1,
                    Longitude = 1
                },
                new RawZipCodeModel
                {
                    Id = 2,
                    CityName = "2",
                    Zip = 2,
                    Latitude = 2,
                    Longitude = 2
                },
                new RawZipCodeModel
                {
                    Id = 3,
                    CityName = "2",
                    Zip = 3,
                    Latitude = 3,
                    Longitude = 3
                }
            };

            var dataProviderMock = new Mock<IDataProvider>(MockBehavior.Loose);
            dataProviderMock.Setup(mock => mock.LoadAsync(It.IsAny<CancellationToken>()))
                            .ReturnsAsync(models);

            var updateRepositoryMock = new Mock<IUpdateRepository>(MockBehavior.Loose);
            updateRepositoryMock.Setup(mock => mock.Updated)
                                .Returns(DateTime.MinValue);

            var citiesRepositoryMock = new Mock<ICitiesRepository>(MockBehavior.Loose);

            var zipCodesRepository = new Mock<IZipCodesRepository>(MockBehavior.Loose);

            var service = new UpdateService(
                dataProviderMock.Object,
                updateRepositoryMock.Object,
                citiesRepositoryMock.Object,
                zipCodesRepository.Object
            );

            // act
            await service.UpdateAsync();

            // assert
            zipCodesRepository.Verify(mock => mock.AddAll(
                It.Is<IEnumerable<ZipCodeModel>>(items => items.Count() == 3)
            ), Times.Once());
        }

        [Test]
        public async Task Update_CalssServicesInCorrectOrder()
        {
            // arrange
            var sequence = new MockSequence();

            var dataProviderMock = new Mock<IDataProvider>(MockBehavior.Strict);

            var updateRepositoryMock = new Mock<IUpdateRepository>(MockBehavior.Strict);

            var citiesRepositoryMock = new Mock<ICitiesRepository>(MockBehavior.Strict);

            var zipCodesRepository = new Mock<IZipCodesRepository>(MockBehavior.Strict);

            updateRepositoryMock.InSequence(sequence)
                                .SetupGet(mock => mock.Updated)
                                .Returns(DateTime.MinValue);

            citiesRepositoryMock.InSequence(sequence)
                                .Setup(mock => mock.Clear());

            zipCodesRepository.InSequence(sequence)
                                .Setup(mock => mock.Clear());

            dataProviderMock.InSequence(sequence)
                            .Setup(mock => mock.LoadAsync(It.IsAny<CancellationToken>()))
                            .Returns(Task.FromResult(Enumerable.Empty<RawZipCodeModel>()));

            citiesRepositoryMock.InSequence(sequence)
                                .Setup(mock => mock.AddAll(It.IsAny<IEnumerable<CityModel>>()));

            zipCodesRepository.InSequence(sequence)
                              .Setup(mock => mock.AddAll(It.IsAny<IEnumerable<ZipCodeModel>>()));

            updateRepositoryMock.InSequence(sequence)
                                .SetupSet<DateTime>(mock => mock.Updated = It.IsAny<DateTime>());

            var service = new UpdateService(
                dataProviderMock.Object,
                updateRepositoryMock.Object,
                citiesRepositoryMock.Object,
                zipCodesRepository.Object
            );

            // act 
            await service.UpdateAsync();

            // assert
            updateRepositoryMock.Verify(mock => mock.Updated, Times.Once());
            citiesRepositoryMock.Verify(mock => mock.Clear(), Times.Once());
            zipCodesRepository.Verify(mock => mock.Clear(), Times.Once());
            dataProviderMock.Verify(mock => mock.LoadAsync(It.IsAny<CancellationToken>()), Times.Once());
            citiesRepositoryMock.Verify(mock => mock.AddAll(It.IsAny<IEnumerable<CityModel>>()), Times.Once());
            zipCodesRepository.Verify(mock => mock.AddAll(It.IsAny<IEnumerable<ZipCodeModel>>()), Times.Once());
            updateRepositoryMock.Verify(mock => mock.Updated, Times.Once());
        }

        #endregion
    }
}
