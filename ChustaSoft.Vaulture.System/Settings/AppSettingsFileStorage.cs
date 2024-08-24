using ChustaSoft.Vaulture.Domain.Settings;
using System.Xml.Serialization;

namespace ChustaSoft.Vaulture.LocalSystem.Settings;


//TODO: Implement validation via xsd of the xml file
public class AppSettingsFileStorage : IAppSettingsStorage
{

    private const string SETTINGS_XML_FILE = "vaulture-settings.xml";


    public AppSettings Load()
    {
        XmlSerializer serializer = new XmlSerializer(typeof(AppSettingsInfraModel));

        using TextReader reader = new StreamReader(SETTINGS_XML_FILE);

        var infraSettings = (AppSettingsInfraModel)serializer.Deserialize(reader);

        return infraSettings.ToEntity();
    }

    public void Save(AppSettings settings)
    {
        var infraModel = new AppSettingsInfraModel(settings);

        XmlSerializer serializer = new XmlSerializer(typeof(AppSettingsInfraModel));

        using TextWriter writer = new StreamWriter(SETTINGS_XML_FILE);

        serializer.Serialize(writer, infraModel);
    }

}
