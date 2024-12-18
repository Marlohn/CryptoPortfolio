using Application.Interfaces;
using Application.Models;

public class PortfolioService : IPortfolioService
{
    private readonly IInvestmentService _investmentService;
    private readonly ICryptoStatusService _cryptoStatusService;

    public PortfolioService(IInvestmentService investmentService, ICryptoStatusService cryptoStatusService)
    {
        _investmentService = investmentService;
        _cryptoStatusService = cryptoStatusService;
    }

    public PortfolioDto GetPortfolio()
    {
        //return _portfolio;

        return ConsolidatePortfolio();
    }

    public void UpdateCrypto(CryptoStatusDto cryptoStatusDto)
    {
        _cryptoStatusService.UpsertCryptoStatus(cryptoStatusDto);

        ////_portfolioService.UpdateCrypto(cryptoName, currentValue);

        //var crypto = _portfolio.Cryptos.SingleOrDefault(x => x.CryptoName == cryptoName);

        //if (crypto == null)
        //    throw new ArgumentException($"Crypto '{cryptoName}' not found.");

        //crypto.CurrentValue = currentValue;
        //crypto.Profit = crypto.CurrentValue - crypto.TotalInvested;

        //if (crypto.TotalInvested > 0)
        //{
        //    crypto.ProfitPercentage = (crypto.Profit / crypto.TotalInvested) * 100;
        //}
    }

    private PortfolioDto ConsolidatePortfolio()
    {
        var investments = _investmentService.GetAllInvestments();
        var cryptoStatusList = _cryptoStatusService.GetAllCryptoStatus();

        if (investments == null || !investments.Any())
            throw new InvalidOperationException("No investments found to consolidate.");

        // Consolida os investimentos por criptomoeda
        var cryptos = investments
            .GroupBy(i => i.CryptoName)
            .Select(group =>
            {
                var cryptoName = group.Key;
                var totalInvested = group.Sum(i => i.InvestedValue);

                // Tenta encontrar o status atual da criptomoeda no arquivo CSV
                var status = cryptoStatusList.SingleOrDefault(cs => cs.CryptoName == cryptoName);

                var currentValue = status?.CurrentValue ?? null;
                var risk = status?.Risk ?? "Unknown";
                var profit = currentValue - totalInvested;
                var profitPercentage = totalInvested > 0 ? (profit / totalInvested) * 100 : null;

                return new CryptoDto()
                {
                    CryptoName = cryptoName,
                    TotalInvested = totalInvested,
                    CurrentValue = currentValue,
                    Profit = profit,
                    ProfitPercentage = profitPercentage,
                    Risk = risk
                };
            })
            .OrderByDescending(x => x.TotalInvested)
            .ToList();

        var totalInvested = cryptos.Sum(c => c.TotalInvested);

        return new PortfolioDto
        {
            Cryptos = cryptos,
            TotalInvested = totalInvested
        };
    }
}