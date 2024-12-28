using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IInvestmentRepository
    {
        void Add(Investment investment);        
        void Delete(string cryptoName);
        List<Investment> GetAll();
        void Backup();
    }
}