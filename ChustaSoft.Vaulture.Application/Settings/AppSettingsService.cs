using ChustaSoft.Vaulture.Application.Secrets;
using ChustaSoft.Vaulture.Domain.Secrets;
using ChustaSoft.Vaulture.Domain.Settings;
using System.Collections.ObjectModel;
using SecretsStorageDto = ChustaSoft.Vaulture.Application.Secrets.SecretsStorageDto;

namespace ChustaSoft.Vaulture.Application.Settings;


public interface IAppSettingsService
{
    Task<AppSettingsDto> LoadAsync();
    Task SaveAsync(SettingsSaveCommand command);
    Task<IDictionary<SecretsStorageType, IEnumerable<SecretsStorageDto>>> GetConnectionsAsync();
    IEnumerable<SecretsStorageDto> GetConnections(SecretsStorageType connectionType);
}


public class AppSettingsService : IAppSettingsService
{

    private readonly IAppSettingsStorage _appSettingsStorage;

    private AppSettings _appSettings = null!;


    public AppSettingsService(IAppSettingsStorage appSettingsStorage)
    {
        _appSettingsStorage = appSettingsStorage;
    }


    public async Task<AppSettingsDto> LoadAsync()
    {
        return await Task.Run(() =>
        {
            PerformLoadingSettings();

            var secretsStorages = GetElements(_appSettings);

            return new AppSettingsDto(_appSettings.Theme, secretsStorages);
        });
    }

    public async Task SaveAsync(SettingsSaveCommand command)
    {
        await Task.Run(() =>
        {
            var secretsStorages = new List<Domain.Secrets.SecretsStorage>();
            
            secretsStorages.AddRange(command.SecretsStorages.Select(x => new Domain.Secrets.SecretsStorage(x.Type, x.Alias, x.Value)));

            _appSettings = new AppSettings(command.Theme, secretsStorages);

            _appSettingsStorage.Save(_appSettings);
        });
    }

    public async Task<IDictionary<SecretsStorageType, IEnumerable<SecretsStorageDto>>> GetConnectionsAsync()
    {
        return await Task.Run((Func<Dictionary<SecretsStorageType, IEnumerable<SecretsStorageDto>>>)(() =>
        {
            PerformLoadingSettings();

            return (Dictionary<SecretsStorageType, IEnumerable<SecretsStorageDto>>)_appSettings.SecretsStorages
                .GroupBy(x => x.Type)
                .ToDictionary(
                        x => x.Key, (Func<IGrouping<SecretsStorageType, SecretsStorage>, IEnumerable<SecretsStorageDto>>)(
                        y => y.Select((Func<SecretsStorage, SecretsStorageDto>)(
                        z => (SecretsStorageDto)new Secrets.SecretsStorageDto(y.Key, z.Alias, z.Value)))));
        }));
    }

    public IEnumerable<SecretsStorageDto> GetConnections(SecretsStorageType resourceType)
    {
        return _appSettings.SecretsStorages
            .Where(x => x.Type == resourceType)
            .Select(x => new SecretsStorageDto(resourceType, x.Alias, x.Value));
    }


    private IDictionary<SecretsStorageType, SecretsStorageDto[]> GetElements(AppSettings appSettings)
    {
        var dictionary = new Dictionary<SecretsStorageType, SecretsStorageDto[]>();
        foreach (var group in appSettings.SecretsStorages.GroupBy(x => x.Type))
            dictionary.Add(group.Key, group.Select(x => new SecretsStorageDto(group.Key, x.Alias, x.Value)).ToArray());

        return dictionary;
    }

    private void PerformLoadingSettings()
    {
        try
        {
            if (_appSettings is null)
                _appSettings = _appSettingsStorage.Load();
        }
        catch (FileNotFoundException)
        {
            //TODO Log error
            _appSettings = new AppSettings();
        }
    }

}
