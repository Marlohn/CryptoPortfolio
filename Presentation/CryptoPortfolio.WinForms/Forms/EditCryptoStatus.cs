using Application.Interfaces;
using Application.Models;
using UtilityExtensions;

namespace CryptoPortfolio.WinForms.Forms
{
    public partial class EditCryptoStatus : Form
    {
        private readonly ICryptoStatusService _cryptoStatusService;
        private readonly IInvestmentsService _investmentService;
        private readonly Main _main;
        private readonly CryptoStatusDto _cryptoStatus;

        public EditCryptoStatus(ICryptoStatusService cryptoStatusService, IInvestmentsService investmentService, Main main, CryptoStatusDto cryptoStatus)
        {
            _cryptoStatus = cryptoStatus;
            _cryptoStatusService = cryptoStatusService;
            _investmentService = investmentService;
            _main = main;

            InitializeComponent();
        }

        private void EditCryptoStatus_Load(object sender, EventArgs e)
        {
            textBox_CryptoName.Text = _cryptoStatus.CryptoName;
            textBox_currentValue.Text = _cryptoStatus.CurrentValue.ToString();
            comboBox_Risk.SelectedItem = _cryptoStatus.Risk;
        }

        private async void ButtonSave_Click(object sender, EventArgs e)
        {
            try
            {
                await _cryptoStatusService.Upsert(new CryptoStatusDto()
                {
                    CryptoName = _cryptoStatus.CryptoName,
                    CurrentValue = textBox_currentValue.Text.ToDecimal(),
                    Risk = comboBox_Risk.SelectedItem!.ToString()!
                });

                await _main.UpdatePortfolio();

                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error adding a new investment: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void Button_delete_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult result = MessageBox.Show($"Do you want to delete this '{_cryptoStatus.CryptoName}'?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    await _investmentService.Delete(_cryptoStatus.CryptoName); // maybe it should return boolean sucesss 
                    await _cryptoStatusService.Delete(_cryptoStatus.CryptoName);

                    await _main.UpdatePortfolio();

                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error adding a new investment: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}