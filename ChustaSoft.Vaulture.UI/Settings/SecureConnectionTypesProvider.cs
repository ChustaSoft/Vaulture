namespace ChustaSoft.Vaulture.UI.Settings;

public class SecureConnectionTypesProvider
{

    private static SecureConnectionTypesModel? _values;


    public static SecureConnectionTypesModel Values
    {
        get
        {
            if (_values is null)
                _values = new SecureConnectionTypesModel();

            return _values;
        }
    }

    public static SecureConnectionTypeModel Default
    {
        get
        {
            if (_values is null)
                _values = new SecureConnectionTypesModel();

            return _values[0];
        }
    }

}