using ChustaSoft.Vaulture.Application.Secrets;
using ChustaSoft.Vaulture.Application.Settings;
using ChustaSoft.Vaulture.UI.Common;
using ChustaSoft.Vaulture.UI.Secrets;
using System.Collections.ObjectModel;
using ThemeMode = ChustaSoft.Vaulture.Domain.Settings.ThemeMode;

namespace ChustaSoft.Vaulture.UI.Settings;


public partial class SettingsPageViewModel : ObservableObject
{

    private readonly IAppSettingsService _appSettingsService;
    private readonly IThemeManager _themeManager;

    public SettingsPageViewModel(IAppSettingsService appSettingsService, IThemeManager themeManager)
    {
        _appSettingsService = appSettingsService;
        _themeManager = themeManager;
    }


    [ObservableProperty]
    private Domain.Settings.ThemeMode themeModeSelected;

    [ObservableProperty]
    private string secretsStorageAliasToAdd = string.Empty;

    [ObservableProperty]
    private string secretsStorageValueToAdd = string.Empty;

    [ObservableProperty]
    private ObservableCollection<SecretTypeStoragesViewModel> secretTypeStorages = new();

    [ObservableProperty]
    private bool enableAddAction = false;

    [ObservableProperty]
    private bool enableSaveAction = false;

    [ObservableProperty]
    private SecretsStorageTypeViewModel secretsStorageTypes = SecretsStorageTypeViewModelProvider.Values;

    [ObservableProperty]
    private SecretsStorageTypeDto secretsStorageTypeSelected = SecretsStorageTypeViewModelProvider.Default;


    [RelayCommand]
    private void OnSystemThemeRadioButtonChecked()
    {
        _themeManager.Apply(ThemeMode.System);

        ThemeModeSelected = ThemeMode.System;
        EnableSaveAction = true;
    }

    [RelayCommand]
    private void OnLightThemeRadioButtonChecked()
    {
        _themeManager.Apply(ThemeMode.Light);

        ThemeModeSelected = ThemeMode.Light;
        EnableSaveAction = true;
    }

    [RelayCommand]
    private void OnDarkThemeRadioButtonChecked()
    {
        _themeManager.Apply(ThemeMode.Dark);

        ThemeModeSelected = ThemeMode.Dark;
        EnableSaveAction = true;
    }

    [RelayCommand]
    private void OnAddSecretsStorage()
    {
        if (!string.IsNullOrWhiteSpace(SecretsStorageAliasToAdd) && !string.IsNullOrWhiteSpace(SecretsStorageValueToAdd))
        {
            if (SecretTypeStorages.Any(x => x.Type == SecretsStorageTypeSelected.Type))
                SecretTypeStorages.First(x => x.Type == SecretsStorageTypeSelected.Type).SecretsStorages.Add(new SecretsStorageDto(SecretsStorageTypeSelected.Type, SecretsStorageAliasToAdd, SecretsStorageValueToAdd));
            else
                SecretTypeStorages.Add(new SecretTypeStoragesViewModel(SecretsStorageTypeSelected.Type, [new SecretsStorageDto(SecretsStorageTypeSelected.Type, SecretsStorageAliasToAdd, SecretsStorageValueToAdd)]));

            SecretsStorageAliasToAdd = string.Empty;
            SecretsStorageValueToAdd = string.Empty;
            EnableSaveAction = true;
        }
    }

    [RelayCommand]
    private async Task OnSave()
    {
        var command = new SettingsSaveCommand(ThemeModeSelected, SecretTypeStorages.SelectMany(x => x.SecretsStorages));

        await _appSettingsService.SaveAsync(command);

        EnableSaveAction = false;
    }

    [RelayCommand]
    public async Task OnLoadAsync()
    {
        var settings = await _appSettingsService.LoadAsync();

        ThemeModeSelected = settings.Theme;
        SecretTypeStorages = new ObservableCollection<SecretTypeStoragesViewModel>(settings.SecretsStorages.Select(x => new SecretTypeStoragesViewModel(x.Key, x.Value)));
    }


    partial void OnSecretsStorageAliasToAddChanging(string value)
    {
        if (!string.IsNullOrEmpty(SecretsStorageValueToAdd) && !string.IsNullOrEmpty(value))
            EnableAddAction = true;
        else
            EnableAddAction = false;
    }

    partial void OnSecretsStorageValueToAddChanging(string value)
    {
        if (!string.IsNullOrEmpty(SecretsStorageAliasToAdd) && !string.IsNullOrEmpty(value))
            EnableAddAction = true;
        else
            EnableAddAction = false;
    }

}
