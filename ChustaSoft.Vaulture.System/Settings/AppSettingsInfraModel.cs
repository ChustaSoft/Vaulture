using ChustaSoft.Vaulture.Domain.Secrets;
using ChustaSoft.Vaulture.Domain.Settings;

namespace ChustaSoft.Vaulture.LocalSystem.Settings;


public class AppSettingsInfraModel
{
    public ThemeMode Theme { get; init; }

    public List<SecureConnectionInfraModel> SecureConnections { get; init; } = new List<SecureConnectionInfraModel>();

    public AppSettingsInfraModel() { }

    public AppSettingsInfraModel(AppSettings appSettings)
        : this()
    {
        Theme = appSettings.Theme;
        SecureConnections = appSettings.SecretsStorages.Select(x => new SecureConnectionInfraModel(x)).ToList();
    }


    public AppSettings ToEntity()
    {
        return new AppSettings(Theme, SecureConnections.Select(x => new SecretsStorage(x.Type, x.Alias, x.Value)).ToList());
    }
}



public class SecureConnectionInfraModel
{
    public SecretsStorageType Type { get; init; }
    public string Alias { get; init; } = null!;
    public string Value { get; init; } = null!;


    public SecureConnectionInfraModel() { }

    public SecureConnectionInfraModel(SecretsStorage secureConnection)
        : this()
    {
        Type = secureConnection.Type;
        Alias = secureConnection.Alias;
        Value = secureConnection.Value;
    }
}