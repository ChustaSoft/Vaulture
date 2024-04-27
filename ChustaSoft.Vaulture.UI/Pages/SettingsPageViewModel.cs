using ChustaSoft.Vaulture.Application.Settings;
using ChustaSoft.Vaulture.Domain.Settings;
using System.Collections.ObjectModel;
using Wpf.Ui.Appearance;
using Wpf.Ui.Controls;

namespace ChustaSoft.Vaulture.UI.Pages;


public partial class SettingsPageViewModel : ObservableObject, INavigationAware
{

    [ObservableProperty]
    private bool darkThemeChecked = false;

    [ObservableProperty]
    private bool lightThemeChecked = false;

    [ObservableProperty]
    private string secureConnectionToAdd = string.Empty;

    [ObservableProperty] //Temporary mocking these values
    private ObservableCollection<SettingsValuesDto> secureConnections = new ObservableCollection<SettingsValuesDto>() { new (SecureConnectionType.AzureVault, new ObservableCollection<string>() { "connection1", "connection2" }) };



    [RelayCommand]
    private void OnLightThemeRadioButtonChecked()
    {
        ApplicationThemeManager.Apply(ApplicationTheme.Light);
    }

    [RelayCommand]
    private void OnDarkThemeRadioButtonChecked()
    {
        ApplicationThemeManager.Apply(ApplicationTheme.Dark);
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

    //TODO: Take default system theme from System

    //TODO: Implement on load, retrieve data from local storage

    //TODO. Add system default theme to options

    //TODO: Implement save button, disable if nothing changed

    //TODO: Inject IAppSettingsService
}
