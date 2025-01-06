using System.ComponentModel.DataAnnotations;
using Application.Interfaces;
using Application.Models;
using Domain.Entities;
using Domain.Interfaces;

namespace Application.Services
{
    public class InvestmentsService : IInvestmentsService
    {
        private readonly IInvestmentsRepository _investmentRepository;

        public InvestmentsService(IInvestmentsRepository investmentRepository)
        {
            _investmentRepository = investmentRepository;
        }

        public List<InvestmentDto> GetAll()
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

        public void Add(InvestmentDto investmentDto)
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

        public void Delete(string cryptoName)
        {
            if (string.IsNullOrEmpty(cryptoName))
            {
                throw new ValidationException("DTO validation failed.");
            }

            _investmentRepository.Delete(cryptoName);
        }

        public void Backup()
        {
            _investmentRepository.Backup();
        }
    }
}