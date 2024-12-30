using System.Data;
using System.Windows.Forms.DataVisualization.Charting;
using Application.Interfaces;
using Application.Models;
using CryptoPortfolio.WinForms.Forms;

namespace CryptoPortfolio.WinForms
{
    public partial class Main : Form
    {
        private readonly IPortfolioService _portfolioService;
        private readonly IInvestmentService _investmentService;
        private readonly ICryptoStatusService _cryptoStatusService;

        private BindingSource _portfolioBindingSource;

        public Main(IInvestmentService investmentService, IPortfolioService portfolioService, ICryptoStatusService cryptoStatusService)
        {
            InitializeComponent();

            _investmentService = investmentService;
            _portfolioService = portfolioService;
            _cryptoStatusService = cryptoStatusService;
        }

        private void Main_Load(object sender, EventArgs e)
        {
            string filePath = "investments.csv"; // check this

            comboBox_ChartFilterType.SelectedIndex = 1; // CurrentValue

            CreateExampleCSV(filePath);
            UpdatePortfolio();
            CustomizeDataGridView();
            //CustomizeDataGridViewRowHeader();
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
            var addInvestmentForm = new AddInvestment(_investmentService, _cryptoStatusService, this);

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
                MessageBox.Show($"Erro ao carregar os investimentos: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void UpdateDatagridViewValues(PortfolioDto portfolio)
        {
            //var bindingList = new BindingList<CryptoDto>(portfolio.Cryptos);
            var bindingList = ConvertToDataTable(portfolio.Cryptos);

            if (_portfolioBindingSource == null)
            {
                _portfolioBindingSource = new BindingSource();
                _portfolioBindingSource.DataSource = bindingList;
                dataGridView1.DataSource = _portfolioBindingSource;

                foreach (DataGridViewColumn column in dataGridView1.Columns)
                {
                    column.SortMode = DataGridViewColumnSortMode.Automatic;
                }
            }
            else
            {
                _portfolioBindingSource.DataSource = bindingList;
                _portfolioBindingSource.ResetBindings(false);
            }
        }

        private DataTable ConvertToDataTable(List<CryptoDto> cryptos)
        {
            var dataTable = new DataTable();

            dataTable.Columns.Add("Rank", typeof(int));
            dataTable.Columns.Add("CryptoName", typeof(string));
            dataTable.Columns.Add("TotalInvested", typeof(decimal));
            dataTable.Columns.Add("CurrentValue", typeof(decimal));
            dataTable.Columns.Add("Profit", typeof(decimal));
            dataTable.Columns.Add("ProfitPercentage", typeof(decimal));
            dataTable.Columns.Add("Risk", typeof(string));

            foreach (var crypto in cryptos)
            {
                dataTable.Rows.Add(
                    crypto.Rank,
                    crypto.CryptoName,
                    crypto.TotalInvested,
                    crypto.CurrentValue,
                    crypto.Profit,
                    crypto.ProfitPercentage,
                    crypto.Risk
                );
            }

            return dataTable;
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
                    series.Points.AddXY(group.Risk, group.Total);
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

        private void dataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            //try
            //{
            //    if (e.RowIndex >= 0)
            //    {
            //        var row = dataGridView1.Rows[e.RowIndex];
            //        string cryptoName = row.Cells["CryptoName"].Value?.ToString() ?? string.Empty; // is that the best approach? maybe exeption

            //        if (string.IsNullOrWhiteSpace(cryptoName))
            //        {
            //            MessageBox.Show("CryptoName is missing or invalid.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //            return;
            //        }

            //        string risk = row.Cells["Risk"].Value?.ToString() ?? string.Empty;  // is that the best approach?

            //        decimal? currentValue = null;
            //        var currentValueString = row.Cells["CurrentValue"].Value?.ToString();
            //        if (!string.IsNullOrWhiteSpace(currentValueString) && decimal.TryParse(currentValueString, out var parsedCurrentValue))
            //        {
            //            currentValue = parsedCurrentValue;
            //        }

            //        _portfolioService.UpdateCrypto(new CryptoStatusDto()
            //        {
            //            CryptoName = cryptoName,
            //            Risk = risk,
            //            CurrentValue = currentValue,
            //        });

            //        UpdatePortfolio();
            //    }
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show($"An unexpected error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //}
        }

        private void CustomizeDataGridView()
        {
            // Desabilita estilos visuais padrão para personalização
            dataGridView1.EnableHeadersVisualStyles = false;

            // Configurações do cabeçalho
            DataGridViewCellStyle columnHeaderStyle = new DataGridViewCellStyle
            {
                BackColor = Color.FromArgb(255, 223, 122), // Amarelo pastel
                ForeColor = Color.Black,
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                Alignment = DataGridViewContentAlignment.MiddleCenter,
                SelectionBackColor = Color.FromArgb(255, 223, 122), // Sem alteração no cabeçalho ao selecionar
                SelectionForeColor = Color.Black
            };
            dataGridView1.ColumnHeadersDefaultCellStyle = columnHeaderStyle;

            // Configurações padrão das células
            DataGridViewCellStyle cellStyle = new DataGridViewCellStyle
            {
                BackColor = Color.White, // Fundo padrão
                ForeColor = Color.Black,
                Font = new Font("Segoe UI", 9),
                SelectionBackColor = Color.White, // Transparente ao selecionar
                SelectionForeColor = Color.Black // Sem alteração no texto
            };
            dataGridView1.DefaultCellStyle = cellStyle;

            // Configurações de linhas alternadas
            DataGridViewCellStyle alternatingCellStyle = new DataGridViewCellStyle
            {
                BackColor = Color.FromArgb(255, 248, 220), // Amarelo claro
                SelectionBackColor = Color.FromArgb(255, 248, 220), // Fundo igual mesmo ao selecionar
                SelectionForeColor = Color.Black
            };
            dataGridView1.AlternatingRowsDefaultCellStyle = alternatingCellStyle;

            // Configuração de seleção e interatividade
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect; // Seleção por linha
            dataGridView1.MultiSelect = false; // Desativa múltiplas seleções

            // Remove seleção inicial
            dataGridView1.ClearSelection();

            // Evento opcional para remover visual de seleção durante interações
            dataGridView1.CellStateChanged += (s, e) =>
            {
                if (e.Cell.RowIndex >= 0)
                    dataGridView1.ClearSelection();
            };
        }



        //private void CustomizeDataGridViewRowHeader()
        //{
        //    // Exibe o cabeçalho da linha
        //    dataGridView1.RowHeadersVisible = true;
        //    dataGridView1.RowHeadersWidth = 50; // Define uma largura consistente

        //    // Configuração de estilo do cabeçalho da linha (RowHeader)
        //    DataGridViewCellStyle rowHeaderStyle = new DataGridViewCellStyle
        //    {
        //        BackColor = Color.FromArgb(255, 223, 122), // Amarelo pastel para combinar com o cabeçalho
        //        ForeColor = Color.Black, // Texto preto para contraste
        //        Font = new Font("Segoe UI", 9, FontStyle.Bold), // Fonte moderna e em negrito
        //        Alignment = DataGridViewContentAlignment.MiddleCenter, // Centraliza o texto
        //        SelectionBackColor = Color.FromArgb(255, 223, 122), // Mesma cor ao selecionar
        //        SelectionForeColor = Color.Black // Texto preto ao selecionar
        //    };
        //    dataGridView1.RowHeadersDefaultCellStyle = rowHeaderStyle;

        //    // Evento para numerar as linhas no RowHeader
        //    dataGridView1.RowPostPaint += (s, e) =>
        //    {
        //        // Obter o índice da linha
        //        string rowIndex = (e.RowIndex + 1).ToString(); // Começa em 1

        //        // Configuração do formato para centralizar o texto
        //        var centerFormat = new StringFormat()
        //        {
        //            Alignment = StringAlignment.Center,
        //            LineAlignment = StringAlignment.Center
        //        };

        //        // Desenhar o índice da linha na RowHeader
        //        e.Graphics.DrawString(rowIndex, dataGridView1.Font, Brushes.Black,
        //            new Rectangle(e.RowBounds.Left, e.RowBounds.Top, dataGridView1.RowHeadersWidth, e.RowBounds.Height),
        //            centerFormat);
        //    };
        //}








        private void dataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            // Formatação condicional para "Profit" e "ProfitPercentage"
            if (dataGridView1.Columns[e.ColumnIndex].Name == "Profit" ||
                dataGridView1.Columns[e.ColumnIndex].Name == "ProfitPercentage")
            {
                if (e.Value != null && decimal.TryParse(e.Value.ToString(), out decimal value1))
                {
                    e.CellStyle.ForeColor = value1 < 0 ? Color.Red : value1 > 0 ? Color.Green : e.CellStyle.ForeColor;
                }
            }

            // Formatação condicional para "Risk"
            if (dataGridView1.Columns[e.ColumnIndex].Name == "Risk")
            {
                if (e.Value != null)
                {
                    var riskValue = e.Value.ToString();
                    switch (riskValue)
                    {
                        case "Low":
                            e.CellStyle.ForeColor = Color.DarkGreen; // Texto verde escuro
                            e.CellStyle.BackColor = Color.FromArgb(198, 239, 206); // Fundo verde claro
                            break;
                        case "Medium":
                            e.CellStyle.ForeColor = Color.DarkOrange; // Texto laranja
                            e.CellStyle.BackColor = Color.FromArgb(255, 229, 153); // Fundo amarelo claro
                            break;
                        case "High":
                            e.CellStyle.ForeColor = Color.Red; // Texto vermelho
                            e.CellStyle.BackColor = Color.FromArgb(255, 199, 206); // Fundo vermelho claro
                            break;
                        case "Very High":
                            e.CellStyle.ForeColor = Color.DarkRed; // Texto vermelho escuro
                            e.CellStyle.BackColor = Color.FromArgb(255, 159, 159); // Fundo vermelho intenso
                            break;
                        case "None":
                            e.CellStyle.ForeColor = Color.Gray; // Texto cinza
                            e.CellStyle.BackColor = Color.FromArgb(224, 224, 224); // Fundo cinza claro
                            break;
                    }
                }
            }

            // Formatação de porcentagens
            if (dataGridView1.Columns[e.ColumnIndex].Name == "ProfitPercentage")
            {
                if (e.Value != null && decimal.TryParse(e.Value.ToString(), out decimal value2))
                {
                    e.Value = $"{value2:F2} %";
                    e.FormattingApplied = true;
                }
            }

            // Formatação de valores monetários
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

            // Mantém o estilo personalizado durante a seleção
            e.CellStyle.SelectionBackColor = e.CellStyle.BackColor;
            e.CellStyle.SelectionForeColor = e.CellStyle.ForeColor;
        }


        private void comboBox_ChartFilterType_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdatePortfolio();
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0)
                {
                    var row = dataGridView1.Rows[e.RowIndex];

                    string cryptoName = row.Cells["CryptoName"].Value?.ToString() ?? string.Empty; // is that the best approach? maybe exeption
                    string risk = row.Cells["Risk"].Value?.ToString() ?? string.Empty;  // is that the best approach?
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
                        Risk = risk
                    };

                    var addInvestmentForm = new EditCryptoStatus(_cryptoStatusService, _investmentService, this, cryptoStatus);

                    addInvestmentForm.Show();

                    //_portfolioService.UpdateCrypto(new CryptoStatusDto()
                    //{
                    //    CryptoName = cryptoName,
                    //    Risk = risk,
                    //    CurrentValue = currentValue,
                    //});

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An unexpected error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void toolStripButton_Backup_Click(object sender, EventArgs e)
        {
            _portfolioService.BackupData(); // maybe it can return sussfull or not

            MessageBox.Show("Backup completed successfully!", "Backup Completed", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                // Seleciona a linha inteira
                dataGridView1.Rows[e.RowIndex].Selected = true;

                //// Exemplo: obtendo os valores da linha selecionada
                //string cryptoName = dataGridView1.Rows[e.RowIndex].Cells["CryptoName"].Value.ToString();
                //MessageBox.Show($"Linha selecionada: {cryptoName}");
            }
        }

        private async void toolStripButton_RefreshIntegrations_Click(object sender, EventArgs e)
        {
            await _portfolioService.RefreshBinanceData();
            UpdatePortfolio();
        }
    }
}