using Domain.Entities;

namespace Application.Interfaces
{
    public interface IInvestmentService
    {
        void AddInvestment(Investment investment);
        List<Investment> GetAllInvestments();
    }
}