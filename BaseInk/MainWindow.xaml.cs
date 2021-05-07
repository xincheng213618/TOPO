using BaseUtil;
using Microsoft.Ink;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;


namespace BaseInk
{
    public delegate void Delegate_Ink_Msg(string Msg);
    public delegate void Delegate();

    public static class InkPut
    {
        public static event Delegate_Ink_Msg delegate_Ink_Msg;
        public static Delegate delegates;
        public static TextBox t;
        public static void Invoke(string Msg)
        {
            delegate_Ink_Msg?.Invoke(Msg);
        }
    }
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class InkWindows : Window
    {
        public class SelectionsList
        {
            public string Content111 { get; set; }
        }
        private ObservableCollection<SelectionsList> selectionsLists = new ObservableCollection<SelectionsList>();
        public InkWindows()
        {
            Topmost = true;
            InitializeComponent();
        }
        private void Window_MouseDrag(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }
        private void Window_Initialized(object sender, EventArgs e)
        {
            ListVierew.ItemsSource = selectionsLists;
            InkPut.delegates += new Delegate(clearText);
        }
        public static event Delegate_Ink_Msg delegate_Ink_Msg;
        private void Clear_Click(object sender, RoutedEventArgs e)
        {
            selectionsLists.Clear();
            inkCanvas.Strokes.Clear();
        }
        //修正不直接对页面负责对窗口负责
        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            KeyHelper.OnKeyPress(KeyHelper.KeyCode.BACK);
        }

        Timer timer = null;
        private void InkCanvas_StrokeCollected(object sender, InkCanvasStrokeCollectedEventArgs e)
        {
            ListVierew.Visibility = Visibility.Visible;
            if (timer != null)
            {
                timer.Dispose();
                timer = null;
            }
            timer = new Timer(_ => Dispatcher.BeginInvoke(new Action(() => HandWriting_Recognize())), null, TimeSpan.FromMilliseconds(100), Timeout.InfiniteTimeSpan);
        }

        private int lastcout = 0;
        private void HandWriting_Recognize()
        {
            timer.Dispose();
            timer = null;
            using (MemoryStream stream = new MemoryStream())
            {
                inkCanvas.Strokes.Save(stream);
                InkCollector inkCollector = new InkCollector();
                Ink ink = new Ink();
                ink.Load(stream.ToArray());
                new Thread(_ =>
                {
                    using (RecognizerContext context = new RecognizerContext())
                    {
                        context.Factoid = Factoid.ChineseSimpleCommon;
                        if (ink.Strokes.Count - lastcout > 10)
                        {
                            lastcout = ink.Strokes.Count;
                            Dispatcher.BeginInvoke(new Action(() =>
                            {
                                selectionsLists.Clear();
                                inkCanvas.Strokes.Clear();
                            }
                            ));
                        }
                        else
                        {

                            lastcout = ink.Strokes.Count;
                        }
                        if (ink.Strokes.Count > 0 && ink.Strokes.Count < 40)
                        {
                            context.Strokes = ink.Strokes;
                            RecognitionStatus status;
                            var result = context.Recognize(out status);
                            if (status == RecognitionStatus.NoError)
                            {
                                RecognitionAlternates selections = result.GetAlternatesFromSelection();

                                Dispatcher.BeginInvoke(new Action(() =>
                                {
                                    selectionsLists.Clear();
                                    foreach (var item in selections)
                                    {
                                        selectionsLists.Add(new SelectionsList() { Content111 = item.ToString() });
                                    };
                                }));
                            }
                        }
                        else
                        {
                            Dispatcher.BeginInvoke(new Action(() =>
                            {
                                selectionsLists.Clear();
                                inkCanvas.Strokes.Clear();
                            }
                            ));
                        }
                    }
                }).Start();
            }
        }
        private void Fun_Click(object sender, RoutedEventArgs e)
        {
            KeyHelper.HotKey(new List<byte>() { KeyHelper.KeyCode.WinL, KeyHelper.KeyCode.SPACE });
        }


        private void Exit(object sender, RoutedEventArgs e)
        {
            selectionsLists.Clear();
            inkCanvas.Strokes.Clear();
            Hide();
        }


