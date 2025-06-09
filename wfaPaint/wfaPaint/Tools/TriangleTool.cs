using System.Drawing;
using System.Windows.Forms;
using wfaPaint;

namespace wfaPaint.Tools
{
    public class TriangleTool : ShapeToolBase
    {
        public override string Name => "Triangle";

        protected override void DrawShape(Graphics g, Point currentPoint, Pen pen)
        {
            int baseY = currentPoint.Y;
            int baseWidth = (currentPoint.X - startPoint.X) * 2;
            int baseX1 = startPoint.X - (baseWidth / 2);
            int baseX2 = startPoint.X + (baseWidth / 2);

            Point p1 = new Point(baseX1, baseY);
            Point p2 = new Point(baseX2, baseY);
            Point[] trianglePoints = { startPoint, p1, p2 };

            g.DrawPolygon(pen, trianglePoints);
        }
    }
}