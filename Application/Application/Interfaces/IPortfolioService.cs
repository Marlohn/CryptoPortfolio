using Application.Models;
using Domain.Entities;

namespace Application.Interfaces
{
    public interface IPortfolioService
    {
        List<PortfolioDto> ConsolidatePortfolio();
    }
}
