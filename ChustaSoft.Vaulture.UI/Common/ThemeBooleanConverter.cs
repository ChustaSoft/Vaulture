﻿using System.Globalization;
using System.Windows.Data;
using ThemeMode = ChustaSoft.Vaulture.Domain.Settings.ThemeMode;

namespace ChustaSoft.Vaulture.UI.Common;


public class ThemeBooleanConverter : IValueConverter
{
    public Object Convert(Object value, Type targetType, Object parameter, CultureInfo culture)
    {
        if (parameter is not string enumString)
            throw new ArgumentException($"Parameter must be an string valid to be casted into a {typeof(ThemeMode).Name}");

        var enumValue = Enum.Parse(typeof(ThemeMode), enumString);

        return enumValue.Equals(value);
    }

    public Object ConvertBack(Object value, Type targetType, Object parameter, CultureInfo culture)
    {
        var enumValue = Enum.Parse(typeof(ThemeMode), (string)parameter);

        return enumValue;
    }
}
