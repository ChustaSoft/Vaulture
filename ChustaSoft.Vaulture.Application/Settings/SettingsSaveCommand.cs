using ChustaSoft.Vaulture.Application.Secrets;
using ChustaSoft.Vaulture.Domain.Settings;

namespace ChustaSoft.Vaulture.Application.Settings;


public record SettingsSaveCommand(ThemeMode Theme, IEnumerable<SecretsStorageDto> SecretsStorages);
