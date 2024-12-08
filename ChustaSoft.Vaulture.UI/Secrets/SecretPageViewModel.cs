using ChustaSoft.Vaulture.Application.Secrets;
using ChustaSoft.Vaulture.UI.Common;

namespace ChustaSoft.Vaulture.UI.Secrets;

public partial class SecretPageViewModel : ObservableObject
{
    [ObservableProperty]
    private PageMode mode;

    [ObservableProperty]
    private CredentialDto credential;


    public SecretPageViewModel(PageMode mode, CredentialDto credential)
    {
        this.mode = mode;
        this.credential = credential;
    }


    [RelayCommand]
    private void OnCopyToClipboard(string text)
    {
        Clipboard.SetText(text);
    }

}