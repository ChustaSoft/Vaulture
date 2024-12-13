using ChustaSoft.Vaulture.Application.Secrets;

namespace ChustaSoft.Vaulture.UI.Secrets;

public class SecretsStorageTypeViewModelProvider
{

    private static SecretsStorageTypeViewModel? _values;


    public static SecretsStorageTypeViewModel Values
    {
        get
        {
            if (_values is null)
                _values = new SecretsStorageTypeViewModel();

            return _values;
        }
    }

    public static SecretsStorageTypeDto Default
    {
        get
        {
            if (_values is null)
                _values = new SecretsStorageTypeViewModel();

            return _values[0];
        }
    }

}
