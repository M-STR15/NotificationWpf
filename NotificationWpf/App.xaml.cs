using Ninject;
using NotificationWpf.Services;
using System.Configuration;
using System.Data;
using System.Windows;

namespace NotificationWpf
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private IKernel _container;

        [STAThread]
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            configureContainer();

            var notificationManagement = _container.Get<NotificationManagement>();
            notificationManagement.Create(Models.eTypeNotification.Suscaise,"test1");
            Thread.Sleep(1000);
            notificationManagement.Create(Models.eTypeNotification.Warning, "test2");
            Thread.Sleep(2000);
            notificationManagement.Create(Models.eTypeNotification.Warning);
            Thread.Sleep(2000);
            notificationManagement.Create(Models.eTypeNotification.Error);
            Thread.Sleep(2000);
            notificationManagement.Create(Models.eTypeNotification.Warning);
        }

        private void configureContainer()
        {
            _container = new StandardKernel();

            _container.Bind<NotificationManagement>().To<NotificationManagement>().InSingletonScope();
        }
    }

}
