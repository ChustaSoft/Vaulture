using ChustaSoft.Vaulture.Domain.Settings;

namespace ChustaSoft.Vaulture.LocalSystem.Settings;


public class AppSettingsFileStorage : IAppSettingsStorage
{
    public Task SaveAsync(AppSettings settings)
    {
        //TODO: Implement persistance of XAML serialized AppSettings

        //TODO: Implement validation via xsd of the xml file
        throw new NotImplementedException();
    }
}
