
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

    private AppSettings _appSettings;


    public AppSettingsService(IAppSettingsStorage appSettingsStorage)
    {
        _appSettingsStorage = appSettingsStorage;
    }


    public async Task<AppSettingsDto> LoadAsync()
    {
        return await Task.Run(() =>
        {
            try
            {
                if (_appSettings is null)
                    _appSettings = _appSettingsStorage.Load();
            }
            catch (FileNotFoundException fnfe)
            {
                //TODO Log error
                _appSettings = new AppSettings();
            }

            var secureConnections = GetElements(_appSettings).ToList();

            return new AppSettingsDto(_appSettings.Theme, secureConnections);
        });
    }

    public async Task SaveAsync(SettingsSaveCommand command)
    {
        await Task.Run(() =>
        {
            var secureConnections = new List<SecureConnection>();
            foreach (var secureType in command.SecureSettings)
                secureConnections.AddRange(secureType.Values.Select(x => new SecureConnection(secureType.Type, x)));

            _appSettings = new AppSettings(command.Theme, secureConnections);

            _appSettingsStorage.Save(_appSettings);
        });
    }


    private IEnumerable<SecureConnectionsDto> GetElements(AppSettings appSettings)
    {
        foreach (var group in appSettings.SecureConnections.GroupBy(x => x.Type))
            yield return new SecureConnectionsDto(group.Key, new ObservableCollection<string>(group.Select(x => x.Value)));
    }

}
