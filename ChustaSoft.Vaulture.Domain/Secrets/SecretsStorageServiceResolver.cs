﻿namespace ChustaSoft.Vaulture.Domain.Secrets;


public delegate ISecretsStorageService SecretsStorageServiceResolver(SecretsStorageType storageType);