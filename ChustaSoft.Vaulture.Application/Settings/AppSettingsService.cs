
namespace ChustaSoft.Vaulture.Application.Settings;



//TODO Implement Save feature

//TODO: Implement retrieve data from local storage
public interface IAppSettingsService
{
    Task SaveAsync(SettingsSaveCommand command);
}


public class AppSettingsService : IAppSettingsService
{
    public Task SaveAsync(SettingsSaveCommand command)
    {
        throw new NotImplementedException();
    }
}
