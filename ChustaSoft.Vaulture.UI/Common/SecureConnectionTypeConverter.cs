using ChustaSoft.Common.Helpers;
using ChustaSoft.Vaulture.Domain.Secrets;
using System.Globalization;
using System.Windows.Data;

namespace ChustaSoft.Vaulture.UI.Common;

public class SecureConnectionTypeConverter : IValueConverter
{
    public Object Convert(Object value, Type targetType, Object parameter, CultureInfo culture)
    {
        var castedObject = (SecretsResourceType)value;

        return castedObject.GetDescription();
    }

    public Object ConvertBack(Object value, Type targetType, Object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
