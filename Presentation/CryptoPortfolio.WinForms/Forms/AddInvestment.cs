using Application.Interfaces;
using Application.Models;
using UtilityExtensions;

namespace CryptoPortfolio.WinForms.Forms
{
    public partial class AddInvestment : Form
    {
        private readonly IInvestmentService _investmentService;
        private readonly ICryptoStatusService _cryptoStatusService;
        private readonly Main _main;

        private List<CryptoStatusDto> _cryptoStatusList = new();

        public AddInvestment(IInvestmentService investmentService, ICryptoStatusService cryptoStatusService, Main main)
        {
            InitializeComponent();
            _investmentService = investmentService;
            _cryptoStatusService = cryptoStatusService;
            _main = main;
        }

        private void AddInvestment_Load(object sender, EventArgs e)
        {
            textBox_Date.Text = DateTime.Now.ToString("yyyy-MM-dd");
            _cryptoStatusList = _cryptoStatusService.GetAll();
        }

        private void AddInvestment_FormClosing(object sender, FormClosingEventArgs e)
        {
            _main.UpdatePortfolio();
        }

        private void buttonAddInvestment_Click(object sender, EventArgs e)
        {
            try
            {
                CryptoStatusDto? cryptoStatus = _cryptoStatusList.FirstOrDefault(x => x.CryptoName == textBox_CryptoName.Text);

                _investmentService.Add(new InvestmentDto
                {
                    Date = textBox_Date.Text,
                    CryptoName = textBox_CryptoName.Text,
                    InvestedValue = textBox_InvestedValue.Text.ToDecimal(),
                    Notes = textBox_Notes.Text,
                });

                _cryptoStatusService.Upsert(new CryptoStatusDto()
                {
                    CryptoName = textBox_CryptoName.Text,
                    Risk = comboBox_Risk.SelectedItem?.ToString() ?? string.Empty, // we have a service validation to avoid save it empty
                    CurrentValue = textBox_currentValue.Text.ToDecimal(),
                });

                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error adding a new investment: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void UpdateCurrentValue()
        {
            textBox_currentValue.Text = textBox_InvestedValue.Text;

            CryptoStatusDto? cryptoStatus = _cryptoStatusList.FirstOrDefault(x => x.CryptoName == textBox_CryptoName.Text);

            comboBox_Risk.SelectedIndex = -1;

            if (cryptoStatus != null)
            {
                textBox_currentValue.Text = cryptoStatus.CurrentValue.ToString();
                comboBox_Risk.SelectedItem = cryptoStatus.Risk;

                if (decimal.TryParse(textBox_InvestedValue.Text, out decimal investedValue))
                {
                    if (investedValue >= 0)
                    {
                        if (decimal.TryParse(textBox_currentValue.Text, out decimal currentValue))
                        {
                            textBox_currentValue.Text = (currentValue + investedValue).ToString();
                        }
                        else
                        {
                            textBox_currentValue.Text = investedValue.ToString();
                        }
                    }
                }
            }
        }

        private void textBox_CryptoName_TextChanged(object sender, EventArgs e)
        {
            UpdateCurrentValue();
        }

        private void textBox_InvestedValue_TextChanged(object sender, EventArgs e)
        {
            UpdateCurrentValue();
        }
    }
}