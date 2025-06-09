using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using wfaPaint;

namespace wfaPaint.Tools
{
    public class ArrowTool : ShapeToolBase 
    {
        public override string Name => "Arrow";

        protected override void DrawShape(Graphics g, Point currentPoint, Pen pen)
        {
            // startPoint - это p1 (хвост) из ShapeToolBase
            // currentPoint - это p2 (острие)
            Point p1 = startPoint;
            Point p2 = currentPoint;

            if (p1 == p2) return; // Не рисовать, если точки совпадают

            // Параметры наконечника
            float arrowHeadLength = pen.Width * 3.0f;
            if (arrowHeadLength < 12) arrowHeadLength = 12;

            float arrowHeadAngle = (float)(Math.PI / 6.0); // Угол между осью стрелки и "усиком"

            float lineAngle = (float)Math.Atan2(p2.Y - p1.Y, p2.X - p1.X);

            PointF wing1 = new PointF(
                p2.X - arrowHeadLength * (float)Math.Cos(lineAngle - arrowHeadAngle),
                p2.Y - arrowHeadLength * (float)Math.Sin(lineAngle - arrowHeadAngle)
            );
            PointF wing2 = new PointF(
                p2.X - arrowHeadLength * (float)Math.Cos(lineAngle + arrowHeadAngle),
                p2.Y - arrowHeadLength * (float)Math.Sin(lineAngle + arrowHeadAngle)
            );

            float bodyLineEndOffset = arrowHeadLength * (float)Math.Cos(arrowHeadAngle);

            PointF bodyLineEndPoint = new PointF(
                p2.X - bodyLineEndOffset * (float)Math.Cos(lineAngle),
                p2.Y - bodyLineEndOffset * (float)Math.Sin(lineAngle)
            );

            // Рисуем тело стрелки
            using (Pen bodyPen = new Pen(pen.Color, pen.Width))
            {
                bodyPen.EndCap = LineCap.Flat;
                bodyPen.StartCap = LineCap.Round;

                float totalLengthSquared = (p2.X - p1.X) * (p2.X - p1.X) + (p2.Y - p1.Y) * (p2.Y - p1.Y);
                if (Math.Sqrt(totalLengthSquared) > bodyLineEndOffset * 0.95f)
                {
                    g.DrawLine(bodyPen, p1, bodyLineEndPoint);
                }
            }

            // Рисуем наконечник
            PointF[] arrowHeadPolygon = { p2, wing1, wing2 };
            using (SolidBrush headBrush = new SolidBrush(pen.Color))
            {
                g.FillPolygon(headBrush, arrowHeadPolygon);
            }
        }
    }
}