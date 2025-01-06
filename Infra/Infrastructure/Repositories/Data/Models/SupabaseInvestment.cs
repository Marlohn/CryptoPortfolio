using Supabase.Postgrest.Attributes;
using Supabase.Postgrest.Models;

namespace Infrastructure.Repositories.Data.Models
{
    [Table("investments")]
    internal class SupabaseInvestment : BaseModel
    {
        [PrimaryKey("id")]
        [Column("id", ignoreOnInsert: true, ignoreOnUpdate: true)]
        public Guid Id { get; set; }

        [Column("date")]
        public string Date { get; set; }

        [Column("crypto_name")]
        public string CryptoName { get; set; }

        [Column("invested_value")]
        public decimal InvestedValue { get; set; }

        [Column("notes")]
        public string Notes { get; set; }
    }
}
