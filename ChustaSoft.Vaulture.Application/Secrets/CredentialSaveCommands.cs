namespace ChustaSoft.Vaulture.Application.Secrets;


public interface ISecretCreationCommand
{
    string Name { get; set; }
    bool IsValid();
}


public record CredentialSaveCommand : ISecretCreationCommand
{
    public string Name { get; set; } = string.Empty;
    public string Key { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;


    public bool IsValid()
    {
        return !string.IsNullOrWhiteSpace(Name) && !string.IsNullOrWhiteSpace(Key) && !string.IsNullOrWhiteSpace(Password);
    }
}


public record ConnectionStringSaveCommand : ISecretCreationCommand
{
    public string Name { get; set; } = string.Empty;
    public string Value { get; set; } = string.Empty;


    public bool IsValid()
    {
        return !string.IsNullOrWhiteSpace(Name) && !string.IsNullOrWhiteSpace(Value);
    }
}