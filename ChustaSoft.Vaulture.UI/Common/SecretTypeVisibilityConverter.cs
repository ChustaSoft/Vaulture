using ChustaSoft.Vaulture.Domain.Secrets;
using ChustaSoft.Vaulture.UI.Secrets;
using System.Globalization;
using System.Windows.Data;

namespace ChustaSoft.Vaulture.UI.Common;


public class SecretTypeVisibilityConverter : IValueConverter
{
    public Object Convert(Object value, Type targetType, Object parameter, CultureInfo culture)
    {
        var castedValue = (SecretTypeVisibilityModel)value;

        return (castedValue.SecretType, castedValue.SelectedConnection, parameter) switch
        {
            (null, _, "Empty") => Visibility.Visible,
            (_, null, "Empty") => Visibility.Visible,
            (SecretType.Credential, not null, "Credential") => Visibility.Visible,
            _ => Visibility.Hidden,
        };
    }

    public Object ConvertBack(Object value, Type targetType, Object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
