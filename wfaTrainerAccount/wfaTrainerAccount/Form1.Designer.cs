namespace wfaTrainerAccount
{
    partial class Form1
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
            label1 = new Label();
            tableLayoutPanel1 = new TableLayoutPanel();
            tableLayoutPanel2 = new TableLayoutPanel();
            label2 = new Label();
            button1 = new Button();
            button2 = new Button();
            label3 = new Label();
            label4 = new Label();
            tableLayoutPanel1.SuspendLayout();
            tableLayoutPanel2.SuspendLayout();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.BackColor = Color.FromArgb(128, 255, 128);
            label1.Dock = DockStyle.Fill;
            label1.Font = new Font("Segoe UI", 14F);
            label1.Location = new Point(3, 0);
            label1.Name = "label1";
            label1.Size = new Size(436, 125);
            label1.TabIndex = 0;
            label1.Text = "Верно = 0";
            label1.TextAlign = ContentAlignment.MiddleCenter;
            label1.Click += label1_Click;
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            tableLayoutPanel1.ColumnCount = 2;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.Controls.Add(label2, 1, 0);
            tableLayoutPanel1.Controls.Add(label1, 0, 0);
            tableLayoutPanel1.Location = new Point(12, 12);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 1;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.Size = new Size(884, 125);
            tableLayoutPanel1.TabIndex = 1;
            // 
            // tableLayoutPanel2
            // 
            tableLayoutPanel2.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            tableLayoutPanel2.ColumnCount = 2;
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel2.Controls.Add(button2, 1, 0);
            tableLayoutPanel2.Controls.Add(button1, 0, 0);
            tableLayoutPanel2.Location = new Point(12, 249);
            tableLayoutPanel2.Name = "tableLayoutPanel2";
            tableLayoutPanel2.RowCount = 1;
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel2.Size = new Size(882, 119);
            tableLayoutPanel2.TabIndex = 1;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.BackColor = Color.FromArgb(255, 128, 128);
            label2.Dock = DockStyle.Fill;
            label2.Font = new Font("Segoe UI", 14F);
            label2.Location = new Point(445, 0);
            label2.Name = "label2";
            label2.Size = new Size(436, 125);
            label2.TabIndex = 1;
            label2.Text = "Неверно = 0";
            label2.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // button1
            // 
            button1.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            button1.Font = new Font("Segoe UI", 25F);
            button1.ForeColor = Color.YellowGreen;
            button1.Location = new Point(3, 3);
            button1.Name = "button1";
            button1.Size = new Size(435, 113);
            button1.TabIndex = 2;
            button1.Text = "Да";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // button2
            // 
            button2.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            button2.Font = new Font("Segoe UI", 25F);
            button2.ForeColor = Color.FromArgb(255, 128, 128);
            button2.Location = new Point(444, 3);
            button2.Name = "button2";
            button2.Size = new Size(435, 113);
            button2.TabIndex = 3;
            button2.Text = "Нет";
            button2.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            label3.Font = new Font("Segoe UI", 20F);
            label3.Location = new Point(12, 140);
            label3.Name = "label3";
            label3.Size = new Size(881, 67);
            label3.TabIndex = 2;
            label3.Text = "10+11=21";
            label3.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label4
            // 
            label4.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            label4.Font = new Font("Segoe UI", 14F);
            label4.Location = new Point(12, 210);
            label4.Name = "label4";
            label4.Size = new Size(879, 39);
            label4.TabIndex = 3;
            label4.Text = "Верно?";
            label4.TextAlign = ContentAlignment.MiddleCenter;
            label4.Click += label4_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(908, 380);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(tableLayoutPanel1);
            Controls.Add(tableLayoutPanel2);
            MinimumSize = new Size(434, 427);
            Name = "Form1";
            Text = "Form1";
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel1.PerformLayout();
            tableLayoutPanel2.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Label label1;
        private TableLayoutPanel tableLayoutPanel1;
        private TableLayoutPanel tableLayoutPanel2;
        private Label label2;
        private Button button1;
        private Button button2;
        private Label label3;
        private Label label4;
    }
}
