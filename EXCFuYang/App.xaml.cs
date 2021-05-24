using BaseInk;
using BaseUtil;
using Startup;
using System;
using System.Runtime.ExceptionServices;
using System.Threading;
using System.Windows;
using System.Windows.Threading;

namespace XinHua
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
            //AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;


            //启动委托
            Startup += new StartupEventHandler(App_Startup);
            this.Startup += new StartupEventHandler(Application_Initialized);


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
                System.Windows.Forms.Application.Restart();
            }
        }
        private void App_FirstChanceException(object sender, FirstChanceExceptionEventArgs e)
        {
            ////修正 winforms 报错不显示
            //if (e.Exception.Source != "System.Windows.Forms")
            //    Log.WriteException(e.Exception);
        }

        public MainWindow mainWindow;
        public StartWindow StartWindow;
        public static InkWindows  inkWindows ;
      
        //启动
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            mainWindow = new MainWindow();
            inkWindows = new InkWindows();
            StartWindow = new StartWindow(mainWindow, new StartupGlobal { IDcardTest = true, CameraTest = true, StampTest = false, VarbTest = false });//启动逻辑照旧，从启动窗口启动
            StartWindow.Show();

        }
        private void Application_Initialized(object sender, StartupEventArgs e)
        {
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
        private void Application_Exit(object sender, ExitEventArgs e)
        {
            Environment.Exit(0);
        }
    }

} 
