using NotificationWpf.Models;
using System.Windows.Media;
using System.Windows;
using System.Windows.Threading;

namespace NotificationWpf
{
    public class NotificationManagement
    {
        internal List<NotificationObject> _notifications { get; set; } = new();
        private DispatcherTimer _timer;

        /// <summary>
        /// Proměná na nastavení délky zobrazení okna.
        /// Výchozí nastavení je na 5s.
        /// </summary>
        public uint DurationSeconds { get; set; }
        public NotificationManagement()
        {
            DurationSeconds = 5;
            _timer = new();
            _timer.Interval = TimeSpan.FromMicroseconds(1000);
            _timer.Tick += _timer_Tick;
            _timer.Start();
        }

        private void _timer_Tick(object? sender, EventArgs e)
        {
            var removeItems = new List<NotificationObject>();
            foreach (var item in _notifications)
            {
                if (item.Duration > TimeSpan.FromSeconds(DurationSeconds))
                {
                    closeWindow(item);
                    removeItems.Add(item);
                }
            }

            if (removeItems != null && removeItems.Count > 0)
            {
                _notifications.RemoveAll(item => removeItems.Contains(item));
                foreach (var item in _notifications)
                {
                    item.ViewModel.LocationTop += 200;
                    item.Window.Top = item.ViewModel.LocationTop;
                }
            }
        }

        public void Create(eNotificationType typeNotification, string message = "", string title = "")
        {
            createWindow(typeNotification, message, title);
        }

        private void createWindow(eNotificationType typeNotification, string message = "", string title = "")
        {
            var viewModel = new MainViewModel(typeNotification, message, title);
            var window = new MainWindow();
            var margin = _notifications.Count > 0 ? 20 : 0;

            viewModel.LocationTop= Convert.ToDouble(SystemParameters.PrimaryScreenHeight - 150 - (_notifications.Count * 100) - margin);
            viewModel.LocationLeft=Convert.ToDouble(SystemParameters.PrimaryScreenWidth - window.Width - 15);
            window.DataContext = viewModel;
            window.Top = viewModel.LocationTop;
            window.Left=viewModel.LocationLeft;

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
