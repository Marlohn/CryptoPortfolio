using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Repositories.Data.Helpers;
using Infrastructure.Repositories.Data.Models;
using Supabase;

namespace Infrastructure.Repositories.Data
{
    public class SupabaseCryptoStatusRepository : ICryptoStatusRepository
    {
        private readonly Client _supabaseClient;

        public SupabaseCryptoStatusRepository(Client supabaseClient)
        {
            _supabaseClient = supabaseClient;
        }

        public async Task<List<CryptoStatus>> GetAll()
        {
            var response = await _supabaseClient.From<SupabaseCryptoStatus>().Get();

            var cryptoStatusList = await SupabaseResponseHelper.ValidateAndReturnModels(response);

            return cryptoStatusList.Select(db => new CryptoStatus()
            {
                CryptoName = db.CryptoName,
                CurrentValue = db.CurrentValue,
                Risk = db.Risk
            }).ToList();
        }

        public async Task Upsert(CryptoStatus investment)
        {
            var existingRecord = await _supabaseClient
                .From<SupabaseCryptoStatus>()
                .Where(x => x.CryptoName == investment.CryptoName)
                .Single();

            var supabaseCryptoStatus = new SupabaseCryptoStatus
            {
                Id = existingRecord?.Id ?? Guid.NewGuid(), // Preserve the current ID or generate a new one
                CryptoName = investment.CryptoName,
                CurrentValue = investment.CurrentValue ?? 0,
                Risk = investment.Risk
            };

            var response = await _supabaseClient.From<SupabaseCryptoStatus>().Upsert(supabaseCryptoStatus, new Supabase.Postgrest.QueryOptions { OnConflict = "crypto_name"});

            await SupabaseResponseHelper.ValidateAndReturnModels(response);
        }

        public async Task Delete(string cryptoName)
        {
            var existingRecord = await _supabaseClient
                .From<SupabaseCryptoStatus>()
                .Where(x => x.CryptoName == cryptoName)
                .Single() ?? throw new Exception($"No record found with CryptoName: {cryptoName}");

            var response = await _supabaseClient.From<SupabaseCryptoStatus>().Delete(existingRecord);

            await SupabaseResponseHelper.ValidateAndReturnModels(response);
        }

        public async Task Backup()
        {
            throw new NotImplementedException();
        }
    }
}