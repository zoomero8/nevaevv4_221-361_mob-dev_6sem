using System.Drawing.Drawing2D;

namespace wfaPaint
{
    public class ToolSettings : IDisposable
    {
        public Pen MyPen { get; private set; }
        public string MyDrawMode { get; set; }

        public ToolSettings(Color initialColor, float initialWidth, string initialMode = "Pencil")
        {
            MyPen = new Pen(initialColor, initialWidth)
            {
                StartCap = LineCap.Round,
                EndCap = LineCap.Round
            };
            MyDrawMode = initialMode;
        }

        public void Dispose()
        {
            MyPen?.Dispose();
        }
    }
}