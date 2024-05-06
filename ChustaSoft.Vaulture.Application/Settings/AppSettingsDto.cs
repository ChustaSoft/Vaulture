using ChustaSoft.Vaulture.Domain.Settings;

namespace ChustaSoft.Vaulture.Application.Settings;

public record AppSettingsDto(ThemeMode Theme, IEnumerable<SecureConnectionsDto> SecureConnections);
