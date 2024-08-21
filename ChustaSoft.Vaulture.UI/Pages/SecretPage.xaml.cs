using System.Windows.Controls;

namespace ChustaSoft.Vaulture.UI.Pages;


public partial class SecretPage : Page
{
    public SecretPage(SecretPageViewModel viewModel)
    {
        DataContext = viewModel;

        InitializeComponent();
    }
}
