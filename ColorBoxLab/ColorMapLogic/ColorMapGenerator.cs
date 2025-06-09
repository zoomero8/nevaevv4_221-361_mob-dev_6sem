using System;
using System.Collections.Generic;
using System.Linq;

namespace ColorMapLogic
{
    public class ColorMapGenerator
    {
        private static readonly char[] AllAvailableColors = { 'R', 'G', 'B', 'Y' };
        private static readonly char[] AvailableShapes = { 'S', 'T', 'C', '*' };

        // Исходный метод GenerateMap, который использует все цвета (для начала нового уровня)
        public MapCell[,] GenerateMap(int rows, int cols, int numberOfUniqueColorsToUse)
        {
            return GenerateMap(rows, cols, numberOfUniqueColorsToUse, AllAvailableColors.ToList());
        }

        public MapCell[,] GenerateMap(int rows, int cols, int numberOfUniqueColorsToUse, List<char> colorsToUse)
        {
            if (rows <= 0 || cols <= 0)
            {
                throw new ArgumentException("Размеры карты должны быть больше нуля.");
            }

            if (colorsToUse == null || !colorsToUse.Any())
            {
                throw new ArgumentException("Список цветов для использования не может быть пустым.");
            }

            if (numberOfUniqueColorsToUse > colorsToUse.Count)
            {
                numberOfUniqueColorsToUse = colorsToUse.Count;
            }

            if (numberOfUniqueColorsToUse <= 1)
            {
                if (numberOfUniqueColorsToUse == 1)
                {
                    MapCell[,] singleColorMap = new MapCell[rows, cols];
                    char singleColor = colorsToUse.First();
                    Random r = new Random();
                    for (int r_idx = 0; r_idx < rows; r_idx++)
                    {
                        for (int c_idx = 0; c_idx < cols; c_idx++)
                        {
                            singleColorMap[r_idx, c_idx] = new MapCell(singleColor, AvailableShapes[r.Next(AvailableShapes.Length)]);
                        }
                    }
                    return singleColorMap;
                }
                throw new ArgumentException($"Количество уникальных цветов должно быть хотя бы 2 для интересной генерации.");
            }

            int totalCells = rows * cols;
            MapCell[,] colorShapeMap = new MapCell[rows, cols];
            Random random = new Random();

            // МОДИФИЦИРОВАННАЯ ЛОГИКА ГЕНЕРАЦИИ УНИКАЛЬНЫХ КОЛИЧЕСТВ
            List<int> counts = new List<int>();
            const int MAX_GENERATION_ATTEMPTS_FOR_COUNTS = 500; // Увеличиваем попытки
            const int MIN_DIFFERENCE = 1; // Минимальная разница между количествами
            const int MAX_DIFFERENCE_FOR_LARGEST = 3; // Максимальная разница между самым большим и вторым по величине

            for (int attempt = 0; attempt < MAX_GENERATION_ATTEMPTS_FOR_COUNTS; attempt++)
            {
                counts.Clear();
                int remainingToDistribute = totalCells;
                List<int> currentGeneratedCounts = new List<int>();

                // Генерируем уникальные количества для (N-1) цветов
                for (int i = 0; i < numberOfUniqueColorsToUse - 1; i++)
                {
                    int minCount = 1; // Каждый цвет должен быть представлен хотя бы раз
                    // Максимальное значение для текущего цвета: оставшиеся ячейки минус (количество оставшихся цветов - 1) * минимальная разница (1)
                    // Это гарантирует, что у нас будет достаточно ячеек для остальных уникальных цветов.
                    int maxCount = remainingToDistribute - (numberOfUniqueColorsToUse - 1 - i) * MIN_DIFFERENCE;

                    if (maxCount < minCount)
                    {
                        currentGeneratedCounts.Clear(); // Невозможно сгенерировать, начинаем заново
                        break;
                    }

                    int newCount;
                    do
                    {
                        newCount = random.Next(minCount, maxCount + 1);
                    } while (currentGeneratedCounts.Contains(newCount)); // Убеждаемся, что количество уникально

                    currentGeneratedCounts.Add(newCount);
                    remainingToDistribute -= newCount;
                }

                // Добавляем последнее оставшееся количество
                if (currentGeneratedCounts.Count == numberOfUniqueColorsToUse - 1)
                {
                    currentGeneratedCounts.Add(remainingToDistribute);
                }

                // Проверяем условия:
                // 1. Все количества сгенерированы
                // 2. Все количества уникальны
                // 3. Сумма всех количеств равна totalCells
                // 4. Дополнительное условие: разница между наибольшим и вторым по величине не слишком велика
                if (currentGeneratedCounts.Count == numberOfUniqueColorsToUse &&
                    currentGeneratedCounts.Distinct().Count() == numberOfUniqueColorsToUse &&
                    currentGeneratedCounts.Sum() == totalCells)
                {
                    // Проверяем разницу между первым и вторым по величине
                    var sortedCounts = currentGeneratedCounts.OrderByDescending(x => x).ToList();
                    if (sortedCounts.Count >= 2)
                    {
                        if (sortedCounts[0] - sortedCounts[1] <= MAX_DIFFERENCE_FOR_LARGEST)
                        {
                            counts = currentGeneratedCounts.OrderBy(x => random.Next()).ToList(); // Перемешиваем порядок
                            break; // Успешно сгенерировано
                        }
                    }
                    else if (sortedCounts.Count == 1) // Если только один цвет, это не требует разницы
                    {
                        counts = currentGeneratedCounts.OrderBy(x => random.Next()).ToList();
                        break;
                    }
                }

                if (attempt == MAX_GENERATION_ATTEMPTS_FOR_COUNTS - 1)
                {
                    throw new InvalidOperationException("Не удалось сгенерировать карту с уникальными и 'сложными' количествами цветов. Попробуйте другие размеры карты или количество цветов. (Слишком много попыток)");
                }
            }
            // =========================================================================

            List<char> selectedColors = colorsToUse.OrderBy(x => random.Next()).Take(numberOfUniqueColorsToUse).ToList();

            List<char> cellsToFill = new List<char>();
            for (int i = 0; i < numberOfUniqueColorsToUse; i++)
            {
                char color = selectedColors[i];
                int count = counts[i];

                for (int j = 0; j < count; j++)
                {
                    cellsToFill.Add(color);
                }
            }

            cellsToFill = cellsToFill.OrderBy(x => random.Next()).ToList(); // Перемешиваем все ячейки

            int cellIndex = 0;
            for (int r = 0; r < rows; r++)
            {
                for (int c = 0; c < cols; c++)
                {
                    char color = cellsToFill[cellIndex];
                    char shape = AvailableShapes[random.Next(AvailableShapes.Length)];
                    colorShapeMap[r, c] = new MapCell(color, shape);
                    cellIndex++;
                }
            }

            return colorShapeMap;
        }

        public void PrintMap(MapCell[,] map)
        {
            for (int r = 0; r < map.GetLength(0); r++)
            {
                for (int c = 0; c < map.GetLength(1); c++)
                {
                    Console.Write($"{map[r, c].Color} ");
                }
                Console.WriteLine();
            }
        }

        public Dictionary<char, int> CountColors(MapCell[,] map)
        {
            Dictionary<char, int> colorCounts = new Dictionary<char, int>();
            foreach (char color in AllAvailableColors)
            {
                colorCounts[color] = 0;
            }

            for (int r = 0; r < map.GetLength(0); r++)
            {
                for (int c = 0; c < map.GetLength(1); c++)
                {
                    char color = map[r, c].Color;
                    colorCounts[color]++;
                }
            }
            return colorCounts.Where(kv => kv.Value > 0).ToDictionary(kv => kv.Key, kv => kv.Value);
        }
    }
}