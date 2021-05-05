using System;
using System.Windows;
using System.Windows.Threading;
using System.Threading;
using System.Runtime.ExceptionServices;
using System.Windows.Controls;
using Startup;
using System.IO;
using BaseUtil;

namespace EXCYiXing
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
            AppDomain.CurrentDomain.FirstChanceException += App_FirstChanceException;
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
                Environment.Exit(0);
            }
        }

        public StartWindow StartWindow;


        //启动
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            if(!Directory.Exists("Temp"))
            {
                Directory.CreateDirectory("Temp");
            }
            MainWindow = new MainWindow(); 
            StartWindow = new StartWindow(MainWindow, new StartupGlobal { IDcardTest = true, CameraTest = true, StampTest = true, VarbTest = true });//启动逻辑照旧，从启动窗口启动
            StartWindow.Show();

        }

        private void Application_Initialized(object sender, StartupEventArgs e)
        {
            Global.Initialized();
            //创建数据库s
            //CSQLite.Insert.CreatUseTabel();

            //获取屏幕数量
            //BackgroundItem.Screens = System.Windows.Forms.Screen.AllScreens.Count()-1;

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
                System.Windows.Forms.Application.Restart();
            }
        }
        private void App_FirstChanceException(object sender, FirstChanceExceptionEventArgs e)
        {
            ////修正 winforms 报错不显示
            //if (e.Exception.Source!="System.Windows.Forms")
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
