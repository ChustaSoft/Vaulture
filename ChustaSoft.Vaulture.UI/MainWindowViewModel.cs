﻿using ChustaSoft.Vaulture.UI.Configuration;
using System.Collections.ObjectModel;
using Wpf.Ui.Controls;

namespace ChustaSoft.Vaulture.UI;


public partial class MainWindowViewModel : ObservableObject
{

    private bool _isInitialized = false;

    [ObservableProperty]
    private string _applicationTitle = string.Empty;

    [ObservableProperty]
    private ObservableCollection<object> _navigationItems = [];

    [ObservableProperty]
    private ObservableCollection<object> _navigationFooter = [];

    [ObservableProperty]
    private ObservableCollection<MenuItem> _trayMenuItems = [];


    public MainWindowViewModel(INavigationService navigationService)
    {
        if (!_isInitialized)
        {
            InitializeViewModel();
        }
    }

    private void InitializeViewModel()
    {
        ApplicationTitle = "WPF UI - MVVM Demo";

        NavigationItems =
        [
            new NavigationViewItem()
            {
                Content = AppConstants.Pages.SECRETS,
                Icon = new SymbolIcon { Symbol = SymbolRegular.Key24 },
                TargetPageType = typeof(Pages.SecretsPage)
            }
        ];

        NavigationFooter =
        [
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
