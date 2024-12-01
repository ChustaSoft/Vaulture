using ChustaSoft.Common.Helpers;
using ChustaSoft.Vaulture.Domain.Settings;
using System.Globalization;
using System.Windows.Data;

namespace ChustaSoft.Vaulture.UI.Helpers;

public class SecureConnectionTypeConverter : IValueConverter
{
    public Object Convert(Object value, Type targetType, Object parameter, CultureInfo culture)
    {
        var castedObject = (SecureConnectionType)value;

        return EnumsHelper.GetDescription(castedObject);
    }

    public Object ConvertBack(Object value, Type targetType, Object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