        private void ListVierew_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ListView listView = sender as ListView;
            if (listView.SelectedIndex > -1)
            {
                InkPut.Invoke(selectionsLists[listView.SelectedIndex].Content111);
                selectionsLists.Clear();
                inkCanvas.Strokes.Clear();
            }
            ListVierew.Visibility = Visibility.Hidden;
        }

        string[] ss = new string[] { "Q", "W", "E", "R", "T", "Y", "U", "I", "O", "P" };
        string[] sss = new string[] { "A", "S", "D", "F", "G", "H", "J", "K", "L" };
        string[] ssss = new string[] { "Z", "X", "C", "V", "B", "N", "M" };
        string[] ss1 = new string[] { "q", "w", "e", "r", "t", "y", "u", "i", "o", "p" };
        string[] ss2 = new string[] { "a", "s", "d", "f", "g", "h", "j", "k", "l" };
        string[] ss3 = new string[] { "z", "x", "c", "v", "b", "n", "m" };
        bool l = true;
        Button Buttones = null;
        Border borders = null;
        KeyHelper.KeyCode B = new KeyHelper.KeyCode();
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            sk.Children.Clear();
            sk2.Children.Clear();
            sk3.Children.Clear();
            if (l)
            {
                add(ss1, ss2, ss3);
                if (Console.CapsLock)
                {
                    KeyHelper.OnKeyPress(KeyHelper.KeyCode.CAPS_LOCK);
                    ((System.Windows.Controls.Button)sender).Content = "小写";
                }
                l = false;
            }
            else
            {
                add(ss, sss, ssss);
                l = true;
                if (!Console.CapsLock)
                {
                    KeyHelper.OnKeyPress(KeyHelper.KeyCode.CAPS_LOCK);
                    ((System.Windows.Controls.Button)sender).Content = "大写";

                }
            }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            sk.Children.Clear();
            sk2.Children.Clear();
            sk3.Children.Clear();
            add(ss1, ss2, ss3);
            l = false;
            if (((System.Windows.Controls.Button)sender).Content.ToString() == "英")
            {
                ((System.Windows.Controls.Button)sender).Content = "中";
                btn1.Content = "小写";
            }
            else
            {
                ((System.Windows.Controls.Button)sender).Content = "英";

            }
            if (Console.CapsLock)
            {

                KeyHelper.OnKeyPress(KeyHelper.KeyCode.CAPS_LOCK);
            }

            KeyHelper.OnKeyPress(KeyHelper.KeyCode.SHIFT);
        }
        private void Button_Clicks(object sender, RoutedEventArgs e)
        {
            string indexy = ((System.Windows.Controls.Button)sender).Content.ToString();
            switch (indexy)
            {

                case "=":
                    break;
                default:
                    KeyHelper.OnKeyPress(B[indexy]);
                    break;

            }
        }
        bool tkey = true;
        private void Button_Click(object sender, RoutedEventArgs e)
        { 
            if (tkey)
            {
                if (InkPut.t==null)
                {
                    InkPut.t = new TextBox();
                }
                tx.Text = InkPut.t.Text;
                key.Visibility = Visibility.Visible;
                add(ss, sss, ssss);
                num();
                tx.Focus();
                KeyHelper.OnKeyPress(KeyHelper.KeyCode.END);

                KeyHelper.OnKeyPress(KeyHelper.KeyCode.CAPS_LOCK);
                tkey = false;
                key.Visibility = Visibility.Visible;

            }
            else
            {
                key.Visibility = Visibility.Hidden;

                sk.Children.Clear();
                sk2.Children.Clear();
                sk3.Children.Clear();
                sk4.Children.Clear();
                tkey = true;

            }

        }
        public void clearText()
        {
            tx.Text = "";
        }
        void num()
        {
            TextBlock textBlock = new TextBlock();
            textBlock.Height = list.Height;
            textBlock.Width = 10;
            sk4.Children.Add(textBlock);

            for (int i = 0; i < 10; i++)
            {
            
                Buttones = new Button();
                Buttones.Click += Button_Clicks;
                Buttones.Background = a; ;
                Buttones.Width = list.Width+20;
                Buttones.Height = list.Height;
                Buttones.BorderBrush = list.BorderBrush;
                Buttones.Focusable = true;
                Buttones.BorderThickness = new Thickness(0, 0, 20, 0); ;
                Buttones.FontSize = 20;
                Buttones.Name = "aa" + i;
                Buttones.Content = i.ToString();
                sk4.Children.Add(Buttones );
            }
        }

        ImageBrush a = new ImageBrush(new BitmapImage(
                    new Uri(Directory.GetCurrentDirectory() + "\\images\\按钮1.png", UriKind.Absolute)
                ));
        ImageBrush b = new ImageBrush(new BitmapImage(
                    new Uri(Directory.GetCurrentDirectory() + "\\images\\按下1.png", UriKind.Absolute)
                ));
        void add(string[] str1, string[] str2, string[] str3)
        {

            int s = 10;
            for (int j = 0; j < 3; j++)
            {
                TextBlock textBlock = new TextBlock();
                textBlock.Height = list.Height;
                if (j == 0)
                {
                    textBlock.Width = 50;
                    s = 9;
                    sk2.Children.Add(textBlock);
                }
                else if (j == 1)
                {
                    textBlock.Width = 10;
                    s = 10;
                    sk.Children.Add(textBlock);
                }
                else
                {
                    textBlock.Width = 90;
                    s = 7;
                    sk3.Children.Add(textBlock);
                }
                for (int i = 0; i < s; i++)
                {
                    Buttones = new Button();
                    Buttones.Click += Button_Clicks;
                    Buttones.Width = list.Width+20;
                    Buttones.Height = list.Height;
                    Buttones.Background = a; ;
                    Buttones.BorderBrush = list.BorderBrush;
                    Buttones.Focusable = false;
                    Buttones.BorderThickness = new Thickness(0,0,20,0);
                    Buttones.FontSize = 20;
                    Buttones.Name = "a" + i;
                    if (j == 0)
                    {
                        Buttones.Content = str2[i];

                        sk2.Children.Add(Buttones);
                    }
                    else if (j == 1)
                    {
                        Buttones.Content = str1[i];
                        sk.Children.Add(Buttones);

                    }
                    else
                    {
                        Buttones.Content = str3[i];
                        sk3.Children.Add(Buttones);

                    }
                }


            }


        }

        private void tx_TextChanged(object sender, TextChangedEventArgs e)
        {
            InkPut.t.Text = tx.Text;
        }

        private void Window_Activated(object sender, EventArgs e)
        {
            tx.Focus();
        }
        int t1 = 0;
        int t2 = 0;

        int t3 = 0;
        WrapPanel InkWindowss = null;


        private void key_MouseDown(object sender, MouseButtonEventArgs e)
        {
            switch (((Label)sender).Name.ToString())
            {
                case "l1":
                    t3 = 10;
                    InkWindowss = sk;
                    break;
                case "l2":
                    t3 = 50;
                    InkWindowss = sk2;
                    break;
                case "l3":
                    t3 = 90;
                    InkWindowss = sk3;
                    break;
                case "l4":
                    t3 = 10;
                    InkWindowss = sk4;
                    break;
            }

            int tk =Convert.ToInt32( e.GetPosition((Label)sender).X.ToString())- t3;
            if (tk<70)
            {
                if (tk<=50)
                {
                    tk = 0;
                    ((Button)(InkWindowss.Children[tk + 1])).Background = b; ;
                }
            }
            else
            {
                t1 = tk % 70;
                t2 = tk / 70;

                if (70-t1>20)
                {
                    try
                    {
                        ((Button)(InkWindowss.Children[t2 + 1])).Background = b; ;

                    }
                    catch (Exception)
                    {

                    }
                }
            }
        }
        private void Label_MouseUp(object sender, MouseButtonEventArgs e)
        {
            int tk = Convert.ToInt32(e.GetPosition((Label)sender).X.ToString()) - t3;
            if (tk < 70)
            {
                if (tk <= 50)
                {
                    tk = 0;
                    try
                    {
                        ((Button)(InkWindowss.Children[tk + 1])).Background = a; ;
                        KeyHelper.OnKeyPress(B[((Button)(InkWindowss.Children[t2 + 1])).Content.ToString()]);
                    }
                    catch (Exception)
                    {
                     
                    }
           
                }
            }
            else
            {
                t1 = tk % 70;
                t2 = tk / 70;

                if (70 - t1 > 20)
                {
                    try
                    {
                        ((Button)(InkWindowss.Children[t2 + 1])).Background = a; ;
                    KeyHelper.OnKeyPress(B[((Button)(InkWindowss.Children[t2 + 1])).Content.ToString()]);

                    }
                    catch (Exception)
                    {

                    }
                }
            }
        }
    }
}
