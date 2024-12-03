namespace ChustaSoft.Vaulture.Domain.Secrets;

public interface ISecretsStorageService
{
    Task<IEnumerable<Secret>> GetAllAsync(string storageName);
    Task SaveAsync(string storageName, Secret secret);
}
