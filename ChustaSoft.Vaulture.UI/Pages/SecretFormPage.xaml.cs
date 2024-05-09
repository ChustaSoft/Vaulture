using System.Windows.Controls;

namespace ChustaSoft.Vaulture.UI.Pages;


public partial class SecretFormPage : Page
{
    public SecretFormPageViewModel ViewModel { get; private set; }


    public SecretFormPage(SecretFormPageViewModel viewModel)
    {
        ViewModel = viewModel;
        DataContext = this;

        InitializeComponent();
    }
}
