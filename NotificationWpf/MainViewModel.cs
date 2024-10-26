using CommunityToolkit.Mvvm.ComponentModel;
using NotificationWpf.Models;
using System.Windows.Media;

namespace NotificationWpf
{
    [ObservableObject]
    internal partial class MainViewModel
    {
        [ObservableProperty]
        private Brush _color;

        [ObservableProperty]
        private string _iconSource;

        [ObservableProperty]
        private double _locationLeft;

        [ObservableProperty]
        private double _locationTop;

        [ObservableProperty]
        private string _message;

        [ObservableProperty]
        private string _title;

        [ObservableProperty]
        private eNotificationType _typeNotification;
        //internal MainViewModel()
        //{
        //    TypeNotification = eNotificationType.Info;
        //    Message = string.Empty;
        //    Title = string.Empty;

        //    setColor();
        //}

        internal MainViewModel(eNotificationType typeNotification, string message = "", string title = "") 
        {
            TypeNotification = typeNotification;
            Message = message;
            Title = title;
            setColor();
        }

        private void setColor()
        {
            switch (TypeNotification)
            {
                case eNotificationType.Success:
                    Color = Brushes.Green;
                    IconSource = @"\Resources\Icons\SVG\success.svg";
                    break;
                case eNotificationType.Info:
                    Color = Brushes.Blue;
                    IconSource = @"\Resources\Icons\SVG\info.svg";
                    break;
                case eNotificationType.Warning:
                    IconSource = @"\Resources\Icons\SVG\warning.svg";
                    Color = Brushes.Orange;
                    break;
                case eNotificationType.Error:
                    IconSource = @"\Resources\Icons\SVG\bomb.svg";
                    Color = Brushes.Red;
                    break;
            }
        }
    }
}
