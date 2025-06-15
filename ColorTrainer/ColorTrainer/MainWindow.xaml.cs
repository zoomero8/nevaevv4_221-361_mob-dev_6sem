using ColorTrainer.Views;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace ColorTrainer
{
    public partial class MainWindow : Window
    {
        private bool isDarkTheme = false;

        // Создаем экземпляр нашего уровня.
        private Level1View _level1View = new Level1View();
        private Level2View _level2View = new Level2View();
        private Level3View _colorMixerView = new Level3View();
        private Level4View _level4View = new Level4View();
        private ExamView _examView = new ExamView();

        public MainWindow()
        {
            InitializeComponent();
            // Подписываемся на событие "Вернуться в меню" от нашего UserControl
            _level1View.GoToMenuClicked += OnGoToMenuClicked;
            _level2View.GoToMenuClicked += OnGoToMenuClicked;
            _colorMixerView.GoToMenuClicked += OnGoToMenuClicked;
            _level4View.GoToMenuClicked += OnGoToMenuClicked;
            _examView.GoToMenuClicked += OnGoToMenuClicked;


            // 1. Читаем сохраненное значение
            isDarkTheme = Properties.Settings.Default.IsDarkTheme;

            // 2. Применяем тему
            ApplyTheme();
        }

        private void ApplyTheme()
        {
            var appResources = Application.Current.Resources.MergedDictionaries;
            appResources.Clear();

            ResourceDictionary newTheme = new ResourceDictionary();
            if (isDarkTheme)
            {
                newTheme.Source = new Uri("Themes/DarkTheme.xaml", UriKind.RelativeOrAbsolute);
            }
            else
            {
                newTheme.Source = new Uri("Themes/LightTheme.xaml", UriKind.RelativeOrAbsolute);
            }

            appResources.Add(newTheme);
        }

        private void SwitchToGameView(UserControl levelView)
        {
            MenuGrid.Visibility = Visibility.Collapsed;
            GameContentControl.Visibility = Visibility.Visible;
            GameContentControl.Content = levelView; // Показываем нужный UserControl
        }

        private void SwitchToMenuView()
        {
            GameContentControl.Visibility = Visibility.Collapsed;
            GameContentControl.Content = null; // Очищаем контейнер
            MenuGrid.Visibility = Visibility.Visible;
        }

        // Этот метод будет вызван, когда UserControl отправит событие
        private void OnGoToMenuClicked(object sender, EventArgs e)
        {
            SwitchToMenuView();
        }

        // --- Обработчики событий кнопок ---
        private void Level1Button_Click(object sender, RoutedEventArgs e)
        {
            _level1View.Start(); // Запускаем внутренний метод уровня
            SwitchToGameView(_level1View); // Показываем этот уровень
        }

        private void Level2Button_Click(object sender, RoutedEventArgs e)
        {
            _level2View.Start();
            SwitchToGameView(_level2View);
        }

        private void Level4Button_Click(object sender, RoutedEventArgs e)
        {
            _level4View.Start();
            SwitchToGameView(_level4View);
        }

        private void ColorMixerButton_Click(object sender, RoutedEventArgs e)
        {
            _colorMixerView.Start();
            SwitchToGameView(_colorMixerView);
        }

        private void ExamButton_Click(object sender, RoutedEventArgs e)
        {
            _examView.Start();
            SwitchToGameView(_examView);
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                this.DragMove();
            }
        }

        private void ThemeToggleButton_Click(object sender, RoutedEventArgs e)
        {
            isDarkTheme = !isDarkTheme;
            ApplyTheme();

            var appResources = Application.Current.Resources.MergedDictionaries;
            appResources.Clear();

            ResourceDictionary newTheme = new ResourceDictionary();
            if (isDarkTheme)
            {
                newTheme.Source = new Uri("Themes/DarkTheme.xaml", UriKind.RelativeOrAbsolute);
            }
            else
            {
                newTheme.Source = new Uri("Themes/LightTheme.xaml", UriKind.RelativeOrAbsolute);
            }

            appResources.Add(newTheme);
        }

        private void MainWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            // 1. Записываем текущее значение флага в настройки
            Properties.Settings.Default.IsDarkTheme = isDarkTheme;

            // 2. Сохраняем все изменения в файле настроек
            Properties.Settings.Default.Save();
        }
    }
}