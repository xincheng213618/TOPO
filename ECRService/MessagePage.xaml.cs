using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ECRService
{
    /// <summary>
    /// MessagePage.xaml 的交互逻辑
    /// </summary>
    public partial class MessagePage : Page
    {
        private DispatcherTimer pageTimer = null;

        public MessagePage(string message)
        {
            InitializeComponent();

            pageTimer = new DispatcherTimer()
            {
                IsEnabled = false,
                Interval = TimeSpan.FromSeconds(5),
            };
            pageTimer.Tick += new EventHandler((sender, e) =>
            {
                pageTimer.IsEnabled = false;
                Dispatcher.BeginInvoke(new Action(() => (Application.Current.MainWindow as MainWindow).frame.Navigate(new HomePage())));
            });
            pageTimer.IsEnabled = true;

            messageBox.Text = message;
        }
    }
}
