namespace ChustaSoft.Vaulture.UI.Pages;

public partial class SettingsPage
{
    public SettingsPage(SettingsPageViewModel viewModel)
    {
        DataContext = viewModel;

        InitializeComponent();
    }

}
