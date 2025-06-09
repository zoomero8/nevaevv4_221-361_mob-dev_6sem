using System.Drawing.Drawing2D;
using System.Windows.Forms;
using wfaPaint.Tools;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ScrollBar;

namespace wfaPaint
{
    public partial class wfaPaint : Form
    {
        private DrawingCanvas _canvas;
        private ToolSettings _toolSettings;
        private ITool _currentTool;

        private int currentX;
        private int currentY;
        public wfaPaint()
        {
            InitializeComponent();

            _toolSettings = new ToolSettings(panel1.BackColor, 10);

            int canvasWidth = image.Width > 0 ? image.Width : Screen.PrimaryScreen.Bounds.Width;
            int canvasHeight = image.Height > 0 ? image.Height : Screen.PrimaryScreen.Bounds.Height;
            _canvas = new DrawingCanvas(image, canvasWidth, canvasHeight, Color.White);
            myColor.BackColor = _toolSettings.MyPen.Color;

            trackBarLineWidth.Minimum = 1;
            trackBarLineWidth.Maximum = 15;
            trackBarLineWidth.Value = (int)_toolSettings.MyPen.Width;
            trackBarLineWidth.ValueChanged += (s, e) =>
            {
                if (_toolSettings != null) _toolSettings.MyPen.Width = trackBarLineWidth.Value;
            };

            panel1.Click += (s, e) => ChangeColor(panel1);
            panel2.Click += (s, e) => ChangeColor(panel2);
            panel3.Click += (s, e) => ChangeColor(panel3);
            panel4.Click += (s, e) => ChangeColor(panel4);
            panel5.Click += (s, e) => ChangeColor(panel5);
            panel6.Click += (s, e) => ChangeColor(panel6);
            panel7.Click += (s, e) => ChangeColor(panel7);
            panel8.Click += (s, e) => ChangeColor(panel8);
            panel9.Click += (s, e) => ChangeColor(panel9);

            pencilButton.Click += (s, e) => SetActiveTool(new PencilTool());
            lineButton.Click += (s, e) => SetActiveTool(new LineTool());
            rectangleButton.Click += (s, e) => SetActiveTool(new RectangleTool());
            triangleButton.Click += (s, e) => SetActiveTool(new TriangleTool());
            ellipsButton.Click += (s, e) => SetActiveTool(new EllipseTool());
            hexagonButton.Click += (s, e) => SetActiveTool(new HexagonTool());
            starButton.Click += (s, e) => SetActiveTool(new StarTool());
            arrowButton.Click += (s, e) => SetActiveTool(new ArrowTool());
            selectToolButton.Click += (s, e) => SetActiveTool(new SelectTool(image));

            SetActiveTool(new PencilTool());

            image.MouseDown += Image_MouseDown_Handler;
            image.MouseMove += Image_MouseMove_Handler;
            image.MouseUp += Image_MouseUp_Handler;
            image.Paint += Image_Paint_Handler;
            image.MouseWheel += Image_MouseWheel_Handler;

            UpdateImageSizeStatus();
        }

        private void UpdateImageSizeStatus()
        {
            if (toolStripImageSizeLabel != null && _canvas?.B != null)
            {
                toolStripImageSizeLabel.Text = $"Size: {_canvas.B.Width}x{_canvas.B.Height}";
            }
            else if (toolStripImageSizeLabel != null)
            {
                toolStripImageSizeLabel.Text = "Size: ---";
            }
        }

        private void Image_MouseWheel_Handler(object? sender, MouseEventArgs e)
        {
            if (_currentTool is IInteractiveTool interactiveTool && ModifierKeys == Keys.Control)
            {
                interactiveTool.OnMouseWheel(e);
            }
        }

        private void SetActiveTool(ITool tool)
        {
            // (ИЗМЕНЕНИЕ) Проверяем, был ли предыдущий инструмент SelectTool и сбрасываем его состояние
            if (_currentTool is SelectTool oldSelectionTool)
            {
                oldSelectionTool.ResetSelection();
            }

            _currentTool = tool;
            if (tool != null)
            {
                _toolSettings.MyDrawMode = tool.Name;
                Cursor = tool.ToolCursor;
                image.Cursor = tool.ToolCursor;
            }
        }

        private void Image_MouseMove_Handler(object? sender, MouseEventArgs e)
        {
            currentX = e.X;
            currentY = e.Y;

            if (toolStripCoordinatesLabel != null) toolStripCoordinatesLabel.Text = $"X: {e.X}, Y: {e.Y}";


            if (toolStripPixelColorLabel != null && _canvas?.B != null)
            {
                if (e.X >= 0 && e.X < _canvas.B.Width && e.Y >= 0 && e.Y < _canvas.B.Height)
                {
                    try { Color pixelColor = _canvas.B.GetPixel(e.X, e.Y); toolStripPixelColorLabel.Text = $"RGB: ({pixelColor.R},{pixelColor.G},{pixelColor.B})"; }
                    catch { toolStripPixelColorLabel.Text = "RGB: Error"; }
                }
                else { toolStripPixelColorLabel.Text = "RGB: ---"; }
            }
            else if (toolStripPixelColorLabel != null) { toolStripPixelColorLabel.Text = "RGB: ---"; }

            _currentTool?.OnMouseMove(e, _canvas, _toolSettings);
        }


        private void Image_MouseDown_Handler(object? sender, MouseEventArgs e)
        {
            _currentTool?.OnMouseDown(e, _canvas, _toolSettings);
        }

        private void Image_MouseUp_Handler(object? sender, MouseEventArgs e)
        {
            _currentTool?.OnMouseUp(e, _canvas, _toolSettings);
        }

        private void Image_Paint_Handler(object? sender, PaintEventArgs e)
        {
            if (_canvas?.B != null)
            {
                e.Graphics.DrawImage(_canvas.B, 0, 0);
            }

            if (_currentTool is SelectTool selectionToolInstance)
            {
                selectionToolInstance.PaintSelectionBorder(e.Graphics, e.ClipRectangle);
            }
        }

        private void PasteImageFromClipboard()
        {
            if (Clipboard.ContainsImage())
            {
                Image clipboardImage = Clipboard.GetImage();
                if (clipboardImage != null)
                {
                    // Принудительно активируем инструмент "Выделение", если он не активен
                    if (!(_currentTool is SelectTool))
                    {
                        SetActiveTool(new SelectTool(image));
                    }

                    // Передаем управление вставкой нашему инструменту
                    if (_currentTool is SelectTool selectionToolInstance)
                    {
                        // Вставляем изображение в текущую позицию курсора
                        selectionToolInstance.InitiatePasting(clipboardImage, new Point(currentX, currentY));
                    }
                }
            }
        }


        private void PasteTextFromClipboard()
        {
            if (_currentTool is SelectTool selectionToolInstance)
            {
                selectionToolInstance.ResetSelection();
            }

            if (Clipboard.ContainsText())
            {
                using (Font font = new Font("Arial", 12))
                {
                    _canvas.DrawStringOnG(Clipboard.GetText(), font, Brushes.Black, new PointF(currentX - 50, currentY));
                    _canvas.BackupState();
                    _canvas.Invalidate();
                }
            }
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == (Keys.Control | Keys.V)) { PasteImageFromClipboard(); return true; }
            if (keyData == (Keys.Alt | Keys.V)) { PasteTextFromClipboard(); return true; }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void ChangeColor(Panel pan)
        {
            _toolSettings.MyPen.Color = pan.BackColor;
            myColor.BackColor = pan.BackColor;
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            _canvas?.Dispose();
            _toolSettings?.Dispose();
            base.OnFormClosing(e);
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
