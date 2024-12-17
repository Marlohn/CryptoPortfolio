using Domain.Entities;
using Domain.Interfaces;

namespace Application.Services
{
    public class PortfolioService
    {
        private readonly IInvestmentRepository _investmentRepository;

        public PortfolioService(IInvestmentRepository investmentRepository)
        {
            _investmentRepository = investmentRepository;
        }

        public List<Portfolio> ConsolidatePortfolio(Dictionary<string, decimal> currentValues)
        {
            var investments = _investmentRepository.GetAll();

            var portfolio = investments
                .GroupBy(i => i.CryptoName)
                .Select(g => new Portfolio
                {
                    CryptoName = g.Key,
                    TotalInvested = g.Sum(i => i.InvestedValue),
                    CurrentValue = currentValues.ContainsKey(g.Key) ? currentValues[g.Key] : 0,
                    Profit = (currentValues.ContainsKey(g.Key) ? currentValues[g.Key] : 0) - g.Sum(i => i.InvestedValue)
                })
                .ToList();

            foreach (var item in portfolio)
            {
                item.ProfitPercentage = item.TotalInvested > 0
                    ? (item.Profit / item.TotalInvested) * 100
                    : 0;
            }

            return portfolio;
        }
    }
}
