using Application.Interfaces;
using Domain.Entities;
using Domain.Interfaces;

namespace Application.Services
{
    public class InvestmentService : IInvestmentService
    {
        private readonly IInvestmentRepository _investmentRepository;

        public InvestmentService(IInvestmentRepository investmentRepository)
        {
            _investmentRepository = investmentRepository;
        }

        public void AddInvestment(Investment investment)
        {
            if (investment == null)
                throw new ArgumentNullException(nameof(investment));

            if (investment.InvestedValue <= 0)
                throw new ArgumentException("Invested value must be greater than zero.");

            _investmentRepository.Add(investment);
        }

        public List<Investment> GetAllInvestments()
        {
            return _investmentRepository.GetAll();
        }
    }
}
