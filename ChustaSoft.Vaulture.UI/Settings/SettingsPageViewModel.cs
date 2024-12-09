﻿using ChustaSoft.Vaulture.Application.Settings;
using ChustaSoft.Vaulture.UI.Common;
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
    private string secureConnectionAliasToAdd = string.Empty;

    [ObservableProperty]
    private string secureConnectionValueToAdd = string.Empty;

    [ObservableProperty]
    private ObservableCollection<SecureConnectionsDto> secureConnections = new();

    [ObservableProperty]
    private bool enableAddAction = false;

    [ObservableProperty]
    private bool enableSaveAction = false;

    [ObservableProperty]
    private SecureConnectionTypesModel secureConnectionTypes = SecureConnectionTypesProvider.Values;

    [ObservableProperty]
    private SecureConnectionTypeModel secureConnectionTypeSelected = SecureConnectionTypesProvider.Default;


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
            if (SecureConnections.Any(x => x.Type == SecureConnectionTypeSelected.Type))
                SecureConnections.First(x => x.Type == SecureConnectionTypeSelected.Type).Values.Add(new SecureConnectionValue(SecureConnectionTypeSelected.Type, SecureConnectionAliasToAdd, SecureConnectionValueToAdd));
            else
                SecureConnections.Add(new SecureConnectionsDto(SecureConnectionTypeSelected.Type, [new SecureConnectionValue(SecureConnectionTypeSelected.Type, SecureConnectionAliasToAdd, SecureConnectionValueToAdd)]));

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
