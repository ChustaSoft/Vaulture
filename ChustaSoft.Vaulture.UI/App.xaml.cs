using ChustaSoft.Vaulture.Application.Settings;
using ChustaSoft.Vaulture.Domain.Settings;
using ChustaSoft.Vaulture.LocalSystem.Settings;
using ChustaSoft.Vaulture.UI.Pages;
using ChustaSoft.Vaulture.UI.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.IO;
using System.Windows.Threading;

namespace ChustaSoft.Vaulture.UI;


public partial class App
{

    private static readonly IHost _host = Host.CreateDefaultBuilder()
        .ConfigureAppConfiguration(c =>
        {
            var basePath = Path.GetDirectoryName(AppContext.BaseDirectory)
                ?? throw new DirectoryNotFoundException("Unable to find the base directory of the application.");

            _ = c.SetBasePath(basePath);
        })
        .ConfigureServices(
            (context, services) =>
            {
                _ = services.AddHostedService<ApplicationHostService>();

                _ = services.AddSingleton<IPageService, PageService>();
                _ = services.AddSingleton<IThemeService, ThemeService>();
                _ = services.AddSingleton<ITaskBarService, TaskBarService>();
                _ = services.AddSingleton<INavigationService, NavigationService>();

                _ = services.AddSingleton<IThemeManager, ThemeManager>();

                _ = services.AddSingleton<INavigationWindow, MainWindow>();
                _ = services.AddSingleton<MainWindowViewModel>();

                _ = services.AddSingleton<DashboardPage>();
                _ = services.AddSingleton<DashboardPageViewModel>();
                _ = services.AddSingleton<SettingsPage>();
                _ = services.AddSingleton<SettingsPageViewModel>();

                _ = services.AddScoped<IAppSettingsService, AppSettingsService>();

                _ = services.AddScoped<IAppSettingsStorage, AppSettingsFileStorage>();
            }
        )
        .Build();


    private async void OnStartup(object sender, StartupEventArgs e)
    {
        await _host.StartAsync();

        await LoadAndApplySettings();
    }

    private static async Task LoadAndApplySettings()
    {
        using (var scope = _host.Services.CreateAsyncScope())
        {
            var settingsStorage = _host.Services.GetRequiredService<IAppSettingsStorage>();
            var themeManager = _host.Services.GetRequiredService<IThemeManager>();

            var settings = await settingsStorage.LoadAsync();
            themeManager.Apply(settings.Theme);
        }
    }

    private async void OnExit(object sender, ExitEventArgs e)
    {
        await _host.StopAsync();

        _host.Dispose();
    }

    private void OnDispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e) { }
}