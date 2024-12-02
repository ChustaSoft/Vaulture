using ChustaSoft.Vaulture.Domain.Secrets;

namespace ChustaSoft.Vaulture.UI.Secrets;


public struct SecretTypeVisibilityModel
{
    public SecretType? SecretType { get; set; }
    public string? SelectedConnection { get; set; }
}
