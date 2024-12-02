using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChustaSoft.Vaulture.Domain.Secrets;

public interface ISecretsStorageService
{
    public Task SaveAsync(string storageName, Secret secret);
}
