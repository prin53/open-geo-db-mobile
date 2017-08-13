using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using MvvmCross.Platform.Converters;
using OpenGeoDB.ViewModels.Cities;

namespace OpenGeoDB.Converters
{
    public class GroupsToFlatValueConverter : MvxValueConverter<IEnumerable<IGrouping<string, CityItemViewModel>>, IEnumerable<CityItemViewModel>>
    {
        protected override IEnumerable<CityItemViewModel> Convert(IEnumerable<IGrouping<string, CityItemViewModel>> value, Type targetType, object parameter, CultureInfo culture)
        {
            return value?.SelectMany(item => item).ToList();
        }
    }
}
