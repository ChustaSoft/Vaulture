using ChustaSoft.Vaulture.UI.Configuration;
using System.Collections.ObjectModel;
using Wpf.Ui.Controls;

namespace ChustaSoft.Vaulture.UI;


public partial class MainWindowViewModel : ObservableObject
{

    private bool _isInitialized = false;

    [ObservableProperty]
    private string _applicationTitle = "ChustaSoft Vaulture";

    [ObservableProperty]
    private ObservableCollection<object> _navigationItems = [];

    [ObservableProperty]
    private ObservableCollection<object> _navigationFooter = [];

    [ObservableProperty]
    private ObservableCollection<MenuItem> _trayMenuItems = [];


    public MainWindowViewModel()
    {
        if (!_isInitialized)
        {
            InitializeViewModel();
        }
    }

    private void InitializeViewModel()
    {
        NavigationItems =
        [
            new NavigationViewItem()
            {
                Content = AppConstants.Pages.SECRETS,
                Icon = new SymbolIcon { Symbol = SymbolRegular.Key24 },
                TargetPageType = typeof(Pages.SecretsPage)
            },
            new NavigationViewItem()
            {
                Content = AppConstants.Pages.ADD_SECRET,
                Icon = new SymbolIcon { Symbol = SymbolRegular.Add24 },
                TargetPageType = typeof(Pages.SecretFormPage)
            }
        ];

        NavigationFooter =
        [
            new NavigationViewItem()
            {
                Content = AppConstants.Pages.ABOUT,
                Icon = new SymbolIcon { Symbol = SymbolRegular.Info24 },
                TargetPageType = typeof(Pages.AboutPage)
            },
            new NavigationViewItem()
            {
                Content = AppConstants.Pages.SETTINGS,
                Icon = new SymbolIcon { Symbol = SymbolRegular.Settings24 },
                TargetPageType = typeof(Pages.SettingsPage)
            },
        ];

        TrayMenuItems = [new() { Header = AppConstants.Pages.SECRETS, Tag = "tray_home" }];

        _isInitialized = true;
    }
}
