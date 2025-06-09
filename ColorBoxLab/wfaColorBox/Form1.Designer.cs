namespace wfaColorBox
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
            colorMapPanel = new TableLayoutPanel();
            colorButtonsPanel = new FlowLayoutPanel();
            btnRed = new Button();
            btnGreen = new Button();
            btnBlue = new Button();
            btnYellow = new Button();
            lblInstruction = new Label();
            colorButtonsPanel.SuspendLayout();
            SuspendLayout();
            // 
            // colorMapPanel
            // 
            colorMapPanel.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            colorMapPanel.ColumnCount = 5;
            colorMapPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20F));
            colorMapPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20F));
            colorMapPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20F));
            colorMapPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20F));
            colorMapPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20F));
            colorMapPanel.Location = new Point(124, 123);
            colorMapPanel.Name = "colorMapPanel";
            colorMapPanel.RowCount = 5;
            colorMapPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 20F));
            colorMapPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 20F));
            colorMapPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 20F));
            colorMapPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 20F));
            colorMapPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 20F));
            colorMapPanel.Size = new Size(521, 256);
            colorMapPanel.TabIndex = 0;
            // 
            // colorButtonsPanel
            // 
            colorButtonsPanel.AutoSize = true;
            colorButtonsPanel.Controls.Add(btnRed);
            colorButtonsPanel.Controls.Add(btnGreen);
            colorButtonsPanel.Controls.Add(btnBlue);
            colorButtonsPanel.Controls.Add(btnYellow);
            colorButtonsPanel.Dock = DockStyle.Bottom;
            colorButtonsPanel.Location = new Point(0, 477);
            colorButtonsPanel.Name = "colorButtonsPanel";
            colorButtonsPanel.Padding = new Padding(10);
            colorButtonsPanel.Size = new Size(782, 76);
            colorButtonsPanel.TabIndex = 1;
            // 
            // btnRed
            // 
            btnRed.BackColor = Color.FromArgb(255, 128, 128);
            btnRed.Location = new Point(13, 13);
            btnRed.Margin = new Padding(3, 3, 85, 3);
            btnRed.Name = "btnRed";
            btnRed.Size = new Size(120, 50);
            btnRed.TabIndex = 0;
            btnRed.Tag = "R";
            btnRed.Text = "Красный";
            btnRed.UseVisualStyleBackColor = false;
            // 
            // btnGreen
            // 
            btnGreen.BackColor = Color.FromArgb(128, 255, 128);
            btnGreen.Location = new Point(221, 13);
            btnGreen.Margin = new Padding(3, 3, 85, 3);
            btnGreen.Name = "btnGreen";
            btnGreen.Size = new Size(120, 50);
            btnGreen.TabIndex = 1;
            btnGreen.Tag = "G";
            btnGreen.Text = "Зеленый";
            btnGreen.UseVisualStyleBackColor = false;
            // 
            // btnBlue
            // 
            btnBlue.BackColor = Color.FromArgb(128, 128, 255);
            btnBlue.Location = new Point(429, 13);
            btnBlue.Margin = new Padding(3, 3, 85, 3);
            btnBlue.Name = "btnBlue";
            btnBlue.Size = new Size(120, 50);
            btnBlue.TabIndex = 2;
            btnBlue.Tag = "B";
            btnBlue.Text = "Синий";
            btnBlue.UseVisualStyleBackColor = false;
            // 
            // btnYellow
            // 
            btnYellow.BackColor = Color.FromArgb(255, 255, 128);
            btnYellow.Location = new Point(637, 13);
            btnYellow.Name = "btnYellow";
            btnYellow.Size = new Size(120, 50);
            btnYellow.TabIndex = 3;
            btnYellow.Tag = "Y";
            btnYellow.Text = "Желтый";
            btnYellow.UseVisualStyleBackColor = false;
            // 
            // lblInstruction
            // 
            lblInstruction.AutoSize = true;
            lblInstruction.Dock = DockStyle.Top;
            lblInstruction.Font = new Font("Arial Narrow", 22.2F, FontStyle.Bold, GraphicsUnit.Point, 204);
            lblInstruction.Location = new Point(0, 0);
            lblInstruction.Name = "lblInstruction";
            lblInstruction.Size = new Size(603, 43);
            lblInstruction.TabIndex = 2;
            lblInstruction.Text = "Посчитай, какого цвета больше всего:";
            lblInstruction.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(782, 553);
            Controls.Add(lblInstruction);
            Controls.Add(colorButtonsPanel);
            Controls.Add(colorMapPanel);
            Name = "Form1";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Цветовод (Color Master)";
            colorButtonsPanel.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TableLayoutPanel colorMapPanel;
        private FlowLayoutPanel colorButtonsPanel;
        private Label lblInstruction;
        private Button btnRed;
        private Button btnGreen;
        private Button btnBlue;
        private Button btnYellow;
    }
}
