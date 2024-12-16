using ChustaSoft.Vaulture.Domain.Secrets;

namespace ChustaSoft.Vaulture.Application.Secrets;

public static class SecretExtensions
{

    public static SecretDto ToSummaryDto(this Secret entity)
    {
        return new SecretDto(entity.Type, entity.Name);
    }

    public static SecretDto ToFullDto(this Secret entity)
    {
        return entity.Type switch
        {
            SecretType.Credential => entity.ToCredentialDto(),
            SecretType.ConnectionString => entity.ToConnectionStringDto(),
            _ => throw new InvalidOperationException("Unsupported Credential Type")
        };
    }


    private static CredentialDto ToCredentialDto(this Secret entity)
    {
        var value = entity.Value.RetrieveCredentialValue();

        return new CredentialDto(entity.Name, value.Key, value.Password);
    }

    private static ConnectionStringDto ToConnectionStringDto(this Secret entity)
    {
        var value = entity.Value.RetrieveConnectionStringValue();

        return new ConnectionStringDto(entity.Name, value.Value);
    }
}
