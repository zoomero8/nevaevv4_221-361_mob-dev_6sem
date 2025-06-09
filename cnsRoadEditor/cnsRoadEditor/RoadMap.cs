using System;
using System.IO;

namespace cnsRoadEditor
{
    public enum RoadType
    {
        None,
        Horizontal, // ─
        Vertical,   // │
        Intersection, // ┼
        CornerTopLeft, // ┌
        CornerTopRight, // ┐
        CornerBottomLeft, // └
        CornerBottomRight, // ┘
        JunctionLeft, // ├ (дорога идет влево, вверх, вниз)
        JunctionRight, // ┤ (дорога идет вправо, вверх, вниз)
        JunctionUp, // ┬ (дорога идет вверх, влево, вправо)
        JunctionDown // ┴ (дорога идет вниз, влево, вправо)
    }

    public class RoadMap
    {
        public RoadType[,] Map { get; private set; }
        public int Width { get; private set; }
        public int Height { get; private set; }

        public RoadMap(int width, int height)
        {
            Width = width;
            Height = height;
            Map = new RoadType[height, width];
            InitializeMap();
        }

        private void InitializeMap()
        {
            for (int i = 0; i < Height; i++)
            {
                for (int j = 0; j < Width; j++)
                {
                    Map[i, j] = RoadType.None;
                }
            }
        }

        public void SetRoad(int x, int y, RoadType type)
        {
            if (x >= 0 && x < Width && y >= 0 && y < Height)
            {
                // Это место, где вы могли бы реализовать логику наложения, если применимы конкретные правила,
                // выходящие за рамки простого перезаписывания.
                // Сейчас он просто устанавливает тип, эффективно "накладывая" его путем замены существующего типа.
                Map[y, x] = type;
            }
        }

        public RoadType GetRoad(int x, int y)
        {
            if (x >= 0 && x < Width && y >= 0 && y < Height)
            {
                return Map[y, x];
            }
            return RoadType.None;
        }

        public void SaveMapToFile(string filePath)
        {
            try
            {
                using (StreamWriter sw = new StreamWriter(filePath))
                {
                    sw.WriteLine($"{Width},{Height}"); // Записать размеры
                    for (int i = 0; i < Height; i++)
                    {
                        for (int j = 0; j < Width; j++)
                        {
                            sw.Write((int)Map[i, j] + (j == Width - 1 ? "" : ","));
                        }
                        sw.WriteLine();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при сохранении карты: {ex.Message}");
            }
        }

        public void LoadMapFromFile(string filePath)
        {
            try
            {
                using (StreamReader sr = new StreamReader(filePath))
                {
                    string dimensionsLine = sr.ReadLine();
                    if (dimensionsLine == null) throw new InvalidDataException("Файл пуст или поврежден.");

                    string[] dimensions = dimensionsLine.Split(',');
                    if (dimensions.Length != 2 || !int.TryParse(dimensions[0], out int loadedWidth) || !int.TryParse(dimensions[1], out int loadedHeight))
                    {
                        throw new InvalidDataException("Неверные размеры карты в файле.");
                    }

                    Width = loadedWidth;
                    Height = loadedHeight;
                    Map = new RoadType[Height, Width];

                    for (int i = 0; i < Height; i++)
                    {
                        string line = sr.ReadLine();
                        if (line == null) throw new InvalidDataException("Файл короче, чем ожидалось.");

                        string[] types = line.Split(',');
                        if (types.Length != Width) throw new InvalidDataException("Неверное количество типов дорог в строке.");

                        for (int j = 0; j < Width; j++)
                        {
                            if (int.TryParse(types[j], out int roadTypeInt))
                            {
                                Map[i, j] = (RoadType)roadTypeInt;
                            }
                            else
                            {
                                throw new InvalidDataException($"Неверное значение типа дороги в [{i},{j}].");
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при загрузке карты: {ex.Message}");
                InitializeMap(); // Переинициализировать пустую карту в случае ошибки
            }
        }
    }
}