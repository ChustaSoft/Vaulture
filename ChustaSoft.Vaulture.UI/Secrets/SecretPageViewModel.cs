using ChustaSoft.Vaulture.Application.Secrets;

namespace ChustaSoft.Vaulture.UI.Secrets;

public partial class SecretPageViewModel : ObservableObject
{

    [ObservableProperty]
    private CredentialDto credential;


    [RelayCommand]
    private void OnCopyToClipboard(string text)
    {
        Clipboard.SetText(text);
    }

}
