using Application.Interfaces;
using Application.Services;
using Binance.Net.Clients;
using Binance.Net.Interfaces.Clients;
using CryptoExchange.Net.Authentication;
using Domain.Interfaces;
using Infrastructure.Repositories.Data;
using Infrastructure.Repositories.Exchanges;
using Microsoft.Extensions.DependencyInjection;

namespace IoC
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            //Domain
            //services.AddScoped<IInvestmentsRepository>(provider => new CsvInvestmentsRepository("investments.csv")); // change it 
            //services.AddScoped<ICryptoStatusRepository>(provider => new CsvCryptoStatusRepository("cryptostatus.csv")); // change it 

            services.AddSingleton<IInvestmentsRepository>(provider => new SupabaseInvestmentsRepository(
                new Supabase.Client("https://lgnimnzxhdgcguenpfii.supabase.co", "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpc3MiOiJzdXBhYmFzZSIsInJlZiI6Imxnbmltbnp4aGRnY2d1ZW5wZmlpIiwicm9sZSI6ImFub24iLCJpYXQiOjE3MzYxNzAzNTUsImV4cCI6MjA1MTc0NjM1NX0.eEp4VXJgXDneF1bwBvUlRc_YalGwDxpmohuT4XtxpF0")
            ));

            services.AddSingleton<ICryptoStatusRepository>(provider => new SupabaseCryptoStatusRepository(
                new Supabase.Client("https://lgnimnzxhdgcguenpfii.supabase.co", "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpc3MiOiJzdXBhYmFzZSIsInJlZiI6Imxnbmltbnp4aGRnY2d1ZW5wZmlpIiwicm9sZSI6ImFub24iLCJpYXQiOjE3MzYxNzAzNTUsImV4cCI6MjA1MTc0NjM1NX0.eEp4VXJgXDneF1bwBvUlRc_YalGwDxpmohuT4XtxpF0")
            ));

            services.AddScoped<IExchangeRepository, BinanceRepository>();
            services.AddSingleton<IBinanceRestClient>(provider => new BinanceRestClient(options =>
            {
                //ToDo: it should be a setting at some point
                options.ApiCredentials = new ApiCredentials("3zC3rGReD5jNRC5cnEmUxf66JAUo3n0QN3h8Xz3bbQGbsw2iXC1U3zvmV2gajnRS", "HuCjx8CLNTFiOJUSVUrLWjyKEpOBbnKBxysrvSioMCgNkDYU1COBu1D5ErxEMHgR");
            }));

            //Application
            services.AddScoped<IInvestmentsService, InvestmentsService>();
            services.AddScoped<IPortfolioService, PortfolioService>();
            services.AddScoped<ICryptoStatusService, CryptoStatusService>();
            services.AddScoped<IBinanceService, BinanceService>();

            return services;
        }
    }
}