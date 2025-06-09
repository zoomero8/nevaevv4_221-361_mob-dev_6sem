using System.Drawing;
using System.Windows.Forms;
using wfaPaint;

namespace wfaPaint.Tools
{
    public abstract class ShapeToolBase : ITool
    {
        protected Point startPoint;
        protected bool isDrawing = false;

        public abstract string Name { get; }
        public virtual Cursor ToolCursor => Cursors.Cross;

        public virtual void OnMouseDown(MouseEventArgs e, DrawingCanvas canvas, ToolSettings settings)
        {
            if (e.Button == MouseButtons.Left)
            {
                isDrawing = true;
                startPoint = e.Location;
                canvas.BackupState();
            }
        }

        public virtual void OnMouseMove(MouseEventArgs e, DrawingCanvas canvas, ToolSettings settings)
        {
            if (isDrawing && e.Button == MouseButtons.Left)
            {
                Graphics g = canvas.RestoreStateAndGetGraphics(); 
                DrawShape(g, e.Location, settings.MyPen);
                canvas.Invalidate();
            }
        }

        public virtual void OnMouseUp(MouseEventArgs e, DrawingCanvas canvas, ToolSettings settings)
        {
            if (isDrawing && e.Button == MouseButtons.Left)
            {
                isDrawing = false;
                Graphics g = canvas.RestoreStateAndGetGraphics();
                DrawShape(g, e.Location, settings.MyPen);     
                canvas.BackupState(); 
                canvas.Invalidate();
            }
        }

        protected abstract void DrawShape(Graphics g, Point currentPoint, Pen pen);
    }
}