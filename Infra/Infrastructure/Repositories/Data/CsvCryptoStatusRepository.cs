﻿using System.Globalization;
using Domain.Entities;
using Domain.Interfaces;

namespace Infrastructure.Repositories.Data
{
    public class CsvCryptoStatusRepository : ICryptoStatusRepository
    {
        private readonly string _filePath;

        public CsvCryptoStatusRepository(string filePath)
        {
            _filePath = filePath;

            if (!File.Exists(_filePath))
            {
                File.WriteAllText(_filePath, "CryptoName,CurrentValue,Risk");
            }
        }
        public async Task<List<CryptoStatus>> GetAll()
        {
            var cryptoStatusList = new List<CryptoStatus>();

            using (var reader = new StreamReader(_filePath))
            {
                string? line;
                bool isFirstLine = true;

                while ((line = await reader.ReadLineAsync()) != null)
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
                        CurrentValue = decimal.TryParse(values[1], NumberStyles.Any, CultureInfo.InvariantCulture, out var investedValue) ? investedValue : null, // maybe error?
                        Risk = values[2]
                    });
                }
            }

            return cryptoStatusList;
        }

        public async Task Upsert(CryptoStatus cryptoStatus) // test it
        {
            var lines = new List<string>();

            // Lê todas as linhas do arquivo
            using (var reader = new StreamReader(_filePath))
            {
                string? line;
                bool isFirstLine = true;

                while ((line = await reader.ReadLineAsync()) != null)
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
                        var currentValue = (cryptoStatus.CurrentValue ?? 0).ToString("F2", CultureInfo.InvariantCulture);
                        line = $"{cryptoStatus.CryptoName},{currentValue},{cryptoStatus.Risk}";
                    }

                    lines.Add(line);
                }
            }

            // Se a criptomoeda não foi encontrada, adiciona uma nova linha
            if (!lines.Any(l => l.StartsWith($"{cryptoStatus.CryptoName},")))
            {
                var currentValue = (cryptoStatus.CurrentValue ?? 0).ToString("F2", CultureInfo.InvariantCulture);
                lines.Add($"{cryptoStatus.CryptoName},{currentValue},{cryptoStatus.Risk}");
            }

            // Reescreve o arquivo
            File.WriteAllLines(_filePath, lines);
        }
        public async Task Delete(string cryptoName)
        {
            var lines = new List<string>();

            using (var reader = new StreamReader(_filePath))
            {
                string? line;
                bool isFirstLine = true;

                while ((line = await reader.ReadLineAsync()) != null)
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

        public Task Backup()
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

            return Task.CompletedTask;
        }
    }
}