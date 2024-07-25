using ChustaSoft.Vaulture.Domain.Secrets;

namespace ChustaSoft.Vaulture.Application.Secrets;


public interface ISecretsService
{
    Task<SecretDto[]> GetAllAsync(string secretConnection);
    Task SaveAsync(CredentialCreationCommand credentialCreation);
}


public class SecretsService : ISecretsService
{
    //TODO: Temporal in memory collection to test it
    private List<Secret> _secrets = new List<Secret>();


    public Task<SecretDto[]> GetAllAsync(string secretConnection)
    {
        //TODO: Retrieve secret names from Vault
        var dtos = _secrets.Select(x => x.ToDto()).ToArray();
        return Task.FromResult<SecretDto[]>(dtos);
    }

    public Task SaveAsync(CredentialCreationCommand credentialCreation)
    {
        var secret = new Secret(credentialCreation.Name, credentialCreation.Key, credentialCreation.Password);

        //TODO: Save new secret, by retrieving first its formatted value
        _secrets.Add(secret);

        return Task.CompletedTask;
    }
}
