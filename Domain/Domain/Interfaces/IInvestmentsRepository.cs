using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IInvestmentsRepository
    {
        Task Add(Investment investment);
        Task Delete(string cryptoName);
        Task<List<Investment>> GetAll();
        Task Backup();
    }
}