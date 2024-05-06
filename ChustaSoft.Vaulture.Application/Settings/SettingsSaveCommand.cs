using ChustaSoft.Vaulture.Domain.Settings;

namespace ChustaSoft.Vaulture.Application.Settings;


public record SettingsSaveCommand(ThemeMode Theme, IEnumerable<SecureConnectionsDto> SecureSettings);
