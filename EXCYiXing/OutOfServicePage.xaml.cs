using System;
using System.Threading;
using System.Windows;
using System.Windows.Controls;

namespace EXCYiXing
{

    //Designed By Mr.xin 2020.5 
    /// <summary>
    /// OutOfServicePage.xaml 的交互逻辑
    /// </summary>
    public partial class OutOfServicePage : Page
    {
        public OutOfServicePage()
        {
            InitializeComponent();
        }

        private void Page_Initialized(object sender, EventArgs e)
        {
            (Application.Current.MainWindow as MainWindow).Topmost = true;
        }
    }
}
