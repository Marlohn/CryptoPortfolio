using Supabase.Postgrest.Models;
using Supabase.Postgrest.Responses;

namespace Infrastructure.Repositories.Data.Helpers
{
    public static class SupabaseResponseHelper
    {
        public static async Task<List<T>> ValidateAndReturnModels<T>(ModeledResponse<T> response) where T : BaseModel, new()
        {
            if (response == null || response.ResponseMessage == null)
            {
                throw new Exception("No response received.");
            }

            if (!response.ResponseMessage.IsSuccessStatusCode)
            {
                var errorContent = await response.ResponseMessage.Content.ReadAsStringAsync();
                throw new Exception($"Request failed with status code: {response.ResponseMessage.StatusCode}. Error: {errorContent}");
            }

            if (response.Models == null || response.Models.Count == 0)
            {
                throw new Exception("No data returned.");
            }

            return response.Models;
        }
    }
}
