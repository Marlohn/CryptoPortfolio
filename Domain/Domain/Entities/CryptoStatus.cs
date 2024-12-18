namespace Domain.Entities
{
    public class CryptoStatus
    {
        public string CryptoName { get; set; }
        public decimal? CurrentValue { get; set; }
        public string Risk { get; set; }
    }
}