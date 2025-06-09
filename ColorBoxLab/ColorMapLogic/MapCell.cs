namespace ColorMapLogic
{
    public class MapCell
    {
        public char Color { get; set; } // Например, 'R', 'G', 'B', 'Y'
        public char Shape { get; set; } // Например, 'S' (Square), 'T' (Triangle), 'C' (Circle), '*' (Star)

        public MapCell(char color, char shape)
        {
            Color = color;
            Shape = shape; // Фигура пока не будет отображаться в консоли, но сохраняется
        }

        public override string ToString()
        {
            // Для консоли пока вернем только цвет
            return Color.ToString();
        }
    }
}