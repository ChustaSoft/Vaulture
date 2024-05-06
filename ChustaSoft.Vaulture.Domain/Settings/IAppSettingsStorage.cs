namespace ChustaSoft.Vaulture.Domain.Settings;


public interface IAppSettingsStorage
{
    Task<AppSettings> LoadAsync();

    Task SaveAsync(AppSettings settings);
}
