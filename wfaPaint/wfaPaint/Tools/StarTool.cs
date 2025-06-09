using System;
using System.Drawing;
using System.Windows.Forms;
using wfaPaint;

namespace wfaPaint.Tools
{
    public class StarTool : ShapeToolBase
    {
        public override string Name => "Star";

        protected override void DrawShape(Graphics g, Point currentPoint, Pen pen)
        {
            int outerRadius = (int)Math.Sqrt(Math.Pow(currentPoint.X - startPoint.X, 2) + Math.Pow(currentPoint.Y - startPoint.Y, 2));
            if (outerRadius < 2) return; 

            int innerRadius = outerRadius / 2; 

            Point[] starPoints = new Point[10]; 
            for (int i = 0; i < 5; i++)
            {
                double outerAngle = -Math.PI / 2 + (2 * Math.PI / 5) * i;
               
                double innerAngle = outerAngle + Math.PI / 5;

                starPoints[2 * i] = new Point(
                    (int)(startPoint.X + outerRadius * Math.Cos(outerAngle)),
                    (int)(startPoint.Y + outerRadius * Math.Sin(outerAngle)) 
                );

                starPoints[2 * i + 1] = new Point(
                    (int)(startPoint.X + innerRadius * Math.Cos(innerAngle)),
                    (int)(startPoint.Y + innerRadius * Math.Sin(innerAngle))
                );
            }
            g.DrawPolygon(pen, starPoints);
        }
    }
}