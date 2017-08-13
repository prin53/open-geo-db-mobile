using System;
using MvvmCross.Test.Core;
using NUnit.Framework;
using OpenGeoDB.Models;
using OpenGeoDB.ViewModels.Cities;

namespace OpenGeoDB.Test.Unit.ViewModels.Cities
{
    [TestFixture]
    public class CityItemViewModelTests : MvxIoCSupportingTest
    {
        [SetUp]
        public void SetupTest()
        {
            Setup();

            SetInvariantCulture();
        }

        #region Creators

        [Test]
        public void Creator_DoesNotThrowException()
        {
            // arrange act assert
            Assert.DoesNotThrow(() => CityItemViewModel.Create(new CityModel()));
        }

        [Test]
        public void Creator_ThrowsArgumentNullException()
        {
            // arrange act assert
            Assert.Throws<ArgumentNullException>(() => CityItemViewModel.Create(null));
        }

        [Test]
        public void Creator_SetsModelCorrectly()
        {
            // arrange
            var model = new CityModel
            {
                Id = 2,
                Name = "test"
            };

            // act
            var viewModel = CityItemViewModel.Create(model);

            // assert
            Assert.AreEqual(model, viewModel.Model);
        }

        [Test]
        public void Creator_SetsZipCorrectly()
        {
            // arrange
            var model = new CityModel
            {
                Id = 2,
                Name = "test"
            };

            // act
            var viewModel = CityItemViewModel.Create(model);

            // assert
            Assert.AreEqual(model.Name, viewModel.Name);
        }

        #endregion
    }
}
