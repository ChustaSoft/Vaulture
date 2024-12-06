namespace ChustaSoft.Vaulture.Domain.Secrets;

public interface ISecretsStorageService
{
    Task<IEnumerable<Secret>> GetAllAsync(string storageName);
    Task<Secret> GetAsync(string storageName, string name);
    Task SaveAsync(string storageName, Secret secret);
    Task DeleteAsync(String secretConnection, String name);
}
