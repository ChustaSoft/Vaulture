using System.Text.Json;

namespace ChustaSoft.Vaulture.Domain.Secrets;

public record SecretValue
{

    public string Value { get; init; } = null!;

    private SecretValue() { }

    public SecretValue(string value)
    {
        Value = value;
    }

    public SecretValue(CredentialSaveCommand credentialSaveCommand)
    {
        var secretObj = new CredentialValue(credentialSaveCommand.Key, credentialSaveCommand.Password);

        Value = JsonSerializer.Serialize(secretObj);
    }

    public SecretValue(ConnectionStringSaveCommand connectionStringSaveCommand)
    {
        var secretObj = new ConnectionStringValue(connectionStringSaveCommand.Value);

        Value = JsonSerializer.Serialize(secretObj);
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
