using Domain.Entities;
using Domain.Interfaces;

namespace Infrastructure.Repositories.Data
{
    internal class SupabaseCryptoStatusRepository : ICryptoStatusRepository
    {
        public List<CryptoStatus> GetAll()
        {
            throw new NotImplementedException();
        }

        public void Upsert(CryptoStatus investment)
        {
            throw new NotImplementedException();
        }

        public void Delete(string cryptoName)
        {
            throw new NotImplementedException();
        }

        public void Backup()
        {
            throw new NotImplementedException();
        }
    }
}