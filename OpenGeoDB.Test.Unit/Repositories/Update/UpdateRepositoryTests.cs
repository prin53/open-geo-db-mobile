using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Moq;
using MvvmCross.Test.Core;
using NUnit.Framework;
using OpenGeoDB.Models;
using OpenGeoDB.Repositories.Update;
using OpenGeoDB.Services.Data;

namespace OpenGeoDB.Test.Unit.Repositories.Update
{
    [TestFixture]
    public class UpdateRepositoryTests : MvxIoCSupportingTest
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
            Assert.DoesNotThrow(() => new UpdateRepository(
                Mock.Of<IDataStore>()
            ));
        }

        [Test]
        public void Constructor_ThrowsArgumentNullException()
        {
            // arrange act assert
            Assert.Throws<ArgumentNullException>(() => new UpdateRepository(null));
        }

        #endregion

        #region Updated Get

        [Test]
        public void Updated_Get_CallsDataStore()
        {
            // arrange
            var dataStoreMock = new Mock<IDataStore>(MockBehavior.Loose);
            dataStoreMock.Setup(mock => mock.FirstOrDefault(It.IsAny<Expression<Func<UpdateModel, bool>>>()))
                         .Returns(new UpdateModel { Updated = DateTime.MaxValue });

            var repository = new UpdateRepository(
                dataStoreMock.Object
            );

            // act
            var updated = repository.Updated;

            // assert
            dataStoreMock.Verify(mock => mock.FirstOrDefault(It.IsAny<Expression<Func<UpdateModel, bool>>>()), Times.Once());
        }

        [Test]
        public void Updated_Get_ReturnsCorrectData()
        {
            // arrange
            var dataStoreMock = new Mock<IDataStore>(MockBehavior.Loose);
            dataStoreMock.Setup(mock => mock.FirstOrDefault(It.IsAny<Expression<Func<UpdateModel, bool>>>()))
                         .Returns(new UpdateModel { Updated = DateTime.MaxValue });

            var repository = new UpdateRepository(
                dataStoreMock.Object
            );

            // act
            var updated = repository.Updated;

            // assert
            Assert.AreEqual(DateTime.MaxValue, updated);
        }

        #endregion

        #region Updated Set

        [Test]
        public void Updated_Set_CallsDataStore()
        {
            // arrange
            var dataStoreMock = new Mock<IDataStore>(MockBehavior.Loose);

            var repository = new UpdateRepository(
                dataStoreMock.Object
            );

            // act
            repository.Updated = DateTime.MinValue;

            // assert
            dataStoreMock.Verify(mock => mock.InsertAll(It.Is<IEnumerable<UpdateModel>>(item => item.First().Updated == DateTime.MinValue)), Times.Once());
        }
       
        #endregion
    }
}
