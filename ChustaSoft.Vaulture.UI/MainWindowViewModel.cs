using ChustaSoft.Vaulture.UI.Common;
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
                TargetPageType = typeof(Secrets.SecretsDashboardPage)
            }
        ];

        NavigationFooter =
        [
            new NavigationViewItem()
            {
                Content = AppConstants.Pages.ABOUT,
                Icon = new SymbolIcon { Symbol = SymbolRegular.Info24 },
                TargetPageType = typeof(About.AboutPage)
            },
            new NavigationViewItem()
            {
                Content = AppConstants.Pages.SETTINGS,
                Icon = new SymbolIcon { Symbol = SymbolRegular.Settings24 },
                TargetPageType = typeof(Settings.SettingsPage)
            },
        ];

        TrayMenuItems = [new() { Header = AppConstants.Pages.SECRETS, Tag = "tray_home" }];

        _isInitialized = true;
    }
}
