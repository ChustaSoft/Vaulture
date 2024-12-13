using ChustaSoft.Vaulture.Domain.Secrets;

namespace ChustaSoft.Vaulture.Domain.Settings;


public class AppSettings
{
    public ThemeMode Theme { get; init; } = ThemeMode.System;

    public List<SecretsStorage> _secretsStorages = [];
    public IReadOnlyList<SecretsStorage> SecretsStorages => _secretsStorages;


    public AppSettings() { }

    public AppSettings(ThemeMode theme, List<SecretsStorage> secretsStorages)
    {
        Theme = theme;
        _secretsStorages = secretsStorages;
    }
}
