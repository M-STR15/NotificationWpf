using CommunityToolkit.Mvvm.ComponentModel;
using NotificationWpf.Models;
using System.Windows.Media;

namespace NotificationWpf
{
    [ObservableObject]
    public partial class MainViewModel
    {
        [ObservableProperty]
        private eTypeNotification _typeNotification;
        [ObservableProperty]
        private string _message;
        [ObservableProperty]
        private string _title;

        [ObservableProperty]
        private Brush _color;

        public MainViewModel()
        {
            TypeNotification = eTypeNotification.Info;
            Message = string.Empty;
            Title = string.Empty;

            setColor();
        }

        public MainViewModel(eTypeNotification typeNotification, string message = "", string title = ""):this()
        {
            TypeNotification = typeNotification;
            setColor();
        }

        private void setColor()
        {
            switch (TypeNotification)
            {
                case eTypeNotification.Suscaise:
                    Color = Brushes.Green;
                    break;
                case eTypeNotification.Info:
                    Color = Brushes.Blue;
                    break;
                case eTypeNotification.Warning:
                    Color = Brushes.Orange;
                    break;
                case eTypeNotification.Error:
                    Color = Brushes.Red;
                    break;
            }
        }
    }
}
