// --- НОВЫЙ ФАЙЛ SelectTool.cs ---

using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using wfaPaint;

namespace wfaPaint.Tools
{
    #region (ИЗМЕНЕНИЕ) Паттерн "Состояние" (State Pattern)
    // Мы создаем интерфейс для всех состояний нашего инструмента.
    internal interface ISelectionState
    {
        void OnMouseDown(SelectTool tool, MouseEventArgs e);
        void OnMouseMove(SelectTool tool, MouseEventArgs e);
        void OnMouseUp(SelectTool tool, MouseEventArgs e);
        void OnPaint(SelectTool tool, Graphics g);
        Cursor GetCursor(SelectTool tool, Point mousePosition);
    }
    #endregion

    public class SelectTool : ITool
    {
        // === Публичные свойства для интерфейса ITool ===
        public string Name => "Select";
        public Cursor ToolCursor { get; private set; } = Cursors.Cross;

        // === Внутренние поля, доступные для состояний ===
        internal PictureBox CanvasControl { get; }
        internal DrawingCanvas DrawingSurface { get; private set; }
        internal ToolSettings CurrentSettings { get; private set; }

        internal Point ActionStartPoint { get; set; }
        internal Rectangle SelectionBounds { get; set; }
        internal Bitmap CopiedFragment { get; set; }
        internal Point OriginalFragmentLocation { get; set; }

        // (ИЗМЕНЕНИЕ) Вместо enum, у нас теперь объект текущего состояния.
        private ISelectionState _currentState;

        // (ИЗМЕНЕНИЕ) Таймер для анимации рамки
        private readonly System.Windows.Forms.Timer _animationTimer;
        private float _dashOffset = 0;

        public SelectTool(PictureBox pictureBox)
        {
            CanvasControl = pictureBox;
            // Устанавливаем начальное состояние.
            SetState(new IdleState());

            // Настраиваем таймер для "бегущих муравьёв"
            _animationTimer = new System.Windows.Forms.Timer { Interval = 100 };
            _animationTimer.Tick += (sender, args) =>
            {
                _dashOffset = (_dashOffset - 1) % 16; // Сдвигаем штрихи
                CanvasControl.Invalidate(SelectionBounds); // Перерисовываем только область выделения
            };
        }

        public void OnMouseWheel(MouseEventArgs e)
        {
            if (_currentState is PastingState pastingState)
            {
                float scaleFactor = e.Delta > 0 ? 1.1f : 0.9f; // 10% увеличение или уменьшение
                pastingState.Scale(scaleFactor);
                CanvasControl.Invalidate();
            }
        }

        public void InitiatePasting(Image imageToPaste, Point location)
        {
            // Сбрасываем любое текущее состояние (например, если было что-то выделено)
            ResetSelection();

            // Переходим в новое состояние вставки
            var pasteState = new PastingState(new Bitmap(imageToPaste), location);
            SetState(pasteState);

            // Запускаем анимацию рамки
            StartAnimation();
            CanvasControl.Invalidate();
        }

        // (ИЗМЕНЕНИЕ) Метод для смены состояний
        internal void SetState(ISelectionState newState)
        {
            _currentState = newState;
            // Обновляем курсор при смене состояния
            ToolCursor = _currentState.GetCursor(this, CanvasControl.PointToClient(Cursor.Position));
            CanvasControl.Cursor = ToolCursor;
        }

        // === Реализация интерфейса ITool ===
        // Все вызовы просто делегируются текущему объекту-состоянию.
        public void OnMouseDown(MouseEventArgs e, DrawingCanvas canvas, ToolSettings settings)
        {
            this.DrawingSurface = canvas;
            this.CurrentSettings = settings;
            _currentState.OnMouseDown(this, e);
        }

        public void OnMouseMove(MouseEventArgs e, DrawingCanvas canvas, ToolSettings settings)
        {
            _currentState.OnMouseMove(this, e);
            // Динамическое изменение курсора в зависимости от положения мыши
            var newCursor = _currentState.GetCursor(this, e.Location);
            if (newCursor != ToolCursor)
            {
                ToolCursor = newCursor;
                CanvasControl.Cursor = ToolCursor;
            }
        }

