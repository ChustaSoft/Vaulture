using System.Windows.Controls;

namespace ChustaSoft.Vaulture.UI.Secrets;


public partial class SecretsPage : Page
{

    public SecretsPage(SecretsPageViewModel viewModel)
    {
        DataContext = viewModel;

        InitializeComponent();

        viewModel.DialogService.SetDialogHost(RootContentDialog);
        viewModel.SnackbarService.SetSnackbarPresenter(SnackbarPresenter);
    }
}
