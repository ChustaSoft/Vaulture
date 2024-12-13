using ChustaSoft.Vaulture.Application.Secrets;
using System.Collections.ObjectModel;

namespace ChustaSoft.Vaulture.UI.Secrets;

public partial class SecretsStorageViewModel : ObservableObject
{

    [ObservableProperty]
    private SecretsStorageDto secretsStorage;

    [ObservableProperty]
    private ObservableCollection<SecretDto> secrets;


    public SecretsStorageViewModel(SecretsStorageDto secretsStorage, SecretDto[] secrets)
    {
        SecretsStorage = secretsStorage;
        Secrets = new ObservableCollection<SecretDto>(secrets);
    }

    internal void RemoveSecret(string secretName)
    {
        var secret = Secrets.First(x => x.Name == secretName);
        Secrets.Remove(secret);
    }
}
