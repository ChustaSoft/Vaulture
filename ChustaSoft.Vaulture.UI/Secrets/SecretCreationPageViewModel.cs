using ChustaSoft.Common.Helpers;
using ChustaSoft.Vaulture.Application.Secrets;
using ChustaSoft.Vaulture.Application.Settings;
using ChustaSoft.Vaulture.Domain.Secrets;
using System.Collections.ObjectModel;

namespace ChustaSoft.Vaulture.UI.Secrets;


public partial class SecretCreationPageViewModel : ObservableObject
{

    private readonly IAppSettingsService _appSettingsService;
    private readonly ISecretsService _secretsService;
    private readonly INavigationService _navigationService;


    public SecretCreationPageViewModel(IAppSettingsService appSettingsService, ISecretsService secretsService, INavigationService navigationService)
    {
        _appSettingsService = appSettingsService;
        _secretsService = secretsService;
        _navigationService = navigationService;
    }


    [ObservableProperty]
    private bool enableSaveAction = false;

    [ObservableProperty]
    private ObservableCollection<SecretsStorageDto> secretsStorages = new ObservableCollection<SecretsStorageDto>();

    [ObservableProperty]
    private SecretsStorageDto? secretsStorageSelected;

    [ObservableProperty]
    private ObservableCollection<SecretType> secretTypes = new ObservableCollection<SecretType>();

    [ObservableProperty]
    private SecretType? secretTypeSelected;

    [ObservableProperty]
    private SecretTypeVisibilityModel secretTypeVisibilityModel;

    [ObservableProperty]
    private string name = string.Empty;

    [ObservableProperty]
    private string key = string.Empty;

    [ObservableProperty]
    private string password = string.Empty;

    [ObservableProperty]
    private string value = string.Empty;

    [ObservableProperty]
    private string visiblePassword = string.Empty;

    [ObservableProperty]
    private SecretsStorageTypeViewModel resourceTypes = SecretsStorageTypeViewModelProvider.Values;

    [ObservableProperty]
    private SecretsStorageTypeDto resourceTypeSelected = SecretsStorageTypeViewModelProvider.Default;

    private ISecretSaveCommand? _creationCommand = null;


    partial void OnSecretsStorageSelectedChanged(Application.Secrets.SecretsStorageDto? value)
        => SecretTypeVisibilityModel = new SecretTypeVisibilityModel { SecretType = SecretTypeSelected, SecretsStorageConnectionSelected = value.ToString() };

    partial void OnSecretTypeSelectedChanged(SecretType? value)
    {
        SecretTypeVisibilityModel = new SecretTypeVisibilityModel { SecretType = value, SecretsStorageConnectionSelected = SecretsStorageSelected!.Value.Value };

        switch (value)
        {
            case SecretType.Credential:
                _creationCommand = new CredentialSaveCommand();
                break;

            case SecretType.ConnectionString:
                _creationCommand = new ConnectionStringSaveCommand();
                break;

            default:
                _creationCommand = null;
                break;
        }

        ResetValues();
    }

    partial void OnNameChanged(string value)
    {
        if (!string.IsNullOrEmpty(value))
        {
            _creationCommand!.Name = value;
            EnableSaveAction = _creationCommand.IsValid();
        }
    }

    partial void OnKeyChanged(string value)
    {
        if (!string.IsNullOrEmpty(value))
        {
            ((CredentialSaveCommand)_creationCommand!).Key = value;
            EnableSaveAction = _creationCommand.IsValid();
        }
    }

    partial void OnPasswordChanged(string value)
    {
        if (!string.IsNullOrEmpty(value))
        {
            ((CredentialSaveCommand)_creationCommand!).Password = value;
            EnableSaveAction = _creationCommand.IsValid();
        }
    }

    partial void OnValueChanged(string value)
    {
        if (!string.IsNullOrEmpty(value))
        {
            ((ConnectionStringSaveCommand)_creationCommand!).Value = value;
            EnableSaveAction = _creationCommand.IsValid();
        }
    }


    [RelayCommand]
    public void OnLoad()
    {
        FilterSecureConnectionsByCurrentSelection();
        SecretTypes = new ObservableCollection<SecretType>(EnumsHelper.GetEnumList<SecretType>());
    }


    [RelayCommand]
    private void OnResourceTypeChanged()
    {
        FilterSecureConnectionsByCurrentSelection();
    }

    [RelayCommand]
    private async Task OnSaveAsync()
    {
        await _secretsService.SaveAsync(ResourceTypeSelected.Type, SecretsStorageSelected!.Value.Value, _creationCommand);

        ResetPage();

        _navigationService.Navigate(typeof(SecretsDashboardPage));
    }


    private void FilterSecureConnectionsByCurrentSelection()
    {
        //TODO (NTH) , we can also allow to configure in Settings page the default provider, enhancing UX pre loading data

        var azureConnections = _appSettingsService.GetConnections(ResourceTypeSelected.Type);
        SecretsStorages = new ObservableCollection<Application.Secrets.SecretsStorageDto>(azureConnections);
    }

    private void ResetPage()
    {
        SecretTypeSelected = null;
        SecretsStorageSelected = null;

        _creationCommand = null;

        ResetValues();
    }

    private void ResetValues()
    {
        Name = string.Empty;
        Key = string.Empty;
        Password = string.Empty;
        Value = string.Empty;
        VisiblePassword = string.Empty;
        EnableSaveAction = false;
    }

}
