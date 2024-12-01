using ChustaSoft.Common.Helpers;
using ChustaSoft.Vaulture.Domain.Settings;
using System.Collections.ObjectModel;

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

public class SecureConnectionTypesModel : ObservableCollection<SecureConnectionTypeModel>
{
    internal SecureConnectionTypesModel()
    {
        foreach (var connectionType in EnumsHelper.GetEnumList<SecureConnectionType>())
            Add(new SecureConnectionTypeModel(connectionType, EnumsHelper.GetDescription(connectionType)));
    }

}

public record struct SecureConnectionTypeModel(SecureConnectionType Type, string Name)
{
    public override string ToString() => Name;
};