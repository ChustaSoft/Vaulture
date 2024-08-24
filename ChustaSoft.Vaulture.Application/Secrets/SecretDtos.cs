using ChustaSoft.Vaulture.Domain.Secrets;

namespace ChustaSoft.Vaulture.Application.Secrets;


public record SecretDto(SecretType Type, string Name);

public record CredentialDto(string Name, string Key, string Password) : SecretDto(SecretType.Credential, Name);
