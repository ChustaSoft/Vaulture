﻿using ChustaSoft.Vaulture.UI.Pages;
using System.Globalization;
using System.Windows.Data;

namespace ChustaSoft.Vaulture.UI.Helpers;

public class SecureConnectionCollectionVisibilityConverter : IValueConverter
{
    public Object Convert(Object value, Type targetType, Object parameter, CultureInfo culture)
    {
        if (parameter is not string option)
            throw new ArgumentException($"Parameter must be 'Empty' or 'Content'");

        var castedObject = (SecureConnectionSecretsViewModel)value;
        var hasData = castedObject.Secrets.Any();

        return (hasData, option) switch
        {
            (false, "Empty") => Visibility.Visible,
            (true, "Content") => Visibility.Visible,
            _ => Visibility.Hidden,
        };
    }

    public Object ConvertBack(Object value, Type targetType, Object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
