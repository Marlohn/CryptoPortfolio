namespace Application.Models
{
    public class PortfolioDto
    {
        public List<CryptoDto> Cryptos { get; set; }
        public decimal TotalInvested { get; set; }
    }
}