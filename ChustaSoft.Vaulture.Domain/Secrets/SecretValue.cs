using System.Text.Json;

namespace ChustaSoft.Vaulture.Domain.Secrets;

public record SecretValue
{

    public string Value { get; init; }


    internal SecretValue(string value)
    {
        Value = value;
    }

    public SecretValue(string key, string password)
    {
        if (string.IsNullOrWhiteSpace(key) || string.IsNullOrWhiteSpace(password))
            throw new ArgumentNullException("Key nor password could be null or empty values");

        var secretObj = new CredentialValue(key, password);

        Value = JsonSerializer.Serialize(secretObj);
    }

    public CredentialValue AsCredentialValue()
    {
        var credentialValue = JsonSerializer.Deserialize<CredentialValue>(Value);

        return credentialValue;
    }
}
