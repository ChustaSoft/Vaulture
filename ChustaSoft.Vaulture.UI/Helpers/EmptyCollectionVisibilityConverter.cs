using System.Globalization;
using System.Windows.Data;

namespace ChustaSoft.Vaulture.UI.Helpers;

public class EmptyCollectionVisibilityConverter : IValueConverter
{
    public Object Convert(Object value, Type targetType, Object parameter, CultureInfo culture)
    {
        if (value is not IEnumerable<object> values)
            throw new ArgumentException($"Value must be a IEnumerable");

        if (parameter is not string option)
            throw new ArgumentException($"Parameter must be 'Empty' or 'Content'");

        return (values.Count(), option) switch
        {
            (0, "Empty") => Visibility.Visible,
            ( > 0, "Content") => Visibility.Visible,
            _ => Visibility.Hidden,
        };
    }

    public Object ConvertBack(Object value, Type targetType, Object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
