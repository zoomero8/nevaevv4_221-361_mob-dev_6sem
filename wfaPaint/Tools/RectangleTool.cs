using System.Drawing;
using System.Windows.Forms;
using wfaPaint;

namespace wfaPaint.Tools
{
    public class RectangleTool : ShapeToolBase
    {
        public override string Name => "Rectangle";

        protected override void DrawShape(Graphics g, Point currentPoint, Pen pen)
        {
            int x = Math.Min(startPoint.X, currentPoint.X);
            int y = Math.Min(startPoint.Y, currentPoint.Y);
            int width = Math.Abs(startPoint.X - currentPoint.X);
            int height = Math.Abs(startPoint.Y - currentPoint.Y);

            g.DrawRectangle(pen, x, y, width, height);
        }
    }
}