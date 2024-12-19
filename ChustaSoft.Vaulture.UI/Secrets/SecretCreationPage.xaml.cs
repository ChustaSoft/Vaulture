using System.Windows.Controls;

namespace ChustaSoft.Vaulture.UI.Secrets;


public partial class SecretCreationPage : Page
{
    public SecretCreationPage(SecretCreationPageViewModel viewModel)
    {
        DataContext = viewModel;

        InitializeComponent();
    }

    private void PasswordBox_PasswordChanged(Object sender, RoutedEventArgs e)
    {
        var vm = (SecretCreationPageViewModel)DataContext;

        vm.Password = ((Wpf.Ui.Controls.PasswordBox)sender).Password;
    }
}
