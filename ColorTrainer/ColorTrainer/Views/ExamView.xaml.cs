using ColorTrainer.Properties;
using ColorTrainer.Services;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace ColorTrainer.Views
{
    public partial class ExamView : UserControl
    {
        public event EventHandler GoToMenuClicked;

        private readonly Level1View _level1 = new Level1View();
        private readonly Level2View _level2 = new Level2View();
        private readonly Level3View _level3 = new Level3View();
        private readonly Level4View _level4 = new Level4View();

        private List<UserControl> _roundSequence;
        private int _currentRoundIndex;

        private int _score;
        private int _lives;
        private int _highScore;
        private const int MaxLives = 5;
        private const int TotalRounds = 15;

        public ExamView()
        {
            InitializeComponent();
            SubscribeToEvents();
        }

        private void SubscribeToEvents()
        {
            _level1.RoundCompleted += OnRoundCompleted;
            _level2.RoundCompleted += OnRoundCompleted;
            _level3.RoundCompleted += OnRoundCompleted;
            _level4.RoundCompleted += OnRoundCompleted;
        }

        public void Start()
        {
            _score = 0;
            _lives = MaxLives;
            _currentRoundIndex = 0;
            _highScore = Settings.Default.ExamHighScore;

            GenerateRoundSequence();
            StartNextRound();
            UpdateUi();
        }

        private void GenerateRoundSequence()
        {
            _roundSequence = new List<UserControl>();
            var random = new Random();
            for (int i = 0; i < TotalRounds; i++)
            {
                int levelType = random.Next(4); // 0, 1, или 2
                switch (levelType)
                {
                    case 0: _roundSequence.Add(_level1); break;
                    case 1: _roundSequence.Add(_level2); break;
                    case 2: _roundSequence.Add(_level3); break;
                }
            }
        }

        private void StartNextRound()
        {
            if (_currentRoundIndex >= TotalRounds || _lives <= 0)
            {
                EndExam(isWin: _lives > 0);
                return;
            }

            var currentLevel = _roundSequence[_currentRoundIndex];

            // Запускаем внутренний генератор раунда каждого уровня
            if (currentLevel is Level1View l1) l1.Start();
            else if (currentLevel is Level2View l2) l2.Start();
            else if (currentLevel is Level3View l3) l3.Start();
            else if (currentLevel is Level4View l4) l4.Start();

            // Убираем нижнюю и верхнюю панель у дочерних уровней
            HideChildLevelUi(currentLevel);

            RoundContentControl.Content = currentLevel;
            UpdateUi();
        }

        private void OnRoundCompleted(object sender, bool isCorrect)
        {
            if (isCorrect)
            {
                _score++;
            }
            else
            {
                _lives--;
            }

            _currentRoundIndex++;
            StartNextRound();
        }

        private void EndExam(bool isWin)
        {
            // Сохраняем рекорд
            if (_score > _highScore)
            {
                Settings.Default.ExamHighScore = _score;
                Settings.Default.Save();
            }

            string title = isWin ? "Экзамен пройден!" : "Экзамен провален";
            string message = $"Вы набрали {_score} очков. Ваш лучший результат: {Settings.Default.ExamHighScore}";
            DialogService.ShowMessage(title, message);

            // Возвращаемся в меню
            GoToMenuClicked?.Invoke(this, EventArgs.Empty);
        }

        private void UpdateUi()
        {
            ScoreText.Text = _score.ToString();
            ProgressText.Text = $"Раунд {_currentRoundIndex + 1}/{TotalRounds}";
            LivesText.Text = new string('♥', _lives) + new string('♡', MaxLives - _lives);
        }

        private void HideChildLevelUi(UserControl level)
        {
            // Находим и скрываем элементы по имени
            var backButton = level.FindName("BackButton") as FrameworkElement;
            if (backButton != null)
            {
                // Получаем родителя (это должен быть Grid) и скрываем его
                var parentGrid = VisualTreeHelper.GetParent(backButton) as UIElement;
                parentGrid?.SetValue(VisibilityProperty, Visibility.Collapsed);
            }

            var scoreText = level.FindName("ScoreText") as FrameworkElement;
            if (scoreText != null)
            {
                // Получаем родителя (StackPanel), затем родителя родителя (Grid) и скрываем его
                var parentStackPanel = VisualTreeHelper.GetParent(scoreText) as UIElement;
                var parentGrid = VisualTreeHelper.GetParent(parentStackPanel) as UIElement;
                parentGrid?.SetValue(VisibilityProperty, Visibility.Collapsed);
            }
        }

        private void HighScoreButton_Click(object sender, RoutedEventArgs e)
        {
            DialogService.ShowMessage("Рекорд Экзамена", $"Ваш лучший результат: {Settings.Default.ExamHighScore}");
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            GoToMenuClicked?.Invoke(this, EventArgs.Empty);
        }
    }
}