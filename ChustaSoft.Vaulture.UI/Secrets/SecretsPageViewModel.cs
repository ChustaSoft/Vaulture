﻿using ChustaSoft.Vaulture.Application.Secrets;
using ChustaSoft.Vaulture.Application.Settings;
using ChustaSoft.Vaulture.Domain.Secrets;
using ChustaSoft.Vaulture.UI.Common;
using System.Collections.Concurrent;
using System.Collections.ObjectModel;
using System.Windows.Media;
using Wpf.Ui.Controls;

namespace ChustaSoft.Vaulture.UI.Secrets;


public partial class SecretsPageViewModel : ObservableObject
{

    private readonly IAppSettingsService _appSettingsService;
    private readonly ISecretsService _secretsService;
    private readonly INavigationService _navigationService;

    public ISnackbarService SnackbarService { get; set; }
    public IContentDialogService DialogService { get; set; }


    public SecretsPageViewModel(IAppSettingsService appSettingsService, ISecretsService secretsService, INavigationService navigationService, ISnackbarService snackbarService, IContentDialogService dialogService)
    {
        _appSettingsService = appSettingsService;
        _secretsService = secretsService;
        _navigationService = navigationService;

        SnackbarService = snackbarService;
        DialogService = dialogService;
    }


    [ObservableProperty]
    private ObservableCollection<SecureConnectionSecretsViewModel> secureConnections = new ObservableCollection<SecureConnectionSecretsViewModel>();


    [RelayCommand]
    public async Task OnLoadAsync()
    {
        var secureConnections = await _appSettingsService.GetConnectionsAsync();
        var connectionsSecretsQueryTasks = new List<Task>();
        var connectionsSecrets = new ConcurrentBag<SecureConnectionSecretsViewModel>();

        foreach (var secureConnectionTypes in secureConnections)
            foreach (var secureConnection in secureConnectionTypes.Value)
                connectionsSecretsQueryTasks.Add(RetrieveSecrets(secureConnectionTypes.Key, connectionsSecrets, secureConnection!));

        await Task.WhenAll(connectionsSecretsQueryTasks);

        SecureConnections = new ObservableCollection<SecureConnectionSecretsViewModel>(connectionsSecrets);
    }

    [RelayCommand]
    private async Task OnViewSecret(SecretActionRequestModel request)
    {
        await OpenSecretDetailPage(request, PageMode.View);
    }

    [RelayCommand]
    private async Task OnEditSecret(SecretActionRequestModel request)
    {
        await OpenSecretDetailPage(request, PageMode.Edit);
    }

    [RelayCommand]
    private async Task OnDeleteSecret(SecretActionRequestModel request)
    {
        var confirmationDialog = ShowConfirmationDialog();
        var dialogResult = await confirmationDialog.ShowDialogAsync();

        if (dialogResult == Wpf.Ui.Controls.MessageBoxResult.Primary)
        {
            await _secretsService.DeleteAsync(request.ResourceType, request.SecretConnection, request.SecretDto.Name);
            SecureConnections.First(x => x.ConnectionValue.Value == request.SecretConnection).RemoveSecret(request.SecretDto.Name);

            SnackbarService.Show("Secret removed", $"Secret {request.SecretDto.Name} has been properly removed from {request.SecretConnection}", ControlAppearance.Info, new SymbolIcon(SymbolRegular.Fluent24), TimeSpan.FromSeconds(3));
        }

        await Task.CompletedTask;
    }


    private static Wpf.Ui.Controls.MessageBox ShowConfirmationDialog()
    {
        return new Wpf.Ui.Controls.MessageBox
        {
            Title = "Are you sure?",
            Content = "Delete Confirmation",
            PrimaryButtonText = "Yes",
            CloseButtonText = "No",
            WindowStartupLocation = WindowStartupLocation.CenterOwner,
            PrimaryButtonAppearance = Wpf.Ui.Controls.ControlAppearance.Danger
        };
    }

    private async Task RetrieveSecrets(SecretsResourceType resourceType, ConcurrentBag<SecureConnectionSecretsViewModel> connectionsSecrets, SecureConnectionValue secureConnection)
    {
        var secrets = await _secretsService.GetAllSummariesAsync(resourceType, secureConnection.Value);
        connectionsSecrets.Add(new SecureConnectionSecretsViewModel(secureConnection, secrets));
    }

    private async Task OpenSecretDetailPage(SecretActionRequestModel request, PageMode pageMode)
    {
        //TODO: Here we should create different type of credentials, and based on a converter, navigate to one or another control, now is forced to Credential
        var secret = await _secretsService.GetAsync(request.ResourceType, request.SecretConnection, request.SecretDto.Name);
        var viewModel = new SecretPageViewModel(pageMode, (CredentialDto)secret);

        _navigationService.Navigate(typeof(SecretPage), viewModel);
    }
}
