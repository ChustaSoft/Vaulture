using ChustaSoft.Vaulture.Domain.Secrets;

namespace ChustaSoft.Vaulture.Application.Secrets;


public interface ISecretsService
{
    Task<SecretDto[]> GetAllSummariesAsync(SecretsStorageType storageType, string storageConnection);
    Task<SecretDto> GetAsync(SecretsStorageType storageType, string storageConnection, string secretName);
    Task SaveAsync(SecretsStorageType storageType, string storageConnection, CredentialSaveCommand credentialCreation);
    Task SaveAsync(SecretsStorageType storageType, string storageConnection, ConnectionStringSaveCommand connectionStringCreation);
    Task DeleteAsync(SecretsStorageType storageType, string storageConnection, string secretName);
}


public class SecretsService : ISecretsService
{

    private readonly SecretsStorageServiceResolver _secretsStorageServiceResolver;


    public SecretsService(SecretsStorageServiceResolver secretsStorageServiceResolver)
    {
        _secretsStorageServiceResolver = secretsStorageServiceResolver;
    }


    public async Task<SecretDto[]> GetAllSummariesAsync(SecretsStorageType storageType, string storageConnection)
    {
        var service = _secretsStorageServiceResolver(storageType);
        var secrets = await service.GetAllAsync(storageConnection);

        return secrets.Select(x => x.ToSummaryDto()).ToArray();
    }

    public async Task<SecretDto> GetAsync(SecretsStorageType storageType, string storageConnection, string secretName)
    {
        var service = _secretsStorageServiceResolver(storageType);
        var secret = await service.GetAsync(storageConnection, secretName);

        return secret.ToFullDto();
    }

    public async Task SaveAsync(SecretsStorageType storageType, string storageConnection, CredentialSaveCommand credentialCreation)
    {
        var secret = new Secret(credentialCreation.Name, credentialCreation.Key, credentialCreation.Password);
        
        await SaveAsync(storageType, storageConnection, secret);
    }

    public async Task SaveAsync(SecretsStorageType storageType, String storageConnection, ConnectionStringSaveCommand connectionStringCreation)
    {
        var secret = new Secret(connectionStringCreation.Name, connectionStringCreation.Value);
        
        await SaveAsync(storageType, storageConnection, secret);
    }

    public async Task DeleteAsync(SecretsStorageType storageType, string storageConnection, string secretName)
    {
        var service = _secretsStorageServiceResolver(storageType);
        
        await service.DeleteAsync(storageConnection, secretName);
    }


    private async Task SaveAsync(SecretsStorageType storageType, String storageConnection, Secret secret)
    {
        var service = _secretsStorageServiceResolver(storageType);

        await service.SaveAsync(storageConnection, secret);
    }
}
