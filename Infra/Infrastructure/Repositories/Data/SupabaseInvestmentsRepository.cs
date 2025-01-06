using Domain.Entities;
using Domain.Interfaces;

namespace Infrastructure.Repositories.Data
{
    internal class SupabaseInvestmentsRepository : IInvestmentsRepository
    {
        public List<Investment> GetAll()
        {
            throw new NotImplementedException();
        }

        public void Add(Investment investment)
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
