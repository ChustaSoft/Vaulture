using ChustaSoft.Vaulture.Domain.Secrets;

namespace ChustaSoft.Vaulture.Application.Secrets;

public record struct SecretsStorageDto(SecretsStorageType Type, string Alias, string Value)
{
    public override string ToString() => $"{Alias} ({Value})";
};