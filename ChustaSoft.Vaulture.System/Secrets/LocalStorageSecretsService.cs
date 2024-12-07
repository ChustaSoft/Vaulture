﻿using ChustaSoft.Vaulture.LocalSystem.Secrets;
using System.Xml.Serialization;

namespace ChustaSoft.Vaulture.Domain.Secrets;


/// <summary>
/// This implementation is only for testing and local development purposes
/// </summary>
public class LocalFileSecretsStorageService : ISecretsStorageService
{

    public async Task<IEnumerable<Secret>> GetAllAsync(string storageName)
    {
        if (!storageName.EndsWith(".xml"))
            storageName += ".xml";

        var localStorageModel = LoadFile(storageName);

        var data = localStorageModel.Secrets.Select(x => new Secret(x.Type, x.Name, x.Value));
        
        return await Task.FromResult(data);
    }

    public async Task<Secret> GetAsync(string storageName, string name)
    {
        if (!storageName.EndsWith(".xml"))
            storageName += ".xml";

        var localStorageModel = LoadFile(storageName);

        var data = localStorageModel.Secrets
            .First(x => x.Name == x.Name);

        return await Task.FromResult(new Secret(data.Type, data.Name, data.Value));
    }

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

    public Task DeleteAsync(string storageName, string name)
    {
        if (!storageName.EndsWith(".xml"))
            storageName += ".xml";

        var localStorageModel = LoadFile(storageName);
      
        var secretToRemove = localStorageModel.Secrets.First(x => x.Name == name);

        localStorageModel.Secrets.Remove(secretToRemove);

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
