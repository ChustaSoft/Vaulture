using ChustaSoft.Vaulture.Application.Secrets;
using ChustaSoft.Vaulture.UI.Common;

namespace ChustaSoft.Vaulture.UI.Secrets;

//TODO: By the moment, is handling directly a Credential object, should be abstract to any kind of Secret
public partial class SecretPageViewModel : ObservableObject
{

    [ObservableProperty]
    private bool enableSaveAction = true;

    [ObservableProperty]
    private PageMode mode;

    [ObservableProperty]
    private string name;

    [ObservableProperty]
    private string key;

    [ObservableProperty]
    private string password;


    public SecretPageViewModel(PageMode mode, CredentialDto credential)
    {
        Mode = mode;
        Name = credential.Name;
        Key = credential.Key;
        Password = credential.Password;
    }

   

    partial void OnKeyChanged(string value)
    {
        //EnableSaveAction = value.IsValid();
    }

    partial void OnPasswordChanged(string value)
    {
        //EnableSaveAction = value.IsValid();
    }


    [RelayCommand]
    private void OnCopyToClipboard(string text)
    {
        Clipboard.SetText(text);
    }

    [RelayCommand]
    private async Task OnSaveAsync()
    {
        //TODO: Save element
        await Task.CompletedTask;
    }

}