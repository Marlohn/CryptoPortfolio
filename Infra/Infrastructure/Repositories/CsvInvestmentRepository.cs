using Domain.Entities;
using Domain.Interfaces;

namespace Infrastructure.Repositories
{
    public class CsvInvestmentRepository : IInvestmentRepository
    {
        private readonly string _filePath;

        public CsvInvestmentRepository(string filePath)
        {
            _filePath = filePath;

            if (!File.Exists(_filePath))
            {
                File.WriteAllText(_filePath, "Date,CryptoName,InvestedValue,Notes\n");
            }
        }

        public void Add(Investment investment)
        {
            var line = $"{investment.Date},{investment.CryptoName},{investment.InvestedValue},{investment.Notes}";
            File.AppendAllText(_filePath, line + "\n");
        }

        public List<Investment> GetAll()
        {
            var investments = new List<Investment>();

            using (var reader = new StreamReader(_filePath))
            {
                string line;
                bool isFirstLine = true;

                while ((line = reader.ReadLine()) != null)
                {
                    if (isFirstLine)
                    {
                        isFirstLine = false;
                        continue;
                    }

                    var values = line.Split(',');

                    if (values.Length == 4)
                    {
                        investments.Add(new Investment
                        {
                            Date = values[0],
                            CryptoName = values[1],
                            InvestedValue = decimal.TryParse(values[2], out var investedValue) ? investedValue : 0,
                            Notes = values[3]
                        });
                    }
                }
            }

            return investments;
        }
    }
}
