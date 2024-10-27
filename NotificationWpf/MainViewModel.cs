using CommunityToolkit.Mvvm.ComponentModel;
using NotificationWpf.Helpers;
using NotificationWpf.Models;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace NotificationWpf
{
    [ObservableObject]
    internal partial class MainViewModel
    {
        internal Guid GuidId { get; private set; }
        internal DateTime DateTimeCreate { get; private set; }
        internal TimeSpan Duration { get => (DateTime.Now - DateTimeCreate); }
        internal int Order { get; private set; }
        [ObservableProperty]
        private int _row;
        [ObservableProperty]
        private int _column;

        private readonly int _sizePadding = 8;
        private int _maxRowsOnColum;
        [ObservableProperty]
        private Brush _color;

        [ObservableProperty]
        private string _iconSource;


        [ObservableProperty]
        private string _message;

        [ObservableProperty]
        private eNotificationType _typeNotification;

        private MainWindow _window { get; set; }

        public EventHandler CloseWindowHandler { get; set; }

        private void onCloseWindowChange()
        {
            CloseWindowHandler.Invoke(this, EventArgs.Empty);
        }

        public ICommand CloseWindowsCommand { get; }

        internal MainViewModel(int order, MainWindow window, eNotificationType typeNotification, string message = "")
        {
            Color = Brushes.LightGray;

            GuidId = Guid.NewGuid();
            Order = order;
            DateTimeCreate = DateTime.Now;
            _window = window;
            TypeNotification = typeNotification;
            Message = message;
            setColor();

            CloseWindowsCommand = new RelayCommand(closeWindow);

            setStartPositionWindow();
        }

        private void setStartPositionWindow()
        {
            _maxRowsOnColum = Convert.ToInt16(Math.Floor(SystemParameters.WorkArea.Height / (_window.Height + _sizePadding)));
            var lim = Order / _maxRowsOnColum;
            Column = Convert.ToInt16(lim) + 1;
            Row = Convert.ToInt16(Order - _maxRowsOnColum * lim) + 1;

            setLocationWindow();
        }

        private void setColor()
        {
            switch (TypeNotification)
            {
                case eNotificationType.Success:
                    Color = Brushes.LightGreen;
                    IconSource = @"\Resources\Icons\SVG\success.svg";
                    break;
                case eNotificationType.Info:
                    Color = Brushes.LightBlue;
                    IconSource = @"\Resources\Icons\SVG\info.svg";
                    break;
                case eNotificationType.Warning:
                    IconSource = @"\Resources\Icons\SVG\warning.svg";
                    Color = Brushes.Orange;
                    break;
                case eNotificationType.Error:
                    IconSource = @"\Resources\Icons\SVG\bomb.svg";
                    Color = Brushes.Tomato;
                    break;
            }
        }

        private void closeWindow(object parameter)
        {
            onCloseWindowChange();
            CloseWindow();
        }

        private void setLocationWindow()
        {
            var top = Convert.ToDouble(SystemParameters.WorkArea.Height - Row * (_window.Height + _sizePadding));
            var left = Convert.ToDouble(SystemParameters.WorkArea.Width - Column * (_window.Width + _sizePadding));

            _window.Left = left;
            _window.Top = top;
        }

        private void moveWindowDown() => _window.Top += _window.Height;

        private void moveWindowRight() => _window.Left += _window.Width;

        public void ScrollInDisplayed()
        {
            Order -= 1;
            if (Row == 1 && Column > 1)
            {
                Column -= 1;
                Row = _maxRowsOnColum;
                moveWindowRight();
                moveWindowDown();
            }
            else
            {
                Row -= 1;
                moveWindowDown();
            }
        }

        public void CloseWindow()
        {
            Order = 0;
            Row = 0;
            Column = 0;
            _window.Close();
        }


        public bool IsWindowClose()
        {
            try
            {
                return !_window.IsActive;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
