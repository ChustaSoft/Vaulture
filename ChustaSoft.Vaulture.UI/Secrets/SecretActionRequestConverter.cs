using ChustaSoft.Vaulture.Application.Secrets;
using System.Globalization;
using System.Windows.Data;

namespace ChustaSoft.Vaulture.UI.Secrets;

public class SecretActionRequestConverter : IMultiValueConverter
{
    public Object Convert(Object[] values, Type targetType, Object parameter, CultureInfo culture)
    {
        var secretsStorage = (SecretsStorageDto)values[0];
        var secret = (SecretDto) values[1];

        return new SecretActionRequestModel(secretsStorage.Type, secretsStorage.Value, new SecretDto(secret.Type, secret.Name));
    }

    public Object[] ConvertBack(Object value, Type[] targetTypes, Object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
