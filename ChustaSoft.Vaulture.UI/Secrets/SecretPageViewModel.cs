using ChustaSoft.Vaulture.Application.Secrets;
using ChustaSoft.Vaulture.Domain.Secrets;
using ChustaSoft.Vaulture.UI.Common;

namespace ChustaSoft.Vaulture.UI.Secrets;

//TODO: By the moment, is handling directly a Credential object, should be abstract to any kind of Secret
public partial class SecretPageViewModel : ObservableObject
{

    private readonly ISecretsService? _secretsService;
    private readonly SecretsStorageType? _secretsStorageType;
    private readonly string? _secretsStorageConnection;

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


    public SecretPageViewModel(CredentialDto credential)
    {
        Mode = PageMode.View;
        Name = credential.Name;
        Key = credential.Key;
        Password = credential.Password;
    }

    public SecretPageViewModel(ISecretsService secretsService, SecretsStorageType resourceType, string secretsStorageConnection, CredentialDto credential)
        : this(credential)
    {
        _secretsService = secretsService;
        _secretsStorageType = resourceType;
        _secretsStorageConnection = secretsStorageConnection;

        Mode = PageMode.Edit;
    }



    partial void OnKeyChanged(string value)
    {
        var credential = CreateCredentialSaveCommand();

        EnableSaveAction = credential.IsValid();
    }

    partial void OnPasswordChanged(string value)
    {
        var credential = CreateCredentialSaveCommand();

        EnableSaveAction = credential.IsValid();
    }


    [RelayCommand]
    private void OnCopyToClipboard(string text)
    {
        Clipboard.SetText(text);
    }

    [RelayCommand]
    private async Task OnSaveAsync()
    {
        var credential = CreateCredentialSaveCommand();

        await _secretsService!.SaveAsync(_secretsStorageType!.Value, _secretsStorageConnection!, credential);
    }

    private CredentialSaveCommand CreateCredentialSaveCommand()
        => new CredentialSaveCommand
            {
                Name = Name,
                Key = Key,
                Password = Password
            };

}