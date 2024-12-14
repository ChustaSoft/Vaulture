using ChustaSoft.Common.Helpers;
using ChustaSoft.Vaulture.Domain.Secrets;
using System.Globalization;
using System.Windows.Data;

namespace ChustaSoft.Vaulture.UI.Secrets;


public class SecretsStorageTypeConverter : IValueConverter
{
    public Object Convert(Object value, Type targetType, Object parameter, CultureInfo culture)
    {
        var castedObject = (SecretsStorageType)value;

        return castedObject.GetDescription();
    }

    public Object ConvertBack(Object value, Type targetType, Object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
