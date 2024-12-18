using Domain.Entities;

namespace Domain.Interfaces
{
    public interface ICryptoStatusRepository
    {
        void Upsert(CryptoStatus investment);
        List<CryptoStatus> GetAll();
    }
}
