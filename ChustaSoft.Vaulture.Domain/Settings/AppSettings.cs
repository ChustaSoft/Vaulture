namespace ChustaSoft.Vaulture.Domain.Settings;


public class AppSettings
{
    public ThemeMode Theme { get; init; }

    public List<SecureConnection> _secureConnection;
    public IReadOnlyList<SecureConnection> SecureConnection => _secureConnection;


    public AppSettings(ThemeMode theme, List<SecureConnection> secureConnection)
    {
        Theme = theme;
        _secureConnection = secureConnection;
    }
}
