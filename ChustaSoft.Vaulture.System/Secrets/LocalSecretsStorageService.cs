using ChustaSoft.Vaulture.LocalSystem.Secrets;
using System.Xml.Serialization;

namespace ChustaSoft.Vaulture.Domain.Secrets;


/// <summary>
/// This implementation is only for testing and local development purposes
/// </summary>
public class LocalFileSecretsStorageService : ISecretsStorageService
{

    public async Task<IEnumerable<Secret>> GetAllAsync(string storageConnection)
    {
        if (!storageConnection.EndsWith(".xml"))
            storageConnection += ".xml";

        var localStorageModel = LoadFile(storageConnection);

        var data = localStorageModel.Secrets.Select(x => new Secret(x.Type, x.Name, new SecretValue(x.Value)));

        return await Task.FromResult(data);
    }

    public async Task<Secret> GetAsync(string storageConnection, string name)
    {
        if (!storageConnection.EndsWith(".xml"))
            storageConnection += ".xml";

        var localStorageModel = LoadFile(storageConnection);

        var data = localStorageModel.Secrets
            .First(x => x.Name == name);

        return await Task.FromResult(new Secret(data.Type, data.Name, new SecretValue(data.Value)));
    }

    public Task SaveAsync(string storageConnection, Secret secret)
    {
        if (!storageConnection.EndsWith(".xml"))
            storageConnection += ".xml";

        var localStorageModel = LoadFile(storageConnection);
        var secretInfraModel = new SecretInfraModel
        {
            Name = secret.Name,
            Type = secret.Type,
            Value = secret.Value.Value
        };

        if (localStorageModel.Secrets.Any(x => x.Name == secret.Name))
        {
            var previous = localStorageModel.Secrets.First(x => x.Name == secret.Name);
            localStorageModel.Secrets.Remove(previous);
        }

        localStorageModel.Secrets.Add(secretInfraModel);

        XmlSerializer serializer = new XmlSerializer(typeof(LocalSecretStorageInfraModel));

        using TextWriter writer = new StreamWriter(storageConnection);

        serializer.Serialize(writer, localStorageModel);

        return Task.CompletedTask;
    }

    public Task DeleteAsync(string storageConnection, string name)
    {
        if (!storageConnection.EndsWith(".xml"))
            storageConnection += ".xml";

        var localStorageModel = LoadFile(storageConnection);

        var secretToRemove = localStorageModel.Secrets.First(x => x.Name == name);

        localStorageModel.Secrets.Remove(secretToRemove);

        XmlSerializer serializer = new XmlSerializer(typeof(LocalSecretStorageInfraModel));

        using TextWriter writer = new StreamWriter(storageConnection);

        serializer.Serialize(writer, localStorageModel);

        return Task.CompletedTask;
    }


    private LocalSecretStorageInfraModel LoadFile(string storageConnection)
    {
        try
        {
            XmlSerializer serializer = new XmlSerializer(typeof(LocalSecretStorageInfraModel));

            using TextReader reader = new StreamReader(storageConnection);

            var infraModel = (LocalSecretStorageInfraModel)serializer.Deserialize(reader)!;

            return infraModel!;
        }
        catch (Exception)
        {
            //TODO: This implies not retrieving the file, but this is a development based implementation, there's no need to control it, but it's required to send an empty model back
            return new LocalSecretStorageInfraModel();
        }
    }
}
