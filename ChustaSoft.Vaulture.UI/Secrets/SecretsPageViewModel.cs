using ChustaSoft.Vaulture.Application.Secrets;
using ChustaSoft.Vaulture.Application.Settings;
using ChustaSoft.Vaulture.Domain.Secrets;
using ChustaSoft.Vaulture.UI.Common;
using System.Collections.Concurrent;
using System.Collections.ObjectModel;
using Wpf.Ui.Controls;

namespace ChustaSoft.Vaulture.UI.Secrets;


public partial class SecretsPageViewModel : ObservableObject
{

    private readonly IAppSettingsService _appSettingsService;
    private readonly ISecretsService _secretsService;
    private readonly INavigationService _navigationService;

    public ISnackbarService SnackbarService { get; set; }
    public IContentDialogService DialogService { get; set; }


    public SecretsPageViewModel(IAppSettingsService appSettingsService, ISecretsService secretsService, INavigationService navigationService, ISnackbarService snackbarService, IContentDialogService dialogService)
    {
        _appSettingsService = appSettingsService;
        _secretsService = secretsService;
        _navigationService = navigationService;

        SnackbarService = snackbarService;
        DialogService = dialogService;
    }


    [ObservableProperty]
    private ObservableCollection<SecretsStorageViewModel> secretsStorages = new ObservableCollection<SecretsStorageViewModel>();


    [RelayCommand]
    public async Task OnLoadAsync()
    {
        var secureConnections = await _appSettingsService.GetConnectionsAsync();
        var connectionsSecretsQueryTasks = new List<Task>();
        var connectionsSecrets = new ConcurrentBag<SecretsStorageViewModel>();

        foreach (var secureConnectionTypes in secureConnections)
            foreach (var secureConnection in secureConnectionTypes.Value)
                connectionsSecretsQueryTasks.Add(RetrieveSecrets(secureConnectionTypes.Key, connectionsSecrets, secureConnection!));

        await Task.WhenAll(connectionsSecretsQueryTasks);

        SecretsStorages = new ObservableCollection<SecretsStorageViewModel>(connectionsSecrets);
    }

    [RelayCommand]
    private async Task OnViewSecret(SecretActionRequestModel request)
    {
        await OpenSecretDetailPage(request, PageMode.View);
    }

    [RelayCommand]
    private async Task OnEditSecret(SecretActionRequestModel request)
    {
        await OpenSecretDetailPage(request, PageMode.Edit);
    }

    [RelayCommand]
    private async Task OnDeleteSecret(SecretActionRequestModel request)
    {
        var confirmationDialog = ShowConfirmationDialog();
        var dialogResult = await confirmationDialog.ShowDialogAsync();

        if (dialogResult == Wpf.Ui.Controls.MessageBoxResult.Primary)
        {
            await _secretsService.DeleteAsync(request.ResourceType, request.SecretsStorageConnection, request.SecretDto.Name);
            SecretsStorages.First(x => x.SecretsStorage.Value == request.SecretsStorageConnection).RemoveSecret(request.SecretDto.Name);

            SnackbarService.Show("Secret removed", $"Secret {request.SecretDto.Name} has been properly removed from {request.SecretsStorageConnection}", ControlAppearance.Info, new SymbolIcon(SymbolRegular.Fluent24), TimeSpan.FromSeconds(3));
        }

        await Task.CompletedTask;
    }


    private static Wpf.Ui.Controls.MessageBox ShowConfirmationDialog()
    {
        return new Wpf.Ui.Controls.MessageBox
        {
            Title = "Are you sure?",
            Content = "Delete Confirmation",
            PrimaryButtonText = "Yes",
            CloseButtonText = "No",
            WindowStartupLocation = WindowStartupLocation.CenterOwner,
            PrimaryButtonAppearance = Wpf.Ui.Controls.ControlAppearance.Danger
        };
    }

    private async Task RetrieveSecrets(SecretsStorageType resourceType, ConcurrentBag<SecretsStorageViewModel> connectionsSecrets, Application.Secrets.SecretsStorageDto secureConnection)
    {
        var secrets = await _secretsService.GetAllSummariesAsync(resourceType, secureConnection.Value);
        connectionsSecrets.Add(new SecretsStorageViewModel(secureConnection, secrets));
    }

    private async Task OpenSecretDetailPage(SecretActionRequestModel request, PageMode pageMode)
    {
        //TODO: Here we should create different type of credentials, and based on a converter, navigate to one or another control, now is forced to Credential
        var secret = await _secretsService.GetAsync(request.ResourceType, request.SecretsStorageConnection, request.SecretDto.Name);
        SecretPageViewModel? viewModel = null;

        switch (pageMode)
        {
            case PageMode.View:
                viewModel = new SecretPageViewModel(secret);
                break;
            case PageMode.Edit:
                viewModel = new SecretPageViewModel(_secretsService, request.ResourceType, request.SecretsStorageConnection, secret);
                break;
            default:
                throw new ArgumentException("Unsupported Page type");
        }


        _navigationService.Navigate(typeof(SecretPage), viewModel);
    }
}
