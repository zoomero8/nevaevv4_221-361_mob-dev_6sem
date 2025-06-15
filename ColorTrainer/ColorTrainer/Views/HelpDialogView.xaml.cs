// В файле Views/HelpDialogView.xaml.cs

using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace ColorTrainer.Views
{
    public partial class HelpDialogView : UserControl
    {
        public HelpDialogView(string title, string message, BitmapImage image)
        {
            InitializeComponent();
            TitleText.Text = title;
            MessageText.Text = message;

            if (image != null)
            {
                HelpImage.Source = image;
                HelpImage.Visibility = Visibility.Visible;
            }
            else
            {
                HelpImage.Visibility = Visibility.Collapsed;
            }

            OkButton.Click += (s, e) => { Window.GetWindow(this)?.Close(); };
        }

        //// Этот метод будет вызван, когда картинка успешно загрузится
        //private void HelpImage_SourceUpdated(object sender, System.Windows.Data.DataTransferEventArgs e)
        //{
        //    // Если источник картинки не null, делаем ее видимой
        //    if (HelpImage.Source != null)
        //    {
        //        HelpImage.Visibility = Visibility.Visible;
        //    }
        //}
    }
}