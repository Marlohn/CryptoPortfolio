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
                        CurrentValue = decimal.TryParse(values[1], out var investedValue) ? investedValue : null, // maybe error?
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
        public void Delete(string cryptoName)
        {
            var lines = new List<string>();

            using (var reader = new StreamReader(_filePath))
            {
                string line;
                bool isFirstLine = true;

                while ((line = reader.ReadLine()) != null)
                {
                    if (isFirstLine)
                    {
                        lines.Add(line);
                        isFirstLine = false;
                        continue;
                    }

                    var values = line.Split(',');

                    if (values[0] != cryptoName)
                    {
                        lines.Add(line);
                    }
                }
            }

            bool isDeleted = lines.Count < File.ReadAllLines(_filePath).Length;

            if (isDeleted)
            {
                File.WriteAllLines(_filePath, lines);
            }
        }

        public void Backup()
        {
            var directory = Path.GetDirectoryName(_filePath) ?? Environment.CurrentDirectory;

            var backupDirectory = Path.Combine(directory, "Backups");

            if (!Directory.Exists(backupDirectory))
            {
                Directory.CreateDirectory(backupDirectory);
            }

            var backupFileName = $"{Path.GetFileNameWithoutExtension(_filePath)}_Backup_{DateTime.Now:yyyyMMdd_HHmmss}.csv";
            var backupFilePath = Path.Combine(backupDirectory, backupFileName);

            File.Copy(_filePath, backupFilePath);
        }
    }
}