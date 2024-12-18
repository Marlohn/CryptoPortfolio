using Application.Interfaces;
using Application.Models;
using UtilityExtensions;

namespace CryptoPortfolio.WinForms.Forms
{
    public partial class AddInvestment : Form
    {
        private readonly IInvestmentService _investmentService;

        public AddInvestment(IInvestmentService investmentService)
        {
            InitializeComponent();
            _investmentService = investmentService;
        }

        private void AddInvestment_Load(object sender, EventArgs e)
        {
            textBox_Date.Text = DateTime.Now.ToString("yyyy-MM-dd");
        }

        private void buttonAddInvestment_Click(object sender, EventArgs e)
        {
            try
            {
                var investment = new InvestmentDto
                {
                    Date = textBox_Date.Text,
                    CryptoName = textBox_CryptoName.Text,
                    InvestedValue = textBox_InvestedValue.Text.ToDecimal(),
                    Notes = textBox_Notes.Text,
                };

                _investmentService.AddInvestment(investment);

                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error adding a new investment: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}