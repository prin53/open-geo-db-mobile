using System;
using OpenGeoDB.Models;

namespace OpenGeoDB.ViewModels.Cities
{
    public class CityItemViewModel
    {
        internal CityModel Model { get; private set;}

        public string Name => Model.Name;

        public static CityItemViewModel Create(CityModel model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            return new CityItemViewModel
            {
                Model = model
            };
        }
    }
}
