using ChustaSoft.Common.Helpers;
using ChustaSoft.Vaulture.Domain.Settings;
using System.Collections.ObjectModel;

namespace ChustaSoft.Vaulture.UI.Settings;


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