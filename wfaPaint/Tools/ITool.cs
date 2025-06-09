// --- ИЗМЕНИТЬ ФАЙЛ ITool.cs ---

using System.Drawing;
using System.Windows.Forms;
using wfaPaint;

namespace wfaPaint.Tools
{
    public interface ITool
    {
        string Name { get; }
        Cursor ToolCursor { get; }

        void OnMouseDown(MouseEventArgs e, DrawingCanvas canvas, ToolSettings settings);
        void OnMouseMove(MouseEventArgs e, DrawingCanvas canvas, ToolSettings settings);
        void OnMouseUp(MouseEventArgs e, DrawingCanvas canvas, ToolSettings settings);
    }

    // (ИЗМЕНЕНИЕ) Новый интерфейс для интерактивных инструментов
    public interface IInteractiveTool
    {
        void OnMouseWheel(MouseEventArgs e);
    }
}