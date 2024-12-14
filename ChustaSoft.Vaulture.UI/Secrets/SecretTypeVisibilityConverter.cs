using ChustaSoft.Vaulture.Domain.Secrets;
using System.Globalization;
using System.Windows.Data;

namespace ChustaSoft.Vaulture.UI.Secrets;


public class SecretTypeVisibilityConverter : IValueConverter
{
    public Object Convert(Object value, Type targetType, Object parameter, CultureInfo culture)
    {
        var castedValue = (SecretTypeVisibilityModel)value;

        return (castedValue.SecretType, castedValue.SecretsStorageConnectionSelected, parameter) switch
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
