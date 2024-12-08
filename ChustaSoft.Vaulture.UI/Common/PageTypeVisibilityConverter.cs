﻿using System.Globalization;
using System.Windows.Data;

namespace ChustaSoft.Vaulture.UI.Common;


public class PageTypeVisibilityConverter : IValueConverter
{

    /// <summary>
    /// Currently, the method is not parametrized, so it's always determining that, every element under PageType = Edit, will hide the element
    /// </summary>
    /// <param name="value"></param>
    /// <param name="targetType"></param>
    /// <param name="parameter"></param>
    /// <param name="culture"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentException"></exception>
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is not PageMode)
            throw new ArgumentException($"Parameter must be an string valid to be casted into a {typeof(PageMode).Name}");

        var pageMode = (PageMode)value;

        return (pageMode == PageMode.Edit) ? Visibility.Collapsed : Visibility.Visible;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
