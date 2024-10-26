using NotificationWpf.Models;
using System.Windows.Threading;

namespace NotificationWpf.Services
{
    public class NotificationManagement
    {
        internal List<NotificationObject> _notifications { get; set; } = new();
        private DispatcherTimer _timer;

        public NotificationManagement()
        {
            _timer = new();
            _timer.Interval = TimeSpan.FromSeconds(1);
            _timer.Tick += _timer_Tick;
            _timer.Start();
        }

        private void _timer_Tick(object? sender, EventArgs e)
        {
            var removeItems = new List<NotificationObject>();
            foreach (var item in _notifications)
            {
                if (item.Duration > TimeSpan.FromSeconds(3))
                {
                    closeWindow(item);
                    removeItems.Add(item);
                }
            }

            if (removeItems != null && removeItems.Count > 0)
            {
                _notifications.RemoveAll(item => removeItems.Contains(item));
            }
        }

        public void Create(eTypeNotification typeNotification, string message = "", string title = "")
        {
            createWindow(typeNotification, message, title);
        }


        private void createWindow(eTypeNotification typeNotification, string message = "", string title = "")
        {
            var viewModel = new MainViewModel(typeNotification);
            var window = new MainWindow();
            window.DataContext = viewModel;
            window.Left = 200;
            window.Top = _notifications.Count * 100;

            var notObj = new NotificationObject(viewModel, window);
            _notifications.Add(notObj);
            window.Show();
        }

        private void closeWindow(NotificationObject notificationObject)
        {
            notificationObject.Window.Close();
        }
    }
}
