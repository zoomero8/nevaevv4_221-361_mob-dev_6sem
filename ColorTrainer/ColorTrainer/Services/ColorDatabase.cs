using ColorTrainer.Models;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Media;

namespace ColorTrainer.Services
{
    public static class ColorDatabase
    {
        // --- Откройте файл Services/ColorDatabase.cs и замените список _allColors на этот ---

        private static readonly List<ColorInfo> _allColors = new List<ColorInfo>
{
    // === ОБЯЗАТЕЛЬНЫЕ ЦВЕТА ДЛЯ РЕЦЕПТОВ ===
    // Способ 1: Прямое использование статических цветов
    new ColorInfo { Name = "Белый", Brush = new SolidColorBrush(Colors.White) },
    new ColorInfo { Name = "Черный", Brush = new SolidColorBrush(Colors.Black) },
    new ColorInfo { Name = "Серый", Brush = new SolidColorBrush(Colors.Gray) },

    // Способ 2: Использование конвертера из строки (HEX)
    new ColorInfo { Name = "Коричневый", Brush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#A52A2A")) },
    
    // === Базовые цвета (с чистыми HEX-кодами) ===
    new ColorInfo { Name = "Красный", Brush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF0000")) },
    new ColorInfo { Name = "Синий", Brush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#0000FF")) },
    new ColorInfo { Name = "Зеленый", Brush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#008000")) },
    new ColorInfo { Name = "Оранжевый", Brush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFA500")) },
    new ColorInfo { Name = "Фиолетовый", Brush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#800080")) },
    new ColorInfo { Name = "Желтый", Brush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFFF00")) },
    new ColorInfo { Name = "Розовый", Brush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFC0CB")) },

    // === Остальные цвета из вашего списка ===
    new ColorInfo { Name = "Бирюзовый", Brush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#39CCCC")) },
    new ColorInfo { Name = "Лаймовый", Brush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#01FF70")) },
    new ColorInfo { Name = "Морской волны", Brush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#7FDBFF")) },
    new ColorInfo { Name = "Малиновый", Brush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#DC143C")) },
    new ColorInfo { Name = "Коралловый", Brush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF7F50")) },
    new ColorInfo { Name = "Лососевый", Brush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FA8072")) },
    new ColorInfo { Name = "Фуксия", Brush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF00FF")) },
    new ColorInfo { Name = "Индиго", Brush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#4B0082")) },
    new ColorInfo { Name = "Ультрамарин", Brush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#120A8F")) },
    new ColorInfo { Name = "Небесно-голубой", Brush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#87CEEB")) },
    new ColorInfo { Name = "Кобальтовый", Brush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#0047AB")) },
    new ColorInfo { Name = "Оливковый", Brush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#808000")) },
    new ColorInfo { Name = "Изумрудный", Brush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#50C878")) },
    new ColorInfo { Name = "Салатовый", Brush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#98FB98")) },
    new ColorInfo { Name = "Нефритовый", Brush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#00A86B")) },
    new ColorInfo { Name = "Лавандовый", Brush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#E6E6FA")) },
    new ColorInfo { Name = "Аметистовый", Brush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#9966CC")) },
    new ColorInfo { Name = "Сливовый", Brush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#DDA0DD")) },
    new ColorInfo { Name = "Шоколадный", Brush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#D2691E")) },
    new ColorInfo { Name = "Бежевый", Brush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#F5F5DC")) },
    new ColorInfo { Name = "Серебряный", Brush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#C0C0C0")) },
    new ColorInfo { Name = "Графитовый", Brush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#36454F")) },
    new ColorInfo { Name = "Золотой", Brush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFD700")) },
    new ColorInfo { Name = "Цвет хаки", Brush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#C3B091")) },
    new ColorInfo { Name = "Горчичный", Brush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFDB58")) },
    new ColorInfo { Name = "Бордовый", Brush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#800000")) }
};

        public static List<ColorInfo> GetColors()
        {
            return _allColors;
        }

        public static ColorInfo GetColorByName(string name)
        {
            // FirstOrDefault вернет null, если цвет не найден
            return _allColors.FirstOrDefault(c => c.Name == name);
        }
    }
}