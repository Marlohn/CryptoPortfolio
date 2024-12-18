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
            services.AddScoped<IInvestmentRepository, CsvInvestmentRepository>();

            services.AddScoped<IInvestmentService, InvestmentService>();

            services.AddScoped<IPortfolioService, PortfolioService>();

            services.AddScoped<IInvestmentRepository>(provider => new CsvInvestmentRepository("investments.csv")); // change it           

            return services;
        }
    }
}