using Application.Models;

namespace Application.Interfaces
{
    public interface IPortfolioService
    {
        Task<PortfolioDto> GetPortfolio();
        Task UpdateCrypto(CryptoStatusDto cryptoStatusDto);
        Task BackupData();
        Task RefreshBinanceData();
    }
}