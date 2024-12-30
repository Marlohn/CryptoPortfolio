using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IExchangeRepository
    {
        Task<List<ExchangeBalance>> GetBalances();
        Task<List<ExchangeCryptoPrice>> GetPrices();
    }
}