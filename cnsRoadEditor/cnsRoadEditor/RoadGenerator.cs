using System;

namespace cnsRoadEditor
{
    public static class RoadGenerator
    {
        // Метод для рисования линии [cite: 2]
        public static void DrawLine(RoadMap map, int x1, int y1, int x2, int y2, RoadType lineType)
        {
            // Простая отрисовка линии (для более надежных линий можно использовать алгоритм Брезенхема или аналогичный)
            // Для простоты, пока предполагаются горизонтальные или вертикальные линии на основе lineType.
            if (x1 == x2) // Вертикальная линия
            {
                for (int y = Math.Min(y1, y2); y <= Math.Max(y1, y2); y++)
                {
                    map.SetRoad(x1, y, lineType);
                }
            }
            else if (y1 == y2) // Горизонтальная линия
            {
                for (int x = Math.Min(x1, x2); x <= Math.Max(x1, x2); x++)
                {
                    map.SetRoad(x, y1, lineType);
                }
            }
            // Для диагональных линий требуется более сложный алгоритм, например, алгоритм Брезенхема.
            // Задание подразумевает простые типы линий, такие как горизонтальные/вертикальные, которые формируют сетки и прямоугольники.
        }

        // Метод для рисования прямоугольника [cite: 2]
        public static void DrawRectangle(RoadMap map, int x1, int y1, int x2, int y2)
        {
            // Нарисовать верхнюю и нижнюю горизонтальные линии
            DrawLine(map, x1, y1, x2, y1, RoadType.Horizontal);
            DrawLine(map, x1, y2, x2, y2, RoadType.Horizontal);

            // Нарисовать левую и правую вертикальные линии
            DrawLine(map, x1, y1, x1, y2, RoadType.Vertical);
            DrawLine(map, x2, y1, x2, y2, RoadType.Vertical);

            // Обработка углов
            map.SetRoad(x1, y1, RoadType.CornerTopLeft);
            map.SetRoad(x2, y1, RoadType.CornerTopRight);
            map.SetRoad(x1, y2, RoadType.CornerBottomLeft);
            map.SetRoad(x2, y2, RoadType.CornerBottomRight);
        }

        // Метод для рисования сетки [cite: 2]
        public static void DrawGrid(RoadMap map, int startX, int startY, int endX, int endY, int cellWidth, int cellHeight)
        {
            for (int y = startY; y <= endY; y += cellHeight)
            {
                DrawLine(map, startX, y, endX, y, RoadType.Horizontal);
            }
            for (int x = startX; x <= endX; x += cellWidth)
            {
                DrawLine(map, x, startY, x, endY, RoadType.Vertical);
            }
            // Логика для определения и установки пересечений внутри сетки будет здесь
            // Это базовая отрисовка сетки. Для идеального рендеринга пересечений вам нужно будет итерировать и проверять соседей.
        }

