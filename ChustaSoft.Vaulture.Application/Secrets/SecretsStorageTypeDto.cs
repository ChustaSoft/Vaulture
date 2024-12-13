using ChustaSoft.Vaulture.Domain.Secrets;

namespace ChustaSoft.Vaulture.Application.Secrets;

public record struct SecretsStorageTypeDto(SecretsStorageType Type, string Name)
{
    public override string ToString() => Name;
};