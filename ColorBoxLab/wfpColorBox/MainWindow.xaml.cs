using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ColorMapLogic;

namespace wfpColorBox
{
    public partial class MainWindow : Window
    {
        private ColorMapGenerator _generator;
        private MapCell[,] _currentMap;
        private char _correctColorChar;
        private Dictionary<char, SolidColorBrush> _charToColorMapping;
        private Dictionary<char, string> _colorNamesMapping;
        private List<char> _availableColorsForCurrentRound;
        private Dictionary<char, Button> _colorButtons;

        public MainWindow()
        {
            InitializeComponent();
            _generator = new ColorMapGenerator();

            _charToColorMapping = new Dictionary<char, SolidColorBrush>
            {
                { 'R', new SolidColorBrush(Color.FromArgb(255, 255, 128, 128)) },
                { 'G', new SolidColorBrush(Color.FromArgb(255, 128, 255, 128)) },
                { 'B', new SolidColorBrush(Color.FromArgb(255, 128, 128, 255)) },
                { 'Y', new SolidColorBrush(Color.FromArgb(255, 255, 255, 128)) }
            };

            _colorNamesMapping = new Dictionary<char, string>
            {
                { 'R', "Красный" },
                { 'G', "Зеленый" },
                { 'B', "Синий" },
                { 'Y', "Желтый" }
            };

            _colorButtons = new Dictionary<char, Button>();

            CreateColorButtons();

            StartNewGame();
        }

        private void StartNewGame()
        {
            EnableAllColorButtons();

            _availableColorsForCurrentRound = new List<char> { 'R', 'G', 'B', 'Y' };

            GenerateAndDisplayMap(_availableColorsForCurrentRound.Count);
        }

        private void CreateColorButtons()
        {
            // Находим StackPanel для кнопок в XAML по ее имени
            StackPanel buttonsPanel = (StackPanel)FindName("buttonsStackPanel");
            if (buttonsPanel == null) // Добавляем проверку на null, если FindName не найдет элемент
            {
                MessageBox.Show("Ошибка: StackPanel с именем 'buttonsStackPanel' не найдена в XAML.", "Ошибка UI", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            buttonsPanel.Children.Clear();

            var buttonOrder = new List<char> { 'R', 'G', 'B', 'Y' };

            foreach (char colorChar in buttonOrder)
            {
                Button button = new Button
                {
                    Content = _colorNamesMapping[colorChar],
                    Background = _charToColorMapping[colorChar],
                    Tag = colorChar.ToString(),
                    Width = 80,
                    Height = 40,
                    Margin = new Thickness(5)
                };
                button.Click += ColorButton_Click;
                buttonsPanel.Children.Add(button);
                _colorButtons.Add(colorChar, button);
            }
        }


        private void GenerateAndDisplayMap(int desiredUniqueColors)
        {
            ClearMapDisplay();

            int rows = 5;
            int cols = 5;

            try
            {
                _currentMap = _generator.GenerateMap(rows, cols, desiredUniqueColors, _availableColorsForCurrentRound);
                DisplayMap(_currentMap);
                DetermineCorrectAnswer(_currentMap);
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show(
                    $"Ошибка генерации карты: {ex.Message}\nИгра перезапустится.",
                    "Ошибка",
                    MessageBoxButton.OK,
                    MessageBoxImage.None
                );
                StartNewGame();
            }
            catch (InvalidOperationException ex)
            {
                MessageBox.Show(
                    $"Ошибка операции при генерации карты: {ex.Message}\nИгра перезапустится.",
                    "Ошибка",
                    MessageBoxButton.OK,
                    MessageBoxImage.None
                );
                StartNewGame();
            }
        }

        private void ClearMapDisplay()
        {
            colorMapGrid.Children.Clear();
            colorMapGrid.RowDefinitions.Clear();
            colorMapGrid.ColumnDefinitions.Clear();
        }

        private void DisplayMap(MapCell[,] map)
        {
            int rows = map.GetLength(0);
            int cols = map.GetLength(1);

            for (int r = 0; r < rows; r++)
            {
                colorMapGrid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
            }

            for (int c = 0; c < cols; c++)
            {
                colorMapGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
            }

            for (int r = 0; r < rows; r++)
            {
                for (int c = 0; c < cols; c++)
                {
                    MapCell cell = map[r, c];
                    Border cellBorder = new Border
                    {
                        Background = _charToColorMapping[cell.Color],
                        Margin = new Thickness(1),
                        BorderBrush = Brushes.Gray,
                        BorderThickness = new Thickness(0.5)
                    };

                    Grid.SetRow(cellBorder, r);
                    Grid.SetColumn(cellBorder, c);

                    colorMapGrid.Children.Add(cellBorder);
                }
            }
        }

        private void DetermineCorrectAnswer(MapCell[,] map)
        {
            Dictionary<char, int> counts = _generator.CountColors(map);

            counts = counts.Where(kv => _availableColorsForCurrentRound.Contains(kv.Key)).ToDictionary(kv => kv.Key, kv => kv.Value);

            if (counts.Any())
            {
                _correctColorChar = counts.OrderByDescending(kv => kv.Value).First().Key;
            }
            else
            {
                _correctColorChar = '\0';
            }
        }

        private void ColorButton_Click(object sender, RoutedEventArgs e)
        {
            Button clickedButton = sender as Button;
            if (clickedButton == null) return;

            char selectedColor = clickedButton.Tag.ToString()[0];

            if (selectedColor == _correctColorChar)
            {
                MessageBox.Show(
                    $"Правильно! Цвет '{_colorNamesMapping[selectedColor]}' убран из игры.",
                    "Успех",
                    MessageBoxButton.OK,
                    MessageBoxImage.None
                );

                clickedButton.IsEnabled = false;
                clickedButton.Visibility = Visibility.Hidden;

                _availableColorsForCurrentRound.Remove(selectedColor);

                if (_availableColorsForCurrentRound.Count <= 1)
                {
                    MessageBox.Show(
                        "Все цвета угаданы в этом раунде! Начинается новая игра.",
                        "Уровень завершен",
                        MessageBoxButton.OK,
                        MessageBoxImage.None
                    );
                    StartNewGame();
                }
                else
                {
                    GenerateAndDisplayMap(_availableColorsForCurrentRound.Count);
                }
            }
            else
            {
                MessageBox.Show(
                    "Неправильно! Игра начинается заново.",
                    "Ошибка",
                    MessageBoxButton.OK,
                    MessageBoxImage.None
                );
                StartNewGame();
            }
        }

        private void EnableAllColorButtons()
        {
            foreach (var kvp in _colorButtons)
            {
                kvp.Value.IsEnabled = true;
                kvp.Value.Visibility = Visibility.Visible;
            }
        }
    }
}