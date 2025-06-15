using System.Windows.Media;

namespace ColorTrainer.Models
{
    public class ColorInfo
    {
        // Название цвета, которое мы будем показывать игроку
        public string Name { get; set; }

        // Кисть, которую мы будем использовать для окрашивания кнопок
        public SolidColorBrush Brush { get; set; }
    }
}