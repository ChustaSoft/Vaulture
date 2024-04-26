using System.Windows.Controls;

namespace ChustaSoft.Vaulture.UI.Pages;


public partial class DashboardPage : Page
{    

    public DashboardPageViewModel ViewModel { get; private set; }


    public DashboardPage(DashboardPageViewModel viewModel)
    {
        ViewModel = viewModel;
        DataContext = this;

        InitializeComponent();
    }
}
