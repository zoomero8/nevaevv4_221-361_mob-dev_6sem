using System;

namespace cnsRoadEditor
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8; // Важно для корректного отображения символов дорог
            Console.WriteLine("Добро пожаловать в cnsRoadEditor!");

            // Пример использования:

            // 1. Создание новой дорожной карты
            int width = 40;
            int height = 20;
            RoadMap map = new RoadMap(width, height);

            // 2. Рисование дорог
            RoadGenerator.DrawLine(map, 2, 2, 10, 2, RoadType.Horizontal); // Горизонтальная линия
            RoadGenerator.DrawLine(map, 5, 2, 5, 7, RoadType.Vertical);   // Вертикальная линия
            RoadGenerator.DrawRectangle(map, 12, 1, 18, 5);              // Прямоугольник
            RoadGenerator.DrawGrid(map, 0, 0, width - 1, height - 1, 4, 4); // Сетка

            // 3. Отображение карты в консоли
            Console.WriteLine("\n--- Сгенерированная дорожная карта (базовая) ---");
            MapRenderer.DisplayMap(map); // [cite: 4]

            // 4. Преобразование в символьную спрайтовую карту и отображение
            Console.WriteLine("\n--- Сгенерированная дорожная карта (символьные спрайты) ---");
            MapRenderer.DisplayCharacterSpriteMap(map); // [cite: 5, 6]

            // 5. Автоматическая генерация (пример параметров)
            Console.WriteLine("\n--- Автоматически сгенерированная дорожная карта (базовая) ---");
            RoadMap autoMap = new RoadMap(50, 25);
            RoadGenerator.GenerateAutomaticMap(autoMap, 5, 15, 0.05); // Пример параметров: minRoads, maxRoads, density [cite: 3]
            MapRenderer.DisplayCharacterSpriteMap(autoMap);

            // 6. Сохранение и загрузка карты (реализовано в классе RoadMap)
            string filename = "roadmap.txt";
            map.SaveMapToFile(filename);
            Console.WriteLine($"\nКарта сохранена в {filename}");

            RoadMap loadedMap = new RoadMap(1, 1); // Инициализация с фиктивным размером, будет изменена при загрузке
            loadedMap.LoadMapFromFile(filename);
            Console.WriteLine($"\nКарта загружена из {filename}");
            Console.WriteLine("\n--- Загруженная дорожная карта (символьные спрайты) ---");
            MapRenderer.DisplayCharacterSpriteMap(loadedMap);

            // Обратите внимание: для спрайтовой карты изображений обычно требуется графическая библиотека
            // (например, System.Drawing, WPF или игровой движок типа Unity),
            // поскольку консольные приложения являются текстовыми. В задании упоминается загрузка листа спрайтов,
            // но прямое отображение его в консольном приложении без внешних библиотек невозможно.
            // Базовое представление того, как вы могли бы концептуально это обработать, находится в MapRenderer,
            // но оно не будет отображать изображения в консоли. [cite: 6, 7]

            Console.ReadKey();
        }
    }
}