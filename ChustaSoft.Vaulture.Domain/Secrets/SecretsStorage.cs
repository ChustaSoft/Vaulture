namespace ChustaSoft.Vaulture.Domain.Secrets;


public class SecretsStorage
{
    public SecretsStorageType Type { get; init; }
    public string Alias { get; init; }
    public string Value { get; init; }

    public SecretsStorage(SecretsStorageType type, string alias, string value)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(alias);
        ArgumentException.ThrowIfNullOrWhiteSpace(value);

        Type = type;
        Alias = alias;
        Value = value;
    }
}
