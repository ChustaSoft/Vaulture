using System.Windows.Controls;

namespace ChustaSoft.Vaulture.UI.Pages;


public partial class SecretsPage : Page
{    

    public SecretsPageViewModel ViewModel { get; private set; }


    public SecretsPage(SecretsPageViewModel viewModel)
    {
        ViewModel = viewModel;
        DataContext = this;

        InitializeComponent();
    }
}
