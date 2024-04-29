using ChustaSoft.Vaulture.Application.Settings;
using ChustaSoft.Vaulture.Domain.Settings;
using System.Collections.ObjectModel;
using System.Runtime.InteropServices;
using Wpf.Ui.Appearance;
using Wpf.Ui.Controls;

namespace ChustaSoft.Vaulture.UI.Pages;


public partial class SettingsPageViewModel : ObservableObject, INavigationAware
{

    [LibraryImport("UXTheme.dll", EntryPoint = "#138", SetLastError = true)]
    [return: MarshalAs(UnmanagedType.Bool)]
    public static partial bool IsSystemUsingDarkMode();


    [ObservableProperty]
    private bool systemThemeChecked;

    [ObservableProperty]
    private bool darkThemeChecked;

    [ObservableProperty]
    private bool lightThemeChecked;

    [ObservableProperty]
    private string secureConnectionToAdd = string.Empty;

    [ObservableProperty]
    private ObservableCollection<SettingsValuesDto> secureConnections = new();


    [RelayCommand]
    private void OnSystemThemeRadioButtonChecked()
    {
        var isUsingBlackTheme = IsSystemUsingDarkMode();

        SystemThemeChecked = true;

        if (isUsingBlackTheme)
            ApplyDarkTheme();
        else
            ApplyLightTheme();
    }

    [RelayCommand]
    private void OnLightThemeRadioButtonChecked()
    {
        LightThemeChecked = true;
        ApplyLightTheme();
    }

    [RelayCommand]
    private void OnDarkThemeRadioButtonChecked()
    {
        DarkThemeChecked = true;
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

    public void OnNavigatedTo()
    {
        if (ApplicationThemeManager.GetAppTheme() == ApplicationTheme.Dark)
            DarkThemeChecked = true;
        else
            LightThemeChecked = true;
    }

    public void OnLoad()
    {
        //TODO: Implement on load, retrieve data from local storage
        SystemThemeChecked = true;
        DarkThemeChecked = false;
        LightThemeChecked = false;

        SecureConnections = new ObservableCollection<SettingsValuesDto>() { new(SecureConnectionType.AzureVault, new ObservableCollection<string>() { "connection1", "connection2" }) };
    }


    public void OnNavigatedFrom() { }


    private void ApplyLightTheme()
    {
        ApplicationThemeManager.Apply(ApplicationTheme.Light);
    }

    private void ApplyDarkTheme()
    {
        ApplicationThemeManager.Apply(ApplicationTheme.Dark);
    }

    //TODO: Take default system theme from System

    //TODO. Add system default theme to options

    //TODO: Implement save button, disable if nothing changed

    //TODO: Inject IAppSettingsService
}
