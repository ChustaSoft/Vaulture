using System.Windows.Controls;

namespace ChustaSoft.Vaulture.UI.Pages;


public partial class SecretFormPage : Page
{
    public SecretFormPage(SecretFormPageViewModel viewModel)
    {
        DataContext = viewModel;

        InitializeComponent();
    }

    private void PasswordBox_PasswordChanged(Object sender, RoutedEventArgs e)
    {
        var vm = (SecretFormPageViewModel)DataContext;

        vm.Password = ((Wpf.Ui.Controls.PasswordBox)sender).Password;
    }
}
