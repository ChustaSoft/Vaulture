namespace ChustaSoft.Vaulture.Domain.Secrets;


public record CredentialValue
{
    public String Key { get; }
    public String Password { get; }


    public CredentialValue(string key, string password)
    {
        if (string.IsNullOrWhiteSpace(key) || string.IsNullOrWhiteSpace(password))
            throw new ArgumentNullException("Key nor password could be null or empty values");

        Key = key;
        Password = password;
    }

};


public record ConnectionStringValue
{
    public String Value { get; }


    public ConnectionStringValue(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentNullException("Connection String value could be null or empty values");

        Value = value;
    }

};
