using System.Data;
using Application.Interfaces;

namespace CryptoPortfolio.WinForms
{
    public partial class Main : Form
    {
        private readonly IInvestmentService _investmentService;

        public Main(IInvestmentService investmentService)
        {
            _investmentService = investmentService;
            InitializeComponent();
        }

        private void Main_Load(object sender, EventArgs e)
        {

            string filePath = "investments.csv";

            CreateExampleCSV(filePath);

            LoadInvestments(filePath);
        }

        private void CreateExampleCSV(string filePath)
        {
            if (!File.Exists(filePath))
            {
                try
                {
                    using (StreamWriter writer = new StreamWriter(filePath))
                    {
                        // Write the header
                        writer.WriteLine("Date,Crypto Name,Invested Value (USD),Notes");

                        // Write the rows
                        writer.WriteLine("2024-06-01,BTC,5000,Initial purchase");
                        writer.WriteLine("2024-06-03,ETH,3000,Monthly investment");
                        writer.WriteLine("2024-06-05,ADA,100,Extra ADA");
                        writer.WriteLine("2024-06-07,SOL,2000,Strategic investment");
                        writer.WriteLine("2024-06-10,DOT,1000,Test");
                        writer.WriteLine("2024-06-15,ADA,200,Extra ADA 2");
                        writer.WriteLine("2024-06-19,BTC,10000,New investment");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error creating the file: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void LoadInvestments(string filePath)
        {
            try
            {
                var investments = _investmentService.GetAllInvestments();

                DataTable tabela = new DataTable();

                // Lê o arquivo CSV
                using (StreamReader leitor = new StreamReader(filePath))
                {
                    string linha = leitor.ReadLine(); // Lê a primeira linha (cabeçalho)

                    if (linha != null)
                    {
                        // Define as colunas com base no cabeçalho
                        string[] colunas = linha.Split(',');
                        foreach (string coluna in colunas)
                        {
                            tabela.Columns.Add(coluna.Trim());
                        }

                        // Lê o restante das linhas
                        while ((linha = leitor.ReadLine()) != null)
                        {
                            string[] dados = linha.Split(',');
                            tabela.Rows.Add(dados);
                        }
                    }
                }

                // Vincula a tabela ao DataGridView
                dataGridView1.DataSource = tabela;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao ler o arquivo: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
