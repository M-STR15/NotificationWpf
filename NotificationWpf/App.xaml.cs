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

            //testMethod();
        }

        private void testMethod()
        {
            var notificationManagement = _container.Get<NotificationManagement>();

            var id = 0;
            notificationManagement.Create(Models.eNotificationType.Success, "test" + ++id);
            //Thread.Sleep(2000);
            notificationManagement.Create(Models.eNotificationType.Warning, "test" + ++id);
            //Thread.Sleep(2000);
            notificationManagement.Create(Models.eNotificationType.Info, "test" + ++id);
            //Thread.Sleep(2000);
            notificationManagement.Create(Models.eNotificationType.Error, "test" + ++id);

            for (var i = 0; i < 20; i++)
            {
                notificationManagement.Create(Models.eNotificationType.Error, "test" + ++id);
                //Thread.Sleep(1000);
            }
        }

        private void configureContainer()
        {
            _container = new StandardKernel();

            _container.Bind<NotificationManagement>().To<NotificationManagement>().InSingletonScope()
                .WithConstructorArgument("durationSeconds", 15)
                .WithConstructorArgument("width", 400)
                .WithConstructorArgument("height", 60)
                .WithConstructorArgument("framing", 2)
                .WithConstructorArgument("cornerRadius", 10);
        }
    }
}