using System.Text.Json;

namespace ChustaSoft.Vaulture.Domain.Secrets;

public record SecretValue
{

    public string Value { get; init; } = null!;

    private SecretValue() { }

    internal SecretValue(string value)
    {
        Value = value;
    }

    public static SecretValue FromCredential(string key, string password)
    {
        var secretObj = new CredentialValue(key, password);
        var secret = new SecretValue
        {
            Value = JsonSerializer.Serialize(secretObj)
        };
        
        return secret;
    }

    public static SecretValue FromConnectionString(string value)
    {
        var secretObj = new ConnectionStringValue(value);
        var secret = new SecretValue
        {
            Value = JsonSerializer.Serialize(secretObj)
        };

        return secret;
    }

    public CredentialValue RetrieveCredentialValue()
    {
        var credentialValue = JsonSerializer.Deserialize<CredentialValue>(Value);

        return credentialValue!;
    }

    public ConnectionStringValue RetrieveConnectionStringValue()
    {
        var credentialValue = JsonSerializer.Deserialize<ConnectionStringValue>(Value);

        return credentialValue!;
    }
}
