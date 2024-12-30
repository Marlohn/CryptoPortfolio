using Application.Models;

namespace Application.Interfaces
{
    public interface IBinanceService
    {
        Task<List<CryptoStatusDto>> GetExchangeData();
    }
}