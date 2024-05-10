namespace ChustaSoft.Vaulture.UI.Pages;


public partial class SecretFormPageViewModel : ObservableObject
{

    [ObservableProperty]
    private bool enableSaveAction = false;


    [RelayCommand]
    private void OnSave()
    {
        //TODO: Save new secret

        EnableSaveAction = false;
    }
}
