using ChustaSoft.Vaulture.Domain.Settings;
using System.Collections.ObjectModel;

namespace ChustaSoft.Vaulture.Application.Settings;

public record SecureConnectionsDto(SecureConnectionType Type, ObservableCollection<SecureConnectionValue> Values);

public record struct SecureConnectionValue(string Alias, string Value)
{
    public override String ToString() => $"{Alias} ({Value})";
};
