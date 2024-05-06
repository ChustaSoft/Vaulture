using ChustaSoft.Vaulture.Domain.Settings;
using System.Xml.Serialization;

namespace ChustaSoft.Vaulture.LocalSystem.Settings;


public class AppSettingsFileStorage : IAppSettingsStorage
{

    private const String SETTINGS_XML_FILE = "vaulture-settings.xml";


    public Task<AppSettings> LoadAsync()
    {
        //TODO: Implement validation via xsd of the xml file
        return Task.Run(() =>
        {
            XmlSerializer serializer = new XmlSerializer(typeof(AppSettingsInfraModel));
            using (TextReader reader = new StreamReader(SETTINGS_XML_FILE))
            {
                try
                {
                    var infraSettings = (AppSettingsInfraModel)serializer.Deserialize(reader);

                    return infraSettings.ToEntity();
                }
                catch (Exception ex)
                {
                    //TODO: Return default if nothing saved yet

                    throw;
                }
            }
        });
    }

    public Task SaveAsync(AppSettings settings)
    {
        //TODO: Implement validation via xsd of the xml file
        return Task.Run(() =>
        {
            var infraModel = new AppSettingsInfraModel(settings);

            XmlSerializer serializer = new XmlSerializer(typeof(AppSettingsInfraModel));
            using (TextWriter writer = new StreamWriter(SETTINGS_XML_FILE))
            {
                serializer.Serialize(writer, infraModel);
            }
        });

    }
}
