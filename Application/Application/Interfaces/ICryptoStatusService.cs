using Application.Models;

namespace Application.Interfaces
{
    public interface ICryptoStatusService
    {
        void UpsertCryptoStatus(CryptoStatusDto investment);
        List<CryptoStatusDto> GetAllCryptoStatus();
    }
}