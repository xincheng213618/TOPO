using Startup;
using System;
using System.Runtime.ExceptionServices;
using System.Threading;
using System.Windows;
using System.Windows.Threading;
using BaseUtil;
using Org.BouncyCastle.Asn1.X500;
using System.IO;
using Background;
using System.Linq;

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
        MainWindow mainWindow;
        StartWindow StartWindow;
        public static BackgroundWindow backgroundWindow;
        //启动
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            if (!Directory.Exists("Temp"))
                Directory.CreateDirectory("Temp");
            mainWindow = new MainWindow();
            StartWindow = new StartWindow(mainWindow, new StartupGlobal { IDcardTest = true, CameraTest = true, StampTest = true, VarbTest = false });//启动逻辑照旧，从启动窗口启动
            StartWindow.Show();

            backgroundWindow = new BackgroundWindow();
            backgroundWindow.Show();
        }

        private void Application_Initialized(object sender, StartupEventArgs e)
        {
            Global.Initialized();

            BackgroundItem.Screens = Global.Config.BackgroundWindow > System.Windows.Forms.Screen.AllScreens.Count() - 1 ? System.Windows.Forms.Screen.AllScreens.Count() - 1 : Global.Config.BackgroundWindow;
        }


        private void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            Log.WriteException(e.ExceptionObject as Exception);
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
            if (e.Exception.Source != "System.Windows.Forms")
                Log.WriteException(e.Exception);
        }



        public void Application_Exit(object sender, ExitEventArgs e)
        {
            Environment.Exit(0);
        }
    }
}
