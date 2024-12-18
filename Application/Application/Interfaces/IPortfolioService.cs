using Domain.Entities;

namespace Application.Interfaces
{
    public interface IPortfolioService
    {
        List<Portfolio> ConsolidatePortfolio(Dictionary<string, decimal> currentValues);
    }
}
