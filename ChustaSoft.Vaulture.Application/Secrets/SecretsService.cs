using ChustaSoft.Vaulture.Domain.Secrets;

namespace ChustaSoft.Vaulture.Application.Secrets;


public interface ISecretsService
{
    Task<SecretDto[]> GetAllAsync(SecretsResourceType resourceType, string secretsConnectionName);
    Task SaveAsync(SecretsResourceType resourceType, string secretsConnectionName, CredentialCreationCommand credentialCreation);
}


public class SecretsService : ISecretsService
{

    private readonly SecretsStorageServiceResolver _secretsStorageServiceResolver;


    public SecretsService(SecretsStorageServiceResolver secretsStorageServiceResolver)
    {
        _secretsStorageServiceResolver = secretsStorageServiceResolver;
    }


    public async Task<SecretDto[]> GetAllAsync(SecretsResourceType resourceType, string secretsConnectionName)
    {
        var service = _secretsStorageServiceResolver(resourceType);
        var secrets = await service.GetAllAsync(secretsConnectionName);

        return secrets.Select(x => x.ToDto()).ToArray();
    }

    public async Task SaveAsync(SecretsResourceType resourceType, string secretsConnectionName, CredentialCreationCommand credentialCreation)
    {
        var service = _secretsStorageServiceResolver(resourceType);
        var secret = new Secret(SecretType.Credential, credentialCreation.Name, credentialCreation.Key, credentialCreation.Password);

        //TODO: Save new secret, by retrieving first its formatted value
        await service.SaveAsync(secretsConnectionName, secret);
    }
}
