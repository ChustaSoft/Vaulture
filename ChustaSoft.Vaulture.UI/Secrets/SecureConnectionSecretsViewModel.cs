using ChustaSoft.Vaulture.Application.Secrets;
using ChustaSoft.Vaulture.Application.Settings;
using System.Collections.ObjectModel;

namespace ChustaSoft.Vaulture.UI.Secrets;

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

    //public void SetSecrets(SecretDto[] secrets)
    //{
    //    Secrets = new ObservableCollection<SecretDto>(secrets);
    //}

    internal void RemoveSecret(string name)
    {
        var secret = Secrets.First(x => x.Name == name);
        Secrets.Remove(secret);
    }
}
