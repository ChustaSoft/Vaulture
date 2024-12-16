using ChustaSoft.Vaulture.Application.Secrets;
using ChustaSoft.Vaulture.Domain.Secrets;
using ChustaSoft.Vaulture.UI.Common;

namespace ChustaSoft.Vaulture.UI.Secrets;


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
    private SecretTypeVisibilityModel secretTypeVisibilityModel = new SecretTypeVisibilityModel { SecretsStorageConnectionSelected = "Valid" };

    [ObservableProperty]
    private string name;

    [ObservableProperty]
    private string? key;

    [ObservableProperty]
    private string? password;

    [ObservableProperty]
    private string? value;

    private ISecretSaveCommand? _creationCommand = null;


    public SecretPageViewModel(SecretDto secret)
    {
        Mode = PageMode.View;
        Name = secret.Name;

        secretTypeVisibilityModel.SecretType = secret.Type;

        switch (secret.Type)
        {
            case SecretType.Credential:
                SetCredentialValues(secret);
                break;

            case SecretType.ConnectionString:
                SetConnectionStringValues(secret);
                break;

            default:
                throw new ArgumentException($"Unsupported Secret type: {secret.Type}");
        }
    }


    public SecretPageViewModel(ISecretsService secretsService, SecretsStorageType resourceType, string secretsStorageConnection, SecretDto credential)
        : this(credential)
    {
        _secretsService = secretsService;
        _secretsStorageType = resourceType;
        _secretsStorageConnection = secretsStorageConnection;

        Mode = PageMode.Edit;
    }



    partial void OnKeyChanged(string? value)
    {
        if (!string.IsNullOrEmpty(value))
        {
            ((CredentialSaveCommand)_creationCommand!).Key = value;

            EnableSaveAction = _creationCommand.IsValid();
        }
    }

    partial void OnPasswordChanged(string? value)
    {
        if (!string.IsNullOrEmpty(value))
        {
            ((CredentialSaveCommand)_creationCommand!).Password = value;

            EnableSaveAction = _creationCommand.IsValid();
        }
    }

    partial void OnValueChanged(string? value)
    {
        if (!string.IsNullOrEmpty(value))
        {
            ((ConnectionStringSaveCommand)_creationCommand!).Value = value;

            EnableSaveAction = _creationCommand.IsValid();
        }
    }


    [RelayCommand]
    private void OnCopyToClipboard(string text)
    {
        Clipboard.SetText(text);
    }

    [RelayCommand]
    private async Task OnSaveAsync()
    {
        await _secretsService!.SaveAsync(_secretsStorageType!.Value, _secretsStorageConnection!, _creationCommand!);
    }


    private void SetCredentialValues(SecretDto secret)
    {
        var castedSecret = (CredentialDto)secret;

        _creationCommand = new CredentialSaveCommand
        {
            Name = castedSecret.Name,
            Key = castedSecret.Key!,
            Password = castedSecret.Password!
        };

        Key = castedSecret.Key;
        Password = castedSecret.Password;
    }

    private void SetConnectionStringValues(SecretDto secret)
    {
        var castedSecret = (ConnectionStringDto)secret;

        _creationCommand = new ConnectionStringSaveCommand
        {
            Name = castedSecret.Name,
            Value = castedSecret.Value!
        };

        Value = castedSecret.Value;
    }

}