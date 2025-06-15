using ColorTrainer.Models;
using ColorTrainer.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using ColorTrainer.Properties; // <<<=== ДОБАВЬТЕ ЭТУ СТРОКУ

namespace ColorTrainer.Views
{
    public partial class Level1View : UserControl
    {
        public event EventHandler<bool> RoundCompleted;
        public event EventHandler GoToMenuClicked;

        private Random _random = new Random();
        private ColorInfo _correctColor;
        private int _score;
        private int _highScore;

        public Level1View()
        {
            InitializeComponent();
        }

        public void Start()
        {
            _score = 0;
            ScoreText.Text = _score.ToString();
            // Теперь компилятор знает, что такое Settings
            _highScore = Settings.Default.Level1HighScore;
            GenerateLevel1Round();
        }

        private void GenerateLevel1Round()
        {
            var randomColors = ColorDatabase.GetColors().OrderBy(c => _random.Next()).Take(4).ToList();
            _correctColor = randomColors[0];
            QuestionText.Text = _correctColor.Name;
            var shuffledOptions = randomColors.OrderBy(c => _random.Next()).ToList();
            ColorOptionsItemsControl.ItemsSource = shuffledOptions;
        }

        private void ColorOptionButton_Click(object sender, RoutedEventArgs e)
        {
            var clickedButton = (Button)sender;
            var selectedColor = (ColorInfo)clickedButton.Tag;
            bool isCorrect = (selectedColor == _correctColor);

            // Если кто-то подписался на событие (режим экзамена), отправляем результат
            if (RoundCompleted != null)
            {
                RoundCompleted?.Invoke(this, isCorrect);
            }
            else // Иначе работаем в обычном режиме (обновляем свой счет)
            {
                if (isCorrect)
                {
                    _score++;
                    ScoreText.Text = _score.ToString();
                }
                GenerateLevel1Round();
            }
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            if (_score > _highScore)
            {
                Settings.Default.Level1HighScore = _score;
                Settings.Default.Save();
            }
            GoToMenuClicked?.Invoke(this, EventArgs.Empty);
        }

        private void HighScoreButton_Click(object sender, RoutedEventArgs e)
        {
            int currentHighScore = Settings.Default.Level1HighScore;
            DialogService.ShowMessage("Лучший результат", $"Ваш рекорд в этом режиме: {currentHighScore}");
        }
    }
}