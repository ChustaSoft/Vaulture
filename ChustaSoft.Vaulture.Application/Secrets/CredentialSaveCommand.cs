namespace ChustaSoft.Vaulture.Application.Secrets;

public record CredentialSaveCommand
{
    public string Name { get; set; } = string.Empty;
    public string Key { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;


    public bool IsValid()
    {
        return !string.IsNullOrWhiteSpace(Name) && !string.IsNullOrWhiteSpace(Key) && !string.IsNullOrWhiteSpace(Password);
    }
}