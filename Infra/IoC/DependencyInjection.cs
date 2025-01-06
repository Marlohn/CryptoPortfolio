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
            services.AddScoped<IInvestmentRepository, CsvInvestmentRepository>();
            services.AddScoped<IInvestmentRepository>(provider => new CsvInvestmentRepository("investments.csv")); // change it 
            services.AddScoped<ICryptoStatusRepository>(provider => new CsvCryptoStatusRepository("cryptostatus.csv")); // change it 
            services.AddScoped<IExchangeRepository, BinanceRepository>();
            services.AddSingleton<IBinanceRestClient>(provider => new BinanceRestClient(options =>
            {
                //ToDo: it should be a setting at some point
                options.ApiCredentials = new ApiCredentials("3zC3rGReD5jNRC5cnEmUxf66JAUo3n0QN3h8Xz3bbQGbsw2iXC1U3zvmV2gajnRS", "HuCjx8CLNTFiOJUSVUrLWjyKEpOBbnKBxysrvSioMCgNkDYU1COBu1D5ErxEMHgR");
            }));

            //Application
            services.AddScoped<IInvestmentService, InvestmentService>();
            services.AddScoped<IPortfolioService, PortfolioService>();
            services.AddScoped<ICryptoStatusService, CryptoStatusService>();
            services.AddScoped<IBinanceService, BinanceService>();

            return services;
        }
    }
}