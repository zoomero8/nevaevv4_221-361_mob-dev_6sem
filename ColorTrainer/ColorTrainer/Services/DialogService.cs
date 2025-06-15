using ColorTrainer.Views;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace ColorTrainer.Services
{
    public static class DialogService
    {
        public static void ShowMessage(string title, string message)
        {
            // 1. Создаем UserControl с нужным текстом
            var messageBoxView = new CustomMessageBoxView(title, message);

            // 2. Создаем кастомное окно
            var dialogWindow = new Window
            {
                Title = title,
                Content = messageBoxView, // Вставляем наш UserControl внутрь
                Width = 320,
                Height = 220,
                WindowStyle = WindowStyle.None,
                ResizeMode = ResizeMode.NoResize,
                AllowsTransparency = true,
                Background = System.Windows.Media.Brushes.Transparent,
                Owner = Application.Current.MainWindow, // Устанавливаем родителя
                WindowStartupLocation = WindowStartupLocation.CenterOwner // Центрируем относительно него
            };


            // 3. Показываем окно как модальный диалог
            dialogWindow.ShowDialog();
        }
        public static void ShowHelp(string title, string message, BitmapImage image)
        {
            // Теперь мы принимаем готовый BitmapImage
            var helpDialogView = new HelpDialogView(title, message, image);

            var dialogWindow = new Window
            {
                Title = title,
                Content = helpDialogView,
                SizeToContent = SizeToContent.WidthAndHeight,
                WindowStyle = WindowStyle.None,
                ResizeMode = ResizeMode.NoResize,
                AllowsTransparency = true,
                Background = System.Windows.Media.Brushes.Transparent,
                Owner = Application.Current.MainWindow,
                WindowStartupLocation = WindowStartupLocation.CenterOwner
            };

            dialogWindow.Effect = new System.Windows.Media.Effects.DropShadowEffect
            {
                Color = Colors.Black,
                Opacity = 0.4,
                BlurRadius = 15,
                ShadowDepth = 0
            };

            dialogWindow.ShowDialog();
        }
    }
}