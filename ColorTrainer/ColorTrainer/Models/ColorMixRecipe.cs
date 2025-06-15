using System.Collections.Generic;

namespace ColorTrainer.Models
{
    public class ColorMixRecipe
    {
        // Цвет, который получается в результате смешивания
        public ColorInfo Result { get; set; }

        // Список цветов-"ингредиентов"
        public List<ColorInfo> Ingredients { get; set; }
    }
}