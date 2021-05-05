using BaseUtil;
using Startup;
using System;
using System.Runtime.ExceptionServices;
using System.Threading;
using System.Windows;
using System.Windows.Threading;

namespace EXC
{
    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public partial class App : Application
    {
        App()
        {
            //异常委托
            DispatcherUnhandledException += App_DispatcherUnhandledException;
            AppDomain.CurrentDomain.FirstChanceException += App_FirstChanceException;
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
            NewTemp();
            //启动委托
            Startup += new StartupEventHandler(App_Startup);
            Startup += new StartupEventHandler(Application_Initialized);
        }
        private void NewTemp()
        {
            System.IO.DirectoryInfo info = new System.IO.DirectoryInfo(System.Environment.CurrentDirectory);
            //如果子目录已存在，则此方法不执行任何操作。
            info.CreateSubdirectory("Temp");
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
                Environment.Exit(0);
            }
        }
        public MainWindow mainWindow;

        public StartWindow StartWindow;


        //启动
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            mainWindow = new MainWindow();
            StartWindow = new StartWindow(mainWindow, new StartupGlobal { IDcardTest = true, CameraTest = true, StampTest = false, VarbTest = false });//启动逻辑照旧，从启动窗口启动
            StartWindow.Show();

        }

        private void Application_Initialized(object sender, StartupEventArgs e)
        {
            Global.Initialized();
        }


        private void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            //Log.WriteException(e.ExceptionObject as Exception);
        }
        private void App_DispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            if (e.Exception.GetType() != typeof(StackOverflowException))
            {
                Log.Write(e.Exception.Message);
                e.Handled = true;
                if (MainWindow.IsActive)
                {
                }
            }
            else
            {
                Current.Shutdown();
            }
        }
        private void App_FirstChanceException(object sender, FirstChanceExceptionEventArgs e)
        {
            ////修正 winforms 报错不显示
            //if (e.Exception.Source != "System.Windows.Forms")
            //    Log.WriteException(e.Exception);
        }



        public void Application_Exit(object sender, ExitEventArgs e)
        {
            Environment.Exit(0);
        }
    }
}
