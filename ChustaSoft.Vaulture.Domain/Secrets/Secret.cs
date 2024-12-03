namespace ChustaSoft.Vaulture.Domain.Secrets;

public class Secret
{

    public SecretType Type { get; init; }
    public string Name { get; set; }
    public SecretValue Value { get; set; } = null!;


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

    public Secret(SecretType type, string name, string key, string password)
       : this(type, name)
    {
        Value = new SecretValue(key, password);
    }

    //TODO: Missing logic here to generate a string value from any given secret, separating elements by :, ensuring that name doesn't have : internally
}
