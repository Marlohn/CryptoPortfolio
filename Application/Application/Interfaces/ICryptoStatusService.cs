using Application.Models;

namespace Application.Interfaces
{
    public interface ICryptoStatusService
    {
        void Upsert(CryptoStatusDto cryptoStatus);
        void Delete(string cryptoStatus);
        List<CryptoStatusDto> GetAll();
        CryptoStatusDto? GetByName(string cryptoName);
    }
}