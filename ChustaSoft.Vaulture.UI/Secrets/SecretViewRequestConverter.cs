using ChustaSoft.Vaulture.Application.Secrets;
using ChustaSoft.Vaulture.Application.Settings;
using ChustaSoft.Vaulture.Domain.Secrets;
using System.Globalization;
using System.Windows.Data;

namespace ChustaSoft.Vaulture.UI.Secrets;

public class SecretViewRequestConverter : IMultiValueConverter
{
    public Object Convert(Object[] values, Type targetType, Object parameter, CultureInfo culture)
    {
        var connectionValues = (SecureConnectionValue)values[0];
        var secretDto = (SecretDto) values[1];

        return new SecretViewRequestModel(connectionValues.Type, connectionValues.Value, new SecretDto(secretDto.Type, secretDto.Name));
    }

    public Object[] ConvertBack(Object value, Type[] targetTypes, Object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
