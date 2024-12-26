namespace Application.Models
{
    public class PortfolioDto
    {
        public List<CryptoDto> Cryptos { get; set; }
        public decimal TotalInvested { get; set; }
        public decimal TotalCurrent { get; set; }
        public decimal TotalProfit { get; set; }
        public decimal TotalProfitPercentage { get; set; }
    }
}