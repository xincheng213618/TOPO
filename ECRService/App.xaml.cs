using System;
using System.IO;
using System.Threading;
using System.Windows;
using System.Windows.Threading;

namespace ECRService
{
    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public partial class App : Application
    {
        App()
        {
            DispatcherUnhandledException += App_DispatcherUnhandledException;
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;

            //启动委托
            Startup += new StartupEventHandler(App_Startup);
            Startup += new StartupEventHandler(Application_Initialized);

        }

        private void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
        }

        private void App_DispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            if (e.Exception.GetType() != typeof(StackOverflowException))
            {
                e.Handled = true;
                (Current.MainWindow as MainWindow).frame.Navigate(new HomePage());
            }
            else
            {
            }
        }

        private void Application_Startup(object sender, StartupEventArgs e)
        {
            MainWindow main = new MainWindow();
            main.Show();
        }

        private void Application_Initialized(object sender, StartupEventArgs e)
        {
            if (!Directory.Exists("Temp"))
                Directory.CreateDirectory("Temp");

            Global.Initialized();
            //获取屏幕数量
        }

        private Mutex mutex;
        private void App_Startup(object sender, StartupEventArgs e)
        {

            bool ret;
            mutex = new Mutex(true, "ElectronicNeedleTherapySystem", out ret);
            if (!ret)
            {

                Environment.Exit(0);
            }
        }

    }
}
