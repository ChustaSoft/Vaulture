using ChustaSoft.Vaulture.Application.Secrets;
using ChustaSoft.Vaulture.Application.Settings;
using ChustaSoft.Vaulture.Domain.Secrets;
using System.Collections.Concurrent;
using System.Collections.ObjectModel;

namespace ChustaSoft.Vaulture.UI.Secrets;


public partial class SecretsPageViewModel : ObservableObject
{

    private readonly IAppSettingsService _appSettingsService;
    private readonly ISecretsService _secretsService;
    private readonly INavigationService _navigationService;


    public SecretsPageViewModel(IAppSettingsService appSettingsService, ISecretsService secretsService, INavigationService navigationService)
    {
        _appSettingsService = appSettingsService;
        _secretsService = secretsService;
        _navigationService = navigationService;
    }


    [ObservableProperty]
    private ObservableCollection<SecureConnectionSecretsViewModel> secureConnections = new ObservableCollection<SecureConnectionSecretsViewModel>();


    [RelayCommand]
    public async Task OnLoadAsync()
    {
        var secureConnections = await _appSettingsService.GetConnectionsAsync();
        var connectionsSecretsQueryTasks = new List<Task>();
        var connectionsSecrets = new ConcurrentBag<SecureConnectionSecretsViewModel>();

        foreach (var secureConnectionTypes in secureConnections)
            foreach (var secureConnection in secureConnectionTypes.Value)
                connectionsSecretsQueryTasks.Add(RetrieveSecrets(secureConnectionTypes.Key, connectionsSecrets, secureConnection!));

        await Task.WhenAll(connectionsSecretsQueryTasks);

        SecureConnections = new ObservableCollection<SecureConnectionSecretsViewModel>(connectionsSecrets);
    }

    [RelayCommand]
    private async Task OnViewSecret(SecretDto secret)
    {
        //TODO: Here we should create different type of credentials, and based on a converter, navigate to one or another control
        //TODO: Retrieve credentials data from service
        var viewModel = new SecretPageViewModel { Credential = new CredentialDto("TEST - X", "TEST - X", "PWD") };
        _navigationService.Navigate(typeof(SecretPage), viewModel);

        await Task.CompletedTask;
    }

    [RelayCommand]
    private async Task OnDeleteSecret(SecretDto secret)
    {
        await Task.CompletedTask;
    }


    private async Task RetrieveSecrets(SecretsResourceType resourceType, ConcurrentBag<SecureConnectionSecretsViewModel> connectionsSecrets, SecureConnectionValue secureConnection)
    {
        var secrets = await _secretsService.GetAllAsync(resourceType, secureConnection.Value);
        connectionsSecrets.Add(new SecureConnectionSecretsViewModel(secureConnection, secrets));
    }
}
