using System;
using OpenGeoDB.Models;

namespace OpenGeoDB.ViewModels.ZipCodes
{
    public class ZipCodeItemViewModel
    {
        internal ZipCodeModel Model { get; private set; }

        public int Zip => Model.Zip;

        public LocationModel Location => Model.Location;

        public static ZipCodeItemViewModel Create(ZipCodeModel model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            return new ZipCodeItemViewModel
            {
                Model = model
            };
        }
    }
}
