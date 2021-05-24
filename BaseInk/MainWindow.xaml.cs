using Microsoft.Ink;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
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
      static  double x1 = SystemParameters.PrimaryScreenWidth;//得到屏幕整体宽度
        static double y1 = SystemParameters.PrimaryScreenHeight;//得到屏幕整体高度
        private void Window_Initialized(object sender, EventArgs e)
        {
            ListVierew.ItemsSource = selectionsLists;
            InkPut.delegates += new Delegate(clearText);
            Top = (y1 - this.Height) / 2;
            Left= (x1 - this.Width) / 2;
            a = ax2.Background;
            b = an.Background;



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
            //KeyHelper.OnKeyPress(KeyHelper.KeyCode.BACK);
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
           
            tx.Focus();
            if (Console.CapsLock)
            {
                KeyHelper.OnKeyPress(0x14);
            }
          
           
            KeyHelper.OnKeyPress(0x10);
             

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
               tx.Text+= selectionsLists[listView.SelectedIndex].Content111;
                selectionsLists.Clear();
                inkCanvas.Strokes.Clear();
            }
            ListVierew.Visibility = Visibility.Hidden;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (KeyBorderLabel.Content.ToString() == "键盘")
            {
                grid_center.Visibility = Visibility.Visible;
                InkGrid.Visibility = Visibility.Hidden;
           KeyBorderLabel.Content = "手写";
            }
            else
            {
                grid_center.Visibility = Visibility.Hidden;
                InkGrid.Visibility = Visibility.Visible;
                KeyBorderLabel.Content = "键盘";

            }
            tx.Focus();
            KeyHelper.OnKeyPress(0x23);

            selectionsLists.Clear();
            inkCanvas.Strokes.Clear();
        }
        public void clearText()
        {
            tx.Text = "";
        }
        private void Window_Activated(object sender, EventArgs e)
        {
        }



        private void Grid_Click(object sender, RoutedEventArgs e)
        {
            Button button = e.OriginalSource as Button;

            if (button == null) return;

            //Debug.WriteLine(button.Content);

            string content = button.Content.ToString();

            if (content == "确定")
            {
                this.OnSumitClick();
            }
            else if (content == "取消")
            {
                this.OnCancelClick();
            }
            else
            {
 
                string tag = button.Tag.ToString();
                tx.Focus();
                byte b = Convert.ToByte(tag);
                
                KeyHelper.OnKeyPress(b);
            }
           
        }

        //声明和注册路由事件
        public static readonly RoutedEvent SumitClickRoutedEvent =
            EventManager.RegisterRoutedEvent("SumitClick", RoutingStrategy.Bubble, typeof(EventHandler<RoutedEventArgs>), typeof(InkWindows));
        //CLR事件包装
        public event RoutedEventHandler SumitClick
        {
            add { this.AddHandler(SumitClickRoutedEvent, value); }
            remove { this.RemoveHandler(SumitClickRoutedEvent, value); }
        }

        //激发路由事件,借用Click事件的激发方法

        protected void OnSumitClick()
        {
            RoutedEventArgs args = new RoutedEventArgs(SumitClickRoutedEvent, this);
            this.RaiseEvent(args);
        }

        //声明和注册路由事件
        public static readonly RoutedEvent CancelClickRoutedEvent =
            EventManager.RegisterRoutedEvent("CancelClick", RoutingStrategy.Bubble, typeof(EventHandler<RoutedEventArgs>), typeof(InkWindows));
        //CLR事件包装
        public event RoutedEventHandler CancelClick
        {
            add { this.AddHandler(CancelClickRoutedEvent, value); }
            remove { this.RemoveHandler(CancelClickRoutedEvent, value); }
        }

        //激发路由事件,借用Click事件的激发方法

        protected void OnCancelClick()
        {
            RoutedEventArgs args = new RoutedEventArgs(CancelClickRoutedEvent, this);
            this.RaiseEvent(args);
        }



        public string InputText
        {
            get { return (string)GetValue(InputTextProperty); }
            set { SetValue(InputTextProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty InputTextProperty =
            DependencyProperty.Register("InputText", typeof(string), typeof(InkWindows), new PropertyMetadata(default(string), (d, e) =>
            {
                InkWindows control = d as InkWindows;

                if (control == null) return;

                string config = e.NewValue as string;


            }));

        private void Btn_Click(object sender, RoutedEventArgs e)
        {
            FuncButtonControl btn = sender as FuncButtonControl;

            string tag = btn.Tag.ToString();

            byte b = Convert.ToByte(tag);

            if (btn.IsChecked)
            {
                KeyHelper.OnKeyUp(b);
            }
            else
            {
                KeyHelper.OnKeyDown(b);
            }

            btn.IsChecked = !btn.IsChecked;

        }

        void RefreshCaps()
        {


            var btns = FindVisualChild<FuncButtonControl>(this.grid_center);

            foreach (var btn in btns)
            {
                if (btn.Content == null) continue;

                if (btn.Content.ToString().Length != 1) continue;

                //btn.Content = this.btn_caps.IsChecked ? btn.Content.ToString().ToUpper() : btn.Content.ToString().ToLower();

            }

        }

        List<T> FindVisualChild<T>(DependencyObject obj) where T : DependencyObject
        {
            List<T> collecion = new List<T>();

            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(obj); i++)
            {
                DependencyObject child = VisualTreeHelper.GetChild(obj, i);

                if (child != null && child is T)
                    collecion.Add((T)child);
                else
                {
                    collecion.AddRange(FindVisualChild<T>(child));
                }
            }

            return collecion;
        }

        private void btn_caps_Click(object sender, RoutedEventArgs e)
        {
            FuncButtonControl btn = sender as FuncButtonControl;

            string tag = btn.Tag.ToString();

            byte b = Convert.ToByte(tag);

            KeyHelper.OnKeyDown(b);

            btn.IsChecked = !btn.IsChecked;

            this.RefreshCaps();
        }

        private void TextBlock_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.OnCancelClick();
        }

        private void tx_TextChanged(object sender, TextChangedEventArgs e)
        {
            InkPut.t.Text = tx.Text;
        }
        Brush a;
        Brush b;// new ImageBrush(new BitmapImage(new Uri(Directory.GetCurrentDirectory()+"\\images\\按钮1.png")));
        private void Label_MouseDown(object sender, MouseButtonEventArgs e)
        {
            ((Label)sender).Background = a;
        }

        private void Label_MouseUp(object sender, MouseButtonEventArgs e)
        {
            ((Label)sender).Background = b;
            Label button = ((Label)sender);

            if (button == null) return;

            //Debug.WriteLine(button.Content);

            string content = button.Content.ToString();

            if (content == "确定")
            {
                this.OnSumitClick();
            }
            else if (content == "取消")
            {
                this.OnCancelClick();
            }
            else
            {

                string tag = button.Tag.ToString();
                tx.Focus();
                byte b = Convert.ToByte(tag);

                KeyHelper.OnKeyPress(b);
            }

        }

        private void Label_MouseMove(object sender, MouseEventArgs e)
        {
            ((Label)sender).Opacity = 0.8;

        }

        private void Label_MouseLeave(object sender, MouseEventArgs e)
        {
            ((Label)sender).Opacity = 1;
            ((Label)sender).Background = b;


        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            tx.Focus();



            KeyHelper.OnKeyPress(0x14);
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            tx.Text = "";
        }

        private void Window_ContentRendered(object sender, EventArgs e)
        {

        }

        private void Window_Closed(object sender, EventArgs e)
        {
            tx.Text = "";

        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {

        }

        private void Label_MouseMove_1(object sender, MouseEventArgs e)
        {
            ((Label)sender).Foreground = Brushes.Black;
        }

        private void Label_MouseLeave_1(object sender, MouseEventArgs e)
        {
            ((Label)sender).Foreground = Brushes.White;

        }
    }
            
 
}