using System;
using System.Linq;

class Program
{
    static void Main()
    {
        // Флаг для перезапуска всей игровой сессии (включая выбор режима)
        bool restartGameSession = true;

        while (restartGameSession) // Главный цикл игровой сессии
        {
            Console.Clear(); // Очищаем консоль для начала новой сессии

            // --- Выбор режима отображения в начале каждой сессии ---
            Console.WriteLine("Выберите режим отображения:");
            Console.WriteLine("1 — Цифры");
            Console.WriteLine("2 — Блоки");
            Console.Write("Ваш выбор: ");
            int mode = 1; // Режим по умолчанию
            string? modeInput = Console.ReadLine();
            // Пытаемся преобразовать ввод в число и проверяем, что это 1 или 2
            if (!int.TryParse(modeInput, out mode) || (mode != 1 && mode != 2))
            {
                Console.WriteLine("Неверный ввод. Используется режим по умолчанию (1 — Цифры).");
                mode = 1; // Возвращаемся к режиму по умолчанию
            }

            // --- Цикл для прохождения нескольких уровней в рамках одной сессии ---
            while (true) // Этот цикл продолжается, пока игрок хочет играть уровни
            {
                Console.Clear(); // Очищаем консоль для нового уровня

                // Генерация нового ключа для каждого уровня
                int[] key = GenerateRandomArray(4, 10, 90); // Ключ из 4 элементов, значения от 10 до 90, кратные 10

                // Генерация нового набора замков для каждого уровня
                int[][] locks = new int[4][]; // Массив из 4 замков
                for (int i = 0; i < locks.Length; i++)
                {
                    locks[i] = GenerateRandomArray(4, 10, 90); // Каждый замок - массив из 4 элементов
                }

                // Гарантируем, что хотя бы один замок идеально подходит к ключу
                Random rand = new Random();
                int perfectMatchLockIndex = rand.Next(0, locks.Length); // Выбираем случайный замок
                locks[perfectMatchLockIndex] = GeneratePerfectComplementMatch(key); // Делаем его подходящим


                // --- Игровой цикл для текущего уровня ---
                bool levelCompleted = false; // Флаг, показывающий, пройден ли текущий уровень

                while (!levelCompleted) // Цикл попыток на текущем уровне
                {
                    // Отображение ключа
                    Console.WriteLine("\nКлюч:");
                    if (mode == 1) // Режим "Цифры"
                        Console.WriteLine("[" + string.Join(", ", key) + "]");
                    else // Режим "Блоки"
                        DrawHorizontalBars(key);

                    // Отображение замков
                    for (int i = 0; i < locks.Length; i++)
                    {
                        Console.WriteLine($"\nЗамок {i + 1}:");
                        if (mode == 1) // Режим "Цифры"
                            Console.WriteLine("[" + string.Join(", ", locks[i]) + "]");
                        else // Режим "Блоки" (зеркально)
                            DrawHorizontalBars(locks[i], mirror: true);
                    }

                    // Получение выбора игрока
                    Console.Write("\nВведите номер замка: ");
                    string? input = Console.ReadLine();

                    // Проверка ввода и выбор замка
                    if (int.TryParse(input, out int choice) && choice >= 1 && choice <= locks.Length)
                    {
                        int[] selectedLock = locks[choice - 1]; // Выбранный игроком замок

                        // Проверка, является ли выбранный замок идеальным дополнением к ключу
                        bool perfectMatch = IsPerfectComplementMatch(key, selectedLock);

                        if (perfectMatch) // Если замок подошел
                        {
                            Console.WriteLine("✅ Замок подходит!");
                            levelCompleted = true; // Уровень пройден
                            Console.WriteLine("Нажмите [пробел] для перехода на СЛЕДУЮЩИЙ УРОВЕНЬ.");
                            Console.WriteLine("Нажмите [Enter] для начала новой игры (с выбором режима).");
                            Console.WriteLine("Нажмите любую другую клавишу для выхода.");

                            ConsoleKeyInfo keyPress = Console.ReadKey(true); // Ожидаем нажатие клавиши
                            if (keyPress.Key == ConsoleKey.Spacebar)
                            {
                                // Переход на следующий уровень (выход из цикла текущего уровня)
                                break;
                            }
                            else if (keyPress.Key == ConsoleKey.Enter)
                            {
                                // Перезапуск всей игровой сессии (возврат к выбору режима)
                                goto RestartGameSession;
                            }
                            else
                            {
                                return; // Выход из игры
                            }
                        }
                        else // Если замок не подошел
                        {
                            Console.WriteLine("❌ Замок не подходит.");
                            Console.WriteLine("Нажмите [пробел] для перезапуска ТЕКУЩЕГО УРОВНЯ.");
                            Console.WriteLine("Нажмите [Enter] для начала новой игры (с выбором режима).");
                            Console.WriteLine("Нажмите любую другую клавишу для выхода.");

                            ConsoleKeyInfo keyPress = Console.ReadKey(true);
                            if (keyPress.Key == ConsoleKey.Spacebar)
                            {
                                // Повторная попытка на текущем уровне
                                Console.Clear();
                            }
                            else if (keyPress.Key == ConsoleKey.Enter)
                            {
                                // Перезапуск всей игровой сессии
                                goto RestartGameSession;
                            }
                            else
                            {
                                return; // Выход из игры
                            }
                        }
                    }
                    else // Если ввод номера замка некорректный
                    {
                        Console.WriteLine("❗ Неверный ввод.");
                        Console.WriteLine("Нажмите [пробел] для перезапуска ТЕКУЩЕГО УРОВНЯ.");
                        Console.WriteLine("Нажмите [Enter] для начала новой игры (с выбором режима).");
                        Console.WriteLine("Нажмите любую другую клавишу для выхода.");

                        ConsoleKeyInfo keyPress = Console.ReadKey(true);
                        if (keyPress.Key == ConsoleKey.Spacebar)
                        {
                            // Повторная попытка на текущем уровне
                            Console.Clear();
                        }
                        else if (keyPress.Key == ConsoleKey.Enter)
                        {
                            // Перезапуск всей игровой сессии
                            goto RestartGameSession;
                        }
                        else
                        {
                            return; // Выход из игры
                        }
                    }
                } // Конец цикла попыток (while !levelCompleted)
                  // Если levelCompleted = true, цикл while(true) для уровней продолжится
            } // Конец цикла уровней (while true)

        // Метка для goto, позволяет перезапустить всю игровую сессию с выбора режима
        RestartGameSession:;
        } // Конец главного цикла игровой сессии (while restartGameSession)
    }

