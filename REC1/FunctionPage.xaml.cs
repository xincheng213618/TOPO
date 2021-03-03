using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;
using BaseDLL;
using BaseUtil;

namespace REC
{
    /// <summary>
    /// FunctionPage.xaml 的交互逻辑
    /// </summary>
    public partial class FunctionPage : Page
    {
        IDCardData iDCardData;

        public FunctionPage(IDCardData iDCardData)
        {
            this.iDCardData = iDCardData;
            InitializeComponent();
        }

        private DispatcherTimer pageTimer = null;
        public TimeCount timeCount = new TimeCount() { Countdown =120};
        private void Page_Initialized(object sender, EventArgs e)
        {
            TopGrid.DataContext = timeCount;
            Countdown();
        }
        
        private void Countdown()
        {  
            pageTimer = new DispatcherTimer() { IsEnabled = true, Interval = TimeSpan.FromSeconds(1), };
            pageTimer.Tick += new EventHandler((sender, e) =>
            {
                if (--timeCount.Countdown <= 0)
                {
                    Content = new HomePage();
                    Pages();
                }
            });
        }

        private void Pages()
        {
            pageTimer.IsEnabled = false;
            Dispatcher.BeginInvoke(new Action(() => (Application.Current.MainWindow as MainWindow).frame.Navigate(Content)));
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            Global.PageType = button.Tag.ToString();
            switch (button.Tag)
            {
                case "Peroson":
                    //Content = new QRCode(iDCardData);
                    Content = new FunctionPage3();
                    Pages();
                    break;
                case "Commission":
                    Global.PageType = "Commission";
                    Content = new IDCardPage();
                    Pages();
                    break;
                case "HomePage":
                    Content = new HomePage();
                    Pages();

                    break;
                default:
                    break;
            }
        }
    }
}
