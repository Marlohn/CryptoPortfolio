using Application.Models;

namespace Application.Interfaces
{
    public interface IPortfolioService
    {
        PortfolioDto GetPortfolio();
        void UpdateCrypto(CryptoStatusDto cryptoStatusDto);
        void BackupData();
    }
}