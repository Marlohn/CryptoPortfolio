namespace CryptoPortfolio.WinForms.Forms
{
    partial class EditCryptoStatus
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
            textBox_currentValue = new TextBox();
            label5 = new Label();
            textBox_CryptoName = new TextBox();
            label2 = new Label();
            buttonSave = new Button();
            label1 = new Label();
            comboBox_Risk = new ComboBox();
            button_delete = new Button();
            SuspendLayout();
            // 
            // textBox_currentValue
            // 
            textBox_currentValue.Location = new Point(159, 50);
            textBox_currentValue.Name = "textBox_currentValue";
            textBox_currentValue.Size = new Size(213, 27);
            textBox_currentValue.TabIndex = 18;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(57, 53);
            label5.Name = "label5";
            label5.Size = new Size(96, 20);
            label5.TabIndex = 17;
            label5.Text = "CurrentValue:";
            // 
            // textBox_CryptoName
            // 
            textBox_CryptoName.Enabled = false;
            textBox_CryptoName.Location = new Point(159, 17);
            textBox_CryptoName.Name = "textBox_CryptoName";
            textBox_CryptoName.Size = new Size(213, 27);
            textBox_CryptoName.TabIndex = 12;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(53, 20);
            label2.Name = "label2";
            label2.Size = new Size(100, 20);
            label2.TabIndex = 11;
            label2.Text = "Crypto Name:";
            // 
            // buttonSave
            // 
            buttonSave.Location = new Point(195, 140);
            buttonSave.Name = "buttonSave";
            buttonSave.Size = new Size(177, 59);
            buttonSave.TabIndex = 19;
            buttonSave.Text = "Save";
            buttonSave.UseVisualStyleBackColor = true;
            buttonSave.Click += buttonSave_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(115, 86);
            label1.Name = "label1";
            label1.Size = new Size(38, 20);
            label1.TabIndex = 20;
            label1.Text = "Risk:";
            // 
            // comboBox_Risk
            // 
            comboBox_Risk.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox_Risk.FormattingEnabled = true;
            comboBox_Risk.Items.AddRange(new object[] { "Low", "Medium", "High", "Very High", "None" });
            comboBox_Risk.Location = new Point(159, 83);
            comboBox_Risk.Name = "comboBox_Risk";
            comboBox_Risk.Size = new Size(213, 28);
            comboBox_Risk.TabIndex = 21;
            // 
            // button_delete
            // 
            button_delete.BackColor = Color.Brown;
            button_delete.Location = new Point(12, 143);
            button_delete.Name = "button_delete";
            button_delete.Size = new Size(116, 59);
            button_delete.TabIndex = 22;
            button_delete.Text = "Delete";
            button_delete.UseVisualStyleBackColor = false;
            button_delete.Click += button_delete_Click;
            // 
            // EditCryptoStatus
            // 
            AcceptButton = buttonSave;
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(387, 214);
            Controls.Add(button_delete);
            Controls.Add(comboBox_Risk);
            Controls.Add(label1);
            Controls.Add(buttonSave);
            Controls.Add(textBox_currentValue);
            Controls.Add(label5);
            Controls.Add(textBox_CryptoName);
            Controls.Add(label2);
            FormBorderStyle = FormBorderStyle.FixedToolWindow;
            Name = "EditCryptoStatus";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "EditCryptoStatus";
            TopMost = true;
            Load += EditCryptoStatus_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox textBox_currentValue;
        private Label label5;
        private TextBox textBox_CryptoName;
        private Label label2;
        private Button buttonSave;
        private Label label1;
        private ComboBox comboBox_Risk;
        private Button button_delete;
    }
}