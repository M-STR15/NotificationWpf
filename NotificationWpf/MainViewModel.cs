using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
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

        private int _row;
        protected int Row
        {
            get => _row;
            set
            {
                _row = value;
                setLocationWindow();
            }
        }
        private int _column;
        protected int Column
        {
            get => _column;
            set
            {
                _column = value;
                setLocationWindow();
            }
        }

        private readonly int _sizePadding = 0;
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

        internal EventHandler CloseWindowHandler { get; set; }

        private void onCloseWindowChange()
        {
            CloseWindowHandler.Invoke(this, EventArgs.Empty);
        }

        //public ICommand CloseWindowsCommand { get; }

        internal MainViewModel(int order, MainWindow window, eNotificationType typeNotification, string message = "")
        {
            Color = Brushes.LightGray;

            GuidId = Guid.NewGuid();
            Order = order;
            DateTimeCreate = DateTime.Now;
            _window = window;
            TypeNotification = typeNotification;
            Message = message;
            setDesignWindow();

            //CloseWindowsCommand = new RelayCommand(closeWindow);

            setStartPositionWindow();
        }

        private void setStartPositionWindow()
        {
            _maxRowsOnColum = Convert.ToInt16(Math.Floor(SystemParameters.WorkArea.Height / (_window.MainGrid.Height + _sizePadding)));
            var lim = Order / _maxRowsOnColum;

            Column = Convert.ToInt32(lim) + 1;
            Row = Convert.ToInt32(Order - _maxRowsOnColum * lim) + 1;
        }

        private void setDesignWindow()
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
        [RelayCommand]
        private void CloseWindow(object parameter)
        {
            onCloseWindowChange();
            CloseWindow();
        }

        private void setLocationWindow()
        {
            if (Row != 0)
            {
                var top = Convert.ToDouble(SystemParameters.WorkArea.Height - Row * (_window.MainGrid.Height + _sizePadding));
                var left = Convert.ToDouble(SystemParameters.WorkArea.Width - Column * (_window.MainGrid.Width + _sizePadding));

                _window.Left = left;
                _window.Top = top;
            }
        }

        internal void ScrollInDisplayed()
        {
            Order -= 1;
            if (Row == 1 && Column > 1)
            {
                Column -= 1;
                Row = _maxRowsOnColum;
                setLocationWindow();
            }
            else
            {
                Row -= 1;
                setLocationWindow();
            }
        }

        internal void CloseWindow()
        {
            Order = 0;
            Row = 0;
            Column = 0;
            _window.Close();
        }


        internal bool IsWindowClose()
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
