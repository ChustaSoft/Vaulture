namespace ChustaSoft.Vaulture.Domain.Settings;


public interface IAppSettingsStorage
{
    Task SaveAsync(AppSettings settings);
}
