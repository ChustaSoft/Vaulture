namespace ChustaSoft.Vaulture.Domain.Secrets;

public class Secret
{

    public SecretType Type { get; init; }
    public string Name { get; init; }
    public SecretValue Value { get; private set; } = null!;


    internal Secret(SecretType type, string name)
    {
        Type = type;
        Name = name;
    }

    public Secret(SecretType type, string name, string value)
        : this(type, name)
    {
        Value = new SecretValue(value);
    }

    public Secret(string name, string key, string password)
       : this(SecretType.Credential, name)
    {
        Value = SecretValue.FromCredential(key, password);
    }

    public Secret(string name, string connectionString)
       : this(SecretType.ConnectionString, name)
    {
        Value = SecretValue.FromConnectionString(connectionString);
    }

}
