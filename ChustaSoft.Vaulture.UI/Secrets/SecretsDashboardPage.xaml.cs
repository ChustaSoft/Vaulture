using System.Windows.Controls;

namespace ChustaSoft.Vaulture.UI.Secrets;


public partial class SecretsDashboardPage : Page
{

    public SecretsDashboardPage(SecretsDashboardPageViewModel viewModel)
    {
        DataContext = viewModel;

        InitializeComponent();

        viewModel.DialogService.SetDialogHost(RootContentDialog);
        viewModel.SnackbarService.SetSnackbarPresenter(SnackbarPresenter);
    }
}
