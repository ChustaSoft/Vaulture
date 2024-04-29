﻿using ChustaSoft.Vaulture.Application.Settings;
using ChustaSoft.Vaulture.Domain.Settings;
using System.Collections.ObjectModel;
using System.Runtime.InteropServices;
using Wpf.Ui.Appearance;
using Wpf.Ui.Controls;
using SettingsSaveCommand = ChustaSoft.Vaulture.Application.Settings.SettingsSaveCommand;

namespace ChustaSoft.Vaulture.UI.Pages;


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
    private ObservableCollection<SecureSettingsValuesDto> secureConnections = new();


    [RelayCommand]
    private void OnSystemThemeRadioButtonChecked()
    {
        var isUsingBlackTheme = IsSystemUsingDarkMode();

        ThemeModeSelected = ThemeMode.System;

        if (isUsingBlackTheme)
            ApplyDarkTheme();
        else
            ApplyLightTheme();
    }

    [RelayCommand]
    private void OnLightThemeRadioButtonChecked()
    {
        ThemeModeSelected = ThemeMode.Light;
        ApplyLightTheme();
    }

    [RelayCommand]
    private void OnDarkThemeRadioButtonChecked()
    {
        ThemeModeSelected = ThemeMode.Dark;
        ApplyDarkTheme();
    }

    [RelayCommand]
    private void OnAddSecureConnection()
    {
        if (!string.IsNullOrWhiteSpace(SecureConnectionToAdd))
        {
            SecureConnections.First(x => x.Type == SecureConnectionType.AzureVault).Values.Add(SecureConnectionToAdd);
            SecureConnectionToAdd = string.Empty;
        }
    }

    [RelayCommand]
    private async Task OnSave()
    {
        //TODO: Implement save button, disable if nothing changed
        var command = new SettingsSaveCommand(ThemeModeSelected, SecureConnections);

        await _appSettingsService.SaveAsync(command);
    }


    public void OnLoad()
    {
        //TODO: Implement on load, retrieve data from local storage
        ThemeModeSelected = ThemeMode.System;

        SecureConnections = new ObservableCollection<SecureSettingsValuesDto>() { new(SecureConnectionType.AzureVault, new ObservableCollection<string>() { "connection1", "connection2" }) };
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
