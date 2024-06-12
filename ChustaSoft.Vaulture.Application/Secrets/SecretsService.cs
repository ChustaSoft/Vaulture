using ChustaSoft.Vaulture.Domain.Secrets;

namespace ChustaSoft.Vaulture.Application.Secrets;


public interface ISecretsService
{
    Task<SecretDto[]> GetAllAsync(string secretConnection);
    Task SaveAsync(CredentialCreationCommand credentialCreation);
}


public class SecretsService : ISecretsService
{

    public Task<SecretDto[]> GetAllAsync(string secretConnection)
    {
        //TODO: Retrieve secret names from Vault

        return Task.FromResult<SecretDto[]>([new (SecretType.Credential, "test-1"), new(SecretType.Credential, "test-2"), new(SecretType.Credential, "test-3")]);
    }

    public Task SaveAsync(CredentialCreationCommand credentialCreation)
    {
        //TODO: Save new secret
        return Task.CompletedTask;
    }
}
