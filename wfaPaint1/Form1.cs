using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

public enum Tool
{
    Pencil,
    Line,
    Rectangle,
    Ellipse
}

namespace wfaPaint1
{
    public partial class Form1 : Form
    {
        private Bitmap canvasBitmap;
        private Graphics canvasGraphics;
        private bool isDrawing = false;
        private Point startPoint;
        private Point currentPoint;
        private Tool currentTool = Tool.Pencil;
        private Color currentColor = Color.Black;
        private int currentThickness = 2;

        public Form1()
        {
            InitializeComponent();
            this.DoubleBuffered = true;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            canvasBitmap = new Bitmap(pictureBoxCanvas.Width, pictureBoxCanvas.Height);
            canvasGraphics = Graphics.FromImage(canvasBitmap);
            canvasGraphics.Clear(Color.White);
            pictureBoxCanvas.Image = canvasBitmap;

            // --- ИСПРАВЛЕНИЕ: ВОЗВРАЩАЕМ ВСЕ ПОДПИСКИ НА СОБЫТИЯ ---
            pictureBoxCanvas.MouseDown += new MouseEventHandler(pictureBoxCanvas_MouseDown);
            pictureBoxCanvas.MouseMove += new MouseEventHandler(pictureBoxCanvas_MouseMove);
            pictureBoxCanvas.MouseUp += new MouseEventHandler(pictureBoxCanvas_MouseUp);
            pictureBoxCanvas.Paint += new PaintEventHandler(pictureBoxCanvas_Paint);
            // --- КОНЕЦ ИСПРАВЛЕНИЯ ---

            thicknessComboBox.Items.AddRange(new object[] { "2", "5", "10", "15", "20" });
            thicknessComboBox.SelectedIndex = 0;
        }

        // --- Обработчики кнопок (без изменений) ---
        private void pencilButton_Click(object sender, EventArgs e) { currentTool = Tool.Pencil; }
        private void lineButton_Click(object sender, EventArgs e) { currentTool = Tool.Line; }
        private void rectButton_Click(object sender, EventArgs e) { currentTool = Tool.Rectangle; }
        private void ellipseButton_Click(object sender, EventArgs e) { currentTool = Tool.Ellipse; }
        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            using (ColorDialog colorDialog = new ColorDialog())
            {
                if (colorDialog.ShowDialog() == DialogResult.OK)
                {
                    currentColor = colorDialog.Color;
                }
            }
        }
        private void thicknessComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (int.TryParse(thicknessComboBox.SelectedItem.ToString(), out int thickness))
            {
                currentThickness = thickness;
            }
        }

        // --- Логика рисования (без изменений) ---
        private void pictureBoxCanvas_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                isDrawing = true;
                startPoint = e.Location;
            }
        }
        private void pictureBoxCanvas_MouseMove(object sender, MouseEventArgs e)
        {
            if (isDrawing)
            {
                currentPoint = e.Location;
                if (currentTool == Tool.Pencil)
                {
                    using (Pen pen = new Pen(currentColor, currentThickness))
                    {
                        canvasGraphics.DrawLine(pen, startPoint, currentPoint);
                    }
                    startPoint = currentPoint;
                }
                pictureBoxCanvas.Invalidate();
            }
        }
        private void pictureBoxCanvas_MouseUp(object sender, MouseEventArgs e)
        {
            if (isDrawing)
            {
                // Для всех инструментов, кроме карандаша, рисуем финальную фигуру
                if (currentTool != Tool.Pencil)
                {
                    using (Pen pen = new Pen(currentColor, currentThickness))
                    {
                        switch (currentTool)
                        {
                            case Tool.Line:
                                canvasGraphics.DrawLine(pen, startPoint, currentPoint);
                                break;
                            case Tool.Rectangle:
                                canvasGraphics.DrawRectangle(pen, GetRectangle(startPoint, currentPoint));
                                break;
                            case Tool.Ellipse:
                                canvasGraphics.DrawEllipse(pen, GetRectangle(startPoint, currentPoint));
                                break;
                        }
                    }
                }

                isDrawing = false;
                // Финальная перерисовка, чтобы убрать пунктирную рамку
                pictureBoxCanvas.Invalidate();
            }
        }
        private void pictureBoxCanvas_Paint(object sender, PaintEventArgs e)
        {
            if (isDrawing && currentTool != Tool.Pencil)
            {
                using (Pen previewPen = new Pen(Color.Gray, 1))
                {
                    previewPen.DashStyle = DashStyle.Dash;
                    switch (currentTool)
                    {
                        case Tool.Line:
                            e.Graphics.DrawLine(previewPen, startPoint, currentPoint);
                            break;
                        case Tool.Rectangle:
                            e.Graphics.DrawRectangle(previewPen, GetRectangle(startPoint, currentPoint));
                            break;
                        case Tool.Ellipse:
                            e.Graphics.DrawEllipse(previewPen, GetRectangle(startPoint, currentPoint));
                            break;
                    }
                }
            }
        }
        private Rectangle GetRectangle(Point p1, Point p2)
        {
            return new Rectangle(Math.Min(p1.X, p2.X), Math.Min(p1.Y, p2.Y), Math.Abs(p1.X - p2.X), Math.Abs(p1.Y - p2.Y));
        }

        private void colorSelectButton_Click(object sender, EventArgs e)
        {
            using (ColorDialog colorDialog = new ColorDialog())
            {
                if (colorDialog.ShowDialog() == DialogResult.OK)
                {
                    currentColor = colorDialog.Color;
                    // Можно добавить и смену цвета кнопки для наглядности
                    // colorSelectButton.BackColor = currentColor;
                }
            }
        }
}