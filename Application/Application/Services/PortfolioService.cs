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
        // Retrieve all investments and cryptocurrency status
        var investments = _investmentService.GetAllInvestments();
        var cryptoStatusList = _cryptoStatusService.GetAllCryptoStatus();

        // Throw an exception if no investments are found
        if (investments == null || !investments.Any())
            throw new InvalidOperationException("No investments found to consolidate.");

        // Consolidate the investments by cryptocurrency
        var cryptos = investments
            .GroupBy(i => i.CryptoName) // Group investments by cryptocurrency name
            .Select(group =>
            {
                var cryptoName = group.Key;

                // Calculate the net invested value (sum of positive and negative invested values)
                var netInvested = group.Sum(i => i.InvestedValue);

                //// Calculate the total withdrawn (negative values of InvestedValue)
                //var totalWithdrawn = group.Where(i => i.InvestedValue < 0).Sum(i => Math.Abs(i.InvestedValue));

                // Try to find the current status of the cryptocurrency from the CSV file
                var status = cryptoStatusList.SingleOrDefault(cs => cs.CryptoName == cryptoName);

                // Use the current value from the CSV as the base value
                var currentValue = status?.CurrentValue ?? 0;

                // Adjust the current value by subtracting the withdrawn amount
                //currentValue -= totalWithdrawn;

                //// Ensure the current value is not zero or negative
                //if (currentValue <= 0)
                //{
                //    currentValue = 0;  // If it's zero or negative, set it to 0
                //}

                // Calculate profit or loss based on the adjusted current value
                var profit = currentValue - netInvested;

                // Calculate profit percentage only if there was an investment
                var profitPercentage = netInvested > 0 ? (profit / netInvested) * 100 : (decimal?)null;
                //var profitPercentage = netInvested > 0 ? ((profit / netInvested) - 1) * 100 : 0;

                // Return a CryptoDto object with the calculated values
                return new CryptoDto()
                {
                    CryptoName = cryptoName,
                    TotalInvested = netInvested, // Displays the net invested value
                    CurrentValue = currentValue,
                    Profit = profit,
                    ProfitPercentage = profitPercentage,
                    Risk = status?.Risk ?? string.Empty // If no risk info, set it as empty
                };
            })
            .OrderByDescending(x => x.TotalInvested) // Order by total invested value in descending order
            .ToList();

        // Sum the total invested and total profit across all cryptocurrencies
        var totalInvested = cryptos.Sum(c => c.TotalInvested);
        var totalProfit = cryptos.Sum(c => c.Profit);

        // Return the portfolio DTO with the calculated values
        return new PortfolioDto
        {
            Cryptos = cryptos,
            TotalInvested = totalInvested,
            TotalProfit = totalProfit ?? 0 // Ensure total profit is not null
        };
    }







}