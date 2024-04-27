using ChustaSoft.Vaulture.Domain.Settings;
using System.Collections.ObjectModel;

namespace ChustaSoft.Vaulture.Application.Settings;

public record SettingsValuesDto(SecureConnectionType Type, ObservableCollection<string> Values);
