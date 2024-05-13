namespace ChustaSoft.Vaulture.Application.Secrets;


public interface ISecretsService
{
    Task SaveAsync(CredentialCreationCommand credentialCreation);
}


public class SecretsService : ISecretsService
{

    public Task SaveAsync(CredentialCreationCommand credentialCreation)
    {
        //TODO: Save new secret
        return Task.CompletedTask;
    }
}