        public void OnMouseUp(MouseEventArgs e, DrawingCanvas canvas, ToolSettings settings)
        {
            _currentState.OnMouseUp(this, e);
        }

        // (ИЗМЕНЕНИЕ) Этот метод вызывается из главного окна для отрисовки рамки
        public void PaintSelectionBorder(Graphics formGraphics, Rectangle clipRectangle)
        {
            _currentState.OnPaint(this, formGraphics);
        }

        // (ИЗМЕНЕНИЕ) Сброс выделения
        public void ResetSelection()
        {
            CopiedFragment?.Dispose();
            CopiedFragment = null;
            SelectionBounds = Rectangle.Empty;
            SetState(new IdleState());
            _animationTimer.Stop();
            CanvasControl.Invalidate();
        }

        #region (ИЗМЕНЕНИЕ) Методы для отрисовки и анимации

        internal void DrawAnimatedBorder(Graphics g, Rectangle rect)
        {
            if (rect.Width < 1 || rect.Height < 1) return;

            using (var pen = new Pen(Color.Black, 1))
            {
                pen.DashStyle = DashStyle.Dash;
                pen.DashPattern = new float[] { 4, 4 };
                pen.DashOffset = _dashOffset;
                g.DrawRectangle(pen, rect);
            }
        }

        internal void StartAnimation() => _animationTimer.Start();
        internal void StopAnimation() => _animationTimer.Stop();

        #endregion
    }

    #region (ИЗМЕНЕНИЕ) Классы состояний

