namespace ChustaSoft.Vaulture.Domain.Secrets;


public interface ISecretSaveCommand
{
    SecretType Type { get; }
    string Name { get; set; }
    bool IsValid();
}


public record CredentialSaveCommand : ISecretSaveCommand
{
    public SecretType Type => SecretType.Credential;
    public string Name { get; set; } = string.Empty;
    public string Key { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;


    public bool IsValid()
    {
        return !string.IsNullOrWhiteSpace(Name) && !string.IsNullOrWhiteSpace(Key) && !string.IsNullOrWhiteSpace(Password);
    }
}


public record ConnectionStringSaveCommand : ISecretSaveCommand
{
    public SecretType Type => SecretType.ConnectionString;
    public string Name { get; set; } = string.Empty;
    public string Value { get; set; } = string.Empty;


    public bool IsValid()
    {
        return !string.IsNullOrWhiteSpace(Name) && !string.IsNullOrWhiteSpace(Value);
    }
}