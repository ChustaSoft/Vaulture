namespace ChustaSoft.Vaulture.UI.Pages;

public partial class SecureConnectionSecretsViewModel : ObservableObject
{

    [ObservableProperty]
    private string name;

    [ObservableProperty]
    private ObservableCollection<SecretDto> secrets;


    public SecureConnectionSecretsViewModel(string name, SecretDto[] secrets)
    {
        Name = name;
        Secrets = new ObservableCollection<SecretDto>(secrets);
    }
}
