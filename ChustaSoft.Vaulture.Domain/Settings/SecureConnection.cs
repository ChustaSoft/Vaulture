using ChustaSoft.Vaulture.Domain.Secrets;

namespace ChustaSoft.Vaulture.Domain.Settings;


public class SecureConnection
{
    public SecretsResourceType Type { get; init; }
    public string Alias { get; init; }
    public string Value { get; init; }

    public SecureConnection(SecretsResourceType type, string alias, string value)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(alias);
        ArgumentException.ThrowIfNullOrWhiteSpace(value);

        Type = type;
        Alias = alias;
        Value = value;
    }
}
