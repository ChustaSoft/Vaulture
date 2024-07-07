using ChustaSoft.Vaulture.Application.Secrets;
using ChustaSoft.Vaulture.Application.Settings;
using System.Collections.Concurrent;
using System.Collections.ObjectModel;

namespace ChustaSoft.Vaulture.UI.Pages;

public partial class SecretsPageViewModel : ObservableObject
{

    private readonly IAppSettingsService _appSettingsService;
    private readonly ISecretsService _secretsService;


    public SecretsPageViewModel(IAppSettingsService appSettingsService, ISecretsService secretsService)
    {
        _appSettingsService = appSettingsService;
        _secretsService = secretsService;
    }

    [ObservableProperty]
    private ObservableCollection<SecureConnectionSecretsViewModel> secureConnections = new ObservableCollection<SecureConnectionSecretsViewModel>();


    [RelayCommand]
    public async Task OnLoadAsync()
    {
        var azureConnections = await _appSettingsService.GetConnectionsAsync();
        var connectionsSecretsQueryTasks = new List<Task>();
        var connectionsSecrets = new ConcurrentBag<SecureConnectionSecretsViewModel>();
        
        foreach (var secureConnection in azureConnections.SelectMany(x => x.Value))
            connectionsSecretsQueryTasks.Add(RetrieveSecrets(connectionsSecrets, secureConnection!));

        await Task.WhenAll(connectionsSecretsQueryTasks);

        SecureConnections = new ObservableCollection<SecureConnectionSecretsViewModel>(connectionsSecrets);
    }

    [RelayCommand]
    private async Task OnViewSecret(SecretDto secret)
    {
        await Task.CompletedTask;
    }

    [RelayCommand]
    private async Task OnDeleteSecret(SecretDto secret)
    {
        await Task.CompletedTask;
    }


    private async Task RetrieveSecrets(ConcurrentBag<SecureConnectionSecretsViewModel> connectionsSecrets, string secureConnection)
    {
        var secrets = await _secretsService.GetAllAsync(secureConnection);
        connectionsSecrets.Add(new SecureConnectionSecretsViewModel(secureConnection, secrets));
    }
}
