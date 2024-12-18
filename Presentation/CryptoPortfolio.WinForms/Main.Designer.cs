namespace CryptoPortfolio.WinForms
{
    partial class Main
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea3 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend3 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series3 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea4 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend4 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series4 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Main));
            dataGridView1 = new DataGridView();
            CryptoName = new DataGridViewTextBoxColumn();
            TotalInvested = new DataGridViewTextBoxColumn();
            CurrentValue = new DataGridViewTextBoxColumn();
            Profit = new DataGridViewTextBoxColumn();
            ProfitPercentage = new DataGridViewTextBoxColumn();
            Risk = new DataGridViewTextBoxColumn();
            toolStrip1 = new ToolStrip();
            toolStripButton_NewInvestment = new ToolStripButton();
            toolStripButton_ViewInvestments = new ToolStripButton();
            toolStripLabel1 = new ToolStripLabel();
            toolStripButton_Settings = new ToolStripButton();
            chart_Risk = new System.Windows.Forms.DataVisualization.Charting.Chart();
            pictureBox1 = new PictureBox();
            label1 = new Label();
            label4 = new Label();
            comboBox_ChartFilterType = new ComboBox();
            chart_TotalCrypto = new System.Windows.Forms.DataVisualization.Charting.Chart();
            label_TotalProfit = new Label();
            label3 = new Label();
            label_TotalInvested = new Label();
            tabControl1 = new TabControl();
            tabPage1 = new TabPage();
            tabPage2 = new TabPage();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)chart_Risk).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)chart_TotalCrypto).BeginInit();
            tabControl1.SuspendLayout();
            tabPage1.SuspendLayout();
            tabPage2.SuspendLayout();
            SuspendLayout();
            // 
            // dataGridView1
            // 
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Columns.AddRange(new DataGridViewColumn[] { CryptoName, TotalInvested, CurrentValue, Profit, ProfitPercentage, Risk });
            dataGridView1.Location = new Point(12, 72);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.RowHeadersWidth = 51;
            dataGridView1.Size = new Size(804, 542);
            dataGridView1.TabIndex = 1;
            dataGridView1.CellFormatting += dataGridView1_CellFormatting;
            dataGridView1.CellValueChanged += dataGridView1_CellValueChanged;
            // 
            // CryptoName
            // 
            CryptoName.DataPropertyName = "CryptoName";
            CryptoName.HeaderText = "CryptoName";
            CryptoName.MinimumWidth = 6;
            CryptoName.Name = "CryptoName";
            CryptoName.Width = 125;
            // 
            // TotalInvested
            // 
            TotalInvested.DataPropertyName = "TotalInvested";
            TotalInvested.HeaderText = "TotalInvested";
            TotalInvested.MinimumWidth = 6;
            TotalInvested.Name = "TotalInvested";
            TotalInvested.Width = 125;
            // 
            // CurrentValue
            // 
            CurrentValue.DataPropertyName = "CurrentValue";
            CurrentValue.HeaderText = "CurrentValue";
            CurrentValue.MinimumWidth = 6;
            CurrentValue.Name = "CurrentValue";
            CurrentValue.Width = 125;
            // 
            // Profit
            // 
            Profit.DataPropertyName = "Profit";
            Profit.HeaderText = "Profit";
            Profit.MinimumWidth = 6;
            Profit.Name = "Profit";
            Profit.Width = 125;
            // 
            // ProfitPercentage
            // 
            ProfitPercentage.DataPropertyName = "ProfitPercentage";
            ProfitPercentage.HeaderText = "ProfitPercentage";
            ProfitPercentage.MinimumWidth = 6;
            ProfitPercentage.Name = "ProfitPercentage";
            ProfitPercentage.Width = 125;
            // 
            // Risk
            // 
            Risk.DataPropertyName = "Risk";
            Risk.HeaderText = "Risk";
            Risk.MinimumWidth = 6;
            Risk.Name = "Risk";
            Risk.Width = 125;
            // 
            // toolStrip1
            // 
            toolStrip1.ImageScalingSize = new Size(25, 25);
            toolStrip1.Items.AddRange(new ToolStripItem[] { toolStripButton_NewInvestment, toolStripButton_ViewInvestments, toolStripLabel1, toolStripButton_Settings });
            toolStrip1.Location = new Point(0, 0);
            toolStrip1.Name = "toolStrip1";
            toolStrip1.Size = new Size(825, 32);
            toolStrip1.TabIndex = 2;
            toolStrip1.Text = "toolStrip1";
            // 
            // toolStripButton_NewInvestment
            // 
            toolStripButton_NewInvestment.Image = Properties.Resources.plus_321;
            toolStripButton_NewInvestment.ImageTransparentColor = Color.Magenta;
            toolStripButton_NewInvestment.Name = "toolStripButton_NewInvestment";
            toolStripButton_NewInvestment.Size = new Size(144, 29);
            toolStripButton_NewInvestment.Text = "New investment";
            toolStripButton_NewInvestment.Click += toolStripButton_NewInvestment_Click;
            // 
            // toolStripButton_ViewInvestments
            // 
            toolStripButton_ViewInvestments.Image = Properties.Resources.list_6216;
            toolStripButton_ViewInvestments.ImageTransparentColor = Color.Magenta;
            toolStripButton_ViewInvestments.Name = "toolStripButton_ViewInvestments";
            toolStripButton_ViewInvestments.Size = new Size(152, 29);
            toolStripButton_ViewInvestments.Text = "View Investments";
            toolStripButton_ViewInvestments.Click += toolStripButton_ViewInvestments_Click;
            // 
            // toolStripLabel1
            // 
            toolStripLabel1.Alignment = ToolStripItemAlignment.Right;
            toolStripLabel1.Name = "toolStripLabel1";
            toolStripLabel1.Size = new Size(73, 29);
            toolStripLabel1.Text = "                ";
            // 
            // toolStripButton_Settings
            // 
            toolStripButton_Settings.Alignment = ToolStripItemAlignment.Right;
            toolStripButton_Settings.Image = Properties.Resources.settings_5666;
            toolStripButton_Settings.ImageTransparentColor = Color.Magenta;
            toolStripButton_Settings.Name = "toolStripButton_Settings";
            toolStripButton_Settings.Size = new Size(91, 29);
            toolStripButton_Settings.Text = "Settings";
            // 
            // chart_Risk
            // 
            chartArea3.Name = "ChartArea1";
            chart_Risk.ChartAreas.Add(chartArea3);
            legend3.Name = "Legend1";
            chart_Risk.Legends.Add(legend3);
            chart_Risk.Location = new Point(6, 6);
            chart_Risk.Name = "chart_Risk";
            series3.ChartArea = "ChartArea1";
            series3.Legend = "Legend1";
            series3.Name = "Series1";
            chart_Risk.Series.Add(series3);
            chart_Risk.Size = new Size(785, 400);
            chart_Risk.TabIndex = 3;
            chart_Risk.Text = "chart1";
            // 
            // pictureBox1
            // 
            pictureBox1.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            pictureBox1.Image = Properties.Resources.coin_statistics_2476;
            pictureBox1.Location = new Point(751, 2);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(70, 70);
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox1.TabIndex = 4;
            pictureBox1.TabStop = false;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 12F);
            label1.Location = new Point(12, 36);
            label1.Name = "label1";
            label1.Size = new Size(136, 28);
            label1.TabIndex = 5;
            label1.Text = "Total Invested:";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(595, 623);
            label4.Name = "label4";
            label4.Size = new Size(65, 20);
            label4.TabIndex = 11;
            label4.Text = "Filter by:";
            // 
            // comboBox_ChartFilterType
            // 
            comboBox_ChartFilterType.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox_ChartFilterType.FormattingEnabled = true;
            comboBox_ChartFilterType.Items.AddRange(new object[] { "TotalInvested", "CurrentValue", "Profit" });
            comboBox_ChartFilterType.Location = new Point(666, 620);
            comboBox_ChartFilterType.Name = "comboBox_ChartFilterType";
            comboBox_ChartFilterType.Size = new Size(151, 28);
            comboBox_ChartFilterType.TabIndex = 10;
            comboBox_ChartFilterType.SelectedIndexChanged += comboBox_ChartFilterType_SelectedIndexChanged;
            // 
            // chart_TotalCrypto
            // 
            chartArea4.Name = "ChartArea1";
            chart_TotalCrypto.ChartAreas.Add(chartArea4);
            legend4.Name = "Legend1";
            chart_TotalCrypto.Legends.Add(legend4);
            chart_TotalCrypto.Location = new Point(6, 6);
            chart_TotalCrypto.Name = "chart_TotalCrypto";
            series4.ChartArea = "ChartArea1";
            series4.Legend = "Legend1";
            series4.Name = "Series1";
            chart_TotalCrypto.Series.Add(series4);
            chart_TotalCrypto.Size = new Size(785, 400);
            chart_TotalCrypto.TabIndex = 9;
            chart_TotalCrypto.Text = "chart1";
            // 
            // label_TotalProfit
            // 
            label_TotalProfit.AutoSize = true;
            label_TotalProfit.Font = new Font("Segoe UI", 12F);
            label_TotalProfit.Location = new Point(400, 36);
            label_TotalProfit.Name = "label_TotalProfit";
            label_TotalProfit.Size = new Size(23, 28);
            label_TotalProfit.TabIndex = 8;
            label_TotalProfit.Text = "0";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 12F);
            label3.Location = new Point(283, 36);
            label3.Name = "label3";
            label3.Size = new Size(111, 28);
            label3.TabIndex = 7;
            label3.Text = "Total Profit:";
            // 
            // label_TotalInvested
            // 
            label_TotalInvested.AutoSize = true;
            label_TotalInvested.Font = new Font("Segoe UI", 12F);
            label_TotalInvested.Location = new Point(154, 36);
            label_TotalInvested.Name = "label_TotalInvested";
            label_TotalInvested.Size = new Size(23, 28);
            label_TotalInvested.TabIndex = 6;
            label_TotalInvested.Text = "0";
            // 
            // tabControl1
            // 
            tabControl1.Controls.Add(tabPage1);
            tabControl1.Controls.Add(tabPage2);
            tabControl1.Location = new Point(12, 620);
            tabControl1.Name = "tabControl1";
            tabControl1.SelectedIndex = 0;
            tabControl1.Size = new Size(805, 446);
            tabControl1.TabIndex = 12;
            // 
            // tabPage1
            // 
            tabPage1.Controls.Add(chart_TotalCrypto);
            tabPage1.Location = new Point(4, 29);
            tabPage1.Name = "tabPage1";
            tabPage1.Padding = new Padding(3);
            tabPage1.Size = new Size(797, 413);
            tabPage1.TabIndex = 0;
            tabPage1.Text = "Crypto";
            tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            tabPage2.Controls.Add(chart_Risk);
            tabPage2.Location = new Point(4, 29);
            tabPage2.Name = "tabPage2";
            tabPage2.Padding = new Padding(3);
            tabPage2.Size = new Size(797, 413);
            tabPage2.TabIndex = 1;
            tabPage2.Text = "Risk";
            tabPage2.UseVisualStyleBackColor = true;
            // 
            // Main
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(825, 1073);
            Controls.Add(pictureBox1);
            Controls.Add(label4);
            Controls.Add(comboBox_ChartFilterType);
            Controls.Add(tabControl1);
            Controls.Add(label_TotalProfit);
            Controls.Add(label3);
            Controls.Add(label_TotalInvested);
            Controls.Add(label1);
            Controls.Add(toolStrip1);
            Controls.Add(dataGridView1);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            Name = "Main";
            Text = "Crypto Portfolio";
            Load += Main_Load;
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            toolStrip1.ResumeLayout(false);
            toolStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)chart_Risk).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ((System.ComponentModel.ISupportInitialize)chart_TotalCrypto).EndInit();
            tabControl1.ResumeLayout(false);
            tabPage1.ResumeLayout(false);
            tabPage2.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DataGridView dataGridView1;
        private ToolStrip toolStrip1;
        private ToolStripButton toolStripButton_NewInvestment;
        private ToolStripButton toolStripButton_ViewInvestments;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart_Risk;
        private ToolStripButton toolStripButton_Settings;
        private PictureBox pictureBox1;
        private DataGridViewTextBoxColumn CryptoName;
        private DataGridViewTextBoxColumn TotalInvested;
        private DataGridViewTextBoxColumn CurrentValue;
        private DataGridViewTextBoxColumn Profit;
        private DataGridViewTextBoxColumn ProfitPercentage;
        private DataGridViewTextBoxColumn Risk;
        private Label label1;
        private Label label_TotalInvested;
        private Label label_TotalProfit;
        private Label label3;
        private Label label4;
        private ComboBox comboBox_ChartFilterType;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart_TotalCrypto;
        private TabControl tabControl1;
        private TabPage tabPage1;
        private TabPage tabPage2;
        private ToolStripLabel toolStripLabel1;
    }
}
