using ChustaSoft.Vaulture.Domain.Settings;

namespace ChustaSoft.Vaulture.LocalSystem.Settings;


public class AppSettingsInfraModel
{
    public ThemeMode Theme { get; init; }

    public List<SecureConnectionInfraModel> SecureConnections { get; init; }

    public AppSettingsInfraModel() { }

    public AppSettingsInfraModel(AppSettings appSettings)
        : this()
    {
        Theme = appSettings.Theme;
        SecureConnections = appSettings.SecureConnections.Select(x => new SecureConnectionInfraModel(x)).ToList();
    }


    public AppSettings ToEntity()
    {
        return new AppSettings(Theme, SecureConnections.Select(x => new SecureConnection(x.Type, x.Value)).ToList());
    }
}



public class SecureConnectionInfraModel
{
    public SecureConnectionType Type { get; init; }
    public string Value { get; init; }


    public SecureConnectionInfraModel() { }

    public SecureConnectionInfraModel(SecureConnection secureConnection)
        : this()
    {
        Type = secureConnection.Type;
        Value = secureConnection.Value;
    }
}