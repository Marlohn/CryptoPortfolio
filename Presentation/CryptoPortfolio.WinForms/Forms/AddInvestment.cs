using Application.Interfaces;
using Domain.Entities;
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
            textBox_Date.Text = DateTime.Now.ToString("dd/MM/yyyy");
        }

        private void buttonAddInvestment_Click(object sender, EventArgs e)
        {
            var investment = new Investment
            {
                Date = textBox_Date.Text,
                CryptoName = textBox_CryptoName.Text,
                InvestedValue = textBox_InvestedValue.Text.ToDecimal(),
                Notes = textBox_Notes.Text,
            };

            _investmentService.AddInvestment(investment);
        }
    }
}