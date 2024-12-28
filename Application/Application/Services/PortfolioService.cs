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
        return ConsolidatePortfolio();
    }

    public void UpdateCrypto(CryptoStatusDto cryptoStatusDto)
    {
        _cryptoStatusService.Upsert(cryptoStatusDto);
    }

    public void BackupData()
    {
        _cryptoStatusService.Backup();
        _investmentService.Backup();
    }

    private PortfolioDto ConsolidatePortfolio()
    {
        // Retrieve all investments and cryptocurrency status
        var investments = _investmentService.GetAll();
        var cryptoStatusList = _cryptoStatusService.GetAll();

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
                var profitPercentage = netInvested > 0 ? (profit / netInvested) * 100 : 0;
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
        var totalCurrent = cryptos.Sum(c => c.CurrentValue);
        var totalProfit = cryptos.Sum(c => c.Profit);
        var totalProfitPercentage = totalInvested > 0 ? ((totalProfit / totalInvested) * 100) : 0;

        // Return the portfolio DTO with the calculated values
        return new PortfolioDto
        {
            Cryptos = cryptos,
            TotalInvested = totalInvested,
            TotalCurrent = totalCurrent ?? 0, // check this
            TotalProfit = totalProfit ?? 0, // Ensure total profit is not null
            TotalProfitPercentage = totalProfitPercentage ?? 0 // check this
        };
    }
}