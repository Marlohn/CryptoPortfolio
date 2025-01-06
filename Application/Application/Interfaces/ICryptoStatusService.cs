using Application.Models;

namespace Application.Interfaces
{
    public interface ICryptoStatusService
    {
        Task Upsert(CryptoStatusDto cryptoStatus);
        Task Delete(string cryptoStatus);
        Task<List<CryptoStatusDto>> GetAll();
        Task<CryptoStatusDto?> GetByName(string cryptoName);
        Task Backup();
    }
}