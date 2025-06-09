using System.Drawing;
using System.Windows.Forms;
using wfaPaint;

namespace wfaPaint.Tools
{
    public class PencilTool : ITool
    {
        private Point _previousPoint;
        private bool _isDrawing = false;

        public string Name => "Pencil";
        public Cursor ToolCursor => Cursors.Cross;

        public void OnMouseDown(MouseEventArgs e, DrawingCanvas canvas, ToolSettings settings)
        {
            if (e.Button == MouseButtons.Left)
            {
                _isDrawing = true;
                _previousPoint = e.Location;
            }
        }

        public void OnMouseMove(MouseEventArgs e, DrawingCanvas canvas, ToolSettings settings)
        {
            if (_isDrawing && e.Button == MouseButtons.Left)
            {
                Graphics g = canvas.G;
                g.DrawLine(settings.MyPen, _previousPoint, e.Location);
                _previousPoint = e.Location;
                canvas.BackupState(); 
                canvas.Invalidate();
            }
        }

        public void OnMouseUp(MouseEventArgs e, DrawingCanvas canvas, ToolSettings settings)
        {
            if (e.Button == MouseButtons.Left)
            {
                _isDrawing = false;
            }
        }
    }
}