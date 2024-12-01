﻿using ChustaSoft.Common.Helpers;
using ChustaSoft.Vaulture.Application.Secrets;
using ChustaSoft.Vaulture.Application.Settings;
using ChustaSoft.Vaulture.Domain.Secrets;
using ChustaSoft.Vaulture.UI.Models;
using ChustaSoft.Vaulture.UI.Settings;
using System.Collections.ObjectModel;

namespace ChustaSoft.Vaulture.UI.Pages;


public partial class SecretFormPageViewModel : ObservableObject
{

    private readonly IAppSettingsService _appSettingsService;
    private readonly ISecretsService _secretsService;


    public SecretFormPageViewModel(IAppSettingsService appSettingsService, ISecretsService secretsService)
    {
        _appSettingsService = appSettingsService;
        _secretsService = secretsService;
    }


    [ObservableProperty]
    private bool enableSaveAction = false;

    [ObservableProperty]
    private ObservableCollection<SecureConnectionValue> secureConnections = new ObservableCollection<SecureConnectionValue>();

    [ObservableProperty]
    private string? selectedConnection;

    [ObservableProperty]
    private ObservableCollection<SecretType> secretTypes = new ObservableCollection<SecretType>();

    [ObservableProperty]
    private SecretType? selectedSecretType;

    [ObservableProperty]
    private SecretTypeVisibilityModel secretTypeVisibilityModel;

    [ObservableProperty]
    private string name = string.Empty;

    [ObservableProperty]
    private string key = string.Empty;

    [ObservableProperty]
    private string password = string.Empty;

    [ObservableProperty]
    private string visiblePassword = string.Empty;

    [ObservableProperty]
    private SecureConnectionTypesModel secureConnectionTypes = SecureConnectionTypesProvider.Values;

    [ObservableProperty]
    private SecureConnectionTypeModel secureConnectionTypeSelected = SecureConnectionTypesProvider.Default;

    private CredentialCreationCommand _credentialCreationCommand = new CredentialCreationCommand();


    partial void OnSelectedConnectionChanged(string? value)
        => SecretTypeVisibilityModel = new SecretTypeVisibilityModel { SecretType = SelectedSecretType, SelectedConnection = value };

    partial void OnSelectedSecretTypeChanged(SecretType? value)
        => SecretTypeVisibilityModel = new SecretTypeVisibilityModel { SecretType = value, SelectedConnection = SelectedConnection };

    partial void OnNameChanged(string value)
    {
        _credentialCreationCommand.Name = value;
        EnableSaveAction = _credentialCreationCommand.IsValid();
    }

    partial void OnKeyChanged(string value)
    {
        _credentialCreationCommand.Key = value;
        EnableSaveAction = _credentialCreationCommand.IsValid();
    }

    partial void OnPasswordChanged(string value)
    {
        _credentialCreationCommand.Password = value;
        EnableSaveAction = _credentialCreationCommand.IsValid();
    }


    [RelayCommand]
    public void OnLoad()
    {
        FilterSecureConnectionsByCurrentSelection();
        SecretTypes = new ObservableCollection<SecretType>(EnumsHelper.GetEnumList<SecretType>());
    }


    [RelayCommand]
    private void OnSecureConnectionTypeChanged()
    {
        FilterSecureConnectionsByCurrentSelection();
    }

    [RelayCommand]
    private async Task OnSaveAsync()
    {
        await _secretsService.SaveAsync(_credentialCreationCommand);

        ResetForm();
    }

    private void FilterSecureConnectionsByCurrentSelection()
    {
        //TODO (NTH) , we can also allow to configure in Settings page the default provider, enhancing UX pre loading data

        var azureConnections = _appSettingsService.GetConnections(SecureConnectionTypeSelected.Type);
        SecureConnections = new ObservableCollection<SecureConnectionValue>(azureConnections);
    }

    private void ResetForm()
    {
        SelectedSecretType = null;
        SelectedConnection = null;
        Name = string.Empty;
        Key = string.Empty;
        Password = string.Empty;
        VisiblePassword = string.Empty;
        _credentialCreationCommand = new CredentialCreationCommand();
        EnableSaveAction = false;
    }

}
