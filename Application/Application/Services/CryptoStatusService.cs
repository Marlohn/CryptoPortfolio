using System.ComponentModel.DataAnnotations;
using Application.Interfaces;
using Application.Models;
using Domain.Entities;
using Domain.Interfaces;

namespace Application.Services
{
    public class CryptoStatusService : ICryptoStatusService
    {
        private readonly ICryptoStatusRepository _cryptoStatusRepository;

        public CryptoStatusService(ICryptoStatusRepository cryptoStatusRepository)
        {
            _cryptoStatusRepository = cryptoStatusRepository;
        }

        public List<CryptoStatusDto> GetAllCryptoStatus()
        {
            var cryptoStatusList = _cryptoStatusRepository.GetAll();

            var criptoStatusDto = cryptoStatusList.Select(investment => new CryptoStatusDto
            {
                CryptoName = investment.CryptoName,
                CurrentValue = investment.CurrentValue,
                Risk = investment.Risk
            }).ToList();

            return criptoStatusDto;
        }

        public void UpsertCryptoStatus(CryptoStatusDto cryptoStatusDto)
        {
            var validationResults = new List<ValidationResult>();
            var context = new ValidationContext(cryptoStatusDto);

            if (!Validator.TryValidateObject(cryptoStatusDto, context, validationResults, true))
            {
                throw new ValidationException("DTO validation failed.");
            }

            _cryptoStatusRepository.Upsert(new CryptoStatus()  // need to create a better map here
            {
                CryptoName = cryptoStatusDto.CryptoName,
                CurrentValue = cryptoStatusDto.CurrentValue,
                Risk = cryptoStatusDto.Risk
            });
        }
    }
}