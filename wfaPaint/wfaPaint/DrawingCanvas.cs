using System.Drawing.Drawing2D;

namespace wfaPaint
{
    public class DrawingCanvas : IDisposable
    {
        public Bitmap B { get; private set; }
        public Graphics G { get; private set; }
        public Bitmap BB { get; private set; }

        private PictureBox _pictureBoxToInvalidate;
        public int Width => B?.Width ?? 0;
        public int Height => B?.Height ?? 0;

        public DrawingCanvas(PictureBox pictureBox, int width, int height, Color initialBackgroundColor)
        {
            _pictureBoxToInvalidate = pictureBox;

            B = new Bitmap(width, height);
            G = Graphics.FromImage(B);
            G.SmoothingMode = SmoothingMode.AntiAlias;
            G.Clear(initialBackgroundColor);

            BB = (Bitmap)B.Clone(); 
        }

        public Graphics RestoreStateAndGetGraphics()
        {
            G?.Dispose();
            B?.Dispose();

            if (BB == null)
            {
                BB = new Bitmap(Width > 0 ? Width : 100,
                               Height > 0 ? Height : 100);
                using (var tempG = Graphics.FromImage(BB)) tempG.Clear(Color.White);
                                                                                    
                B = (Bitmap)BB.Clone();
                G = Graphics.FromImage(B);
                G.SmoothingMode = SmoothingMode.AntiAlias;
                return G;
            }

            B = (Bitmap)BB.Clone();
            G = Graphics.FromImage(B);
            G.SmoothingMode = SmoothingMode.AntiAlias;
            return G;
        }

        public void BackupState()
        {
            BB?.Dispose();
            if (B == null)
            {
                B = new Bitmap(100, 100);
                using (var tempG = Graphics.FromImage(B)) tempG.Clear(Color.Transparent);
            }
            BB = (Bitmap)B.Clone();
        }

        public void Invalidate()
        {
            _pictureBoxToInvalidate.Invalidate();
        }

        public Bitmap CloneFragmentFromB(Rectangle rect, System.Drawing.Imaging.PixelFormat format)
        {
            if (B == null) return null; 
            Rectangle imageBounds = new Rectangle(0, 0, B.Width, B.Height);
            Rectangle validRect = Rectangle.Intersect(rect, imageBounds);
            if (validRect.Width > 0 && validRect.Height > 0)
            {
                return B.Clone(validRect, format);
            }
            return null; 
        }

        public void FillRectangleOnB(Rectangle rect, Brush brush)
        {
            if (G == null) return;
            G.FillRectangle(brush, rect);
        }

        public void DrawImageOnG(Image image, Point point)
        {
            if (G == null) return;
            G.DrawImage(image, point);
        }

        public void DrawStringOnG(string s, Font font, Brush brush, PointF point)
        {
            if (G == null) return;
            G.DrawString(s, font, brush, point);
        }

        public void Dispose()
        {
            G?.Dispose();
            B?.Dispose();
            BB?.Dispose();
        }
    }
}