using CommunityToolkit.Mvvm.ComponentModel;
using NotificationWpf.Helpers;
using NotificationWpf.Models;
using System.Windows.Input;
using System.Windows.Media;

namespace NotificationWpf
{
    [ObservableObject]
    internal partial class MainViewModel
    {
        internal DateTime DateTimeCreate { get; private set; }
        internal TimeSpan Duration { get => (DateTime.Now - DateTimeCreate); }
        internal int Order { get; set; }
        [ObservableProperty]
        private Brush _color;

        [ObservableProperty]
        private string _iconSource;


        [ObservableProperty]
        private string _message;

        [ObservableProperty]
        private string _title;

        [ObservableProperty]
        private eNotificationType _typeNotification;

        public MainWindow Window { get; private set; }

        public readonly NotificationManagement? _notificationManagement;

        public ICommand CloseWindowsCommand { get; }

        internal MainViewModel(int order, MainWindow window, eNotificationType typeNotification, string message = "", string title = "", NotificationManagement notificationManagement = null)
        {
            Order = order;
            DateTimeCreate = DateTime.Now;
            Window = window;
            _notificationManagement = notificationManagement;
            TypeNotification = typeNotification;
            Message = message;
            Title = title;
            setColor();

            CloseWindowsCommand = new RelayCommand(closeWindow);
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

        private void closeWindow(object parameter)
        {
            closeWindow();
        }

        public void SetLocationWindow(double left, double top)
        {
            Window.Left = left;
            Window.Top = top;
        }

        public void MoveWindowTopDown()
        {
            Window.Top += Window.Height;
        }

        private void closeWindow()
        {
            Window.Close();
            if (_notificationManagement != null)
                _notificationManagement.CloseWindow(Order);
        }
    }
}
