using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotificationWpf.Models
{
    internal class NotificationObject
    {
        internal DateTime DateTimeCreate { get; private set; }
        internal MainViewModel ViewModel { get; private set; }
        internal MainWindow Window { get; private set; }

        internal TimeSpan Duration { get => (DateTime.Now - DateTimeCreate); }

        internal NotificationObject(MainViewModel viewModel, MainWindow window)
        {
            DateTimeCreate = DateTime.Now;
            ViewModel = viewModel;
            Window = window;
        }

    }
}
