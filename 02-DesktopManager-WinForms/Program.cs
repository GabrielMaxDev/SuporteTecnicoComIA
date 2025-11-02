using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.IO;
using System.Windows.Forms;
using DesktopManager.Services;
using DesktopManager.Utils;
using DesktopManager.Forms;

namespace DesktopManager
{
    static class Program
    {
        public static IServiceProvider ServiceProvider { get; private set; } = null!;

        [STAThread]
        static void Main()
        {
            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            var host = CreateHostBuilder().Build();
            ServiceProvider = host.Services;

            Application.Run(ServiceProvider.GetRequiredService<FormLogin>());
        }

        static IHostBuilder CreateHostBuilder()
        {
            return Host.CreateDefaultBuilder()
            .ConfigureAppConfiguration((context, builder) =>
            {
                builder.SetBasePath(Directory.GetCurrentDirectory())
                        .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
            })
            .ConfigureServices((context, services) =>
            {
                // Registra o SessionManager como "Singleton" (só um para o app todo)
                services.AddSingleton<SessionManager>();

                // Registra o ApiService usando a HttpClientFactory (melhor prática)
                services.AddHttpClient<ApiService>(client =>
                {
                    string? apiUrl = context.Configuration.GetValue<string>("ApiSettings:BaseUrl");

                    if (string.IsNullOrEmpty(apiUrl))
                    {
                        throw new InvalidOperationException(
                            "A 'ApiSettings:BaseUrl' não foi encontrada ou está vazia no appsettings.json.");
                    }
                    client.BaseAddress = new Uri(apiUrl);
                });
                services.AddTransient<FormLogin>();
                services.AddTransient<FormPrincipal>();
            });
        }
    }
}