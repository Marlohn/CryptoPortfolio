using Application.Models.Enums;

namespace Application.Models
{
    public class CryptoDto
    {
        public int Rank { get; set; }
        public string CryptoName { get; set; }
        public decimal TotalInvested { get; set; }
        public decimal? CurrentValue { get; set; }
        public decimal? Profit { get; set; }
        public decimal? ProfitPercentage { get; set; }
        public RiskLevel Risk { get; set; }
    }
}