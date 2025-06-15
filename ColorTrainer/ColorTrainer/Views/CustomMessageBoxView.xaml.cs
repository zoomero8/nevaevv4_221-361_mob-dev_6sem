using System.Windows;
using System.Windows.Controls;

namespace ColorTrainer.Views
{
    public partial class CustomMessageBoxView : UserControl
    {
        public CustomMessageBoxView(string title, string message)
        {
            InitializeComponent();
            TitleText.Text = title;
            MessageText.Text = message;

            OkButton.Click += (s, e) =>
            {
                // Находим родительское окно и закрываем его
                Window.GetWindow(this)?.Close();
            };
        }
    }
}