namespace CryptoPortfolio.WinForms.Forms
{
    partial class AddInvestment
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
            buttonAddInvestment = new Button();
            label1 = new Label();
            textBox_Date = new TextBox();
            textBox_CryptoName = new TextBox();
            label2 = new Label();
            textBox_InvestedValue = new TextBox();
            label3 = new Label();
            textBox_Notes = new TextBox();
            label4 = new Label();
            textBox_currentValue = new TextBox();
            label5 = new Label();
            comboBox_Risk = new ComboBox();
            label6 = new Label();
            label7 = new Label();
            CheckBox_AutoDecreaseBTC = new CheckBox();
            CheckBox_AutoDecreaseUSDT = new CheckBox();
            CheckBox_AutoDecreaseNA = new CheckBox();
            SuspendLayout();
            // 
            // buttonAddInvestment
            // 
            buttonAddInvestment.Location = new Point(208, 264);
            buttonAddInvestment.Name = "buttonAddInvestment";
            buttonAddInvestment.Size = new Size(177, 59);
            buttonAddInvestment.TabIndex = 0;
            buttonAddInvestment.Text = "Add";
            buttonAddInvestment.UseVisualStyleBackColor = true;
            buttonAddInvestment.Click += ButtonAddInvestment_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(122, 19);
            label1.Name = "label1";
            label1.Size = new Size(44, 20);
            label1.TabIndex = 1;
            label1.Text = "Date:";
            // 
            // textBox_Date
            // 
            textBox_Date.Location = new Point(172, 16);
            textBox_Date.Name = "textBox_Date";
            textBox_Date.Size = new Size(213, 27);
            textBox_Date.TabIndex = 2;
            // 
            // textBox_CryptoName
            // 
            textBox_CryptoName.Location = new Point(172, 49);
            textBox_CryptoName.Name = "textBox_CryptoName";
            textBox_CryptoName.Size = new Size(213, 27);
            textBox_CryptoName.TabIndex = 4;
            textBox_CryptoName.TextChanged += TextBox_CryptoName_TextChanged;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(66, 52);
            label2.Name = "label2";
            label2.Size = new Size(100, 20);
            label2.TabIndex = 3;
            label2.Text = "Crypto Name:";
            // 
            // textBox_InvestedValue
            // 
            textBox_InvestedValue.Location = new Point(172, 82);
            textBox_InvestedValue.Name = "textBox_InvestedValue";
            textBox_InvestedValue.Size = new Size(213, 27);
            textBox_InvestedValue.TabIndex = 6;
            textBox_InvestedValue.TextChanged += TextBox_InvestedValue_TextChanged;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(19, 89);
            label3.Name = "label3";
            label3.Size = new Size(150, 20);
            label3.TabIndex = 5;
            label3.Text = "Invested Value (USD):";
            // 
            // textBox_Notes
            // 
            textBox_Notes.Location = new Point(172, 115);
            textBox_Notes.Name = "textBox_Notes";
            textBox_Notes.Size = new Size(213, 27);
            textBox_Notes.TabIndex = 8;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(115, 118);
            label4.Name = "label4";
            label4.Size = new Size(51, 20);
            label4.TabIndex = 7;
            label4.Text = "Notes:";
            // 
            // textBox_currentValue
            // 
            textBox_currentValue.Location = new Point(172, 148);
            textBox_currentValue.Name = "textBox_currentValue";
            textBox_currentValue.Size = new Size(213, 27);
            textBox_currentValue.TabIndex = 10;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(70, 151);
            label5.Name = "label5";
            label5.Size = new Size(96, 20);
            label5.TabIndex = 9;
            label5.Text = "CurrentValue:";
            // 
            // comboBox_Risk
            // 
            comboBox_Risk.DisplayMember = "None";
            comboBox_Risk.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox_Risk.FormattingEnabled = true;
            comboBox_Risk.Items.AddRange(new object[] { "Low", "Medium", "High", "Very High", "None" });
            comboBox_Risk.Location = new Point(172, 181);
            comboBox_Risk.Name = "comboBox_Risk";
            comboBox_Risk.Size = new Size(213, 28);
            comboBox_Risk.TabIndex = 23;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(128, 184);
            label6.Name = "label6";
            label6.Size = new Size(38, 20);
            label6.TabIndex = 22;
            label6.Text = "Risk:";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(23, 216);
            label7.Name = "label7";
            label7.Size = new Size(143, 20);
            label7.TabIndex = 24;
            label7.Text = "Auto decrease from:";
            // 
            // CheckBox_AutoDecreaseBTC
            // 
            CheckBox_AutoDecreaseBTC.AutoSize = true;
            CheckBox_AutoDecreaseBTC.Location = new Point(173, 215);
            CheckBox_AutoDecreaseBTC.Name = "CheckBox_AutoDecreaseBTC";
            CheckBox_AutoDecreaseBTC.Size = new Size(55, 24);
            CheckBox_AutoDecreaseBTC.TabIndex = 25;
            CheckBox_AutoDecreaseBTC.Text = "BTC";
            CheckBox_AutoDecreaseBTC.UseVisualStyleBackColor = true;
            // 
            // CheckBox_AutoDecreaseUSDT
            // 
            CheckBox_AutoDecreaseUSDT.AutoSize = true;
            CheckBox_AutoDecreaseUSDT.Location = new Point(234, 215);
            CheckBox_AutoDecreaseUSDT.Name = "CheckBox_AutoDecreaseUSDT";
            CheckBox_AutoDecreaseUSDT.Size = new Size(67, 24);
            CheckBox_AutoDecreaseUSDT.TabIndex = 26;
            CheckBox_AutoDecreaseUSDT.Text = "USDT";
            CheckBox_AutoDecreaseUSDT.UseVisualStyleBackColor = true;
            // 
            // CheckBox_AutoDecreaseNA
            // 
            CheckBox_AutoDecreaseNA.AutoSize = true;
            CheckBox_AutoDecreaseNA.Checked = true;
            CheckBox_AutoDecreaseNA.CheckState = CheckState.Checked;
            CheckBox_AutoDecreaseNA.Location = new Point(307, 215);
            CheckBox_AutoDecreaseNA.Name = "CheckBox_AutoDecreaseNA";
            CheckBox_AutoDecreaseNA.Size = new Size(58, 24);
            CheckBox_AutoDecreaseNA.TabIndex = 27;
            CheckBox_AutoDecreaseNA.Text = "N/A";
            CheckBox_AutoDecreaseNA.UseVisualStyleBackColor = true;
            // 
            // AddInvestment
            // 
            AcceptButton = buttonAddInvestment;
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(401, 336);
            Controls.Add(CheckBox_AutoDecreaseNA);
            Controls.Add(CheckBox_AutoDecreaseUSDT);
            Controls.Add(CheckBox_AutoDecreaseBTC);
            Controls.Add(label7);
            Controls.Add(comboBox_Risk);
            Controls.Add(label6);
            Controls.Add(textBox_currentValue);
            Controls.Add(label5);
            Controls.Add(textBox_Notes);
            Controls.Add(label4);
            Controls.Add(textBox_InvestedValue);
            Controls.Add(label3);
            Controls.Add(textBox_CryptoName);
            Controls.Add(label2);
            Controls.Add(textBox_Date);
            Controls.Add(label1);
            Controls.Add(buttonAddInvestment);
            FormBorderStyle = FormBorderStyle.FixedToolWindow;
            Name = "AddInvestment";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "AddInvestment";
            TopMost = true;
            FormClosing += AddInvestment_FormClosing;
            Load += AddInvestment_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button buttonAddInvestment;
        private Label label1;
        private TextBox textBox_Date;
        private TextBox textBox_CryptoName;
        private Label label2;
        private TextBox textBox_InvestedValue;
        private Label label3;
        private TextBox textBox_Notes;
        private Label label4;
        private TextBox textBox_currentValue;
        private Label label5;
        private ComboBox comboBox_Risk;
        private Label label6;
        private Label label7;
        private CheckBox CheckBox_AutoDecreaseBTC;
        private CheckBox CheckBox_AutoDecreaseUSDT;
        private CheckBox CheckBox_AutoDecreaseNA;
    }
}