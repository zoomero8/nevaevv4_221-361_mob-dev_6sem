using System.Drawing;
using System.Windows.Forms;
using wfaPaint;

namespace wfaPaint.Tools
{
    public class LineTool : ShapeToolBase
    {
        public override string Name => "Line";
       
        protected override void DrawShape(Graphics g, Point currentPoint, Pen pen)
        {
            g.DrawLine(pen, startPoint, currentPoint);
        }
    }
}