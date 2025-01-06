using Application.Models;

namespace Application.Interfaces
{
    public interface IInvestmentsService
    {
        Task Add(InvestmentDto investment);
        Task Delete(string cryptoName);
        Task<List<InvestmentDto>> GetAll();
        Task Backup();
    }
}