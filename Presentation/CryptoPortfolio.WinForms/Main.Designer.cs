﻿namespace CryptoPortfolio.WinForms
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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea2 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend2 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
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
            toolStripButton_Settings = new ToolStripButton();
            chart1 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            pictureBox1 = new PictureBox();
            label1 = new Label();
            groupBox1 = new GroupBox();
            label_TotalInvested = new Label();
            chart2 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)chart1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)chart2).BeginInit();
            SuspendLayout();
            // 
            // dataGridView1
            // 
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Columns.AddRange(new DataGridViewColumn[] { CryptoName, TotalInvested, CurrentValue, Profit, ProfitPercentage, Risk });
            dataGridView1.Location = new Point(12, 35);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.RowHeadersWidth = 51;
            dataGridView1.Size = new Size(804, 607);
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
            toolStrip1.Items.AddRange(new ToolStripItem[] { toolStripButton_NewInvestment, toolStripButton_ViewInvestments, toolStripButton_Settings });
            toolStrip1.Location = new Point(0, 0);
            toolStrip1.Name = "toolStrip1";
            toolStrip1.Size = new Size(1331, 32);
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
            // toolStripButton_Settings
            // 
            toolStripButton_Settings.Alignment = ToolStripItemAlignment.Right;
            toolStripButton_Settings.Image = Properties.Resources.settings_5666;
            toolStripButton_Settings.ImageTransparentColor = Color.Magenta;
            toolStripButton_Settings.Name = "toolStripButton_Settings";
            toolStripButton_Settings.Size = new Size(91, 29);
            toolStripButton_Settings.Text = "Settings";
            // 
            // chart1
            // 
            chartArea1.Name = "ChartArea1";
            chart1.ChartAreas.Add(chartArea1);
            legend1.Name = "Legend1";
            chart1.Legends.Add(legend1);
            chart1.Location = new Point(18, 92);
            chart1.Name = "chart1";
            series1.ChartArea = "ChartArea1";
            series1.Legend = "Legend1";
            series1.Name = "Series1";
            chart1.Series.Add(series1);
            chart1.Size = new Size(329, 205);
            chart1.TabIndex = 3;
            chart1.Text = "chart1";
            // 
            // pictureBox1
            // 
            pictureBox1.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            pictureBox1.Image = Properties.Resources.coin_statistics_2476;
            pictureBox1.Location = new Point(391, 16);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(100, 100);
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox1.TabIndex = 4;
            pictureBox1.TabStop = false;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 12F);
            label1.Location = new Point(22, 34);
            label1.Name = "label1";
            label1.Size = new Size(136, 28);
            label1.TabIndex = 5;
            label1.Text = "Total invested:";
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(chart2);
            groupBox1.Controls.Add(label_TotalInvested);
            groupBox1.Controls.Add(label1);
            groupBox1.Controls.Add(chart1);
            groupBox1.Controls.Add(pictureBox1);
            groupBox1.Location = new Point(822, 35);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(497, 608);
            groupBox1.TabIndex = 6;
            groupBox1.TabStop = false;
            groupBox1.Text = "Wallet Details:";
            // 
            // label_TotalInvested
            // 
            label_TotalInvested.AutoSize = true;
            label_TotalInvested.Font = new Font("Segoe UI", 12F);
            label_TotalInvested.Location = new Point(164, 34);
            label_TotalInvested.Name = "label_TotalInvested";
            label_TotalInvested.Size = new Size(23, 28);
            label_TotalInvested.TabIndex = 6;
            label_TotalInvested.Text = "0";
            // 
            // chart2
            // 
            chartArea2.Name = "ChartArea1";
            chart2.ChartAreas.Add(chartArea2);
            legend2.Name = "Legend1";
            chart2.Legends.Add(legend2);
            chart2.Location = new Point(0, 378);
            chart2.Name = "chart2";
            series2.ChartArea = "ChartArea1";
            series2.Legend = "Legend1";
            series2.Name = "Series1";
            chart2.Series.Add(series2);
            chart2.Size = new Size(347, 230);
            chart2.TabIndex = 7;
            chart2.Text = "chart2";
            // 
            // Main
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1331, 655);
            Controls.Add(groupBox1);
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
            ((System.ComponentModel.ISupportInitialize)chart1).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)chart2).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DataGridView dataGridView1;
        private ToolStrip toolStrip1;
        private ToolStripButton toolStripButton_NewInvestment;
        private ToolStripButton toolStripButton_ViewInvestments;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart1;
        private ToolStripButton toolStripButton_Settings;
        private PictureBox pictureBox1;
        private DataGridViewTextBoxColumn CryptoName;
        private DataGridViewTextBoxColumn TotalInvested;
        private DataGridViewTextBoxColumn CurrentValue;
        private DataGridViewTextBoxColumn Profit;
        private DataGridViewTextBoxColumn ProfitPercentage;
        private DataGridViewTextBoxColumn Risk;
        private Label label1;
        private GroupBox groupBox1;
        private Label label_TotalInvested;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart2;
    }
}
