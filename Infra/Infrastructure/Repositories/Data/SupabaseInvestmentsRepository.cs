using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Repositories.Data.Helpers;
using Infrastructure.Repositories.Data.Models;
using Supabase;

namespace Infrastructure.Repositories.Data
{
    public class SupabaseInvestmentsRepository : IInvestmentsRepository
    {
        private readonly Client _supabaseClient;

        public SupabaseInvestmentsRepository(Client supabaseClient)
        {
            _supabaseClient = supabaseClient;
        }

        public async Task<List<Investment>> GetAll()
        {
            var response = await _supabaseClient.From<SupabaseInvestment>().Get();

            var investments = await SupabaseResponseHelper.ValidateAndReturnModels(response);

            return investments.Select(db => new Investment()
            {
                CryptoName = db.CryptoName,
                Date = db.Date,
                InvestedValue = db.InvestedValue,
                Notes = db.Notes
            }).ToList();
        }

        public async Task Add(Investment investment)
        {
            var response = await _supabaseClient.From<SupabaseInvestment>().Insert(new SupabaseInvestment()
            {
                CryptoName = investment.CryptoName,
                Date = investment.Date,
                InvestedValue = investment.InvestedValue,
                Notes = investment.Notes,
            });

            await SupabaseResponseHelper.ValidateAndReturnModels(response);
        }

        public async Task Delete(string cryptoName)
        {
            //In this way it does not return response :(
            //await _supabaseClient.From<SupaBaseInvestment>().Where(x => x.CryptoName == cryptoName).Delete();

            var investments = await GetByName(cryptoName);

            foreach (var investment in investments)
            {
                var response = await _supabaseClient.From<SupabaseInvestment>().Delete(investment);

                await SupabaseResponseHelper.ValidateAndReturnModels(response);
            }
        }

        public async Task Backup()
        {
            throw new NotImplementedException();
        }

        private async Task<List<SupabaseInvestment>> GetByName(string cryptoName)
        {
            var response = await _supabaseClient.From<SupabaseInvestment>().Where(x => x.CryptoName == cryptoName).Get();

            return await SupabaseResponseHelper.ValidateAndReturnModels(response);
        }
    }
}