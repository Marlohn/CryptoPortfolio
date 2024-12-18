using System.ComponentModel;
using Application.Interfaces;
using Application.Models;
using CryptoPortfolio.WinForms.Forms;

namespace CryptoPortfolio.WinForms
{
    public partial class Main : Form
    {
        private readonly IInvestmentService _investmentService;
        private readonly IPortfolioService _portfolioService;
        private BindingSource _portfolioBindingSource;

        public Main(IInvestmentService investmentService, IPortfolioService portfolioService)
        {
            InitializeComponent();

            _investmentService = investmentService;
            _portfolioService = portfolioService;
        }

        private void Main_Load(object sender, EventArgs e)
        {
            string filePath = "investments.csv"; // check this

            CreateExampleCSV(filePath);
            UpdatePortfolio();
        }

        private void CreateExampleCSV(string filePath)
        {
            if (!File.Exists(filePath))
            {
                try
                {
                    using (StreamWriter writer = new StreamWriter(filePath))
                    {
                        writer.WriteLine("Date,Crypto Name,Invested Value (USD),Notes");

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

        private void toolStripButton_NewInvestment_Click(object sender, EventArgs e)
        {
            var addInvestmentForm = new AddInvestment(_investmentService, this);

            addInvestmentForm.Show();
        }

        private void toolStripButton_ViewInvestments_Click(object sender, EventArgs e)
        {
            var investmentsForm = new Investments(_investmentService, this);

            investmentsForm.Show();
        }

        public void UpdatePortfolio()
        {
            try // remove this try catch create a midlesware
            {
                var portfolio = _portfolioService.GetPortfolio();
                UpdateFormPortfolioValues(portfolio);

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao carregar os investimentos: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void UpdateFormPortfolioValues(PortfolioDto portfolio)
        {
            if (_portfolioBindingSource == null)
            {
                _portfolioBindingSource = new BindingSource();
                _portfolioBindingSource.DataSource = portfolio.Cryptos;
                dataGridView1.DataSource = _portfolioBindingSource;
            }
            else
            {
                _portfolioBindingSource.DataSource = portfolio.Cryptos;
                _portfolioBindingSource.ResetBindings(false);
            }

            label_TotalInvested.Text = portfolio.TotalInvested.ToString();
        }

        private void dataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0)
                {
                    var row = dataGridView1.Rows[e.RowIndex];
                    string cryptoName = row.Cells["CryptoName"].Value?.ToString() ?? string.Empty; // is that the best approach? maybe exeption

                    if (string.IsNullOrWhiteSpace(cryptoName))
                    {
                        MessageBox.Show("CryptoName is missing or invalid.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    string risk = row.Cells["Risk"].Value?.ToString() ?? string.Empty;  // is that the best approach?

                    decimal? currentValue = null;
                    var currentValueString = row.Cells["CurrentValue"].Value?.ToString();
                    if (!string.IsNullOrWhiteSpace(currentValueString) && decimal.TryParse(currentValueString, out var parsedCurrentValue))
                    {
                        currentValue = parsedCurrentValue;
                    }

                    _portfolioService.UpdateCrypto(new CryptoStatusDto()
                    {
                        CryptoName = cryptoName,
                        Risk = risk,
                        CurrentValue = currentValue,
                    });

                    UpdatePortfolio();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An unexpected error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e) // improve this
        {
            if (dataGridView1.Columns[e.ColumnIndex].Name == "ProfitPercentage")
            {
                if (e.Value != null && decimal.TryParse(e.Value.ToString(), out decimal value))
                {
                    e.Value = $"{value:F2} %";
                    e.FormattingApplied = true;
                }
            }

            if (dataGridView1.Columns[e.ColumnIndex].Name == "TotalInvested" ||
                dataGridView1.Columns[e.ColumnIndex].Name == "CurrentValue" ||
                dataGridView1.Columns[e.ColumnIndex].Name == "Profit")
            {
                if (e.Value != null && decimal.TryParse(e.Value.ToString(), out decimal value2))
                {
                    // Formata como valor monetário com símbolo do dólar
                    e.Value = $"${value2:F2}";
                    e.FormattingApplied = true;
                }
            }

        }
    }
}