using System;
using System.Windows;
using System.Windows.Input;
using System.Xml.Linq;
using EXCResources;

namespace EXC
{
    /// <summary>
    /// AboutMsg.xaml 的交互逻辑
    /// </summary>
    public partial class AboutMsg : Window
    {
        public AboutMsg()
        {
            InitializeComponent();
        }

        private void Button_Close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Window_MouseDrag(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void Window_Initialized(object sender, EventArgs e)
        {
            ComputerInfo.Text += " .NET Framework 版本:" + XInfo.NetVersion() + Environment.NewLine; ;
            XDocument Doc = XDocument.Load("Config.xml");
            foreach (var col in Doc.Element("ConfigData").Elements())
            {
                ComputerInfo.Text += col.Name + ":" + col.Value + Environment.NewLine;
            }


        }
    }
}
