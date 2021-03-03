using Startup;
using System;
using System.Runtime.ExceptionServices;
using System.Threading;
using System.Windows;
using System.Windows.Threading;
using BaseUtil;
using System.IO;

namespace REC
{
    //Designed By Mr.Xin 2020.8.20
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


            //启动委托
            Startup += new StartupEventHandler(App_Startup);
            Startup += new StartupEventHandler(Application_Initialized);


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
        public StartWindow startWindow;

        //启动
        private void Application_Startup(object sender, StartupEventArgs e)
        {

            mainWindow = new MainWindow();
            startWindow = new StartWindow(mainWindow, new StartupGlobal { IDcardTest = true, CameraTest = true, StampTest = false, VarbTest = true });//启动逻辑照旧，从启动窗口启动
            startWindow.Show();
        }

        private void Application_Initialized(object sender, StartupEventArgs e)
        {
            Log.Write("软件启动：" + DateTime.Now.ToString("yyyy年MM月dd日 HH:mm:ss"));
            Log.Write(Global.IP + Environment.NewLine + Global.MAC);
            if (!Directory.Exists("Temp"))
                Directory.CreateDirectory("Temp");
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
                    (Current.MainWindow as MainWindow).frame.Navigate(new HomePage(e.Exception.Message));
                }
            }
            else
            {
                Current.Shutdown();
            }
        }
        private void App_FirstChanceException(object sender, FirstChanceExceptionEventArgs e)
        {
            //if (e.Exception.Source != "System.Windows.Forms")
            //    Log.WriteException(e.Exception);
        }
    }
}
