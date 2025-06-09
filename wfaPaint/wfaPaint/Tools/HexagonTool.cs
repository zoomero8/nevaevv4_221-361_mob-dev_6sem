using System;
using System.Drawing;
using System.Windows.Forms;
using wfaPaint;

namespace wfaPaint.Tools
{
    public class HexagonTool : ShapeToolBase
    {
        public override string Name => "Hexagon";

        protected override void DrawShape(Graphics g, Point currentPoint, Pen pen)
        {
           
            int sideLength = (int)Math.Sqrt(Math.Pow(currentPoint.X - startPoint.X, 2) + Math.Pow(currentPoint.Y - startPoint.Y, 2));
            if (sideLength == 0) return; 

            Point[] hexagonPoints = new Point[6];
            for (int i = 0; i < 6; i++)
            {
                double ang = Math.PI / 3 * i;
                hexagonPoints[i] = new Point(
                    (int)(startPoint.X + sideLength * Math.Cos(ang)),
                    (int)(startPoint.Y + sideLength * Math.Sin(ang))
                );
            }
            g.DrawPolygon(pen, hexagonPoints);
        }
    }
}