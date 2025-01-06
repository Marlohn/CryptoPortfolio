using Application.Interfaces;
using Application.Models;
using UtilityExtensions;

namespace CryptoPortfolio.WinForms.Forms
{
    public partial class AddInvestment : Form
    {
        private readonly IInvestmentsService _investmentService;
        private readonly ICryptoStatusService _cryptoStatusService;
        private readonly Main _main;

        private List<CryptoStatusDto> _cryptoStatusList = new();

        public AddInvestment(IInvestmentsService investmentService, ICryptoStatusService cryptoStatusService, Main main)
        {
            InitializeComponent();
            _investmentService = investmentService;
            _cryptoStatusService = cryptoStatusService;
            _main = main;
        }

        private void AddInvestment_Load(object sender, EventArgs e)
        {
            textBox_Date.Text = DateTime.Now.ToString("yyyy-MM-dd");
            comboBox_Risk.SelectedIndex = 0;
            _cryptoStatusList = _cryptoStatusService.GetAll();

            CheckBox_AutoDecreaseBTC.CheckedChanged += CheckBox_AutoDecrease_CheckedChanged;
            CheckBox_AutoDecreaseUSDT.CheckedChanged += CheckBox_AutoDecrease_CheckedChanged;
            CheckBox_AutoDecreaseNA.CheckedChanged += CheckBox_AutoDecrease_CheckedChanged;
        }

        private void AddInvestment_FormClosing(object sender, FormClosingEventArgs e)
        {
            _main.UpdatePortfolio();
        }

        private void ButtonAddInvestment_Click(object sender, EventArgs e)
        {
            try
            {
                decimal investedValue = textBox_InvestedValue.Text.ToDecimal();
                AddInvestments(textBox_CryptoName.Text, textBox_InvestedValue.Text.ToDecimal(), textBox_Notes.Text);
                
                if (CheckBox_AutoDecreaseBTC.Checked)
                {
                    AddInvestments("BTC", -investedValue, $"Auto-decrease due to investment in {textBox_CryptoName.Text}");
                }
                else if (CheckBox_AutoDecreaseUSDT.Checked)
                {
                    AddInvestments("USDT", -investedValue, $"Auto-decrease due to investment in {textBox_CryptoName.Text}");
                }

                AddCryptoStatus();

                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error adding a new investment: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void AddInvestments(string cryptoName, decimal investedValue, string notes)
        {
            _investmentService.Add(new InvestmentDto
            {
                Date = textBox_Date.Text,
                CryptoName = cryptoName,
                InvestedValue = investedValue,
                Notes = notes,
            });
        }

        private void AddCryptoStatus()
        {
            CryptoStatusDto? cryptoStatus = _cryptoStatusList.FirstOrDefault(x => x.CryptoName == textBox_CryptoName.Text);

            _cryptoStatusService.Upsert(new CryptoStatusDto()
            {
                CryptoName = textBox_CryptoName.Text,
                Risk = comboBox_Risk.SelectedItem?.ToString() ?? string.Empty, // we have a service validation to avoid save it empty
                CurrentValue = textBox_currentValue.Text.ToDecimal(),
            });
        }

        private void TextBox_CryptoName_TextChanged(object sender, EventArgs e)
        {
            UpdateCurrentValue();
        }

        private void TextBox_InvestedValue_TextChanged(object sender, EventArgs e)
        {
            UpdateCurrentValue();
        }

        private void UpdateCurrentValue()
        {
            textBox_currentValue.Text = textBox_InvestedValue.Text;

            CryptoStatusDto? cryptoStatus = _cryptoStatusList.FirstOrDefault(x => x.CryptoName == textBox_CryptoName.Text);

            comboBox_Risk.SelectedIndex = 0;

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

        private void CheckBox_AutoDecrease_CheckedChanged(object? sender, EventArgs? e)
        {
            if (sender is CheckBox currentCheckbox && currentCheckbox.Checked)
            {
                CheckBox_AutoDecreaseBTC.Checked = currentCheckbox == CheckBox_AutoDecreaseBTC;
                CheckBox_AutoDecreaseUSDT.Checked = currentCheckbox == CheckBox_AutoDecreaseUSDT;
                CheckBox_AutoDecreaseNA.Checked = currentCheckbox == CheckBox_AutoDecreaseNA;
            }

            if (!CheckBox_AutoDecreaseBTC.Checked && !CheckBox_AutoDecreaseUSDT.Checked && !CheckBox_AutoDecreaseNA.Checked)
            {
                CheckBox_AutoDecreaseNA.Checked = true;
            }
        }
    }
}