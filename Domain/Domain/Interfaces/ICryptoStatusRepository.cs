using Domain.Entities;

namespace Domain.Interfaces
{
    public interface ICryptoStatusRepository
    {
        Task Upsert(CryptoStatus investment);
        Task Delete(string cryptoName);
        Task<List<CryptoStatus>> GetAll();
        Task Backup();
    }
}
