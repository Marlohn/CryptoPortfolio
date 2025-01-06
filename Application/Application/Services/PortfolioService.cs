using Application.Interfaces;
using Application.Models;
using Application.Models.Enums;
using Domain.Interfaces;
using UtilityExtensions;

public class PortfolioService : IPortfolioService
{
    private readonly IInvestmentsService _investmentService;
    private readonly ICryptoStatusService _cryptoStatusService;
    private readonly IBinanceService _binanceService;
    private readonly IExchangeRepository _exchangeRepository;

    public PortfolioService(IInvestmentsService investmentService, ICryptoStatusService cryptoStatusService, IBinanceService binanceService, IExchangeRepository exchangeRepository)
    {
        _investmentService = investmentService;
        _cryptoStatusService = cryptoStatusService;
        _binanceService = binanceService;
        _exchangeRepository = exchangeRepository;
    }

    public async Task<PortfolioDto> GetPortfolio()
    {
        return await ConsolidatePortfolio();
    }

    public async Task UpdateCrypto(CryptoStatusDto cryptoStatusDto)
    {
        await _cryptoStatusService.Upsert(cryptoStatusDto);
    }

    public async Task BackupData()
    {
        await _cryptoStatusService.Backup();
        await _investmentService.Backup();
    }

    public async Task RefreshBinanceData()
    {
        //_exchangeRepository.
        var cryptoStatusList = await _binanceService.GetExchangeData();

        foreach (var cryptoStatus in cryptoStatusList) 
        {
            //Update only existing cryptos and also we need the required risk to update it
            var currentCryptoStatus = await _cryptoStatusService.GetByName(cryptoStatus.CryptoName);
            if (currentCryptoStatus != null)
            {
                cryptoStatus.Risk = currentCryptoStatus.Risk;

                await _cryptoStatusService.Upsert(cryptoStatus);
            }
            
        }
    }

    private async Task<PortfolioDto> ConsolidatePortfolio()
    {
        // Retrieve all investments and cryptocurrency status
        var investments = await _investmentService.GetAll();
        var cryptoStatusList = await _cryptoStatusService.GetAll();

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
                    Risk = (status?.Risk ?? string.Empty).ToEnum<RiskLevel>() // If no risk info error
                };
            })
            .OrderByDescending(x => x.TotalInvested) // Order by total invested value in descending order
            .ToList();

        int rank = 1;
        cryptos.ForEach(c => c.Rank = rank++);

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