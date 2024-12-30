using Binance.Net.Clients;
using CryptoExchange.Net.Authentication;
using Domain.Entities;
using Domain.Interfaces;

namespace Infrastructure.Repositories
{
    public class BinanceRepository : IExchangeRepository
    {
        public async Task<List<ExchangeBalance>> GetBalances()
        {
            var binanceClient = new BinanceRestClient(options =>
            {
                options.ApiCredentials = new ApiCredentials("3zC3rGReD5jNRC5cnEmUxf66JAUo3n0QN3h8Xz3bbQGbsw2iXC1U3zvmV2gajnRS", "HuCjx8CLNTFiOJUSVUrLWjyKEpOBbnKBxysrvSioMCgNkDYU1COBu1D5ErxEMHgR");
            });

            var result = await binanceClient.SpotApi.Account.GetBalancesAsync();

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
            var binanceClient = new BinanceRestClient(options =>
            {
                options.ApiCredentials = new ApiCredentials("3zC3rGReD5jNRC5cnEmUxf66JAUo3n0QN3h8Xz3bbQGbsw2iXC1U3zvmV2gajnRS", "HuCjx8CLNTFiOJUSVUrLWjyKEpOBbnKBxysrvSioMCgNkDYU1COBu1D5ErxEMHgR");
            });

            var result = await binanceClient.UsdFuturesApi.ExchangeData.GetBookPricesAsync();

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