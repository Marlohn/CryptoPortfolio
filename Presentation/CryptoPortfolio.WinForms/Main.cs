using System.Data;
using System.Windows.Forms.DataVisualization.Charting;
using Application.Interfaces;
using Application.Models;
using Application.Models.Enums;
using CryptoPortfolio.WinForms.Forms;
using UtilityExtensions;

namespace CryptoPortfolio.WinForms
{
    public partial class Main : Form
    {
        private readonly IPortfolioService _portfolioService;
        private readonly IInvestmentService _investmentService;
        private readonly ICryptoStatusService _cryptoStatusService;

        private readonly BindingSource _portfolioBindingSource = [];

        public Main(IInvestmentService investmentService, IPortfolioService portfolioService, ICryptoStatusService cryptoStatusService)
        {
            InitializeComponent();

            _investmentService = investmentService;
            _portfolioService = portfolioService;
            _cryptoStatusService = cryptoStatusService;
        }

        private void Main_Load(object sender, EventArgs e)
        {
            comboBox_ChartFilterType.SelectedIndex = 1; // CurrentValue

            UpdatePortfolio();
            CustomizeDataGridView();
        }

        private void ToolStripButton_NewInvestment_Click(object sender, EventArgs e)
        {
            var addInvestmentForm = new AddInvestment(_investmentService, _cryptoStatusService, this);

            addInvestmentForm.Show();
        }

        private void ToolStripButton_ViewInvestments_Click(object sender, EventArgs e)
        {
            var investmentsForm = new Investments(_investmentService, this);

            investmentsForm.Show();
        }

