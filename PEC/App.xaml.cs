using Startup;
using System;
using System.Runtime.ExceptionServices;
using System.Threading;
using System.Windows;
using System.Windows.Threading;
using BaseUtil;
using Background;
using System.Linq;

namespace PEC
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
            this.Startup += new StartupEventHandler(App_Startup);
            this.Startup += new StartupEventHandler(Application_Initialized);


        }
        public static BackgroundWindow backgroundWindow;

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



        //启动
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            StartWindow StartWindow = new StartWindow(mainWindow, new StartupGlobal { IDcardTest = true, CameraTest = true, StampTest = true , VarbTest=true });
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
            //Log.WriteException(e.ExceptionObject as Exception);
        }
        private void App_DispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            if (e.Exception.GetType() != typeof(StackOverflowException))
            {
                Log.Write(e.Exception.Message);
                Log.Write(e.Exception.StackTrace);
                Log.Write(e.Exception.Source);
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
            ////修正 winforms 报错不显示
            //    if (e.Exception.Source != "System.Windows.Forms")
            //    Log.WriteException(e.Exception);
        }



        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.Close();
        }

        public void Application_Exit(object sender, ExitEventArgs e)
        {
            Environment.Exit(0);
        }

    }
}
