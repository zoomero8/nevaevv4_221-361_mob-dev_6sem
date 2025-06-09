namespace wfaPaint1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            pictureBoxCanvas = new PictureBox();
            toolStrip1 = new ToolStrip();
            colorSelectButton = new ToolStripButton();
            toolStripLabel1 = new ToolStripLabel();
            thicknessComboBox = new ToolStripComboBox();
            toolStripSeparator1 = new ToolStripSeparator();
            pencilButton = new ToolStripButton();
            colorDialog1 = new ColorDialog();
            toolStripDropDownButton1 = new ToolStripDropDownButton();
            lineButton_Click = new ToolStripMenuItem();
            rectButton_Click = new ToolStripMenuItem();
            ellipseButton_Click = new ToolStripMenuItem();
            стрелкаToolStripMenuItem = new ToolStripMenuItem();
            звездаToolStripMenuItem = new ToolStripMenuItem();
            шестиугольникToolStripMenuItem = new ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)pictureBoxCanvas).BeginInit();
            toolStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // pictureBoxCanvas
            // 
            pictureBoxCanvas.Dock = DockStyle.Fill;
            pictureBoxCanvas.Location = new Point(0, 0);
            pictureBoxCanvas.Name = "pictureBoxCanvas";
            pictureBoxCanvas.Size = new Size(800, 450);
            pictureBoxCanvas.TabIndex = 0;
            pictureBoxCanvas.TabStop = false;
            pictureBoxCanvas.Paint += pictureBoxCanvas_Paint;
            pictureBoxCanvas.MouseMove += pictureBoxCanvas_MouseMove;
            pictureBoxCanvas.MouseUp += pictureBoxCanvas_MouseUp;
            // 
            // toolStrip1
            // 
            toolStrip1.ImageScalingSize = new Size(20, 20);
            toolStrip1.Items.AddRange(new ToolStripItem[] { colorSelectButton, toolStripLabel1, thicknessComboBox, toolStripSeparator1, pencilButton, toolStripDropDownButton1 });
            toolStrip1.Location = new Point(0, 0);
            toolStrip1.Name = "toolStrip1";
            toolStrip1.Size = new Size(800, 28);
            toolStrip1.TabIndex = 1;
            toolStrip1.Text = "toolStrip1";
            // 
            // colorSelectButton
            // 
            colorSelectButton.DisplayStyle = ToolStripItemDisplayStyle.Text;
            colorSelectButton.Image = (Image)resources.GetObject("colorSelectButton.Image");
            colorSelectButton.ImageTransparentColor = Color.Magenta;
            colorSelectButton.Name = "colorSelectButton";
            colorSelectButton.Size = new Size(46, 25);
            colorSelectButton.Text = "Цвет";
            colorSelectButton.Click += colorSelectButton_Click;
            // 
            // toolStripLabel1
            // 
            toolStripLabel1.Name = "toolStripLabel1";
            toolStripLabel1.Size = new Size(75, 25);
            toolStripLabel1.Text = "Толщина:";
            // 
            // thicknessComboBox
            // 
            thicknessComboBox.Name = "thicknessComboBox";
            thicknessComboBox.Size = new Size(121, 28);
            thicknessComboBox.SelectedIndexChanged += thicknessComboBox_SelectedIndexChanged;
            // 
            // toolStripSeparator1
            // 
            toolStripSeparator1.Name = "toolStripSeparator1";
            toolStripSeparator1.Size = new Size(6, 28);
            // 
            // pencilButton
            // 
            pencilButton.DisplayStyle = ToolStripItemDisplayStyle.Text;
            pencilButton.Image = (Image)resources.GetObject("pencilButton.Image");
            pencilButton.ImageTransparentColor = Color.Magenta;
            pencilButton.Name = "pencilButton";
            pencilButton.Size = new Size(84, 25);
            pencilButton.Text = "Карандаш";
            pencilButton.Click += pencilButton_Click;
            // 
            // toolStripDropDownButton1
            // 
            toolStripDropDownButton1.DisplayStyle = ToolStripItemDisplayStyle.Text;
            toolStripDropDownButton1.DropDownItems.AddRange(new ToolStripItem[] { lineButton_Click, rectButton_Click, ellipseButton_Click, стрелкаToolStripMenuItem, звездаToolStripMenuItem, шестиугольникToolStripMenuItem });
            toolStripDropDownButton1.Image = (Image)resources.GetObject("toolStripDropDownButton1.Image");
            toolStripDropDownButton1.ImageTransparentColor = Color.Magenta;
            toolStripDropDownButton1.Name = "toolStripDropDownButton1";
            toolStripDropDownButton1.Size = new Size(76, 25);
            toolStripDropDownButton1.Text = "Фигуры";
            // 
            // lineButton_Click
            // 
            lineButton_Click.Name = "lineButton_Click";
            lineButton_Click.Size = new Size(224, 26);
            lineButton_Click.Text = "Линия";
            lineButton_Click.Click += this.lineButton_Click_Click;
            // 
            // rectButton_Click
            // 
            rectButton_Click.Name = "rectButton_Click";
            rectButton_Click.Size = new Size(224, 26);
            rectButton_Click.Text = "Прямоугольник";
            // 
            // ellipseButton_Click
            // 
            ellipseButton_Click.Name = "ellipseButton_Click";
            ellipseButton_Click.Size = new Size(224, 26);
            ellipseButton_Click.Text = "Эллипс";
            // 
            // стрелкаToolStripMenuItem
            // 
            стрелкаToolStripMenuItem.Name = "стрелкаToolStripMenuItem";
            стрелкаToolStripMenuItem.Size = new Size(224, 26);
            стрелкаToolStripMenuItem.Text = "Стрелка";
            // 
            // звездаToolStripMenuItem
            // 
            звездаToolStripMenuItem.Name = "звездаToolStripMenuItem";
            звездаToolStripMenuItem.Size = new Size(224, 26);
            звездаToolStripMenuItem.Text = "Звезда";
            // 
            // шестиугольникToolStripMenuItem
            // 
            шестиугольникToolStripMenuItem.Name = "шестиугольникToolStripMenuItem";
            шестиугольникToolStripMenuItem.Size = new Size(224, 26);
            шестиугольникToolStripMenuItem.Text = "Шестиугольник";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(toolStrip1);
            Controls.Add(pictureBoxCanvas);
            Name = "Form1";
            Text = "Form1";
            Load += Form1_Load;
            ((System.ComponentModel.ISupportInitialize)pictureBoxCanvas).EndInit();
            toolStrip1.ResumeLayout(false);
            toolStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private PictureBox pictureBoxCanvas;
        private ToolStrip toolStrip1;
        private ToolStripButton colorSelectButton;
        private ToolStripComboBox thicknessComboBox;
        private ToolStripLabel toolStripLabel1;
        private ColorDialog colorDialog1;
        private ToolStripSeparator toolStripSeparator1;
        private ToolStripButton pencilButton;
        private ToolStripButton toolStripButton2;
        private ToolStripDropDownButton toolStripDropDownButton1;
        private ToolStripMenuItem lineButton_Click;
        private ToolStripMenuItem rectButton_Click;
        private ToolStripMenuItem ellipseButton_Click;
        private ToolStripMenuItem ellipseButton_Click;
        private ToolStripMenuItem rectButton_Click;
        private ToolStripMenuItem lineButton_Click;
        private ToolStripMenuItem rectButton_Click;
        private ToolStripMenuItem rectButton_Click;
        private ToolStripMenuItem ellipseButton_Click;
        private ToolStripMenuItem стрелкаToolStripMenuItem;
        private ToolStripMenuItem звездаToolStripMenuItem;
        private ToolStripMenuItem шестиугольникToolStripMenuItem;
        private ToolStripMenuItem lineButton_Click;
        private ToolStripMenuItem lineButton_Click;
    }
}
