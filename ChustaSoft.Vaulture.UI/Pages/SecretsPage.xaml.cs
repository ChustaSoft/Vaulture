using System.Windows.Controls;

namespace ChustaSoft.Vaulture.UI.Pages;


public partial class SecretsPage : Page
{    

    public SecretsPage(SecretsPageViewModel viewModel)
    {
        DataContext = viewModel;

        InitializeComponent();
    }
}