    // Генерирует массив случайных целых чисел, кратных 10
    static int[] GenerateRandomArray(int size, int min, int max)
    {
        Random rand = new Random(); // Создаем новый экземпляр Random каждый раз (может быть не лучшей практикой для частых вызовов)
        int[] arr = new int[size];
        for (int i = 0; i < size; i++)
        {
            // Генерируем случайное число десятков и умножаем на 10
            arr[i] = rand.Next(min / 10, max / 10 + 1) * 10;
        }
        return arr;
    }

    // Генерирует замок, который является идеальным дополнением к ключу (сумма соответствующих элементов равна 100)
    static int[] GeneratePerfectComplementMatch(int[] key)
    {
        int[] perfectMatch = new int[key.Length];
        for (int i = 0; i < key.Length; i++)
        {
            perfectMatch[i] = 100 - key[i]; // Каждый элемент замка = 100 - соответствующий элемент ключа
        }
        return perfectMatch;
    }

    // Проверяет, является ли замок 'test' идеальным дополнением ключа 'key'
    static bool IsPerfectComplementMatch(int[] key, int[] test)
    {
        if (key.Length != test.Length) return false; // Длины должны совпадать
        for (int i = 0; i < key.Length; i++)
            if (key[i] + test[i] != 100) // Сумма соответствующих элементов должна быть 100
                return false; // Если хотя бы одна пара не сходится, замок не подходит
        return true; // Все пары сошлись, замок подходит
    }

    // Рисует горизонтальные полосы (блоки) для отображения значений массива
    // mirror = true смещает блоки вправо для "зеркального" отображения замков
    static void DrawHorizontalBars(int[] values, bool mirror = false)
    {
        foreach (int value in values)
        {
            int blocks = value / 10; // Количество блоков = значение / 10
            if (mirror)
                // Для зеркального отображения: сначала пробелы, потом блоки
                Console.WriteLine(new string(' ', 10 - blocks) + new string('█', blocks));
            else
                // Обычное отображение: блоки слева
                Console.WriteLine(new string('█', blocks));
        }
    }
}