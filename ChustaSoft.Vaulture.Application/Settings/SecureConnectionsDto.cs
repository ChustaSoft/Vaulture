using ChustaSoft.Vaulture.Domain.Settings;
using System.Collections.ObjectModel;

namespace ChustaSoft.Vaulture.Application.Settings;

public record SecureConnectionsDto(SecureConnectionType Type, ObservableCollection<string> Values);
