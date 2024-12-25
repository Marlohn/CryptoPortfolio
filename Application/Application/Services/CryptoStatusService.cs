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

        public CryptoStatusDto? GetCryptoStatusByName(string cryptoName)
        {
            var cryptoStatusList = _cryptoStatusRepository.GetAll();
            var cryptoStatus = cryptoStatusList.SingleOrDefault(x => x.CryptoName == cryptoName);

            return cryptoStatus == null ? null : new CryptoStatusDto
            {
                CryptoName = cryptoStatus.CryptoName,
                CurrentValue = cryptoStatus.CurrentValue,
                Risk = cryptoStatus.Risk
            };
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