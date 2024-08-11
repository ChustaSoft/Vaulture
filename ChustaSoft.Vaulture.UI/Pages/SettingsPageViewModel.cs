using ChustaSoft.Vaulture.Application.Settings;
using ChustaSoft.Vaulture.Domain.Settings;
using ChustaSoft.Vaulture.UI.Services;
using System.Collections.ObjectModel;
using SettingsSaveCommand = ChustaSoft.Vaulture.Application.Settings.SettingsSaveCommand;

namespace ChustaSoft.Vaulture.UI.Pages;


//TODO: By the moment, only Azure Vaults are supported
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
    private ThemeMode themeModeSelected;

    [ObservableProperty]
    private string secureConnectionAliasToAdd = string.Empty;

    [ObservableProperty]
    private string secureConnectionValueToAdd = string.Empty;

    [ObservableProperty]
    private ObservableCollection<SecureConnectionsDto> secureConnections = new();

    [ObservableProperty]
    private bool enableAddAction = false;

    [ObservableProperty]
    private bool enableSaveAction = false;


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
    private void OnAddSecureConnection()
    {
        if (!string.IsNullOrWhiteSpace(SecureConnectionAliasToAdd) && !string.IsNullOrWhiteSpace(SecureConnectionValueToAdd))
        {
            if (SecureConnections.Any(x => x.Type == SecureConnectionType.AzureVault))
                SecureConnections.First(x => x.Type == SecureConnectionType.AzureVault).Values.Add(new SecureConnectionValue(SecureConnectionAliasToAdd, SecureConnectionValueToAdd));
            else
                SecureConnections.Add(new SecureConnectionsDto(SecureConnectionType.AzureVault, [new SecureConnectionValue(SecureConnectionAliasToAdd, SecureConnectionValueToAdd)]));

            SecureConnectionAliasToAdd = string.Empty;
            SecureConnectionValueToAdd = string.Empty;
            EnableSaveAction = true;
        }
    }

    [RelayCommand]
    private async Task OnSave()
    {
        var command = new SettingsSaveCommand(ThemeModeSelected, SecureConnections);

        await _appSettingsService.SaveAsync(command);

        EnableSaveAction = false;
    }

    [RelayCommand]
    public async Task OnLoadAsync()
    {
        var settings = await _appSettingsService.LoadAsync();

        ThemeModeSelected = settings.Theme;
        SecureConnections = new ObservableCollection<SecureConnectionsDto>(settings.SecureConnections);
    }


    partial void OnSecureConnectionAliasToAddChanging(string value)
    {
        if (!string.IsNullOrEmpty(SecureConnectionValueToAdd) && !string.IsNullOrEmpty(value))
            EnableAddAction = true;
        else
            EnableAddAction = false;
    }

    partial void OnSecureConnectionValueToAddChanging(string value)
    {
        if (!string.IsNullOrEmpty(SecureConnectionAliasToAdd) && !string.IsNullOrEmpty(value))
            EnableAddAction = true;
        else
            EnableAddAction = false;
    }

}
