using Wpf.Ui.Appearance;
using Wpf.Ui.Controls;

namespace ChustaSoft.Vaulture.UI.Pages;


public partial class SettingsPageViewModel : ObservableObject, INavigationAware
{

    [ObservableProperty]
    private bool _darkThemeChecked = false;

    [ObservableProperty]
    private bool _lightThemeChecked = false;   


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

    public void OnNavigatedTo()
    {
        if (ApplicationThemeManager.GetAppTheme() == ApplicationTheme.Dark)
            DarkThemeChecked = true;
        else
            LightThemeChecked = true;        
    }

    public void OnNavigatedFrom() { }
}
