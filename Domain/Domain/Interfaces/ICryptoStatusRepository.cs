using Domain.Entities;

namespace Domain.Interfaces
{
    public interface ICryptoStatusRepository
    {
        void Upsert(CryptoStatus investment);
        public void Delete(string cryptoName);
        List<CryptoStatus> GetAll();
    }
}
