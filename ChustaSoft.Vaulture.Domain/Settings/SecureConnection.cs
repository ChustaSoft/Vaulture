namespace ChustaSoft.Vaulture.Domain.Settings;


public class SecureConnection
{
    public SecureConnectionType Type { get; init; }
    public string Value { get; init; }

    public SecureConnection(SecureConnectionType type, string value)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(value);

        Type = type;
        Value = value;
    }
}
