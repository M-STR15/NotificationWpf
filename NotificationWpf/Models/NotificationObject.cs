using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotificationWpf.Models
{
    public class NotificationObject
    {
        public DateTime DateTimeCreate { get; private set; }
        public MainViewModel ViewModel { get; private set; }
        public MainWindow Window { get; private set; }

        public TimeSpan Duration { get => (DateTime.Now - DateTimeCreate); }

        public NotificationObject(MainViewModel viewModel, MainWindow window)
        {
            DateTimeCreate = DateTime.Now;
            ViewModel = viewModel;
            Window = window;
        }

    }
}
