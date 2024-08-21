using ChustaSoft.Vaulture.Application.Secrets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChustaSoft.Vaulture.UI.Pages;

public partial class SecretPageViewModel : ObservableObject
{

    [ObservableProperty]
    private CredentialDto credential;

}
