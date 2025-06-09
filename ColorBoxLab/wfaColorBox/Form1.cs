using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ColorMapLogic;

namespace wfaColorBox
{
    public partial class Form1 : Form
    {
        private ColorMapGenerator _generator;
        private MapCell[,] _currentMap; // Текущая карта
        private char _correctColorChar; // Символ цвета, которого сейчас больше всего на текущем поле
        private Dictionary<char, Color> _charToColorMapping; // Для преобразования 'R' в System.Drawing.Color.Red

        // НОВЫЙ СЛОВАРЬ для хранения строковых имен цветов
        private Dictionary<char, string> _colorNamesMapping;

        // Набор цветов, которые еще НЕ БЫЛИ угаданы на ТЕКУЩЕМ раунде
        private List<char> _availableColorsForCurrentRound;

        // Словарь для хранения ссылок на кнопки по их цвету, для удобства управления
        // Мы все равно будем использовать этот словарь, чтобы удобно получать ссылки на кнопки
        // по их символу цвета, так как они будут в дизайнере, но мы будем к ним обращаться.
        private Dictionary<char, Button> _colorButtons;

        // Конструктор формы
        public Form1()
        {
            InitializeComponent();
            _generator = new ColorMapGenerator();

            // Инициализация сопоставления символов цветов с реальными цветами System.Drawing.Color
            // Используем менее яркие цвета, как вы просили
            _charToColorMapping = new Dictionary<char, Color>
            {
                { 'R', Color.FromArgb(255, 128, 128) }, // Светлый красный
                { 'G', Color.FromArgb(128, 255, 128) }, // Светлый зеленый
                { 'B', Color.FromArgb(128, 128, 255) }, // Светлый синий
                { 'Y', Color.FromArgb(255, 255, 128) }  // Светлый желтый
            };

            // ИНИЦИАЛИЗАЦИЯ НОВОГО СЛОВАРЯ для названий цветов
            _colorNamesMapping = new Dictionary<char, string>
            {
                { 'R', "Красный" },
                { 'G', "Зеленый" },
                { 'B', "Синий" },
                { 'Y', "Желтый" }
            };

            // Инициализация словаря для хранения ссылок на кнопки, которые в дизайнере
            _colorButtons = new Dictionary<char, Button>();

            // Привязываем обработчик события Click ко всем кнопкам на панели colorButtonsPanel
            // И заполняем _colorButtons. Предполагается, что Tag кнопок в дизайнере установлен
            // как "R", "G", "B", "Y" соответственно.
            foreach (Control control in colorButtonsPanel.Controls)
            {
                if (control is Button button)
                {
                    // Проверяем, что Tag установлен и является ожидаемым символом цвета
                    if (button.Tag != null && button.Tag.ToString().Length == 1 && _charToColorMapping.ContainsKey(button.Tag.ToString()[0]))
                    {
                        char colorChar = button.Tag.ToString()[0];
                        button.Click += ColorButton_Click; // Привязываем обработчик
                        _colorButtons.Add(colorChar, button); // Сохраняем ссылку на кнопку

                        // Устанавливаем цвета и текст кнопок из наших словарей
                        button.BackColor = _charToColorMapping[colorChar];
                        button.Text = _colorNamesMapping[colorChar];
                    }
                    else
                    {
                        // Если Tag не установлен или некорректен, можно вывести предупреждение
                        // или пропустить эту кнопку.
                        Console.WriteLine($"Предупреждение: Кнопка '{button.Name}' на colorButtonsPanel не имеет корректного Tag для цвета.");
                    }
                }
            }

            StartNewGame(); // Начинаем новую игру при запуске
        }

        /// <summary>
        /// Начинает игру полностью заново (сбрасывает счетчик уровней и доступные цвета).
        /// </summary>
        private void StartNewGame()
        {
            // Инициализируем доступные цвета для текущего раунда полным набором
            _availableColorsForCurrentRound = new List<char> { 'R', 'G', 'B', 'Y' };

            EnableAllColorButtons(); // Активируем и делаем видимыми все кнопки

            // Генерируем и отображаем первую карту нового раунда/игры (используем 4 цвета, если доступно)
            GenerateAndDisplayMap(_availableColorsForCurrentRound.Count);
        }

        // CreateColorButtons() теперь не нужен, так как кнопки из дизайнера

        /// <summary>
        /// Генерирует и отображает карту на основе текущего состояния _availableColorsForCurrentRound.
        /// </summary>
        /// <param name="desiredUniqueColors">Желаемое количество уникальных цветов для генерации карты.</param>
        private void GenerateAndDisplayMap(int desiredUniqueColors)
        {
            ClearMapDisplay(); // Очищаем старую карту

            int rows = 5; // Размеры карты
            int cols = 5;

            try
            {
                // Используем модифицированный GenerateMap, который принимает список доступных цветов
                _currentMap = _generator.GenerateMap(rows, cols, desiredUniqueColors, _availableColorsForCurrentRound);
                DisplayMap(_currentMap); // Отображаем сгенерированную карту
                DetermineCorrectAnswer(_currentMap); // Определяем правильный ответ для этой карты
            }
            catch (ArgumentException ex)
            {
                // При ошибке генерации, начинаем игру заново и отключаем звук уведомления
                MessageBox.Show(
                    $"Ошибка генерации карты: {ex.Message}\nИгра перезапустится.",
                    "Ошибка",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.None
                );
                StartNewGame();
            }
            catch (InvalidOperationException ex)
            {
                // При ошибке операции, начинаем игру заново и отключаем звук уведомления
                MessageBox.Show(
                    $"Ошибка операции при генерации карты: {ex.Message}\nИгра перезапустится.",
                    "Ошибка",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.None
                );
                StartNewGame();
            }
        }

