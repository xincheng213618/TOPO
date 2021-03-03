
using BaseUtil;
using Microsoft.Ink;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Threading;

namespace XinHua
{
    /// <summary>
    /// SearchPage.xaml 的交互逻辑
    /// </summary>
    public partial class SearchPage : Page
    {
        private string companyName = null;
        //查询
        private bool querying = false;
        private Timer timer = null;
        //private int pageNo = 1;
        public SearchPage()
        {
            InitializeComponent();
        }
        public SearchPage(string Msg)
        {
            InitializeComponent();
            PopAlert(Msg, 3);
        }

        private void Page_Initialized(object sender, EventArgs e)
        {
            FocusManager.SetFocusedElement(this, CompanySearchBox);
            Countdown_timer();
            switch(Global.PageType)
            {
                case ("QiYeXinXi"):
                    searchTitle.Content = "企业信息查询";
                    break;
                case ("NaShuiXinYongA"):
                    searchTitle.Content = "纳税信用A级企业列表搜索";
                    break;
                case ("ShuiShouWeiFa"):
                    searchTitle.Content = "重大税收违法案件人查询";
                    break;
                case ("ShiXinRen"):
                    searchTitle.Content = "失信被执行人查询";
                    break;
                default:
                    searchTitle.Content = "企业名称查询页面";
                    break;
            }
        }

        private void Home_Click(object sender, RoutedEventArgs e)
        {
            Content = new HomePage();
            Pages();
        }
        //计时器模块
        private DispatcherTimer pageTimer = null;
        TimeCount Time = new TimeCount();

        private void Countdown_timer()
        {
            this.DataContext = Time;
            pageTimer = new DispatcherTimer()
            {
                IsEnabled = true,
                Interval = TimeSpan.FromSeconds(1),
            };
            pageTimer.Tick += new EventHandler((sender, e) =>
            {

                if (--Time.Countdown <= 0)
                {
                    Content = new HomePage();
                    Pages();
                }
            });
        }
        //页面转换
        private void Pages()
        {
            pageTimer.IsEnabled = false;
            Dispatcher.BeginInvoke(new Action(() => (Application.Current.MainWindow as MainWindow).frame.Navigate(Content)));
        }

        private void Query_Click(object sender, RoutedEventArgs e)
        {
            Search();
        }

        private void Search()
        {
            companyName = CompanySearchBox.Text;
            if (companyName.Length ==0)
            {
                PopAlert("请输入需要查询的内容", 2);
                return;
            }
            if (querying) return;
            querying = true;
            WaitShow.Visibility = Visibility.Visible;
            hintLabel.Content = "正在查询请稍候";
            Time.Countdown = 59;


            Content = new SearchListPage(companyName);
            Pages();

            //thread = new Thread(() => SearchRequests())
            //{
            //    IsBackground = true
            //};
            //thread.Start();         
        }

        private async void PopAlert(string Msg, int time)
        {           
            PopTips.Text = Msg;
            Pop.Visibility = Visibility.Visible;
            await Task.Delay(TimeSpan.FromSeconds(time));
            Pop.Visibility = Visibility.Hidden;
        }
        private void TextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            inkPanel.Visibility = Visibility.Hidden;
        }

        private void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            inkPanel.Visibility = Visibility.Visible;
            inkCanvas.Strokes.Clear();
            Clear_ink();
        }

        private void TextChanged(object sender, TextChangedEventArgs e)
        {
            Time.Countdown = 59;
        }
        //回车搜索
        private void KeyEnter(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                Search();
            }
        }

        private void Recognizer_Select(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            if (button.Content != null)
            {
                int oldSelectionStart = CompanySearchBox.SelectionStart;
               
                CompanySearchBox.Text = CompanySearchBox.Text.Substring(0, oldSelectionStart) + button.Content.ToString()+ CompanySearchBox.Text.Substring(oldSelectionStart);
                CompanySearchBox.Select(CompanySearchBox.Text.Length, 0);
                Clear_ink();
            }
        }
        private void Clear_ink()
        {
            inkCanvas.Strokes.Clear();
            select0.Content = null;
            select1.Content = null;
            select2.Content = null;
            select3.Content = null;
            select4.Content = null;
            select5.Content = null;
            select6.Content = null;
            select7.Content = null;
            select8.Content = null;
            select9.Content = null;
        }
        private void Fun_Click(object sender, RoutedEventArgs e)
        {
            User32dll.KeyHelper.HotKey(new List<byte>() { User32dll.KeyHelper.KeyCode.WinL, User32dll.KeyHelper.KeyCode.SPACE });
        }

        private void Clear_Click(object sender, RoutedEventArgs e)
        {
            inkCanvas.Strokes.Clear();
            select0.Content = null;
            select1.Content = null;
            select2.Content = null;
            select3.Content = null;
            select4.Content = null;
            select5.Content = null;
            select6.Content = null;
            select7.Content = null;
            select8.Content = null;
            select9.Content = null;
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            User32dll.KeyHelper.OnKeyPress(User32dll.KeyHelper.KeyCode.BACK);
        }

        private void InkCanvas_StrokeCollected(object sender, InkCanvasStrokeCollectedEventArgs e)
        {
            if (timer != null)
            {
                timer.Dispose();
                timer = null;
            }
            timer = new Timer(_ => Dispatcher.BeginInvoke(new Action(() => HandWriting_Recognize())), null, TimeSpan.FromMilliseconds(200), Timeout.InfiniteTimeSpan);
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
                            Dispatcher.BeginInvoke(new Action(() => Clear_ink()));
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
                                Dispatcher.BeginInvoke(new Action(() => {
                                    select0.Content = selections.Count > 1 ? selections[0].ToString() : null;
                                    select1.Content = selections.Count > 2 ? selections[1].ToString() : null;
                                    select2.Content = selections.Count > 3 ? selections[2].ToString() : null;
                                    select3.Content = selections.Count > 4 ? selections[3].ToString() : null;
                                    select4.Content = selections.Count > 5 ? selections[4].ToString() : null;
                                    select5.Content = selections.Count > 6 ? selections[5].ToString() : null;
                                    select6.Content = selections.Count > 7 ? selections[6].ToString() : null;
                                    select7.Content = selections.Count > 8 ? selections[7].ToString() : null;
                                    select8.Content = selections.Count > 9 ? selections[8].ToString() : null;
                                    select9.Content = selections.Count > 10 ? selections[9].ToString() : null;
                                }));
                            }
                        }
                        else
                        {
                            Dispatcher.BeginInvoke(new Action(() => Clear_ink()));
                        }
                    }
                }).Start();
            }
        }
        private void POP_Button(object sender, RoutedEventArgs e)
        {
            Pop.Visibility = Visibility.Hidden;
        }

        private void KeyBoard_Click(object sender, RoutedEventArgs e)
        {
            //Process.Start("SystemKeyBoard.exe");
            User32dll.KeyHelper.HotKey(new List<byte>() { User32dll.KeyHelper.KeyCode.WinL, User32dll.KeyHelper.KeyCode.CTRL, User32dll.KeyHelper.KeyCode.O });
        }
    }
}
