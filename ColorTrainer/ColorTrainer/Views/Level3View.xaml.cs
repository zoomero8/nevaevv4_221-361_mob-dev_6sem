using ColorTrainer.Models;
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
    public partial class Level3View : UserControl
    {
        public event EventHandler GoToMenuClicked;
        public event EventHandler<bool> RoundCompleted;

        private Random _random = new Random();
        private ColorMixRecipe _currentRecipe;
        private int _score;
        private int _highScore;

        public Level3View()
        {
            InitializeComponent();
        }

        public void Start()
        {
            _score = 0;
            ScoreText.Text = _score.ToString();
            _highScore = Settings.Default.Level3HighScore;
            GenerateRound();
        }

        private void GenerateRound()
        {
            var recipes = ColorMixingService.GetRecipes();
            _currentRecipe = recipes[_random.Next(recipes.Count)];
            TargetColorBorder.Background = _currentRecipe.Result.Brush;

            var options = new List<ColorInfo>();
            options.AddRange(_currentRecipe.Ingredients);

            var allPossibleColors = ColorDatabase.GetColors();
            var forbiddenNames = new HashSet<string>(_currentRecipe.Ingredients.Select(i => i.Name));
            forbiddenNames.Add(_currentRecipe.Result.Name);

            var distractors = allPossibleColors
                .Where(c => !forbiddenNames.Contains(c.Name))
                .OrderBy(x => _random.Next())
                .Take(7)
                .ToList();

            options.AddRange(distractors);

            IngredientsItemsControl.ItemsSource = options.OrderBy(x => _random.Next()).ToList();
            ResetToggleButtons();
        }

        private void CheckButton_Click(object sender, RoutedEventArgs e)
        {
            var userSelectedIngredients = new HashSet<ColorInfo>();
            foreach (var item in IngredientsItemsControl.Items)
            {
                var container = IngredientsItemsControl.ItemContainerGenerator.ContainerFromItem(item) as FrameworkElement;
                if (container == null) continue;

                var toggleButton = FindVisualChild<ToggleButton>(container);
                if (toggleButton != null && toggleButton.IsChecked == true)
                {
                    userSelectedIngredients.Add(toggleButton.Tag as ColorInfo);
                }
            }

            var correctIngredients = new HashSet<ColorInfo>(_currentRecipe.Ingredients);
            bool isCorrect = userSelectedIngredients.SetEquals(correctIngredients);

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
                    GenerateRound();
                }
                else
                {
                    string correctAnswer = string.Join(" + ", _currentRecipe.Ingredients.Select(i => i.Name));
                    DialogService.ShowMessage("Ошибка", $"Неверно! Правильный ответ: {correctAnswer}");
                    ResetToggleButtons();
                }
            }
        }

        private void HighScoreButton_Click(object sender, RoutedEventArgs e)
        {
            int currentHighScore = Settings.Default.Level3HighScore;
            DialogService.ShowMessage("Лучший результат", $"Ваш рекорд в этом режиме: {currentHighScore}");
        }

        private void ResetToggleButtons()
        {
            Dispatcher.BeginInvoke(new Action(() =>
            {
                foreach (var item in IngredientsItemsControl.Items)
                {
                    var container = IngredientsItemsControl.ItemContainerGenerator.ContainerFromItem(item) as FrameworkElement;
                    if (container == null) continue;

                    var toggleButton = FindVisualChild<ToggleButton>(container);
                    if (toggleButton != null)
                    {
                        toggleButton.IsChecked = false;
                    }
                }
            }), System.Windows.Threading.DispatcherPriority.ContextIdle);
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            if (_score > _highScore)
            {
                Settings.Default.Level3HighScore = _score;
                Settings.Default.Save();
            }
            GoToMenuClicked?.Invoke(this, EventArgs.Empty);
        }

        private static T FindVisualChild<T>(DependencyObject parent) where T : DependencyObject
        {
            if (parent == null) return null;
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(parent); i++)
            {
                var child = VisualTreeHelper.GetChild(parent, i);
                if (child is T) return (T)child;
                var result = FindVisualChild<T>(child);
                if (result != null) return result;
            }
            return null;
        }
    }
}