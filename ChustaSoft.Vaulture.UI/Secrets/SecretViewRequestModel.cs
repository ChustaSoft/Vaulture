using ChustaSoft.Vaulture.Application.Secrets;
using ChustaSoft.Vaulture.Domain.Secrets;

namespace ChustaSoft.Vaulture.UI.Secrets;

public record SecretViewRequestModel(SecretsResourceType ResourceType, string SecretConnection, SecretDto SecretDto);
