﻿namespace Application.Models
{
    public class PortfolioDto
    {
        public string CryptoName { get; set; }
        public decimal TotalInvested { get; set; }
        public decimal CurrentValue { get; set; }
        public decimal Profit { get; set; }
        public decimal ProfitPercentage { get; set; }
        public string Risk { get; set; }
    }
}