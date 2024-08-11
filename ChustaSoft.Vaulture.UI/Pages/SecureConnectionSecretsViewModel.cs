using ChustaSoft.Vaulture.Application.Secrets;
using ChustaSoft.Vaulture.Application.Settings;
using System.Collections.ObjectModel;

namespace ChustaSoft.Vaulture.UI.Pages;

public partial class SecureConnectionSecretsViewModel : ObservableObject
{

    [ObservableProperty]
    private SecureConnectionValue connectionValue;

    [ObservableProperty]
    private ObservableCollection<SecretDto> secrets;


    public SecureConnectionSecretsViewModel(SecureConnectionValue connectionValue, SecretDto[] secrets)
    {
        ConnectionValue = connectionValue;
        Secrets = new ObservableCollection<SecretDto>(secrets);
    }
}
