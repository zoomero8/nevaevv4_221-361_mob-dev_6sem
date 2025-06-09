using System;
using System.Collections.Generic;

namespace cnsRoadEditor
{
    public static class MapRenderer
    {
        // Простое отображение карты (используя 'X' для дороги, ' ' для пустого места) [cite: 4]
        public static void DisplayMap(RoadMap map)
        {
            for (int i = 0; i < map.Height; i++)
            {
                for (int j = 0; j < map.Width; j++)
                {
                    Console.Write(map.GetRoad(j, i) != RoadType.None ? 'X' : ' ');
                }
                Console.WriteLine();
            }
        }

        // Преобразование и отображение карты с использованием символьных спрайтов [cite: 5]
        public static void DisplayCharacterSpriteMap(RoadMap map)
        {
            // Эти символы взяты из предоставленной таблицы символов [cite: 6]
            Dictionary<RoadType, char> spriteChars = new Dictionary<RoadType, char>
            {
                { RoadType.None, ' ' },
                { RoadType.Horizontal, '─' },
                { RoadType.Vertical, '│' },
                { RoadType.Intersection, '┼' },
                { RoadType.CornerTopLeft, '┌' },
                { RoadType.CornerTopRight, '┐' },
                { RoadType.CornerBottomLeft, '└' },
                { RoadType.CornerBottomRight, '┘' },
                { RoadType.JunctionUp, '┴' },    // T-образное пересечение, указывающее вверх (дорога идет сверху, слева, справа)
                { RoadType.JunctionDown, '┬' },  // T-образное пересечение, указывающее вниз (дорога идет снизу, слева, справа)
                { RoadType.JunctionLeft, '├' },  // T-образное пересечение, указывающее влево (дорога идет слева, сверху, снизу)
                { RoadType.JunctionRight, '┤' }  // T-образное пересечение, указывающее вправо (дорога идет справа, сверху, снизу)
            };

            for (int i = 0; i < map.Height; i++)
            {
                for (int j = 0; j < map.Width; j++)
                {
                    RoadType currentRoad = map.GetRoad(j, i);
                    Console.Write(spriteChars.ContainsKey(currentRoad) ? spriteChars[currentRoad] : '?'); // Использовать '?' для неизвестных
                }
                Console.WriteLine();
            }
        }

        // Заполнитель для преобразования в спрайтовую карту изображений
        // Этот метод концептуально показывает идею, но не может отображать реальные изображения в стандартном консольном приложении.
        // Для фактического отображения изображений вам понадобится графическая библиотека или игровой движок. [cite: 6]
        public static void ConvertToImageSpriteMap(RoadMap map)
        {
            Console.WriteLine("\n--- Преобразование в спрайтовую карту изображений (концептуальное) ---");
            Console.WriteLine("Примечание: Для отображения реальных изображений в консольном приложении требуется графическая библиотека.");
            Console.WriteLine("Этот метод концептуально сопоставляет типы дорог с координатами на листе спрайтов.");

            // Предоставленный URL листа спрайтов: https://lpc.opengameart.org/sites/default/files/preview_22.jpg [cite: 7]
            // В реальном приложении вы бы загрузили это изображение, а затем рисовали бы определенные области
            // на основе RoadType в каждой ячейке.

            // Пример: Словарь, сопоставляющий RoadType с координатами (x, y) на листе спрайтов
            // Это очень зависит от расположения на листе спрайтов.
            Dictionary<RoadType, Tuple<int, int>> spriteCoordinates = new Dictionary<RoadType, Tuple<int, int>>
            {
                // Эти координаты являются иллюстративными; вам нужно будет изучить фактический лист спрайтов
                // и определить пиксельные координаты для каждого типа дорожного сегмента.
                { RoadType.Horizontal, new Tuple<int, int>(0, 0) }, // Пример: Верхний левый угол спрайта горизонтальной дороги
                { RoadType.Vertical, new Tuple<int, int>(32, 0) },   // Пример: Некоторое смещение для спрайта вертикальной дороги
                { RoadType.Intersection, new Tuple<int, int>(64, 0) },// Пример: Спрайт пересечения
                // ... и так далее для всех перечислений RoadType
            };

            for (int i = 0; i < map.Height; i++)
            {
                for (int j = 0; j < map.Width; j++)
                {
                    RoadType currentRoad = map.GetRoad(j, i);
                    if (spriteCoordinates.ContainsKey(currentRoad))
                    {
                        Tuple<int, int> coords = spriteCoordinates[currentRoad];
                        // В графическом контексте вы бы нарисовали спрайт по координатам (coords.Item1, coords.Item2)
                        // в позиции текущей ячейки на экране.
                        Console.Write($"[{coords.Item1},{coords.Item2}] "); // Для консоли: вывести координаты
                    }
                    else
                    {
                        Console.Write("[  _  ] "); // Заполнитель для неизвестного/пустого
                    }
                }
                Console.WriteLine();
            }
        }
    }
}