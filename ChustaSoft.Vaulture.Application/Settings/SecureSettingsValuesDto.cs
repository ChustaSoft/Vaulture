using ChustaSoft.Vaulture.Domain.Settings;
using System.Collections.ObjectModel;

namespace ChustaSoft.Vaulture.Application.Settings;

public record SecureSettingsValuesDto(SecureConnectionType Type, ObservableCollection<string> Values);
