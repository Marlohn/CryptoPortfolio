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
            SuspendLayout();
            // 
            // buttonAddInvestment
            // 
            buttonAddInvestment.Location = new Point(208, 162);
            buttonAddInvestment.Name = "buttonAddInvestment";
            buttonAddInvestment.Size = new Size(177, 59);
            buttonAddInvestment.TabIndex = 0;
            buttonAddInvestment.Text = "Add";
            buttonAddInvestment.UseVisualStyleBackColor = true;
            buttonAddInvestment.Click += buttonAddInvestment_Click;
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
            // AddInvestment
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(401, 233);
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
            Text = "AddInvestment";
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
    }
}