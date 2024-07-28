using ChustaSoft.Vaulture.UI.Pages;
using System.Globalization;
using System.Windows.Data;

namespace ChustaSoft.Vaulture.UI.Helpers;

public class SecureConnectionCollectionSecretsConverter : IValueConverter
{
    public Object Convert(Object value, Type targetType, Object parameter, CultureInfo culture)
    {
        var castedObject = (SecureConnectionSecretsViewModel)value;

        return castedObject.Secrets;
    }

    public Object ConvertBack(Object value, Type targetType, Object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