    // 1. Состояние "Ожидание" (ничего не выделено)
    internal class IdleState : ISelectionState
    {
        public void OnMouseDown(SelectTool tool, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                tool.ActionStartPoint = e.Location;
                tool.SetState(new SelectingState()); // Переходим в режим выделения
            }
        }
        public void OnMouseMove(SelectTool tool, MouseEventArgs e) { }
        public void OnMouseUp(SelectTool tool, MouseEventArgs e) { }
        public void OnPaint(SelectTool tool, Graphics g) { }
        public Cursor GetCursor(SelectTool tool, Point mousePosition) => Cursors.Cross;
    }

    // 2. Состояние "Выделение" (пользователь тянет рамку)
    internal class SelectingState : ISelectionState
    {
        public void OnMouseDown(SelectTool tool, MouseEventArgs e) { }

        public void OnMouseMove(SelectTool tool, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                int x = Math.Min(tool.ActionStartPoint.X, e.X);
                int y = Math.Min(tool.ActionStartPoint.Y, e.Y);
                int width = Math.Abs(tool.ActionStartPoint.X - e.X);
                int height = Math.Abs(tool.ActionStartPoint.Y - e.Y);
                tool.SelectionBounds = new Rectangle(x, y, width, height);
                tool.CanvasControl.Invalidate(); // Перерисовать, чтобы показать рамку
            }
        }

        public void OnMouseUp(SelectTool tool, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                // Если выделение слишком маленькое, сбрасываем
                if (tool.SelectionBounds.Width < 2 || tool.SelectionBounds.Height < 2)
                {
                    tool.ResetSelection();
                }
                else
                {
                    tool.SetState(new SelectedState()); // Переходим в состояние "Выделено"
                    tool.StartAnimation(); // Запускаем "бегущих муравьёв"
                }
            }
        }

        public void OnPaint(SelectTool tool, Graphics g)
        {
            // Во время выделения рисуем простую пунктирную рамку
            using (var pen = new Pen(Color.Black, 1) { DashStyle = DashStyle.Dot })
            {
                g.DrawRectangle(pen, tool.SelectionBounds);
            }
        }
        public Cursor GetCursor(SelectTool tool, Point mousePosition) => Cursors.Cross;
    }

    // 3. Состояние "Выделено" (рамка есть, ждем действия)
    internal class SelectedState : ISelectionState
    {
        public void OnMouseDown(SelectTool tool, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left) return;

            // Если клик внутри выделения - начинаем перемещение
            if (tool.SelectionBounds.Contains(e.Location))
            {
                tool.ActionStartPoint = e.Location; // Запомнить точку начала перетаскивания
                tool.OriginalFragmentLocation = tool.SelectionBounds.Location;

                // (ИЗМЕНЕНИЕ) Новая логика: только копируем, не затираем оригинал сразу
                tool.CopiedFragment = tool.DrawingSurface.CloneFragmentFromB(tool.SelectionBounds, System.Drawing.Imaging.PixelFormat.Format32bppArgb);

                tool.DrawingSurface.BackupState(); // Сохраняем холст ДО перемещения
                tool.SetState(new MovingState()); // Переходим в режим перемещения
                tool.StopAnimation();
            }
            // Если клик снаружи - сбрасываем выделение и начинаем новое
            else
            {
                tool.ResetSelection();
                tool.ActionStartPoint = e.Location;
                tool.SetState(new SelectingState());
            }
        }

        public void OnMouseMove(SelectTool tool, MouseEventArgs e) { }
        public void OnMouseUp(SelectTool tool, MouseEventArgs e) { }
        public void OnPaint(SelectTool tool, Graphics g)
        {
            tool.DrawAnimatedBorder(g, tool.SelectionBounds);
        }
        public Cursor GetCursor(SelectTool tool, Point mousePosition)
        {
            return tool.SelectionBounds.Contains(mousePosition) ? Cursors.SizeAll : Cursors.Cross;
        }
    }

    // 4. Состояние "Перемещение"
    internal class MovingState : ISelectionState
    {
        public void OnMouseDown(SelectTool tool, MouseEventArgs e) { }

        public void OnMouseMove(SelectTool tool, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && tool.CopiedFragment != null)
            {
                // (ИЗМЕНЕНИЕ) Логика отрисовки превью
                Graphics g = tool.DrawingSurface.RestoreStateAndGetGraphics();

                int dx = e.X - tool.ActionStartPoint.X;
                int dy = e.Y - tool.ActionStartPoint.Y;
                Point newLocation = new Point(tool.OriginalFragmentLocation.X + dx, tool.OriginalFragmentLocation.Y + dy);

                // Рисуем скопированный фрагмент на новом месте
                g.DrawImage(tool.CopiedFragment, newLocation);

                // Обновляем границы для рамки
                tool.SelectionBounds = new Rectangle(newLocation, tool.CopiedFragment.Size);

                tool.CanvasControl.Invalidate();
            }
        }

        public void OnMouseUp(SelectTool tool, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                // (ИЗМЕНЕНИЕ) Фиксация результата
                Graphics g = tool.DrawingSurface.G; // Получаем финальный Graphics

                // 1. Затираем старое место белым цветом
                g.FillRectangle(Brushes.White, new Rectangle(tool.OriginalFragmentLocation, tool.SelectionBounds.Size));

                // 2. Рисуем фрагмент на новом месте
                g.DrawImage(tool.CopiedFragment, tool.SelectionBounds.Location);

                tool.DrawingSurface.BackupState(); // "Впечатываем" результат в холст

                tool.CopiedFragment?.Dispose();
                tool.CopiedFragment = null;

                tool.SetState(new SelectedState()); // Возвращаемся в состояние "Выделено"
                tool.StartAnimation();
                tool.CanvasControl.Invalidate();
            }
        }

        public void OnPaint(SelectTool tool, Graphics g)
        {
            // Во время перемещения рисуем простую пунктирную рамку
            using (var pen = new Pen(Color.Black, 1) { DashStyle = DashStyle.Dot })
            {
                g.DrawRectangle(pen, tool.SelectionBounds);
            }
        }
        public Cursor GetCursor(SelectTool tool, Point mousePosition) => Cursors.SizeAll;
    }
    // 5. Состояние "Вставка" (пользователь вставил изображение и может его двигать/масштабировать)
    // --- ЗАМЕНИТЕ ЭТИМ КОДОМ ВЕСЬ КЛАСС PastingState ---

    internal class PastingState : ISelectionState
    {
        private Bitmap _pastedImage;
        private RectangleF _pastedBounds;

        // Конструктор для ПЕРВОНАЧАЛЬНОЙ вставки
        public PastingState(Bitmap pastedImage, Point initialLocation)
        {
            _pastedImage = pastedImage;
            _pastedBounds = new RectangleF(
                initialLocation.X - pastedImage.Width / 2.0f,
                initialLocation.Y - pastedImage.Height / 2.0f,
                pastedImage.Width,
                pastedImage.Height
            );
        }

        // (ИСПРАВЛЕНИЕ) Новый конструктор для ВОЗВРАТА из режима перемещения
        public PastingState(Bitmap pastedImage, RectangleF currentBounds)
        {
            _pastedImage = pastedImage;
            _pastedBounds = currentBounds;
        }

        public void OnMouseDown(SelectTool tool, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && !_pastedBounds.Contains(e.Location))
            {
                FinalizePaste(tool);
            }
            else if (e.Button == MouseButtons.Left && _pastedBounds.Contains(e.Location))
            {
                tool.SetState(new MovingPastedState(_pastedImage, _pastedBounds, e.Location));
            }
            else if (e.Button == MouseButtons.Right)
            {
                CancelPaste(tool);
            }
        }

        public void OnMouseMove(SelectTool tool, MouseEventArgs e) { }
        public void OnMouseUp(SelectTool tool, MouseEventArgs e) { }

        public void OnPaint(SelectTool tool, Graphics g)
        {
            g.DrawImage(_pastedImage, _pastedBounds);
            tool.DrawAnimatedBorder(g, Rectangle.Round(_pastedBounds));
        }

        public Cursor GetCursor(SelectTool tool, Point mousePosition)
        {
            return _pastedBounds.Contains(mousePosition) ? Cursors.SizeAll : Cursors.Default;
        }

        public void Scale(float factor)
        {
            if (_pastedBounds.Width * factor < 10 || _pastedBounds.Height * factor < 10) return;

            var oldCenter = new PointF(_pastedBounds.X + _pastedBounds.Width / 2, _pastedBounds.Y + _pastedBounds.Height / 2);
            _pastedBounds.Width *= factor;
            _pastedBounds.Height *= factor;
            _pastedBounds.X = oldCenter.X - _pastedBounds.Width / 2;
            _pastedBounds.Y = oldCenter.Y - _pastedBounds.Height / 2;
        }

        internal void FinalizePaste(SelectTool tool)
        {
            using (var scaledImage = new Bitmap(_pastedImage, Size.Round(_pastedBounds.Size)))
            {
                tool.DrawingSurface.DrawImageOnG(scaledImage, Point.Round(_pastedBounds.Location));
            }
            tool.DrawingSurface.BackupState();
            tool.ResetSelection();
        }

        private void CancelPaste(SelectTool tool)
        {
            _pastedImage?.Dispose();
            tool.ResetSelection();
        }
    }

    // Вспомогательное состояние для перемещения вставляемого изображения
    internal class MovingPastedState : ISelectionState
    {
        private Bitmap _pastedImage;
        private RectangleF _pastedBounds;
        private Point _dragStartPoint;

        public MovingPastedState(Bitmap image, RectangleF bounds, Point startPoint)
        {
            _pastedImage = image;
            _pastedBounds = bounds;
            _dragStartPoint = startPoint;
        }

        public void OnMouseDown(SelectTool tool, MouseEventArgs e) { }

        public void OnMouseMove(SelectTool tool, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                float dx = e.X - _dragStartPoint.X;
                float dy = e.Y - _dragStartPoint.Y;
                _pastedBounds.X += dx;
                _pastedBounds.Y += dy;
                _dragStartPoint = e.Location; // Обновляем стартовую точку для плавного движения
                tool.CanvasControl.Invalidate();
            }
        }

        public void OnMouseUp(SelectTool tool, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                // Возвращаемся в состояние ожидания команды (масштабирование, подтверждение и т.д.)
                tool.SetState(new PastingState(_pastedImage, this._pastedBounds));
            }
        }

        public void OnPaint(SelectTool tool, Graphics g)
        {
            g.DrawImage(_pastedImage, _pastedBounds);
            tool.DrawAnimatedBorder(g, Rectangle.Round(_pastedBounds));
        }

        public Cursor GetCursor(SelectTool tool, Point mousePosition) => Cursors.SizeAll;
    }
    #endregion
}