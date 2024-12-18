namespace CryptoPortfolio.WinForms
{
    partial class Investments
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            dataGridView1 = new DataGridView();
            Date = new DataGridViewTextBoxColumn();
            CryptoName = new DataGridViewTextBoxColumn();
            InvestedValue = new DataGridViewTextBoxColumn();
            Notes = new DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            SuspendLayout();
            // 
            // dataGridView1
            // 
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Columns.AddRange(new DataGridViewColumn[] { Date, CryptoName, InvestedValue, Notes });
            dataGridView1.Location = new Point(10, 11);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.RowHeadersWidth = 51;
            dataGridView1.Size = new Size(554, 549);
            dataGridView1.TabIndex = 0;
            // 
            // Date
            // 
            Date.DataPropertyName = "Date";
            Date.HeaderText = "Date";
            Date.MinimumWidth = 6;
            Date.Name = "Date";
            Date.Width = 125;
            // 
            // CryptoName
            // 
            CryptoName.DataPropertyName = "CryptoName";
            CryptoName.HeaderText = "CryptoName";
            CryptoName.MinimumWidth = 6;
            CryptoName.Name = "CryptoName";
            CryptoName.Width = 125;
            // 
            // InvestedValue
            // 
            InvestedValue.DataPropertyName = "InvestedValue";
            InvestedValue.HeaderText = "InvestedValue";
            InvestedValue.MinimumWidth = 6;
            InvestedValue.Name = "InvestedValue";
            InvestedValue.Width = 125;
            // 
            // Notes
            // 
            Notes.DataPropertyName = "Notes";
            Notes.HeaderText = "Notes";
            Notes.MinimumWidth = 6;
            Notes.Name = "Notes";
            Notes.Width = 125;
            // 
            // Investments
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(571, 565);
            Controls.Add(dataGridView1);
            FormBorderStyle = FormBorderStyle.FixedToolWindow;
            MaximizeBox = false;
            Name = "Investments";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Investments";
            FormClosing += Investments_FormClosing;
            Load += Investments_Load;
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private DataGridView dataGridView1;
        private DataGridViewTextBoxColumn Date;
        private DataGridViewTextBoxColumn CryptoName;
        private DataGridViewTextBoxColumn InvestedValue;
        private DataGridViewTextBoxColumn Notes;
    }
}