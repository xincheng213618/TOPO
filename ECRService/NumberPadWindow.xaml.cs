using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace ECRService
{
    public delegate void NumberPadInputHandler(object sender, NumberPadInputEventArgs e);

    public enum NumberPadKey : Int32
    {
        Number_0 = 0,
        Number_1 = 1,
        Number_2 = 2,
        Number_3 = 3,
        Number_4 = 4,
        Number_5 = 5,
        Number_6 = 6,
        Number_7 = 7,
        Number_8 = 8,
        Number_9 = 9,
        Clear,
        Enter
    }

    public class NumberPadInputEventArgs : EventArgs
    {
        public NumberPadKey key { get; }

        public NumberPadInputEventArgs(NumberPadKey key)
        {
            this.key = key;
        }
    }

    public class NumberPadInputListener
    {
        public Dispatcher dispatcher { get; }
        public NumberPadInputHandler handler { get; }

        public NumberPadInputListener(Dispatcher dispatcher, NumberPadInputHandler handler)
        {
            this.dispatcher = dispatcher;
            this.handler = handler;
        }
    }

    /// <summary>
    /// NumberPadWindow.xaml 的交互逻辑
    /// </summary>
    public partial class NumberPadWindow : Window
    {
        private static NumberPadWindow numberPadWindow = null;
        private NumberPadInputListener listener = null;

        private NumberPadWindow()
        {
            InitializeComponent();

            Owner = Application.Current.MainWindow;
        }

        public static NumberPadWindow GetNumberPadWindow()
        {
            if (numberPadWindow == null)
                numberPadWindow = new NumberPadWindow();

            return numberPadWindow;
        }

        private void OKButton_Click(object sender, RoutedEventArgs e)
        {
            if (listener == null) return;
            listener.dispatcher.BeginInvoke(new Action(() => listener.handler(this, new NumberPadInputEventArgs(NumberPadKey.Enter))));
            Hide();
        }

        private void ClearButton_Click(object sender, RoutedEventArgs e)
        {
            if (listener == null) return;
            listener.dispatcher.BeginInvoke(new Action(() => listener.handler(this, new NumberPadInputEventArgs(NumberPadKey.Clear))));
        }

        private void Zero_Click(object sender, RoutedEventArgs e)
        {
            if (listener == null) return;
            listener.dispatcher.BeginInvoke(new Action(() => listener.handler(this, new NumberPadInputEventArgs(NumberPadKey.Number_0))));
        }

        private void One_Click(object sender, RoutedEventArgs e)
        {
            if (listener == null) return;
            listener.dispatcher.BeginInvoke(new Action(() => listener.handler(this, new NumberPadInputEventArgs(NumberPadKey.Number_1))));
        }

        private void Two_Click(object sender, RoutedEventArgs e)
        {
            if (listener == null) return;
            listener.dispatcher.BeginInvoke(new Action(() => listener.handler(this, new NumberPadInputEventArgs(NumberPadKey.Number_2))));
        }

        private void Three_Click(object sender, RoutedEventArgs e)
        {
            if (listener == null) return;
            listener.dispatcher.BeginInvoke(new Action(() => listener.handler(this, new NumberPadInputEventArgs(NumberPadKey.Number_3))));
        }

        private void Four_Click(object sender, RoutedEventArgs e)
        {
            if (listener == null) return;
            listener.dispatcher.BeginInvoke(new Action(() => listener.handler(this, new NumberPadInputEventArgs(NumberPadKey.Number_4))));
        }

        private void Five_Click(object sender, RoutedEventArgs e)
        {
            if (listener == null) return;
            listener.dispatcher.BeginInvoke(new Action(() => listener.handler(this, new NumberPadInputEventArgs(NumberPadKey.Number_5))));
        }

        private void Six_Click(object sender, RoutedEventArgs e)
        {
            if (listener == null) return;
            listener.dispatcher.BeginInvoke(new Action(() => listener.handler(this, new NumberPadInputEventArgs(NumberPadKey.Number_6))));
        }

        private void Seven_Click(object sender, RoutedEventArgs e)
        {
            if (listener == null) return;
            listener.dispatcher.BeginInvoke(new Action(() => listener.handler(this, new NumberPadInputEventArgs(NumberPadKey.Number_7))));
        }

        private void Eight_Click(object sender, RoutedEventArgs e)
        {
            if (listener == null) return;
            listener.dispatcher.BeginInvoke(new Action(() => listener.handler(this, new NumberPadInputEventArgs(NumberPadKey.Number_8))));
        }

        private void Nine_Click(object sender, RoutedEventArgs e)
        {
            if (listener == null) return;
            listener.dispatcher.BeginInvoke(new Action(() => listener.handler(this, new NumberPadInputEventArgs(NumberPadKey.Number_9))));
        }

        public void EnableNumberPadWindow(Dispatcher dispatcher, NumberPadInputHandler handler)
        {
            Dispatcher.BeginInvoke(new Action(() =>
            {
                listener = new NumberPadInputListener(dispatcher, handler);
                Show();
            }));
        }

        public void DisableNumberPadWindow()
        {
            Dispatcher.BeginInvoke(new Action(() =>
            {
                listener = null;
                Hide();
            }));
        }
    }
}
