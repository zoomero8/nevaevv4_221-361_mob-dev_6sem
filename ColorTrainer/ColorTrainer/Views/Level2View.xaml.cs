using ColorTrainer.Services;
using ColorTrainer.Properties; // Директива для доступа к Settings
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;

namespace ColorTrainer.Views
{
    public partial class Level2View : UserControl
    {
        public event EventHandler GoToMenuClicked;
        public event EventHandler<bool> RoundCompleted;

        private Random _random = new Random();
        private int _score;
        private int _highScore;
        private List<Color> _correctColors = new List<Color>();

        public Level2View()
        {
            InitializeComponent();
        }

        public void Start()
        {
            _score = 0;
            ScoreText.Text = _score.ToString();
            _highScore = Settings.Default.Level2HighScore;
            GenerateLevel2Round();
        }

        private void GenerateLevel2Round()
        {
            _correctColors.Clear();
            byte r = (byte)_random.Next(256);
            byte g = (byte)_random.Next(256);
            byte b = (byte)_random.Next(256);
            var baseColor = Color.FromRgb(r, g, b);

            int offset = 20;
            var oddColor = Color.FromRgb(
                (byte)Math.Max(0, r - offset),
                (byte)Math.Max(0, g - offset),
                (byte)Math.Max(0, b - offset));

            int oddTilesCount = _random.Next(1, 4);
            int totalTiles = 9;
            var colorBrushes = new List<SolidColorBrush>();
            var allIndices = Enumerable.Range(0, totalTiles).ToList();
            var oddTileIndices = allIndices.OrderBy(x => _random.Next()).Take(oddTilesCount).ToList();

            for (int i = 0; i < totalTiles; i++)
            {
                if (oddTileIndices.Contains(i))
                {
                    colorBrushes.Add(new SolidColorBrush(oddColor));
                    _correctColors.Add(oddColor);
                }
                else
                {
                    colorBrushes.Add(new SolidColorBrush(baseColor));
                }
            }
            ColorGridItemsControl.ItemsSource = colorBrushes;
        }

        private void CheckButton_Click(object sender, RoutedEventArgs e)
        {
            var userSelectedColors = new List<Color>();
            foreach (var item in ColorGridItemsControl.Items)
            {
                var container = ColorGridItemsControl.ItemContainerGenerator.ContainerFromItem(item) as FrameworkElement;
                if (container == null) continue;

                var toggleButton = FindVisualChild<ToggleButton>(container);
                if (toggleButton != null && toggleButton.IsChecked == true)
                {
                    var brush = toggleButton.Tag as SolidColorBrush;
                    if (brush != null)
                    {
                        userSelectedColors.Add(brush.Color);
                    }
                }
            }
            bool correctCount = userSelectedColors.Count == _correctColors.Count;
            bool allAreCorrectColor = _correctColors.Any() && userSelectedColors.All(c => c == _correctColors[0]);
            bool isCorrect = correctCount && allAreCorrectColor;

            if (RoundCompleted != null)
            {
                RoundCompleted?.Invoke(this, isCorrect);
            }
            else
            {
                if (isCorrect)
                {
                    _score++;
                    ScoreText.Text = _score.ToString();
                }
                GenerateLevel2Round();
            }
        }

        private void HighScoreButton_Click(object sender, RoutedEventArgs e)
        {
            int currentHighScore = Settings.Default.Level2HighScore;
            DialogService.ShowMessage("Лучший результат", $"Ваш рекорд в этом режиме: {currentHighScore}");
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            if (_score > _highScore)
            {
                Settings.Default.Level2HighScore = _score;
                Settings.Default.Save();
            }
            GoToMenuClicked?.Invoke(this, EventArgs.Empty);
        }

        private static T FindVisualChild<T>(DependencyObject parent) where T : DependencyObject
        {
            if (parent != null)
            {
                for (int i = 0; i < VisualTreeHelper.GetChildrenCount(parent); i++)
                {
                    DependencyObject child = VisualTreeHelper.GetChild(parent, i);
                    if (child != null && child is T)
                    {
                        return (T)child;
                    }
                    T childOfChild = FindVisualChild<T>(child);
                    if (childOfChild != null)
                    {
                        return childOfChild;
                    }
                }
            }
            return null;
        }
    }
}