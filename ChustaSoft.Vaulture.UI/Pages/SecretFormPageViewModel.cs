using ChustaSoft.Common.Helpers;
using ChustaSoft.Vaulture.Application.Settings;
using ChustaSoft.Vaulture.Domain.Secrets;
using ChustaSoft.Vaulture.Domain.Settings;
using System.Collections.ObjectModel;

namespace ChustaSoft.Vaulture.UI.Pages;


public partial class SecretFormPageViewModel : ObservableObject
{

    private readonly IAppSettingsService _appSettingsService;


    public SecretFormPageViewModel(IAppSettingsService appSettingsService)
    {
        _appSettingsService = appSettingsService;
    }


    [ObservableProperty]
    private bool enableSaveAction = false;

    [ObservableProperty]
    private ObservableCollection<string> secureConnections = new ObservableCollection<string>();

    [ObservableProperty]
    private string selectedConnection;

    [ObservableProperty]
    private ObservableCollection<SecretType> secretTypes = new ObservableCollection<SecretType>();

    [ObservableProperty]
    private SecretType selectedSecretType;


    [RelayCommand]
    private void OnSave()
    {
        //TODO: Save new secret

        EnableSaveAction = false;
    }

    [RelayCommand]
    public void OnLoad()
    {
        /* 
         * TODO: 
         * By the moment, only Azure is supported, so its forced to load values of Azure when loading
         * In a final version with multiple providers support, it might 
         * Alternatively, we can also allow to configure in Settings page the default provider, enhancing UX pre loading data
         */

        var azureConnections = _appSettingsService.GetConnections(SecureConnectionType.AzureVault);
        SecureConnections = new ObservableCollection<string>(azureConnections);
        SecretTypes = new ObservableCollection<SecretType>(EnumsHelper.GetEnumList<SecretType>());
    }
}
