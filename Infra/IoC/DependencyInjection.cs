using Application.Interfaces;
using Application.Services;
using Domain.Interfaces;
using Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace IoC
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            //Domain
            services.AddScoped<IInvestmentRepository, InvestmentRepository>();
            services.AddScoped<IInvestmentRepository>(provider => new InvestmentRepository("investments.csv")); // change it 
            services.AddScoped<ICryptoStatusRepository>(provider => new CryptoStatusRepository("cryptostatus.csv")); // change it 
            services.AddScoped<IExchangeRepository, BinanceRepository>();

            //Application
            services.AddScoped<IInvestmentService, InvestmentService>();
            services.AddScoped<IPortfolioService, PortfolioService>();
            services.AddScoped<ICryptoStatusService, CryptoStatusService>();
            services.AddScoped<IBinanceService, BinanceService>();

            return services;
        }
    }
}