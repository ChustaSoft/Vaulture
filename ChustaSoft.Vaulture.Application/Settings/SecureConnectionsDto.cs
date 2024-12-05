using ChustaSoft.Vaulture.Domain.Secrets;
using System.Collections.ObjectModel;

namespace ChustaSoft.Vaulture.Application.Settings;


public record SecureConnectionsDto(SecretsResourceType Type, ObservableCollection<SecureConnectionValue> Values);


public record struct SecureConnectionValue(SecretsResourceType Type, string Alias, string Value)
{
    public override string ToString() => $"{Alias} ({Value})";
};
