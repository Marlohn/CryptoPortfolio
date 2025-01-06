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

        public async Task<List<CryptoStatusDto>> GetAll()
        {
            var cryptoStatusList = await _cryptoStatusRepository.GetAll();

            var criptoStatusDto = cryptoStatusList.Select(investment => new CryptoStatusDto
            {
                CryptoName = investment.CryptoName,
                CurrentValue = investment.CurrentValue,
                Risk = investment.Risk
            }).ToList();

            return criptoStatusDto;
        }

        public async Task<CryptoStatusDto?> GetByName(string cryptoName)
        {
            var cryptoStatusList = await _cryptoStatusRepository.GetAll();
            var cryptoStatus = cryptoStatusList.SingleOrDefault(x => x.CryptoName == cryptoName);

            return cryptoStatus == null ? null : new CryptoStatusDto
            {
                CryptoName = cryptoStatus.CryptoName,
                CurrentValue = cryptoStatus.CurrentValue,
                Risk = cryptoStatus.Risk
            };
        }

        public async Task Upsert(CryptoStatusDto cryptoStatusDto)
        {
            var validationResults = new List<ValidationResult>();
            var context = new ValidationContext(cryptoStatusDto);

            if (!Validator.TryValidateObject(cryptoStatusDto, context, validationResults, true))
            {
                throw new ValidationException("DTO validation failed.");
            }

            await _cryptoStatusRepository.Upsert(new CryptoStatus()  // need to create a better map here
            {
                CryptoName = cryptoStatusDto.CryptoName,
                CurrentValue = cryptoStatusDto.CurrentValue,
                Risk = cryptoStatusDto.Risk
            });
        }

        public async Task Delete(string cryptoName)
        {
            if (string.IsNullOrEmpty(cryptoName))
            {
                throw new ValidationException("DTO validation failed.");
            }

            await _cryptoStatusRepository.Delete(cryptoName);
        }

        public async Task Backup()
        {
            await _cryptoStatusRepository.Backup();
        }
    }
}