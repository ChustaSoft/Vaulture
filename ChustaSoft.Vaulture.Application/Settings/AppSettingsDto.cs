using ChustaSoft.Vaulture.Application.Secrets;
using ChustaSoft.Vaulture.Domain.Secrets;
using ChustaSoft.Vaulture.Domain.Settings;

namespace ChustaSoft.Vaulture.Application.Settings;

public record AppSettingsDto(ThemeMode Theme, IDictionary<SecretsStorageType, SecretsStorageDto[]> SecretsStorages);
