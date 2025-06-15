// --- НОВЫЙ ФАЙЛ: Views/Level4View.xaml.cs ---

using ColorTrainer.Models;
using ColorTrainer.Properties;
using ColorTrainer.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace ColorTrainer.Views
{
    public partial class Level4View : UserControl
    {
        public event EventHandler<bool> RoundCompleted;
        public event EventHandler GoToMenuClicked;

        private Random _random = new Random();
        private ColorInfo _correctColor;
        private ColorInfo _baseColor;
        private int _score;
        private int _highScore;
        private HarmonyType _currentHarmonyType;

        public Level4View()
        {
            InitializeComponent();
        }

        public void Start()
        {
            _score = 0;
            ScoreText.Text = _score.ToString();
            _highScore = Settings.Default.Level4HighScore;
            GenerateRound();
        }

        private void GenerateRound()
        {
            // 1. Выбираем случайный базовый цвет из нашей основной палитры
            // (исключаем ахроматические цвета типа белого/черного, с ними гармонии работают плохо)
            var suitableColors = ColorDatabase.GetColors()
                .Where(c => c.Name != "Белый" && c.Name != "Черный" && c.Name != "Серый")
                .ToList();
            _baseColor = suitableColors[_random.Next(suitableColors.Count)];

            // 2. Используем наш новый сервис для получения гармоничного цвета и типа задания
            var harmonyResult = ColorHarmonyService.GetHarmony(_baseColor);
            _correctColor = harmonyResult.CorrectColor;
            var harmonyType = harmonyResult.Type;
            _currentHarmonyType = harmonyType;

            // 3. Обновляем UI
            BaseColorBorder.Background = _baseColor.Brush;
            QuestionText.Text = GetQuestionText(harmonyType);

            // 4. Готовим варианты ответа
            var options = new List<ColorInfo> { _correctColor };
            var distractors = ColorDatabase.GetColors()
                .Where(c => c.Name != _correctColor.Name && c.Name != _baseColor.Name) // Исключаем правильный и базовый цвета
                .OrderBy(x => _random.Next())
                .Take(3) // Берем 3 неправильных варианта
                .ToList();
            options.AddRange(distractors);

            // 5. Перемешиваем и отображаем
            ColorOptionsItemsControl.ItemsSource = options.OrderBy(x => _random.Next()).ToList();
        }

        private string GetQuestionText(HarmonyType type)
        {
            switch (type)
            {
                case HarmonyType.Complementary: return "Найдите комплементарный цвет";
                case HarmonyType.Analogous: return "Найдите аналоговый цвет";
                case HarmonyType.Triadic: return "Найдите цвет из триады";
                case HarmonyType.Monochromatic: return "Найдите монохромный оттенок";
                default: return "Найдите гармоничный цвет";
            }
        }

        private void ColorOptionButton_Click(object sender, RoutedEventArgs e)
        {
            var clickedButton = (Button)sender;
            var selectedColor = (ColorInfo)clickedButton.Tag;
            bool isCorrect = (selectedColor.Name == _correctColor.Name);

            if (RoundCompleted != null) // Режим экзамена
            {
                RoundCompleted?.Invoke(this, isCorrect);
            }
            else // Обычный режим
            {
                if (isCorrect)
                {
                    _score++;
                    ScoreText.Text = _score.ToString();
                }
                GenerateRound();
            }
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            if (_score > _highScore)
            {
                Settings.Default.Level4HighScore = _score;
                Settings.Default.Save();
            }
            GoToMenuClicked?.Invoke(this, EventArgs.Empty);
        }

        private void HighScoreButton_Click(object sender, RoutedEventArgs e)
        {
            int currentHighScore = Settings.Default.Level4HighScore;
            DialogService.ShowMessage("Лучший результат", $"Ваш рекорд в этом режиме: {currentHighScore}");
        }

        private void HelpButton_Click(object sender, RoutedEventArgs e)
        {
            string title = "";
            string description = "";
            BitmapImage helpImage = null; // Переменная для нашей картинки

            try
            {
                // Создаем URI, используя самый надежный синтаксис.
                // ЗАМЕНИТЕ "ColorTrainer" на имя вашей сборки из Шага 1, если оно отличается!
                var uri = new Uri("pack://application:,,,/ColorTrainer;component/Assets/color_wheel.png", UriKind.Absolute);
                helpImage = new BitmapImage(uri);
            }
            catch (Exception ex)
            {
                // Если картинка не загрузится, helpImage останется null.
                // Можно вывести ошибку в консоль для отладки.
                System.Diagnostics.Debug.WriteLine($"Ошибка загрузки изображения: {ex.Message}");
            }


            switch (_currentHarmonyType)
            {
                case HarmonyType.Complementary:
                    title = "Комплементарная гармония";
                    description = "Это цвета, расположенные друг напротив друга на цветовом круге. Они создают максимальный контраст.";
                    break;
                case HarmonyType.Analogous:
                    title = "Аналоговая гармония";
                    description = "Это 2-3 цвета, расположенные рядом на цветовом круге. Такое сочетание выглядит спокойным и приятным.";
                    break;
                case HarmonyType.Triadic:
                    title = "Триадическая гармония";
                    description = "Это три цвета, которые образуют равносторонний треугольник на круге. Создает яркое и сбалансированное сочетание.";
                    break;
                case HarmonyType.Monochromatic:
                    title = "Монохромная гармония";
                    description = "Это использование разных оттенков, тонов и насыщенности одного и того же цвета. Создает элегантный и утонченный вид.";
                    break;
            }

            // Вызываем сервис, передавая уже готовый объект BitmapImage
            DialogService.ShowHelp(title, description, helpImage);
        }
    }
}