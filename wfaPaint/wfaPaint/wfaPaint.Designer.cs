using System.Windows.Forms;

namespace wfaPaint
{
    partial class wfaPaint
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
            pencilButton = new Button();
            panel1 = new Panel();
            panel2 = new Panel();
            panel3 = new Panel();
            panel4 = new Panel();
            panel5 = new Panel();
            panel6 = new Panel();
            panel7 = new Panel();
            panel8 = new Panel();
            panel9 = new Panel();
            image = new PictureBox();
            richTextBox1 = new RichTextBox();
            trackBarLineWidth = new TrackBar();
            lineButton = new Button();
            triangleButton = new Button();
            rectangleButton = new Button();
            ellipsButton = new Button();
            hexagonButton = new Button();
            starButton = new Button();
            arrowButton = new Button();
            selectToolButton = new Button();
            myColor = new Panel();
            statusStrip = new StatusStrip();
            toolStripCoordinatesLabel = new ToolStripStatusLabel();
            toolStripPixelColorLabel = new ToolStripStatusLabel();
            toolStripImageSizeLabel = new ToolStripStatusLabel();
            toolStrip1 = new ToolStrip();
            ((System.ComponentModel.ISupportInitialize)image).BeginInit();
            image.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)trackBarLineWidth).BeginInit();
            statusStrip.SuspendLayout();
            SuspendLayout();
            // 
            // pencilButton
            // 
            pencilButton.Location = new Point(12, 133);
            pencilButton.Name = "pencilButton";
            pencilButton.Size = new Size(130, 29);
            pencilButton.TabIndex = 0;
            pencilButton.Text = "Карандаш";
            pencilButton.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            panel1.BackColor = Color.Black;
            panel1.Location = new Point(161, 39);
            panel1.Name = "panel1";
            panel1.Size = new Size(30, 20);
            panel1.TabIndex = 1;
            // 
            // panel2
            // 
            panel2.BackColor = Color.White;
            panel2.BorderStyle = BorderStyle.FixedSingle;
            panel2.Location = new Point(201, 39);
            panel2.Name = "panel2";
            panel2.Size = new Size(30, 20);
            panel2.TabIndex = 2;
            // 
            // panel3
            // 
            panel3.BackColor = Color.Red;
            panel3.Location = new Point(241, 39);
            panel3.Name = "panel3";
            panel3.Size = new Size(30, 20);
            panel3.TabIndex = 3;
            // 
            // panel4
            // 
            panel4.BackColor = Color.Lime;
            panel4.Location = new Point(161, 69);
            panel4.Name = "panel4";
            panel4.Size = new Size(30, 20);
            panel4.TabIndex = 4;
            // 
            // panel5
            // 
            panel5.BackColor = Color.Blue;
            panel5.Location = new Point(201, 69);
            panel5.Name = "panel5";
            panel5.Size = new Size(30, 20);
            panel5.TabIndex = 5;
            // 
            // panel6
            // 
            panel6.BackColor = Color.Yellow;
            panel6.Location = new Point(241, 69);
            panel6.Name = "panel6";
            panel6.Size = new Size(30, 20);
            panel6.TabIndex = 6;
            // 
            // panel7
            // 
            panel7.BackColor = Color.Fuchsia;
            panel7.Location = new Point(161, 99);
            panel7.Name = "panel7";
            panel7.Size = new Size(30, 20);
            panel7.TabIndex = 7;
            // 
            // panel8
            // 
            panel8.BackColor = Color.Cyan;
            panel8.Location = new Point(201, 99);
            panel8.Name = "panel8";
            panel8.Size = new Size(30, 20);
            panel8.TabIndex = 8;
            // 
            // panel9
            // 
            panel9.BackColor = Color.Gray;
            panel9.Location = new Point(241, 99);
            panel9.Name = "panel9";
            panel9.Size = new Size(30, 20);
            panel9.TabIndex = 9;
            // 
            // image
            // 
            image.BorderStyle = BorderStyle.FixedSingle;
            image.Controls.Add(richTextBox1);
            image.Location = new Point(287, 28);
            image.Name = "image";
            image.Size = new Size(683, 609);
            image.TabIndex = 18;
            image.TabStop = false;
            // 
            // richTextBox1
            // 
            richTextBox1.Location = new Point(760, 4);
            richTextBox1.Name = "richTextBox1";
            richTextBox1.Size = new Size(0, 25);
            richTextBox1.TabIndex = 0;
            richTextBox1.Text = "";
            // 
            // trackBarLineWidth
            // 
            trackBarLineWidth.Location = new Point(12, 33);
            trackBarLineWidth.Maximum = 15;
            trackBarLineWidth.Name = "trackBarLineWidth";
            trackBarLineWidth.Size = new Size(101, 56);
            trackBarLineWidth.TabIndex = 19;
            // 
            // lineButton
            // 
            lineButton.Location = new Point(12, 168);
            lineButton.Name = "lineButton";
            lineButton.Size = new Size(130, 29);
            lineButton.TabIndex = 20;
            lineButton.Text = "Линия";
            lineButton.UseVisualStyleBackColor = true;
            // 
            // triangleButton
            // 
            triangleButton.Location = new Point(12, 217);
            triangleButton.Name = "triangleButton";
            triangleButton.Size = new Size(130, 29);
            triangleButton.TabIndex = 21;
            triangleButton.Text = "Треугольник";
            triangleButton.UseVisualStyleBackColor = true;
            // 
            // rectangleButton
            // 
            rectangleButton.Location = new Point(12, 252);
            rectangleButton.Name = "rectangleButton";
            rectangleButton.Size = new Size(130, 29);
            rectangleButton.TabIndex = 22;
            rectangleButton.Text = "Прямоугольник";
            rectangleButton.UseVisualStyleBackColor = true;
            // 
            // ellipsButton
            // 
            ellipsButton.Location = new Point(12, 287);
            ellipsButton.Name = "ellipsButton";
            ellipsButton.Size = new Size(130, 29);
            ellipsButton.TabIndex = 23;
            ellipsButton.Text = "Эллипс";
            ellipsButton.UseVisualStyleBackColor = true;
            // 
            // hexagonButton
            // 
            hexagonButton.Location = new Point(151, 217);
            hexagonButton.Name = "hexagonButton";
            hexagonButton.Size = new Size(130, 29);
            hexagonButton.TabIndex = 24;
            hexagonButton.Text = "Шестиугольник";
            hexagonButton.UseVisualStyleBackColor = true;
            // 
            // starButton
            // 
            starButton.Location = new Point(151, 252);
            starButton.Name = "starButton";
            starButton.Size = new Size(130, 29);
            starButton.TabIndex = 25;
            starButton.Text = "Звезда";
            starButton.UseVisualStyleBackColor = true;
            // 
            // arrowButton
            // 
            arrowButton.Location = new Point(151, 287);
            arrowButton.Name = "arrowButton";
            arrowButton.Size = new Size(130, 29);
            arrowButton.TabIndex = 25;
            arrowButton.Text = "Стрелочка";
            arrowButton.UseVisualStyleBackColor = true;
            // 
            // selectToolButton
            // 
            selectToolButton.Location = new Point(151, 133);
            selectToolButton.Name = "selectToolButton";
            selectToolButton.Size = new Size(130, 29);
            selectToolButton.TabIndex = 25;
            selectToolButton.Text = "Выделить";
            selectToolButton.UseVisualStyleBackColor = true;
            // 
            // myColor
            // 
            myColor.BackColor = SystemColors.ActiveCaptionText;
            myColor.BorderStyle = BorderStyle.FixedSingle;
            myColor.Location = new Point(112, 39);
            myColor.Name = "myColor";
            myColor.Size = new Size(30, 20);
            myColor.TabIndex = 14;
            // 
            // statusStrip
            // 
            statusStrip.ImageScalingSize = new Size(20, 20);
            statusStrip.Items.AddRange(new ToolStripItem[] { toolStripCoordinatesLabel, toolStripPixelColorLabel, toolStripImageSizeLabel });
            statusStrip.Location = new Point(0, 640);
            statusStrip.Name = "statusStrip";
            statusStrip.Size = new Size(982, 30);
            statusStrip.TabIndex = 26;
            statusStrip.Text = "statusStrip";
            // 
            // toolStripCoordinatesLabel
            // 
            toolStripCoordinatesLabel.Name = "toolStripCoordinatesLabel";
            toolStripCoordinatesLabel.Size = new Size(63, 24);
            toolStripCoordinatesLabel.Text = "X: 0, Y: 0";
            // 
            // toolStripPixelColorLabel
            // 
            toolStripPixelColorLabel.BorderSides = ToolStripStatusLabelBorderSides.Left;
            toolStripPixelColorLabel.BorderStyle = Border3DStyle.Etched;
            toolStripPixelColorLabel.Name = "toolStripPixelColorLabel";
            toolStripPixelColorLabel.Size = new Size(66, 24);
            toolStripPixelColorLabel.Text = "RGB: ---";
            // 
            // toolStripImageSizeLabel
            // 
            toolStripImageSizeLabel.BorderSides = ToolStripStatusLabelBorderSides.Left;
            toolStripImageSizeLabel.BorderStyle = Border3DStyle.Etched;
            toolStripImageSizeLabel.Name = "toolStripImageSizeLabel";
            toolStripImageSizeLabel.Size = new Size(838, 24);
            toolStripImageSizeLabel.Spring = true;
            toolStripImageSizeLabel.Text = "Size: ---";
            toolStripImageSizeLabel.TextAlign = ContentAlignment.MiddleRight;
            // 
            // toolStrip1
            // 
            toolStrip1.ImageScalingSize = new Size(20, 20);
            toolStrip1.Location = new Point(0, 0);
            toolStrip1.Name = "toolStrip1";
            toolStrip1.Size = new Size(982, 25);
            toolStrip1.TabIndex = 27;
            toolStrip1.Text = "toolStrip1";
            // 
            // wfaPaint
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.Window;
            ClientSize = new Size(982, 670);
            Controls.Add(toolStrip1);
            Controls.Add(myColor);
            Controls.Add(starButton);
            Controls.Add(arrowButton);
            Controls.Add(selectToolButton);
            Controls.Add(hexagonButton);
            Controls.Add(ellipsButton);
            Controls.Add(rectangleButton);
            Controls.Add(triangleButton);
            Controls.Add(lineButton);
            Controls.Add(trackBarLineWidth);
            Controls.Add(image);
            Controls.Add(panel1);
            Controls.Add(panel2);
            Controls.Add(panel3);
            Controls.Add(panel4);
            Controls.Add(panel5);
            Controls.Add(panel6);
            Controls.Add(panel7);
            Controls.Add(panel8);
            Controls.Add(panel9);
            Controls.Add(pencilButton);
            Controls.Add(statusStrip);
            Name = "wfaPaint";
            Text = "MyPaint";
            ((System.ComponentModel.ISupportInitialize)image).EndInit();
            image.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)trackBarLineWidth).EndInit();
            statusStrip.ResumeLayout(false);
            statusStrip.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button pencilButton;
        private Panel panel1;
        private Panel panel2;
        private Panel panel3;
        private Panel panel4;
        private Panel panel5;
        private Panel panel6;
        private Panel panel7;
        private Panel panel8;
        private Panel panel9;
        private PictureBox image;
        private TrackBar trackBarLineWidth;
        private Button lineButton;
        private Button triangleButton;
        private Button rectangleButton;
        private Button ellipsButton;
        private Button hexagonButton;
        private Button starButton;
        private Button arrowButton;
        private Button selectToolButton;
        private Panel myColor;
        private RichTextBox richTextBox1;
        private StatusStrip statusStrip;
        private ToolStripStatusLabel toolStripCoordinatesLabel;
        private ToolStripStatusLabel toolStripPixelColorLabel;
        private ToolStripStatusLabel toolStripImageSizeLabel;
        private ToolStrip toolStrip1;
    }
}
