using System;
using System.Globalization;
using MvvmCross.Platform.Converters;
using OpenGeoDB.Models;

namespace OpenGeoDB.Converters
{
    public class LocationToStringConverter : MvxValueConverter<LocationModel, string>
    {
        protected override string Convert(LocationModel value, Type targetType, object parameter, CultureInfo culture)
        {
            return value == null 
                ? string.Empty
                : $"{value.Latitude:0.####}, {value.Longitude:0.####}";
        }
    }
}
