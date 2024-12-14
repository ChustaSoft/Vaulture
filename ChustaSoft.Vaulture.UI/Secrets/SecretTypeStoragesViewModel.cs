using ChustaSoft.Vaulture.Application.Secrets;
using ChustaSoft.Vaulture.Domain.Secrets;
using System.Collections.ObjectModel;

namespace ChustaSoft.Vaulture.UI.Secrets;


public partial class SecretTypeStoragesViewModel : ObservableObject
{

    [ObservableProperty]
    private SecretsStorageType? type;

    [ObservableProperty]
    private ObservableCollection<SecretsStorageDto> secretsStorages = new ObservableCollection<SecretsStorageDto>();


    public SecretTypeStoragesViewModel()
    {
        
    }

    public SecretTypeStoragesViewModel(SecretsStorageType type, SecretsStorageDto[] secretsStorages)
    {
        Type = type;
        SecretsStorages = new ObservableCollection<SecretsStorageDto>(secretsStorages);
    }
}