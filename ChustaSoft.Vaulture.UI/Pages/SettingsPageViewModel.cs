using ChustaSoft.Vaulture.Application.Settings;
using ChustaSoft.Vaulture.Domain.Settings;
using System.Collections.ObjectModel;
using System.Runtime.InteropServices;
using Wpf.Ui.Appearance;
using SettingsSaveCommand = ChustaSoft.Vaulture.Application.Settings.SettingsSaveCommand;

namespace ChustaSoft.Vaulture.UI.Pages;


//TODO: By the moment, only Azure Vaults are supported
public partial class SettingsPageViewModel : ObservableObject
{

    [LibraryImport("UXTheme.dll", EntryPoint = "#138", SetLastError = true)]
    [return: MarshalAs(UnmanagedType.Bool)]
    public static partial bool IsSystemUsingDarkMode();


    private readonly IAppSettingsService _appSettingsService;

    public SettingsPageViewModel(IAppSettingsService appSettingsService)
    {
        _appSettingsService = appSettingsService;
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
        var isUsingBlackTheme = IsSystemUsingDarkMode();

        ThemeModeSelected = ThemeMode.System;

        if (isUsingBlackTheme)
            ApplyDarkTheme();
        else
            ApplyLightTheme();

        EnableSaveAction = true;
    }

    [RelayCommand]
    private void OnLightThemeRadioButtonChecked()
    {
        ThemeModeSelected = ThemeMode.Light;
        ApplyLightTheme();
        EnableSaveAction = true;
    }

    [RelayCommand]
    private void OnDarkThemeRadioButtonChecked()
    {
        ThemeModeSelected = ThemeMode.Dark;
        ApplyDarkTheme();
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


    private void ApplyLightTheme()
    {
        ApplicationThemeManager.Apply(ApplicationTheme.Light);
    }

    private void ApplyDarkTheme()
    {
        ApplicationThemeManager.Apply(ApplicationTheme.Dark);
    }

}
