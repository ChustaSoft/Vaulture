namespace ChustaSoft.Vaulture.Domain.Settings;


public interface IAppSettingsStorage
{
    AppSettings Load();

    void Save(AppSettings settings);
}