        /// <summary>
        /// Очищает все панели с карты.
        /// </summary>
        private void ClearMapDisplay()
        {
            colorMapPanel.Controls.Clear();
            // Освобождаем ресурсы старых панелей
            foreach (var control in colorMapPanel.Controls.OfType<Panel>())
            {
                control.Dispose();
            }
        }

        /// <summary>
        /// Отображает сгенерированную карту на TableLayoutPanel.
        /// </summary>
        /// <param name="map">Двумерный массив MapCell.</param>
        private void DisplayMap(MapCell[,] map)
        {
            colorMapPanel.SuspendLayout(); // Приостанавливаем компоновку для повышения производительности

            // Настройка TableLayoutPanel для правильного количества строк и столбцов
            colorMapPanel.RowCount = map.GetLength(0);
            colorMapPanel.ColumnCount = map.GetLength(1);

            // Очищаем и настраиваем стили строк/столбцов, чтобы они равномерно распределялись
            colorMapPanel.RowStyles.Clear();
            for (int r = 0; r < map.GetLength(0); r++)
            {
                colorMapPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 100f / map.GetLength(0)));
            }

            colorMapPanel.ColumnStyles.Clear();
            for (int c = 0; c < map.GetLength(1); c++)
            {
                colorMapPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100f / map.GetLength(1)));
            }

            for (int r = 0; r < map.GetLength(0); r++)
            {
                for (int c = 0; c < map.GetLength(1); c++)
                {
                    MapCell cell = map[r, c];
                    Panel cellPanel = new Panel
                    {
                        Dock = DockStyle.Fill,
                        Margin = new Padding(2), // Небольшие отступы между ячейками
                        BackColor = _charToColorMapping[cell.Color],
                        Tag = cell.Color.ToString()
                    };

                    colorMapPanel.Controls.Add(cellPanel, c, r); // Добавляем панель в TableLayoutPanel
                }
            }
            colorMapPanel.ResumeLayout(true); // Возобновляем компоновку
            colorMapPanel.Refresh(); // Принудительное обновление, чтобы убедиться, что все отрисовалось
        }

        /// <summary>
        /// Определяет правильный цвет (тот, которого больше всего) на основе переданной карты.
        /// </summary>
        /// <param name="map">Карта MapCell для анализа.</param>
        private void DetermineCorrectAnswer(MapCell[,] map)
        {
            Dictionary<char, int> counts = _generator.CountColors(map);

            // Фильтруем подсчеты, оставляя только те цвета, чьи кнопки активны (и видны)
            // Мы используем _availableColorsForCurrentRound, который управляет тем, какие цвета активны в игре.
            counts = counts.Where(kv => _availableColorsForCurrentRound.Contains(kv.Key)).ToDictionary(kv => kv.Key, kv => kv.Value);

            if (counts.Any())
            {
                _correctColorChar = counts.OrderByDescending(kv => kv.Value).First().Key;
            }
            else
            {
                _correctColorChar = '\0'; // Все цвета убраны, или ошибка
            }
        }

        /// <summary>
        /// Обработчик события клика по кнопке цвета.
        /// </summary>
        private void ColorButton_Click(object sender, EventArgs e)
        {
            Button clickedButton = sender as Button;
            if (clickedButton == null) return;

            char selectedColor = clickedButton.Tag.ToString()[0];

            if (selectedColor == _correctColorChar)
            {
                // Правильный ответ: убираем цвет из доступных и генерируем новое поле
                MessageBox.Show(
                    $"Правильно! Цвет '{_colorNamesMapping[selectedColor]}' убран из игры.", // ИСПОЛЬЗУЕМ _colorNamesMapping
                    "Успех",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.None
                );

                clickedButton.Enabled = false; // Деактивируем кнопку
                clickedButton.Visible = false; // Скрываем кнопку

                // Удаляем угаданный цвет из списка для текущего раунда
                _availableColorsForCurrentRound.Remove(selectedColor);

                // Проверяем, сколько цветов осталось для текущего раунда
                if (_availableColorsForCurrentRound.Count <= 1) // Если остался 1 или 0 цветов
                {
                    // Все цвета угаданы в этом раунде, игра начинается полностью заново
                    MessageBox.Show(
                        "Все цвета угаданы в этом раунде! Начинается новая игра.",
                        "Уровень завершен",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.None
                    );
                    StartNewGame(); // Начинаем игру полностью заново
                }
                else
                {
                    // Продолжаем текущий раунд, генерируя новое поле с оставшимися цветами
                    // Количество уникальных цветов для новой генерации - это количество оставшихся цветов
                    GenerateAndDisplayMap(_availableColorsForCurrentRound.Count);
                }
            }
            else
            {
                // Неправильный ответ: игра начинается полностью заново
                MessageBox.Show(
                    "Неправильно! Игра начинается заново.",
                    "Ошибка",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.None
                );
                StartNewGame(); // Начинаем игру полностью заново
            }
        }

        /// <summary>
        /// Активирует и делает видимыми все кнопки выбора цвета.
        /// Этот метод теперь вызывается при StartNewGame().
        /// </summary>
        private void EnableAllColorButtons()
        {
            foreach (Control control in colorButtonsPanel.Controls)
            {
                if (control is Button button)
                {
                    button.Enabled = true;
                    button.Visible = true;
                }
            }
        }

        /// <summary>
        /// Деактивирует все кнопки выбора цвета. (Этот метод сейчас не используется, но оставлен для примера)
        /// </summary>
        private void DisableAllColorButtons()
        {
            foreach (Control control in colorButtonsPanel.Controls)
            {
                if (control is Button button)
                {
                    button.Enabled = false;
                }
            }
        }
    }
}