using System.ComponentModel;

namespace ChustaSoft.Vaulture.Domain.Secrets;

public enum SecretsStorageType
{
    [Description("Local File")]
    LocalFile = 0,

    [Description("Azure Key Vault")]
    AzureKeyVault
}
