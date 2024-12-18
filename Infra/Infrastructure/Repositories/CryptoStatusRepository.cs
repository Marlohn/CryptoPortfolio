using Domain.Entities;
using Domain.Interfaces;

namespace Infrastructure.Repositories
{
    public class CryptoStatusRepository : ICryptoStatusRepository
    {
        private readonly string _filePath;

        public CryptoStatusRepository(string filePath)
        {
            _filePath = filePath;

            if (!File.Exists(_filePath))
            {
                File.WriteAllText(_filePath, "CryptoName,CurrentValue,Risk");
            }
        }
        public List<CryptoStatus> GetAll()
        {
            var cryptoStatusList = new List<CryptoStatus>();

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

                    cryptoStatusList.Add(new CryptoStatus
                    {
                        CryptoName = values[0],
                        CurrentValue = decimal.TryParse(values[1], out var investedValue) ? investedValue : 0,
                        Risk = values[2]
                    });
                }
            }

            return cryptoStatusList;
        }

        public void Upsert(CryptoStatus cryptoStatus) // test it
        {
            var lines = new List<string>();

            // Lê todas as linhas do arquivo
            using (var reader = new StreamReader(_filePath))
            {
                string line;
                bool isFirstLine = true;

                while ((line = reader.ReadLine()) != null)
                {
                    if (isFirstLine)
                    {
                        lines.Add(line); // Mantém o cabeçalho
                        isFirstLine = false;
                        continue;
                    }

                    var values = line.Split(',');

                    // Se a criptomoeda já existe, atualiza a linha
                    if (values[0] == cryptoStatus.CryptoName)
                    {
                        line = $"{cryptoStatus.CryptoName},{cryptoStatus.CurrentValue},{cryptoStatus.Risk}";
                    }

                    lines.Add(line);
                }
            }

            // Se a criptomoeda não foi encontrada, adiciona uma nova linha
            if (!lines.Any(l => l.StartsWith($"{cryptoStatus.CryptoName},")))
            {
                lines.Add($"{cryptoStatus.CryptoName},{cryptoStatus.CurrentValue},{cryptoStatus.Risk}");
            }

            // Reescreve o arquivo
            File.WriteAllLines(_filePath, lines);
        }
    }
}