        // Метод для автоматической генерации карты дорог на основе входящих параметров [cite: 3]
        public static void GenerateAutomaticMap(RoadMap map, int minRoads, int maxRoads, double density)
        {
            Random rand = new Random();
            int numberOfRoads = rand.Next(minRoads, maxRoads + 1);

            for (int i = 0; i < numberOfRoads; i++)
            {
                int roadType = rand.Next(3); // 0 для линии, 1 для прямоугольника, 2 для сетки (простой пример)

                switch (roadType)
                {
                    case 0: // Линия
                        int x1 = rand.Next(map.Width);
                        int y1 = rand.Next(map.Height);
                        int x2, y2;
                        if (rand.Next(2) == 0) // Горизонтальная линия
                        {
                            x2 = rand.Next(map.Width);
                            y2 = y1;
                            DrawLine(map, x1, y1, x2, y2, RoadType.Horizontal);
                        }
                        else // Вертикальная линия
                        {
                            x2 = x1;
                            y2 = rand.Next(map.Height);
                            DrawLine(map, x1, y1, x2, y2, RoadType.Vertical);
                        }
                        break;
                    case 1: // Прямоугольник
                        int rx1 = rand.Next(map.Width / 2);
                        int ry1 = rand.Next(map.Height / 2);
                        int rx2 = rand.Next(map.Width / 2, map.Width);
                        int ry2 = rand.Next(map.Height / 2, map.Height);
                        DrawRectangle(map, rx1, ry1, rx2, ry2);
                        break;
                    case 2: // Сетка
                        int gx1 = rand.Next(map.Width / 4);
                        int gy1 = rand.Next(map.Height / 4);
                        int gx2 = rand.Next(map.Width / 2, map.Width);
                        int gy2 = rand.Next(map.Height / 2, map.Height);
                        int cellW = rand.Next(2, 5); // Случайная ширина ячейки
                        int cellH = rand.Next(2, 5); // Случайная высота ячейки
                        DrawGrid(map, gx1, gy1, gx2, gy2, cellW, cellH);
                        break;
                }
            }

            // Дополнительно: Добавить случайные одиночные сегменты дорог на основе плотности
            for (int i = 0; i < map.Height; i++)
            {
                for (int j = 0; j < map.Width; j++)
                {
                    if (rand.NextDouble() < density && map.GetRoad(j, i) == RoadType.None)
                    {
                        if (rand.Next(2) == 0)
                            map.SetRoad(j, i, RoadType.Horizontal);
                        else
                            map.SetRoad(j, i, RoadType.Vertical);
                    }
                }
            }

            // После отрисовки базовых форм, итерация для уточнения типов дорог (пересечения, углы, развилки)
            // Это важный шаг для точного символьного представления.
            RefineRoadTypes(map);
        }

        // Вспомогательный метод для уточнения типов дорог после первоначальной отрисовки
        private static void RefineRoadTypes(RoadMap map)
        {
            for (int y = 0; y < map.Height; y++)
            {
                for (int x = 0; x < map.Width; x++)
                {
                    if (map.GetRoad(x, y) != RoadType.None)
                    {
                        bool up = (y > 0 && map.GetRoad(x, y - 1) != RoadType.None);
                        bool down = (y < map.Height - 1 && map.GetRoad(x, y + 1) != RoadType.None);
                        bool left = (x > 0 && map.GetRoad(x - 1, y) != RoadType.None);
                        bool right = (x < map.Width - 1 && map.GetRoad(x + 1, y) != RoadType.None);

                        // Определите правильный символ на основе соседей
                        // Эта логика может быть обширной для всех возможных комбинаций.
                        // Здесь приведена упрощенная версия для распространенных случаев:
                        if (up && down && left && right)
                            map.SetRoad(x, y, RoadType.Intersection); // ┼
                        else if (up && down && left)
                            map.SetRoad(x, y, RoadType.JunctionRight); // ┤
                        else if (up && down && right)
                            map.SetRoad(x, y, RoadType.JunctionLeft); // ├
                        else if (left && right && up)
                            map.SetRoad(x, y, RoadType.JunctionDown); // ┬
                        else if (left && right && down)
                            map.SetRoad(x, y, RoadType.JunctionUp); // ┴
                        else if (up && down)
                            map.SetRoad(x, y, RoadType.Vertical); // │
                        else if (left && right)
                            map.SetRoad(x, y, RoadType.Horizontal); // ─
                        else if (up && right)
                            map.SetRoad(x, y, RoadType.CornerBottomLeft); // └
                        else if (up && left)
                            map.SetRoad(x, y, RoadType.CornerBottomRight); // ┘
                        else if (down && right)
                            map.SetRoad(x, y, RoadType.CornerTopLeft); // ┌
                        else if (down && left)
                            map.SetRoad(x, y, RoadType.CornerTopRight); // ┐
                        // Если только один сосед, это может быть тупик или просто прямой участок.
                        // Изначальные DrawLine/Rectangle установят базовые типы, это их уточнит.
                    }
                }
            }
        }
    }
}