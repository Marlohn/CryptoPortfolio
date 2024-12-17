using IoC;
using Microsoft.Extensions.DependencyInjection;

namespace CryptoPortfolio.WinForms
{
    internal static class Program
    {
        ///// <summary>
        /////  The main entry point for the application.
        ///// </summary>
        //[STAThread]
        //static void Main()
        //{
        //    // To customize application configuration such as set high DPI settings or default font,
        //    // see https://aka.ms/applicationconfiguration.
        //    ApplicationConfiguration.Initialize();
        //    Application.Run(new Main());
        //}

        [STAThread]
        static void Main()
        {
            var serviceProvider = ConfigureServices();

            ApplicationConfiguration.Initialize();
            var mainForm = serviceProvider.GetRequiredService<Main>();
            System.Windows.Forms.Application.Run(mainForm);
        }

        private static ServiceProvider ConfigureServices()
        {
            var services = new ServiceCollection();

            // Chama o método de configuração IoC da Infrastructure
            services.AddInfrastructure();

            // Registra a Main Form para permitir injeção de dependências
            services.AddTransient<Main>();

            return services.BuildServiceProvider();
        }
    }
}