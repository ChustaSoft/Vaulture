namespace ChustaSoft.Vaulture.UI.Settings;

public partial class SettingsPage
{
    public SettingsPage(SettingsPageViewModel viewModel)
    {
        DataContext = viewModel;

        InitializeComponent();
    }
}
