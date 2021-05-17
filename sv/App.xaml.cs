using System;
using System.Threading;
using System.Windows;
using System.Windows.Threading;
using ACE;

namespace sv
{
    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public partial class App : Application
    {
        //Designed By Mr.Xin 
        App()
        {
            //异常委托
            DispatcherUnhandledException += App_DispatcherUnhandledException;
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;

            //启动委托
            this.Startup += new StartupEventHandler(App_Startup);
            this.Startup += new StartupEventHandler(Application_Initialized);
        }


        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
        }

        private Mutex mutex;
        private void App_Startup(object sender, StartupEventArgs e)
        {
            bool ret;
            mutex = new Mutex(true, "ElectronicNeedleTherapySystem", out ret);
            if (!ret)
            {
                MessageBox.Show("软件已运行，请不要多次打开");
                Environment.Exit(0);
            }
        }

        //启动
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            if (License.Check())
            {
                MainWindow mainWindow = new MainWindow();
                mainWindow.Show();

            }
            else
            {
                MessageBox.Show("使用之前请先注册","TOPO", MessageBoxButton.OK,MessageBoxImage.Warning);
            }

        }

        private void Application_Initialized(object sender, StartupEventArgs e)
        {
            //创建数据库s
            CSQLite.Insert.CreatUseTabel();
        }


        private void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
        }

        private void App_DispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            if (e.Exception.GetType() != typeof(StackOverflowException))
            {
                e.Handled = true;
            }
        }
    }
}
