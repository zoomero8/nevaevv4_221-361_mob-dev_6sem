using System;
using System.Collections.Generic;
using System.Linq;
using ColorMapLogic;

public class Program
{
    private static readonly Dictionary<char, string> ColorNames = new Dictionary<char, string>
    {
        { 'R', "Красный (R)" },
        { 'G', "Зеленый (G)" },
        { 'B', "Синий (B)" },
        { 'Y', "Желтый (Y)" }
    };

    public static void Main(string[] args)
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;
        ColorMapGenerator generator = new ColorMapGenerator();

        Console.WriteLine("Добро пожаловать в игру 'Цветовод' (Консольная версия)!");

        while (true) // Цикл для повторения игры
        {
            try
            {
                Console.WriteLine("\n--- Новый уровень ---");
                int rows = 5;
                int cols = 5;
                int numberOfColors = 4; // Используем все 4 цвета, как указано

                // Генерируем карту
                MapCell[,] map = generator.GenerateMap(rows, cols, numberOfColors);

                Console.WriteLine("\nКарта цветов:"); // Изменил текст
                generator.PrintMap(map);

                // Подсчитываем цвета на карте (для определения правильного ответа)
                Dictionary<char, int> counts = generator.CountColors(map);

                Console.WriteLine("\nПосчитай, какого цвета больше всего:");

                List<char> sortedColors = counts.Keys.OrderBy(c => c).ToList(); // Для стабильного порядка вывода вариантов
                for (int i = 0; i < sortedColors.Count; i++)
                {
                    Console.WriteLine($"{i + 1}. {ColorNames[sortedColors[i]]}");
                }

                int correctColorIndex = -1;
                // Находим цвет, которого больше всего
                if (counts.Any())
                {
                    var maxColorEntry = counts.OrderByDescending(kv => kv.Value).First();
                    char maxColorChar = maxColorEntry.Key;
                    correctColorIndex = sortedColors.IndexOf(maxColorChar) + 1; // +1, потому что варианты от 1 до 4
                }
                else
                {
                    Console.WriteLine("На карте нет цветов. Этого не должно быть.");
                    continue; // Начать новый уровень
                }

                Console.Write("Введите номер варианта (1-4): ");
                string input = Console.ReadLine();
                int userChoice;

                if (int.TryParse(input, out userChoice) && userChoice >= 1 && userChoice <= numberOfColors)
                {
                    if (userChoice == correctColorIndex)
                    {
                        Console.WriteLine("Правильно! Отличный цветовод!");
                    }
                    else
                    {
                        Console.WriteLine($"Неправильно. Правильный ответ был {ColorNames[sortedColors[correctColorIndex - 1]]}.");
                    }
                }
                else
                {
                    Console.WriteLine("Некорректный ввод. Пожалуйста, введите число от 1 до 4.");
                }
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"Ошибка при генерации карты: {ex.Message}");
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine($"Ошибка операции при генерации карты: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Произошла непредвиденная ошибка: {ex.Message}");
            }

            Console.WriteLine("\nНажмите любую клавишу для следующего уровня или 'q' для выхода...");
            if (Console.ReadKey().KeyChar == 'q')
            {
                break;
            }
        }

        Console.WriteLine("Спасибо за игру!");
    }
}