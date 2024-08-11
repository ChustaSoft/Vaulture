
using ChustaSoft.Vaulture.Domain.Settings;
using System.Collections.ObjectModel;

namespace ChustaSoft.Vaulture.Application.Settings;


public interface IAppSettingsService
{
    Task<AppSettingsDto> LoadAsync();
    Task SaveAsync(SettingsSaveCommand command);
    Task<IDictionary<SecureConnectionType, IEnumerable<SecureConnectionValue>>> GetConnectionsAsync();
    IEnumerable<SecureConnectionValue> GetConnections(SecureConnectionType connectionType);
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
            PerformLoadingSettings();

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
                secureConnections.AddRange(secureType.Values.Select(x => new SecureConnection(secureType.Type, x.Alias, x.Value)));

            _appSettings = new AppSettings(command.Theme, secureConnections);

            _appSettingsStorage.Save(_appSettings);
        });
    }

    public async Task<IDictionary<SecureConnectionType, IEnumerable<SecureConnectionValue>>> GetConnectionsAsync()
    {
        return await Task.Run(() =>
        {
            PerformLoadingSettings();

            return _appSettings.SecureConnections
                .GroupBy(x => x.Type)
                .ToDictionary(x => x.Key, y => y.Select(z => new SecureConnectionValue(z.Alias, z.Value)));
        });
    }

    public IEnumerable<SecureConnectionValue> GetConnections(SecureConnectionType connectionType)
    {
        return _appSettings.SecureConnections
                .Where(x => x.Type == connectionType)
                .Select(x => new SecureConnectionValue(x.Alias, x.Value));
    }


    private IEnumerable<SecureConnectionsDto> GetElements(AppSettings appSettings)
    {
        foreach (var group in appSettings.SecureConnections.GroupBy(x => x.Type))
            yield return new SecureConnectionsDto(group.Key, new ObservableCollection<SecureConnectionValue>(group.Select(x => new SecureConnectionValue(x.Alias, x.Value))));
    }

    private void PerformLoadingSettings()
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
    }

}
