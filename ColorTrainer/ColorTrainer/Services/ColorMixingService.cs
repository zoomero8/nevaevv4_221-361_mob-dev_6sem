using ColorTrainer.Models;
using System.Collections.Generic;

namespace ColorTrainer.Services
{
    public static class ColorMixingService
    {
        // Поля теперь просто объявляются, без инициализации
        private static readonly List<ColorMixRecipe> _recipes;
        private static readonly List<ColorInfo> _primaryIngredients;

        // Статический конструктор - он будет вызван автоматически и в нужный момент
        static ColorMixingService()
        {
            _recipes = new List<ColorMixRecipe>
    {
        new ColorMixRecipe // Оранжевый = Красный + Желтый
        {
            Result = ColorDatabase.GetColorByName("Оранжевый"), // Существует
            Ingredients = new List<ColorInfo> { ColorDatabase.GetColorByName("Красный"), ColorDatabase.GetColorByName("Желтый") } // Существуют
        },
        new ColorMixRecipe // Зеленый = Синий + Желтый
        {
            Result = ColorDatabase.GetColorByName("Зеленый"), // Существует
            Ingredients = new List<ColorInfo> { ColorDatabase.GetColorByName("Синий"), ColorDatabase.GetColorByName("Желтый") } // Существуют
        },
        // и так далее для всех рецептов...
        new ColorMixRecipe // Коричневый = Красный + Зеленый
        {
            Result = ColorDatabase.GetColorByName("Коричневый"), // ТЕПЕРЬ СУЩЕСТВУЕТ
            Ingredients = new List<ColorInfo> { ColorDatabase.GetColorByName("Красный"), ColorDatabase.GetColorByName("Зеленый") } // Существуют
        },
    };

            // Инициализируем список ингредиентов
            _primaryIngredients = new List<ColorInfo>
            {
                ColorDatabase.GetColorByName("Красный"),
                ColorDatabase.GetColorByName("Желтый"),
                ColorDatabase.GetColorByName("Синий"),
                ColorDatabase.GetColorByName("Белый"),
                ColorDatabase.GetColorByName("Черный"),
                ColorDatabase.GetColorByName("Зеленый")
            };
        }

        // Методы для получения данных теперь просто возвращают готовые списки
        public static List<ColorMixRecipe> GetRecipes() => _recipes;

        public static List<ColorInfo> GetPrimaryIngredients() => _primaryIngredients;
    }
}