        public void UpdatePortfolio()
        {
            try // remove this try catch create a midlesware
            {
                var portfolio = _portfolioService.GetPortfolio();

                string formattedProfitPercentage = Math.Abs(portfolio.TotalProfitPercentage).ToString("P2");

                string formattedProfitPercentage2 = portfolio.TotalProfitPercentage.ToString("F2");

                label_TotalInvested.Text = portfolio.TotalInvested.ToString("C", new System.Globalization.CultureInfo("en-US"));
                label_totalCurrent.Text = portfolio.TotalCurrent.ToString("C", new System.Globalization.CultureInfo("en-US"));
                label_TotalProfit.Text = portfolio.TotalProfit.ToString("C", new System.Globalization.CultureInfo("en-US")) +
                                         $" ({portfolio.TotalProfitPercentage:F2}%)";

                label_TotalProfit.ForeColor = Color.Green;
                if (portfolio.TotalProfit < 0)
                {
                    label_TotalProfit.ForeColor = Color.DarkRed;
                }

                UpdateDatagridViewValues(portfolio);
                UpdateRiskDistributionChart(portfolio);
                UpdateCryptoDistributionChart(portfolio);

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading investments: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void UpdateDatagridViewValues(PortfolioDto portfolio)
        {
            DataTable bindingList = portfolio.Cryptos.ToDataTable();

            _portfolioBindingSource.DataSource = bindingList;
            dataGridView1.DataSource = _portfolioBindingSource;
        }

        private void UpdateRiskDistributionChart(PortfolioDto portfolio)
        {
            try
            {
                string selectedItem = comboBox_ChartFilterType.SelectedItem.ToString();

                var riskGroups = portfolio.Cryptos
                    .GroupBy(c => c.Risk)
                    .Select(group => new
                    {
                        Risk = group.Key,
                        Total = selectedItem switch
                        {
                            "TotalInvested" => group.Sum(c => c.TotalInvested),
                            "CurrentValue" => group.Sum(c => c.CurrentValue),
                            "Profit" => group.Sum(c => c.Profit),
                            _ => 0m // Valor padrão se nenhum filtro for aplicado
                        }
                    })
                    .Where(group => group.Total > 0) // Remove riscos com total 0
                    .ToList();

                // Limpa os dados anteriores no Chart
                chart_Risk.Series.Clear();
                chart_Risk.Titles.Clear();

                // Configura o Chart
                chart_Risk.Titles.Add("Wallet Distribution by Risk");

                var series = new Series
                {
                    Name = "RiskDistribution",
                    ChartType = SeriesChartType.Pie
                };

                chart_Risk.Series.Add(series);

                // Adiciona os dados ao Chart
                foreach (var group in riskGroups)
                {
                    series.Points.AddXY(group.Risk.GetDescription(), group.Total);
                }

                // Configura os rótulos no gráfico
                series.Label = "#PERCENT{P2}"; // Exibe a porcentagem com duas casas decimais
                series.LegendText = "#AXISLABEL"; // Exibe o nome do risco na legenda

                // Exibe a legenda
                //chart1.Legends.Clear();
                //chart1.Legends.Add(new Legend("RiskLegend")
                //{
                //    Docking = Docking.Bottom
                //});
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error updating chart: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void UpdateCryptoDistributionChart(PortfolioDto portfolio)
        {
            try
            {
                string selectedItem = comboBox_ChartFilterType.SelectedItem?.ToString();

                var cryptoGroups = portfolio.Cryptos
                    .GroupBy(c => c.CryptoName) // Agrupa por CryptoName
                    .Select(group => new
                    {
                        CryptoName = group.Key,
                        Total = selectedItem switch
                        {
                            "TotalInvested" => group.Sum(c => c.TotalInvested),
                            "CurrentValue" => group.Sum(c => c.CurrentValue),
                            "Profit" => group.Sum(c => c.Profit),
                            _ => 0m
                        }
                    })
                    .Where(group => group.Total > 0) // Remove criptos com total 0
                    .ToList();

                // Limpa os dados anteriores no Chart
                chart_TotalCrypto.Series.Clear();
                chart_TotalCrypto.Titles.Clear();

                // Configura o Chart
                chart_TotalCrypto.Titles.Add($"Wallet Distribution by {selectedItem}");

                var series = new Series
                {
                    Name = "CryptoDistribution",
                    ChartType = SeriesChartType.Pie
                };

                chart_TotalCrypto.Series.Add(series);

                // Adiciona os dados ao Chart
                foreach (var group in cryptoGroups)
                {
                    series.Points.AddXY(group.CryptoName, group.Total);
                }

                // Configura os rótulos no gráfico
                //series.Label = "#PERCENT{P2}"; // Exibe a porcentagem com duas casas decimais
                series.Label = "#AXISLABEL: #PERCENT{P2}";
                series.LegendText = "#AXISLABEL"; // Exibe o nome da cripto na legenda
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error updating chart: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //ToDo: Maybe we can remove this method and keep all the settings in the component properties
        private void CustomizeDataGridView()
        {
            // Disable default visual styles for custom styling
            dataGridView1.EnableHeadersVisualStyles = false;

            // Configure header styles
            DataGridViewCellStyle columnHeaderStyle = new DataGridViewCellStyle
            {
                BackColor = Color.FromArgb(255, 223, 122), // Pastel yellow
                ForeColor = Color.Black,
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                Alignment = DataGridViewContentAlignment.MiddleCenter,
                SelectionBackColor = Color.FromArgb(255, 223, 122), // No visual change on header selection
                SelectionForeColor = Color.Black
            };
            dataGridView1.ColumnHeadersDefaultCellStyle = columnHeaderStyle;

            // Configure default cell styles
            DataGridViewCellStyle cellStyle = new DataGridViewCellStyle
            {
                BackColor = Color.White, // Default background
                ForeColor = Color.Black,
                Font = new Font("Segoe UI", 9),
                SelectionBackColor = Color.White, // Transparent background on selection
                SelectionForeColor = Color.Black // No change in text color
            };
            dataGridView1.DefaultCellStyle = cellStyle;

            // Configure alternating row styles
            DataGridViewCellStyle alternatingCellStyle = new DataGridViewCellStyle
            {
                BackColor = Color.FromArgb(255, 248, 220), // Light yellow
                SelectionBackColor = Color.FromArgb(255, 248, 220), // Same background when selected
                SelectionForeColor = Color.Black
            };
            dataGridView1.AlternatingRowsDefaultCellStyle = alternatingCellStyle;

            // Configure selection behavior
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect; // Select entire row
            dataGridView1.MultiSelect = false; // Disable multi-selection

            // Remove initial selection
            dataGridView1.ClearSelection();

            // Optional event to remove selection visuals during interactions
            dataGridView1.CellStateChanged += (s, e) =>
            {
                if (e.Cell.RowIndex >= 0)
                    dataGridView1.ClearSelection();
            };
        }

        private void DataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            // Conditional formatting for "Profit" and "ProfitPercentage"
            if (dataGridView1.Columns[e.ColumnIndex].Name == "Profit" ||
                dataGridView1.Columns[e.ColumnIndex].Name == "ProfitPercentage")
            {
                if (e.Value != null && decimal.TryParse(e.Value.ToString(), out decimal value1))
                {
                    e.CellStyle.ForeColor = value1 < 0 ? Color.Red : value1 > 0 ? Color.Green : e.CellStyle.ForeColor;
                }
            }

            // Conditional formatting for "Risk"
            if (dataGridView1.Columns[e.ColumnIndex].Name == "Risk")
            {
                if (e.Value != null)
                {
                    switch ((RiskLevel)e.Value)
                    {
                        case RiskLevel.None:
                            e.Value = RiskLevel.None.GetDescription();
                            e.CellStyle.ForeColor = Color.Gray; // Gray text
                            e.CellStyle.BackColor = Color.FromArgb(224, 224, 224); // Light gray background
                            break;
                        case RiskLevel.Low:
                            e.Value = RiskLevel.Low.GetDescription();
                            e.CellStyle.ForeColor = Color.DarkGreen; // Dark green text
                            e.CellStyle.BackColor = Color.FromArgb(198, 239, 206); // Light green background
                            break;
                        case RiskLevel.Medium:
                            e.Value = RiskLevel.Medium.GetDescription();
                            e.CellStyle.ForeColor = Color.DarkOrange; // Orange text
                            e.CellStyle.BackColor = Color.FromArgb(255, 229, 153); // Light yellow background
                            break;
                        case RiskLevel.High:
                            e.Value = RiskLevel.High.GetDescription();
                            e.CellStyle.ForeColor = Color.Red; // Red text
                            e.CellStyle.BackColor = Color.FromArgb(255, 199, 206); // Light red background
                            break;
                        case RiskLevel.VeryHigh:
                            e.Value = RiskLevel.VeryHigh.GetDescription();
                            e.CellStyle.ForeColor = Color.DarkRed; // Dark red text
                            e.CellStyle.BackColor = Color.FromArgb(255, 159, 159); // Intense red background
                            break;
                    }
                }
            }

            // Formatting for percentages
            if (dataGridView1.Columns[e.ColumnIndex].Name == "ProfitPercentage")
            {
                if (e.Value != null && decimal.TryParse(e.Value.ToString(), out decimal value2))
                {
                    e.Value = $"{value2:F2} %";
                    e.FormattingApplied = true;
                }
            }

            // Formatting for monetary values
            if (dataGridView1.Columns[e.ColumnIndex].Name == "TotalInvested" ||
                dataGridView1.Columns[e.ColumnIndex].Name == "CurrentValue" ||
                dataGridView1.Columns[e.ColumnIndex].Name == "Profit")
            {
                if (e.Value != null && decimal.TryParse(e.Value.ToString(), out decimal value3))
                {
                    e.Value = $"${value3:F2}";
                    e.FormattingApplied = true;
                }
            }

            // Maintain custom style during selection
            e.CellStyle.SelectionBackColor = e.CellStyle.BackColor;
            e.CellStyle.SelectionForeColor = e.CellStyle.ForeColor;
        }


        private void ComboBox_ChartFilterType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_portfolioBindingSource.DataSource != null)
            {
                UpdatePortfolio();
            }
        }

        private void DataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0)
                {
                    var row = dataGridView1.Rows[e.RowIndex];

                    string cryptoName = row.Cells["CryptoName"].Value?.ToString() ?? string.Empty; // is that the best approach? maybe exeption
                    var risk = (RiskLevel)row.Cells["Risk"].Value!;
                    var currentValueString = row.Cells["CurrentValue"].Value?.ToString();

                    decimal? currentValue = null;
                    if (!string.IsNullOrWhiteSpace(currentValueString) && decimal.TryParse(currentValueString, out var parsedCurrentValue))
                    {
                        currentValue = parsedCurrentValue;
                    }

                    var cryptoStatus = new CryptoStatusDto()
                    {
                        CryptoName = cryptoName,
                        CurrentValue = currentValue,
                        Risk = risk.GetDescription()
                    };

                    var addInvestmentForm = new EditCryptoStatus(_cryptoStatusService, _investmentService, this, cryptoStatus);

                    addInvestmentForm.Show();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An unexpected error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ToolStripButton_Backup_Click(object sender, EventArgs e)
        {
            _portfolioService.BackupData(); // maybe it can return sussfull or not

            MessageBox.Show("Backup completed successfully!", "Backup Completed", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void DataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                dataGridView1.Rows[e.RowIndex].Selected = true;
            }
        }

        private async void ToolStripButton_RefreshIntegrations_Click(object sender, EventArgs e)
        {
            await _portfolioService.RefreshBinanceData();
            UpdatePortfolio();
        }
    }
}