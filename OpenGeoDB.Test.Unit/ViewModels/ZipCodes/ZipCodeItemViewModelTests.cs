using System;
using MvvmCross.Test.Core;
using NUnit.Framework;
using OpenGeoDB.Models;
using OpenGeoDB.ViewModels.ZipCodes;

namespace OpenGeoDB.Test.Unit.ViewModels.ZipCodes
{
    [TestFixture]
    public class ZipCodeItemViewModelTests : MvxIoCSupportingTest
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
            Assert.DoesNotThrow(() => ZipCodeItemViewModel.Create(new ZipCodeModel()));
        }

        [Test]
        public void Creator_ThrowsArgumentNullException()
        {
            // arrange act assert
            Assert.Throws<ArgumentNullException>(() => ZipCodeItemViewModel.Create(null));
        }

        [Test]
        public void Creator_SetsModelCorrectly()
        {
            // arrange
            var model = new ZipCodeModel
            {
                Id = 2,
                Zip = 1234
            };

            // act
            var viewModel = ZipCodeItemViewModel.Create(model);

            // assert
            Assert.AreEqual(model, viewModel.Model);
        }

        [Test]
        public void Creator_SetsZipCorrectly()
        {
            // arrange
            var model = new ZipCodeModel
            {
                Id = 2,
                Zip = 1234
            };

            // act
            var viewModel = ZipCodeItemViewModel.Create(model);

            // assert
            Assert.AreEqual(model.Zip, viewModel.Zip);
        }

        #endregion
    }
}
