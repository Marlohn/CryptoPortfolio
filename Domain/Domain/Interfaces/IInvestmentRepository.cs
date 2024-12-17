using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IInvestmentRepository
    {
        void Add(Investment investment);
        List<Investment> GetAll();
    }
}