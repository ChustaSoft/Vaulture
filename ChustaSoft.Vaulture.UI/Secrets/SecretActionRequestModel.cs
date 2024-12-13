using ChustaSoft.Vaulture.Application.Secrets;
using ChustaSoft.Vaulture.Domain.Secrets;

namespace ChustaSoft.Vaulture.UI.Secrets;

public record SecretActionRequestModel(SecretsStorageType ResourceType, string SecretConnection, SecretDto SecretDto);
