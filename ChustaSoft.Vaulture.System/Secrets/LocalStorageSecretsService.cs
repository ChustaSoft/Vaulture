using ChustaSoft.Vaulture.LocalSystem.Secrets;
using System.Xml.Serialization;

namespace ChustaSoft.Vaulture.Domain.Secrets;


/// <summary>
/// This implementation is only for testing and local development purposes
/// </summary>
public class LocalFileSecretsStorageService : ISecretsStorageService
{
    public Task SaveAsync(string storageName, Secret secret)
    {
        if(!storageName.EndsWith(".xml"))
            storageName += ".xml";

        var localStorageModel = LoadFile(storageName);
        var secretInfraModel = new SecretInfraModel
        {
            Name = secret.Name,
            Type = secret.Type,
            Value = secret.Value.Value
        };
        localStorageModel.Secrets.Add(secretInfraModel);

        XmlSerializer serializer = new XmlSerializer(typeof(LocalFileSecretStorageInfraModel));

        using TextWriter writer = new StreamWriter(storageName);

        serializer.Serialize(writer, localStorageModel);

        return Task.CompletedTask;
    }


    private LocalFileSecretStorageInfraModel LoadFile(string storageName)
    {
        try
        {
            XmlSerializer serializer = new XmlSerializer(typeof(LocalFileSecretStorageInfraModel));

            using TextReader reader = new StreamReader(storageName);

            var infraModel = (LocalFileSecretStorageInfraModel)serializer.Deserialize(reader);

            return infraModel;
        }
        catch (Exception)
        {
            return new LocalFileSecretStorageInfraModel();
        }
    }
}
