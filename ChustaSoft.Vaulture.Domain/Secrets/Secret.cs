namespace ChustaSoft.Vaulture.Domain.Secrets;

public class Secret
{

    public SecretType Type { get; init; }
    public string Name { get; init; }
    public SecretValue Value { get; private set; } = null!;


    public Secret(SecretType type, string name, SecretValue value)
    {
        Type = type;
        Name = name;
        Value = value;
    }

    public Secret(ISecretSaveCommand saveCommand)
    {
        Type = saveCommand.Type;
        Name = saveCommand.Name;

        Value = saveCommand.Type switch
        {
            SecretType.Credential => new SecretValue((CredentialSaveCommand)saveCommand),
            SecretType.ConnectionString => new SecretValue((ConnectionStringSaveCommand)saveCommand),

            _ => throw new ArgumentException($"Unsupported Secret type: {saveCommand.Type}")
        };
    }

}
