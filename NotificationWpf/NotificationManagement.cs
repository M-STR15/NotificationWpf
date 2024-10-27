using NotificationWpf.Models;
using System.Windows;
using System.Windows.Threading;

namespace NotificationWpf
{
    public class NotificationManagement
    {
        internal List<MainViewModel> _notifications { get; set; } = new();
        private DispatcherTimer _timer;

        /// <summary>
        /// Proměná na nastavení délky zobrazení okna.
        /// Výchozí nastavení je na 5s.
        /// </summary>
        public uint DurationSeconds { get; set; }
        public NotificationManagement()
        {
            DurationSeconds = 15;
            _timer = new();
            _timer.Interval = TimeSpan.FromMicroseconds(1000);
            _timer.Tick += _timer_Tick;
            _timer.Start();
        }

        private void _timer_Tick(object? sender, EventArgs e)
        {
            var removeItems = new List<MainViewModel>();
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
                var removeItemsCount = removeItems.Count;
                _notifications.RemoveAll(item => removeItems.Contains(item));
                foreach (var item in _notifications.OrderBy(x => x.Order))
                {
                    item.Order -= removeItemsCount;
                    item.MoveWindowTopDown();
                }
            }
        }

        public void Create(eNotificationType typeNotification, string message = "", string title = "")
        {
            createWindow(typeNotification, message, title);
        }

        private void createWindow(eNotificationType typeNotification, string message = "", string title = "")
        {
            var window = new MainWindow();
            window.Topmost = true;
            var order = _notifications.Count + 1;
            var viewModel = new MainViewModel(order, window, typeNotification, message, title, this);

            var top = Convert.ToDouble(SystemParameters.WorkArea.Height - (_notifications.Count + 1) * (window.Height + 8));
            var left = Convert.ToDouble(SystemParameters.WorkArea.Width - window.Width - 8);
            window.DataContext = viewModel;
            viewModel.SetLocationWindow(left, top);

            _notifications.Add(viewModel);
            window.Show();
        }

        private void closeWindow(MainViewModel viewModel)
        {
            CloseWindow(viewModel.Order);
        }

        public void CloseWindow(int order)
        {
            var modWindow = _notifications.FirstOrDefault(x => x.Order == order);
            if (modWindow != null)
                modWindow.Window.Close();

            foreach (var item in _notifications.Where(x => x.Order > order))
            {
                item.Order -= 1;
                item.MoveWindowTopDown();
            }
        }
    }
}
