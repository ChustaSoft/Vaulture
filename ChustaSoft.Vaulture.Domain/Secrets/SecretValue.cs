using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ChustaSoft.Vaulture.Domain.Secrets;

public record SecretValue
{

    public string Value { get; init; }


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
