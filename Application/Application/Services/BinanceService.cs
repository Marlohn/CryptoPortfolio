using Application.Interfaces;
using Application.Models;
using Domain.Interfaces;

namespace Application.Services
{
    public class BinanceService : IBinanceService
    {
        private readonly IExchangeRepository _exchangeRepository;

        public BinanceService(IExchangeRepository exchangeRepository)
        {
            _exchangeRepository = exchangeRepository;
        }

        public async Task<List<CryptoStatusDto>> GetExchangeData()
        {
            var cryptoStatusList = new List<CryptoStatusDto>();

            var prices = await _exchangeRepository.GetPrices();
            var balances = await _exchangeRepository.GetBalances();

            foreach (var balance in balances)
            {
                var pairName = $"{balance.Asset}USDT";
                var matchingPrice = prices.FirstOrDefault(p => p.Symbol == pairName);

                if (matchingPrice != null)
                {
                    var total = Math.Round(balance.Ammount * matchingPrice.Price, 2);
                    cryptoStatusList.Add(new CryptoStatusDto()
                    {
                        CryptoName = balance.Asset,
                        CurrentValue = total
                    });
                }
            }

            return cryptoStatusList;
        }
    }
}