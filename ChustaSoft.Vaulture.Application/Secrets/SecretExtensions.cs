using ChustaSoft.Vaulture.Domain.Secrets;

namespace ChustaSoft.Vaulture.Application.Secrets;

public static class SecretExtensions
{

    public static SecretDto ToDto(this Secret entity)
    {
        return entity.Type switch
        {
            SecretType.Credential => entity.ToCredentialDto(),
            _ => throw new InvalidOperationException("Unsupported Credential Type")
        };
    }


    private static CredentialDto ToCredentialDto(this Secret entity)
    {
        var value = entity.Value.AsCredentialValue();

        return new CredentialDto(entity.Name, value.Key, value.Password);
    }
}
