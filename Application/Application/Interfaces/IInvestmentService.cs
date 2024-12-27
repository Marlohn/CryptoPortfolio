using Application.Models;

namespace Application.Interfaces
{
    public interface IInvestmentService
    {
        void Add(InvestmentDto investment);
        void Delete(string cryptoName);
        List<InvestmentDto> GetAll();
    }
}