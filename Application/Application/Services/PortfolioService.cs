using Application.Interfaces;
using Application.Models;

public class PortfolioService : IPortfolioService
{
    private readonly IInvestmentService _investmentService;
    private PortfolioDto _portfolio;

    public PortfolioService(IInvestmentService investmentService)
    {
        _investmentService = investmentService;
        _portfolio = ConsolidatePortfolio();
    }

    public PortfolioDto GetPortfolio()
    {
        return _portfolio;
    }

    public void UpdateCrypto(string cryptoName, decimal currentValue)
    {
        var crypto = _portfolio.Cryptos.SingleOrDefault(x => x.CryptoName == cryptoName);

        if (crypto == null)
            throw new ArgumentException($"Crypto '{cryptoName}' not found.");

        crypto.CurrentValue = currentValue;
        crypto.Profit = crypto.CurrentValue - crypto.TotalInvested;

        if (crypto.TotalInvested > 0)
        {
            crypto.ProfitPercentage = (crypto.Profit / crypto.TotalInvested) * 100;
        }
    }

    private PortfolioDto ConsolidatePortfolio()
    {
        var investments = _investmentService.GetAllInvestments();

        if (investments == null || !investments.Any())
            throw new InvalidOperationException("No investments found to consolidate.");

        var cryptos = investments
            .GroupBy(i => i.CryptoName)
            .Select(group => new CryptoDto
            {
                CryptoName = group.Key,
                TotalInvested = group.Sum(i => i.InvestedValue),
                CurrentValue = 0,
                Profit = 0,
                ProfitPercentage = 0,
                Risk = string.Empty //group.FirstOrDefault()?.Risk ?? string.Empty
            })
            .ToList();

        return new PortfolioDto
        {
            Cryptos = cryptos
        };
    }
}
