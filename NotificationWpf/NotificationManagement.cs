using NotificationWpf.Models;
using System.Windows.Controls;
using System.Windows.Threading;

namespace NotificationWpf
{
    public class NotificationManagement
    {
        private List<MainViewModel> _notifications { get; set; } = new();
        private DispatcherTimer _timer;

        /// <summary>
        /// Proměná na nastavení délky zobrazení okna.
        /// Výchozí nastavení je na 5s.
        /// </summary>
        public int DurationSeconds { get; private set; }
        public NotificationManagement(int durationSeconds = 5)
        {
            DurationSeconds = durationSeconds;
            _timer = new();
            _timer.Interval = TimeSpan.FromMicroseconds(1000);
            _timer.Tick += _timer_Tick;
            _timer.Start();
        }


        private void _timer_Tick(object? sender, EventArgs e)
        {
            var removeItems = new List<MainViewModel>();
            var durationSecond = TimeSpan.FromSeconds(DurationSeconds);
            foreach (var item in _notifications)
            {
                if (item.Duration > durationSecond)
                {
                    item.CloseWindow();
                    removeItems.Add(item);
                }
            }

            if (removeItems != null && removeItems.Count > 0)
            {
                foreach (var item in removeItems)
                {
                    scrollAllOrhersWindowsOver(item.Order);

                    if (item.IsWindowClose())
                        _notifications.Remove(item);
                }
            }
        }

        public void Create(eNotificationType typeNotification, string message = "")
        {
            createWindow(typeNotification, message);
        }

        private void createWindow(eNotificationType typeNotification, string message = "")
        {
            var window = new MainWindow();
            var order = _notifications.Count;
            var viewModel = new MainViewModel(order, window, typeNotification, message);

            _notifications.Add(viewModel);
            setWindows(window, viewModel);
            window.Show();
        }

        private void setWindows(MainWindow window, MainViewModel viewModel)
        {
            window.Topmost = true;
            window.DataContext = viewModel;
        }

        private void scrollAllOrhersWindowsOver(int order)
        {
            foreach (var item in _notifications.Where(x => x.Order > order))
            {
                item.ScrollInDisplayed();
            }
        }
    }
}
