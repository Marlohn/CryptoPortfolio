using Application.Models;

namespace Application.Interfaces
{
    public interface IInvestmentsService
    {
        void Add(InvestmentDto investment);        
        void Delete(string cryptoName);
        List<InvestmentDto> GetAll();
        void Backup();
    }
}