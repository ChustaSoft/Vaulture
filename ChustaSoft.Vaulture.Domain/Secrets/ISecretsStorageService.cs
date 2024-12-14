namespace ChustaSoft.Vaulture.Domain.Secrets;

public interface ISecretsStorageService
{
    Task<IEnumerable<Secret>> GetAllAsync(string storageConnection);
    Task<Secret> GetAsync(string storageConnection, string name);
    Task SaveAsync(string storageConnection, Secret secret);
    Task DeleteAsync(string storageConnection, string name);
}
