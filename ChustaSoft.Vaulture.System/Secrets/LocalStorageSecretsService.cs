using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChustaSoft.Vaulture.Domain.Secrets;

public class LocalFileSecretsStorageService : ISecretsStorageService
{
    public Task SaveAsync(String storageName, Secret secret)
    {
        throw new NotImplementedException();
    }
}
