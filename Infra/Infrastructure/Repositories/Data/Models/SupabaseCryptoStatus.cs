using Supabase.Postgrest.Attributes;
using Supabase.Postgrest.Models;

namespace Infrastructure.Repositories.Data.Models
{
    [Table("cryptostatus")]
    public class SupabaseCryptoStatus : BaseModel
    {
        [PrimaryKey("id")]
        [Column("id", ignoreOnInsert:true, ignoreOnUpdate:true)]
        public Guid Id { get; set; }

        [Column("crypto_name")]
        public string CryptoName { get; set; }

        [Column("current_value")]
        public decimal CurrentValue { get; set; }

        [Column("risk")]
        public string Risk { get; set; }
    }
}
