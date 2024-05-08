using ChustaSoft.Vaulture.Domain.Settings;
using System.Globalization;
using System.Windows.Data;

namespace ChustaSoft.Vaulture.UI.Helpers;


public class ThemeBooleanConverter : IValueConverter
{
    public Object Convert(Object value, Type targetType, Object parameter, CultureInfo culture)
    {
        if (parameter is not string enumString)
            throw new ArgumentException($"Parameter must be an enum value of {typeof(ThemeMode).Name}");

        var enumValue = Enum.Parse(typeof(ThemeMode), enumString);

        return enumValue.Equals(value);
    }

    public Object ConvertBack(Object value, Type targetType, Object parameter, CultureInfo culture)
    {
        var enumValue = Enum.Parse(typeof(ThemeMode), (string)parameter);

        return enumValue;
    }
}
