namespace ChustaSoft.Vaulture.Application.Secrets;


public interface ISecretsService
{
    Task<string[]> GetAllAsync(string secretConnection);
    Task SaveAsync(CredentialCreationCommand credentialCreation);
}


public class SecretsService : ISecretsService
{

    public Task<string[]> GetAllAsync(string secretConnection)
    {
        //TODO: Retrieve secret names from Vault

        return Task.FromResult<string[]>(["test1", "test2", "test3"]);
    }

    public Task SaveAsync(CredentialCreationCommand credentialCreation)
    {
        //TODO: Save new secret
        return Task.CompletedTask;
    }
}
