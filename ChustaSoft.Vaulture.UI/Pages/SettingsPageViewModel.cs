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
    private string secureConnectionToAdd = string.Empty;

    [ObservableProperty]
    private ObservableCollection<SecureConnectionsDto> secureConnections = new();

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
        if (!string.IsNullOrWhiteSpace(SecureConnectionToAdd))
        {
            if (SecureConnections.Any(x => x.Type == SecureConnectionType.AzureVault))
                SecureConnections.First(x => x.Type == SecureConnectionType.AzureVault).Values.Add(SecureConnectionToAdd);
            else
                SecureConnections.Add(new SecureConnectionsDto(SecureConnectionType.AzureVault, [SecureConnectionToAdd]));

            SecureConnectionToAdd = string.Empty;
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

}
