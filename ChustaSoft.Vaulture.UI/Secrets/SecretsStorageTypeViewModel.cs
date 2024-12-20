﻿using ChustaSoft.Common.Helpers;
using ChustaSoft.Vaulture.Application.Secrets;
using ChustaSoft.Vaulture.Domain.Secrets;
using System.Collections.ObjectModel;

namespace ChustaSoft.Vaulture.UI.Secrets;


public class SecretsStorageTypeViewModel : ObservableCollection<SecretsStorageTypeDto>
{
    internal SecretsStorageTypeViewModel()
    {
        foreach (var secretsStorageTypes in EnumsHelper.GetEnumList<SecretsStorageType>())
            Add(new SecretsStorageTypeDto(secretsStorageTypes, secretsStorageTypes.GetDescription()));
    }

}
