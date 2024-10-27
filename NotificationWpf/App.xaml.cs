using Ninject;
using System.Windows;

namespace NotificationWpf
{
    internal partial class App : Application
    {
        private IKernel _container;

        [STAThread]
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            configureContainer();

            var notificationManagement = _container.Get<NotificationManagement>();
            notificationManagement.Create(Models.eNotificationType.Success, "test1");
            //Thread.Sleep(2000);
            notificationManagement.Create(Models.eNotificationType.Warning, "test2");
            //Thread.Sleep(2000);
            notificationManagement.Create(Models.eNotificationType.Info, "test3");
            //Thread.Sleep(2000);
            notificationManagement.Create(Models.eNotificationType.Error, "test4");
        }

        private void configureContainer()
        {
            _container = new StandardKernel();

            _container.Bind<NotificationManagement>().To<NotificationManagement>().InSingletonScope()
                .WithConstructorArgument("durationSeconds", 15);
        }
    }

}
