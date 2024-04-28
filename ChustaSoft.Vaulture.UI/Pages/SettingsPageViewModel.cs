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
    private bool systemThemeChecked = true; //TODO: For sake of testing, by now is active by default

    [ObservableProperty]
    private bool darkThemeChecked = false;

    [ObservableProperty]
    private bool lightThemeChecked = false;

    [ObservableProperty]
    private string secureConnectionToAdd = string.Empty;

    [ObservableProperty] //Temporary mocking these values
    private ObservableCollection<SettingsValuesDto> secureConnections = new ObservableCollection<SettingsValuesDto>() { new (SecureConnectionType.AzureVault, new ObservableCollection<string>() { "connection1", "connection2" }) };


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

    //TODO: Implement on load, retrieve data from local storage

    //TODO. Add system default theme to options

    //TODO: Implement save button, disable if nothing changed

    //TODO: Inject IAppSettingsService
}
