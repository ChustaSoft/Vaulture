namespace ChustaSoft.Vaulture.Domain.Settings;


public class AppSettings
{
    public ThemeMode Theme { get; init; } = ThemeMode.System;

    public List<SecureConnection> _secureConnections = [];
    public IReadOnlyList<SecureConnection> SecureConnections => _secureConnections;


    public AppSettings() { }

    public AppSettings(ThemeMode theme, List<SecureConnection> secureConnections)
    {
        Theme = theme;
        _secureConnections = secureConnections;
    }
}
