
using ChustaSoft.Vaulture.Domain.Settings;
using System.Collections.ObjectModel;

namespace ChustaSoft.Vaulture.Application.Settings;


public interface IAppSettingsService
{
    Task<AppSettingsDto> LoadAsync();
    Task SaveAsync(SettingsSaveCommand command);
}


public class AppSettingsService : IAppSettingsService
{

    private readonly IAppSettingsStorage _appSettingsStorage;


    public AppSettingsService(IAppSettingsStorage appSettingsStorage)
    {
        _appSettingsStorage = appSettingsStorage;
    }


    public async Task<AppSettingsDto> LoadAsync()
    {
        var settings = await _appSettingsStorage.LoadAsync();
        var secureConnections = GetElements(settings).ToList();

        return new AppSettingsDto(settings.Theme, secureConnections);
    }

    public async Task SaveAsync(SettingsSaveCommand command)
    {
        var secureConnections = new List<SecureConnection>();
        foreach (var secureType in command.SecureSettings)
            secureConnections.AddRange(secureType.Values.Select(x => new SecureConnection(secureType.Type, x)));

        var settings = new AppSettings(command.Theme, secureConnections);

        await _appSettingsStorage.SaveAsync(settings);
    }


    private IEnumerable<SecureConnectionsDto> GetElements(AppSettings appSettings)
    {
        foreach (var group in appSettings.SecureConnections.GroupBy(x => x.Type))
            yield return new SecureConnectionsDto(group.Key, new ObservableCollection<string>(group.Select(x => x.Value)));
    }

}
