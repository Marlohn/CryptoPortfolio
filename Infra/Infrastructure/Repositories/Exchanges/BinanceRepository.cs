using Binance.Net.Interfaces.Clients;
using Domain.Entities;
using Domain.Interfaces;

namespace Infrastructure.Repositories.Exchanges
{
    public class BinanceRepository : IExchangeRepository
    {
        private readonly IBinanceRestClient _binanceRestClient;

        public BinanceRepository(IBinanceRestClient binanceRestClient)
        {
            _binanceRestClient = binanceRestClient;
        }

        public async Task<List<ExchangeBalance>> GetBalances()
        {
            var result = await _binanceRestClient.SpotApi.Account.GetBalancesAsync();

            if (!result.Success || result.Data == null)
                throw new Exception($"Failed to retrieve balances: {result.Error?.Message}");

            return result.Data
                .Select(b => new ExchangeBalance
                {
                    Asset = b.Asset,
                    Ammount = b.Total
                })
                .ToList();
        }

        public async Task<List<ExchangeCryptoPrice>> GetPrices()
        {
            var result = await _binanceRestClient.UsdFuturesApi.ExchangeData.GetBookPricesAsync();

            if (!result.Success || result.Data == null)
                throw new Exception($"Failed to retrieve prices: {result.Error?.Message}");

            return result.Data
                .Select(b => new ExchangeCryptoPrice
                {
                    Symbol = b.Symbol,
                    Price = (b.BestBidPrice + b.BestAskPrice) / 2
                })
                .ToList();
        }
    }
}
