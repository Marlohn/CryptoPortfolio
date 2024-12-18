using Application.Interfaces;
using Application.Models;

namespace Application.Services
{
    public class PortfolioService : IPortfolioService
    {
        private readonly IInvestmentService _investmentService;

        public PortfolioService(IInvestmentService investmentService)
        {
            _investmentService = investmentService;
        }

        public List<PortfolioDto> ConsolidatePortfolio()
        {
            var investments = _investmentService.GetAllInvestments();

            if (investments == null || !investments.Any())
                throw new InvalidOperationException("No investments found to consolidate.");

            var consolidatedByCrypto = investments
                .GroupBy(i => i.CryptoName)
                .Select(group => new PortfolioDto
                {
                    CryptoName = group.Key,
                    TotalInvested = group.Sum(i => i.InvestedValue),
                    CurrentValue = 0,
                    Profit = 0,
                    ProfitPercentage = 0,
                    Risk = string.Empty
                })
                .OrderByDescending(x => x.TotalInvested)
                .ToList();

            return consolidatedByCrypto;
        }
    }
}