using System.Collections.ObjectModel;

namespace ChustaSoft.Vaulture.UI.Pages;

public partial class SecureConnectionSecretsViewModel : ObservableObject
{

    [ObservableProperty]
    private string name;

    [ObservableProperty]
    private ObservableCollection<string> secrets;


    public SecureConnectionSecretsViewModel(string name, string[] secrets)
    {
        Name = name;
        Secrets = new ObservableCollection<string>(secrets);
    }
}
