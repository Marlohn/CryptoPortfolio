using Application.Interfaces;

namespace CryptoPortfolio.WinForms
{
    public partial class Investments : Form
    {
        private readonly IInvestmentService _investmentService;
        private readonly Main _main;

        public Investments(IInvestmentService investmentService, Main main)
        {
            InitializeComponent();
            _investmentService = investmentService;
            _main = main;
        }

        private void Investments_Load(object sender, EventArgs e)
        {
            LoadInvestments();
        }

        private void Investments_FormClosing(object sender, FormClosingEventArgs e)
        {
            _main.UpdatePortfolio();
        }

        private void LoadInvestments()
        {
            try // remove this try catch create a midlesware
            {
                var investments = _investmentService.GetAllInvestments();


                dataGridView1.AutoGenerateColumns = false;
                dataGridView1.DataSource = investments;

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao carregar os investimentos: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}