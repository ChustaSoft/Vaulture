using ChustaSoft.Vaulture.Domain.Secrets;

namespace ChustaSoft.Vaulture.Application.Secrets;


public interface ISecretsService
{
    Task<SecretDto[]> GetAllSummariesAsync(SecretsResourceType resourceType, string storageName);
    Task<SecretDto> GetAsync(SecretsResourceType resourceType, string storageName, string name);
    Task SaveAsync(SecretsResourceType resourceType, string secretsConnectionName, CredentialCreationCommand credentialCreation);
    Task DeleteAsync(SecretsResourceType resourceType, string secretConnection, string name);
}


public class SecretsService : ISecretsService
{

    private readonly SecretsStorageServiceResolver _secretsStorageServiceResolver;


    public SecretsService(SecretsStorageServiceResolver secretsStorageServiceResolver)
    {
        _secretsStorageServiceResolver = secretsStorageServiceResolver;
    }


    public async Task<SecretDto[]> GetAllSummariesAsync(SecretsResourceType resourceType, string storageName)
    {
        var service = _secretsStorageServiceResolver(resourceType);
        var secrets = await service.GetAllAsync(storageName);

        return secrets.Select(x => x.ToSummaryDto()).ToArray();
    }

    public async Task<SecretDto> GetAsync(SecretsResourceType resourceType, string storageName, string secretName)
    {
        var service = _secretsStorageServiceResolver(resourceType);
        var secret = await service.GetAsync(storageName, secretName);

        return secret.ToFullDto();
    }

    public async Task SaveAsync(SecretsResourceType resourceType, string secretsConnectionName, CredentialCreationCommand credentialCreation)
    {
        var service = _secretsStorageServiceResolver(resourceType);
        var secret = new Secret(SecretType.Credential, credentialCreation.Name, credentialCreation.Key, credentialCreation.Password);

        //TODO: Save new secret, by retrieving first its formatted value
        await service.SaveAsync(secretsConnectionName, secret);
    }

    public async Task DeleteAsync(SecretsResourceType resourceType, string secretsConnectionName, string secretName)
    {
        var service = _secretsStorageServiceResolver(resourceType);
        
        await service.DeleteAsync(secretsConnectionName, secretName);
    }

}
