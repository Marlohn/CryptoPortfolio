using Application.Interfaces;

namespace CryptoPortfolio.WinForms
{
    public partial class Investments : Form
    {
        private readonly IInvestmentsService _investmentService;
        private readonly Main _main;

        public Investments(IInvestmentsService investmentService, Main main)
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
                var investments = _investmentService.GetAll();


                dataGridView1.AutoGenerateColumns = false;
                dataGridView1.DataSource = investments;

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading investments: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}