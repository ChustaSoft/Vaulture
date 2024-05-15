using ChustaSoft.Vaulture.Application.Settings;
using System.Collections.ObjectModel;

namespace ChustaSoft.Vaulture.UI.Pages;

public partial class SecretsPageViewModel : ObservableObject
{
    
    private readonly IAppSettingsService _appSettingsService;


    public SecretsPageViewModel(IAppSettingsService appSettingsService)
    {
        _appSettingsService = appSettingsService;
    }


    [ObservableProperty]
    private ObservableCollection<string> secureConnections;


    [RelayCommand]
    public async Task OnLoadAsync()
    {
        var azureConnections = await _appSettingsService.GetConnectionsAsync();
        
        SecureConnections = new ObservableCollection<string>(azureConnections.SelectMany(x => x.Value));
    }
}
