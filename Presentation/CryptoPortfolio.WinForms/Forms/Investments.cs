using Application.Interfaces;

namespace CryptoPortfolio.WinForms
{
    public partial class Investments : Form
    {
        private readonly IInvestmentService _investmentService;

        public Investments(IInvestmentService investmentService)
        {
            InitializeComponent();
            _investmentService = investmentService;
        }

        private void Investments_Load(object sender, EventArgs e)
        {
            LoadInvestments();
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