using System.Windows.Controls;

namespace ChustaSoft.Vaulture.UI.Pages;


public partial class SecretFormPage : Page
{
    public SecretFormPage(SecretFormPageViewModel viewModel)
    {
        DataContext = viewModel;

        InitializeComponent();
    }
}
