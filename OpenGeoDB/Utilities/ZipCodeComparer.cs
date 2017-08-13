using System;
using System.Collections.Generic;
using OpenGeoDB.Models;
using OpenGeoDB.Services.Location;

namespace OpenGeoDB.Utilities
{
    public class ZipCodeComparer : IComparer<ZipCodeModel>
    {
        public ZipCodeComparer(ILocationService locationService, ZipCodeModel zipCodeToCompare)
        {
            LocationService = locationService ?? throw new ArgumentNullException(nameof(locationService));
            ZipCodeToCompare = zipCodeToCompare ?? throw new ArgumentNullException(nameof(zipCodeToCompare));
        }

        protected ILocationService LocationService { get; }

        protected ZipCodeModel ZipCodeToCompare { get; }

        public int Compare(ZipCodeModel x, ZipCodeModel y)
        {
            var distanceX = LocationService.GetDistance(
                ZipCodeToCompare.Location,
                x.Location
            );

            var distanceY = LocationService.GetDistance(
                ZipCodeToCompare.Location,
                y.Location
            );

            return distanceX.CompareTo(distanceY);
        }
    }
}
