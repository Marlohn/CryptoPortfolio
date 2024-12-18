using System.ComponentModel.DataAnnotations;

namespace Application.Models
{
    public class InvestmentDto
    {
        [Required(ErrorMessage = "Date is required.")]
        public string Date { get; set; }

        [Required(ErrorMessage = "CryptoName is required.")]
        public string CryptoName { get; set; }

        [Range(0.01, double.MaxValue, ErrorMessage = "InvestedValue must be greater than 0.")] // review this validation
        public decimal InvestedValue { get; set; }

        //[Required(ErrorMessage = "Notes cannot be empty.")]
        public string Notes { get; set; }
    }
}