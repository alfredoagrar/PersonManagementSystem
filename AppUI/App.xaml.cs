using AppUI.Services.Interfaces;
using AppUI.Utils.Service;
using Microsoft.Extensions.DependencyInjection;
using System.Windows;

namespace AppUI
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private readonly ServiceProvider _serviceProvider;

        public App()
        {
            var services = new ServiceCollection();
            services.AddTransient<IHttpClientService, HttpClientService>();
            services.AddTransient<MainWindow>();
            _serviceProvider = services.BuildServiceProvider();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            var serviceProvider = new ServiceCollection()
                .AddTransient<IHttpClientService, HttpClientService>()
                .AddTransient<MainWindow>() // Asegúrate de registrar MainWindow si tiene un constructor no predeterminado
                .BuildServiceProvider();

            var mainWindow = serviceProvider.GetService<MainWindow>();
            mainWindow.Show();
        }

    }
}