using System.ComponentModel;

namespace ChustaSoft.Vaulture.Domain.Settings;

public enum SecureConnectionType
{
    [Description("Local File")]
    LocalFile = 0,

    [Description("Azure Key Vault")]
    AzureKeyVault
}
