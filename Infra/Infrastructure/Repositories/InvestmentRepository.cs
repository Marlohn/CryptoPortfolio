﻿using Domain.Entities;
using Domain.Interfaces;

namespace Infrastructure.Repositories
{
    public class InvestmentRepository : IInvestmentRepository
    {
        private readonly string _filePath;

        public InvestmentRepository(string filePath)
        {
            _filePath = filePath;

            if (!File.Exists(_filePath))
            {
                File.WriteAllText(_filePath, "Date,CryptoName,InvestedValue,Notes");
            }
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

                    investments.Add(new Investment
                    {
                        Date = values[0],
                        CryptoName = values[1],
                        InvestedValue = decimal.TryParse(values[2], out var investedValue) ? investedValue : 0,
                        Notes = values[3]
                    });
                }
            }

            return investments;
        }

        public void Add(Investment investment)
        {
            var line = $"{investment.Date},{investment.CryptoName},{investment.InvestedValue},{investment.Notes}";
            File.AppendAllText(_filePath, line + "\n");
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

                    if (values[1] != cryptoName)
                    {
                        lines.Add(line);
                    }
                }
            }

            File.WriteAllLines(_filePath, lines);
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
