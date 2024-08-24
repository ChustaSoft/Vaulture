namespace ChustaSoft.Vaulture.Domain.Secrets;

public class Secret
{

    public SecretType Type { get; init; }
    public string Name { get; set; }
    public SecretValue Value { get; set; }


    internal Secret(string name)
    {
        Name = name;
    }

    public Secret(string name, string key, string password)
        : this(name)
    {
        Value = new SecretValue(key, password);
    }

    //TODO: Missing logic here to generate a string value from any given secret, separating elements by :, ensuring that name doesn't have : internally
}
