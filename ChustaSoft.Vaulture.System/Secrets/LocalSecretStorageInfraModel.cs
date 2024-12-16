using ChustaSoft.Vaulture.Domain.Secrets;

namespace ChustaSoft.Vaulture.LocalSystem.Secrets;

public record LocalSecretStorageInfraModel
{
    public List<SecretInfraModel> Secrets { get; set; } = new List<SecretInfraModel>();
}

public record SecretInfraModel
{
    public SecretType Type { get; set; }
    public string Name { get; set; } = null!;
    public string Value { get; set; } = null!;
}