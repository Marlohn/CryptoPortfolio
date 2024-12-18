using Application.Models;

namespace Application.Interfaces
{
    public interface IInvestmentService
    {
        void AddInvestment(InvestmentDto investment);
        List<InvestmentDto> GetAllInvestments();
    }
}