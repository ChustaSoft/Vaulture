namespace ChustaSoft.Vaulture.UI.Pages;

public partial class SettingsPage
{

    public SettingsPageViewModel ViewModel { get; private set; }


    public SettingsPage(SettingsPageViewModel viewModel)
    {
        ViewModel = viewModel;
        DataContext = this;

        InitializeComponent();
    }

}
