using ChustaSoft.Vaulture.Domain.Secrets;

namespace ChustaSoft.Vaulture.Application.Secrets;


public interface ISecretsService
{
    Task<SecretDto[]> GetAllAsync(string secretConnection);
    Task SaveAsync(SecretsResourceType resourceType, string secretsConnectionName, CredentialCreationCommand credentialCreation);
}


public class SecretsService : ISecretsService
{

    private readonly SecretsStorageServiceResolver _secretsStorageServiceResolver;

    //TODO: Temporal in memory collection to test it
    private List<Secret> _secrets = new List<Secret>();


    public SecretsService(SecretsStorageServiceResolver secretsStorageServiceResolver)
    {
        _secretsStorageServiceResolver = secretsStorageServiceResolver;
    }


    public Task<SecretDto[]> GetAllAsync(string secretConnection)
    {
        //TODO: Retrieve secret names from Vault
        var dtos = _secrets.Select(x => x.ToDto()).ToArray();
        return Task.FromResult<SecretDto[]>(dtos);
    }

    public async Task SaveAsync(SecretsResourceType resourceType, string secretsConnectionName, CredentialCreationCommand credentialCreation)
    {
        var service = _secretsStorageServiceResolver(resourceType);
        var secret = new Secret(credentialCreation.Name, credentialCreation.Key, credentialCreation.Password);

        //TODO: Save new secret, by retrieving first its formatted value
        await service.SaveAsync(secretsConnectionName, secret);
    }
}
