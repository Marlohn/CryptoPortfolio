using System.ComponentModel.DataAnnotations;

namespace Application.Models
{
    public class CryptoStatusDto
    {
        [Required(ErrorMessage = "CryptoName is required.")]
        public string CryptoName { get; set; }

        //[Range(0.01, double.MaxValue, ErrorMessage = "InvestedValue must be greater than 0.")]
        [Required(ErrorMessage = "CurrentValue is required.")] // CHECK IT
        public decimal CurrentValue { get; set; }

        [Required(ErrorMessage = "Risk is required.")]
        public string Risk { get; set; }
    }
}