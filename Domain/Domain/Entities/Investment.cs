namespace Domain.Entities
{
    public class Investment
    {
        public string Date { get; set; }
        public string CryptoName { get; set; }
        public decimal InvestedValue { get; set; }
        public string Notes { get; set; }
    }
}