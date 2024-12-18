using System.ComponentModel.DataAnnotations;
using Application.Interfaces;
using Application.Models;
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
        public List<InvestmentDto> GetAllInvestments()
        {
            var investments = _investmentRepository.GetAll();

            var investmentDtos = investments.Select(investment => new InvestmentDto
            {
                Date = investment.Date,
                CryptoName = investment.CryptoName,
                InvestedValue = investment.InvestedValue,
                Notes = investment.Notes
            }).ToList();

            return investmentDtos;
        }


        public void AddInvestment(InvestmentDto investmentDto)
        {
            var validationResults = new List<ValidationResult>();
            var context = new ValidationContext(investmentDto);

            if (!Validator.TryValidateObject(investmentDto, context, validationResults, true))
            {
                throw new ValidationException("DTO validation failed.");
            }

            _investmentRepository.Add(new Investment()  // need to create a better map here
            {
                Date = investmentDto.Date,
                CryptoName = investmentDto.CryptoName,
                InvestedValue = investmentDto.InvestedValue,
                Notes = investmentDto.Notes
            });
        }
    }
}