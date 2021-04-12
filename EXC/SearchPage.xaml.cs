using Microsoft.Ink;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Threading;
using BaseDLL;
using BaseUtil;

namespace EXC
{

    //Designed By Mr.Xin 2020.5.25 5:38  修正
    public partial class SearchPage : Page
    {
        private Timer timer = null;
        //查询
        private bool querying = false;
        private int pageNo = 1;
        private string SearchContent = null;
        IDCardData iDCardData = new IDCardData();

        public SearchPage()
        {
            InitializeComponent();
        }

        public SearchPage(IDCardData iDCardData)
        {
            this.iDCardData = iDCardData;
            InitializeComponent();
        }

        private void Page_Initialized(object sender, EventArgs e)
        {
            //if (!Function.Info.OSVersion().Contains("10"))
            //    Process.Start("SystemKeyBoard.exe");

            Countdown_timer();
            switch (Global.PageType)
            {
                case "TOPOPunish":
                case "TOPOAllow":
                case "TopoSearch":
                    SearchTitleLabel.Content = "企业信息列表搜索";
                    break;
                case "QingDaoReport":
                    SearchTitleLabel.Content = "请输入企业名称或者统一社会信用代码";
                    break;
                case "XinHuaTaxA":
                    SearchTitleLabel.Content = "纳税信用A级企业列表搜索";
                    break;
                case "XinHuaTaxV":
                    SearchTitleLabel.Content = "重大税收违法案件人查询";
                    break;
                case "LostCredit":
                    SearchTitleLabel.Content = "失信被执行人查询";
                    break;
                case "SZHQMoney":
                    SearchTitleLabel.Content = "请输入要查询资金的编号";
                    break;
                case "SZHQProgress":
                    SearchTitleLabel.Content = "请输入要查询进程的编号";
                    break;

                case "ReportWeiHai":
                    SearchTitleLabel.Content = "请输入企业名称或者统一社会信用代码";
                    break;
                default:
                    break;
            }
        }

        //企业查询页面
        private void Query_Click(object sender, RoutedEventArgs e)
        {
            Search();
        }
        //回车搜索
        private void KeyEnter(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
                Search();
        }

        private void Search()
        {
            SearchContent = CompanySearchBox.Text;
            if (SearchContent.Length == 0)
            {
                PopAlert("请输入需要查询的内容", 1);
                return;
            }
            if (querying) return;
            querying = true;
            WaitShow.Visibility = Visibility.Visible;
            hintLabel.Content = "正在查询请稍候";
            Time.Countdown = 59;

            pageNo = 1;
            Thread worker = new Thread(() => SearchRequests())
            {
                IsBackground = true
            };
            worker.Start();
        }

        private void SearchRequests()
        {
            string response = null;
            switch (Global.PageType)
            {
                case "TopoSearch":
                    response = Http.TOPO.CompanySearch(SearchContent, pageNo);
                    Dispatcher.BeginInvoke(new Action(() => GetCompayQueryListCompleted(response)));

                    break;
                case "QingDaoReport":
                    string filePath = "Temp\\" + SearchContent + ".pdf";
                    if (File.Exists(filePath))
                    {
                        Dispatcher.BeginInvoke(new Action(() => Content = new Pdfshow(filePath)));
                        Dispatcher.BeginInvoke(new Action(() => Pages()));
                        return;
                    }
                    if (Check.CheckIsHasZH(SearchContent))
                    {
                        response = Webservice.QingDao.GetReport(MC: SearchContent);
                    }
                    else
                    {
                        response = Webservice.QingDao.GetReport(DM: SearchContent);
                    }
                    Dispatcher.BeginInvoke(new Action(() => GetCompayQueryListCompleted(response)));
                    break;
                default:
                    break;
            }

        }



        //返回
        private void Return_Click(object sender, RoutedEventArgs e)
        {
            some();
        }

        private void some()
        {
            Time.Countdown = 59;
            CompanyListNum = 0;
            CompayQueryListItem.Clear();
            SearchGrid.Visibility = Visibility.Hidden;
            queryInput.Visibility = Visibility.Visible;
        }
        private void Home_Click(object sender, RoutedEventArgs e)
        {
            Content = new HomePage();
            Pages();
        }


        //下拉框
        private void TextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            border1.Visibility = Visibility.Hidden;
            inkPanel.Visibility = Visibility.Hidden;
        }
        private void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            border1.Visibility = Visibility.Hidden;
            inkPanel.Visibility = Visibility.Visible;
            inkCanvas.Strokes.Clear();
            Clear_ink();
        }

        //清除手写板
        private void Clear_Click(object sender, RoutedEventArgs e)
        {
            Clear_ink();
        }

        ///手写板清除
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


        //修正不直接对页面负责对窗口负责
        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            User32dll.KeyHelper.OnKeyPress(User32dll.KeyHelper.KeyCode.BACK);
        }

        private void Recognizer_Select(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;

            if (button.Content != null)
            {
                CompanySearchBox.Text = CompanySearchBox.Text.ToString() + button.Content.ToString();
                TextBox focus = FocusManager.GetFocusedElement(this) as TextBox;
                focus.Select(CompanySearchBox.Text.Length, 0);
                Clear_ink();
            }
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
        private void Fun_Click(object sender, RoutedEventArgs e)
        {
            User32dll.KeyHelper.HotKey(new List<byte>() { User32dll.KeyHelper.KeyCode.WinL, User32dll.KeyHelper.KeyCode.SPACE });
        }


        #region 数据解析
        //数据展示
        private int CompanyListNum = 0;
        private readonly ObservableCollection<CompayQueryListItem> CompayQueryListItem = new ObservableCollection<CompayQueryListItem>();
        private void GetCompayQueryListCompleted(string response)
        {
            WaitShow.Visibility = Visibility.Hidden;
            querying = false;

            SearchGrid.Visibility = Visibility.Visible;
            listView.Visibility = Visibility.Visible;
            listView.ItemsSource = CompayQueryListItem;

            try
            {
                JObject compayList = (JObject)JsonConvert.DeserializeObject(response);
                string resultCode = compayList["resultCode"].ToString();

                if (resultCode.Equals("0"))
                {
                    JObject data = (JObject)compayList["data"];
                    JObject companyName = (JObject)data["companyName"];

                    totalLabel.Content = $"共{companyName["total"] ?? ""}条数据";

                    if (!companyName.Equals(""))
                    {
                        JArray returnDataArray = (JArray)companyName["list"];

                        if (returnDataArray != null && !returnDataArray.Equals("") && returnDataArray.Count != 0)
                        {
                            foreach (JObject returnData in returnDataArray)
                            {
                                CompayQueryListItem item = new CompayQueryListItem();
                                CompanyListNum += 1;
                                item.ListNo = CompanyListNum;

                                item.CompanyID = (string)returnData.GetValue("id");
                                item.CompanyName = (string)returnData.GetValue("companyname");
                                item.USCI = (string)returnData.GetValue("uniscid");

                                try { item.Establishment = "成立时间：" + Convert.ToDateTime((string)returnData.GetValue("esdate")).ToString("yyyy-MM-dd"); }
                                catch { item.Establishment = "成立时间：" + (string)returnData.GetValue("esdate"); }
                                item.BusinessStatus = (string)returnData.GetValue("entstatus");
                                item.Phone = (string)returnData.GetValue("phone");
                                item.Repname = "法人代表：" + (string)returnData.GetValue("repname");
                                item.RegisteredCapital = "注册资本：" + (string)returnData.GetValue("regmoney");
                                item.Industry = "所属行业：" + (string)returnData.GetValue("industry");

                                CompayQueryListItem.Add(item);
                            }

                            NextPageBorder.Visibility = Visibility.Visible;

                            NowTotalLabel.Content = "已经加载" + CompayQueryListItem.Count + "条数据";
                        }
                        else
                        {
                            if (pageNo == 1)
                            {
                                PopAlert("未搜索到相关信息", 2);
                            }
                            NextPageBorder.Visibility = Visibility.Hidden;
                        }
                    }
                }
                else
                {
                    if (resultCode.Equals("50102"))
                    {
                        PopAlert("查询列表查询失败", 1);
                    }
                    else if (resultCode.Equals("50103"))
                    {
                        PopAlert("需要输入更多搜索信息", 1);
                    }
                    else if ("-2".Equals(resultCode))
                    {
                        PopAlert("暂无访问权限", 1);
                    }
                    SearchGrid.Visibility = Visibility.Hidden;
                    FocusManager.SetFocusedElement(this, CompanySearchBox);
                    PopAlert("未搜索到关键字", 1);
                }
            }
            catch
            {
                Content = new HomePage("数据解析错误，联系管理人员");
                Pages();
            }
        }


        private void QingDaoPharse(string response)
        {
            if (response == "0" || response == "1")
            {
                Content = new HomePage(response == "0" ? "未查询到对应的公司信息" : "授权码错误");
            }
            else
            {
                string filePath = "Temp\\" + SearchContent + ".pdf";
                Covert.Base64ToFile(response, filePath);
                Content = new Pdfshow(filePath);
            }
            Pages();
        }


        private void SZHQParse(string response)
        {
            try
            {
                JObject json = (JObject)JsonConvert.DeserializeObject(response);
                if (json != null && !json.Equals(""))
                {
                    string code = json["code"].ToString();
                    string msg = json["Message"].ToString();
                    if (code.Equals("0"))
                    {
                        string result = json["result"].ToString();
                        InfoLabel.Text = result;
                        if (result.Length == 0)
                        {
                            InfoLabel.Text = "未查询到此数据";
                        }
                        InfoBorder.Visibility = Visibility.Visible;
                    }
                    else
                    {
                        PopAlert(msg, 2);
                    }
                }
                else
                {
                    PopAlert("未搜索到相关信息", 2);
                }
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
                Content = new HomePage("该接口解析错误");
                Pages();
            }
        }

        private void WeiHaiParse(JObject response)
        {
            WaitShow.Visibility = Visibility.Hidden;
            querying = false;

            SearchGrid.Visibility = Visibility.Visible;
            WeiHaiListView.Visibility = Visibility.Visible;
            WeiHaiListView.ItemsSource = CompayQueryListItem;

            if ((string)response.GetValue("code") == "0")
            {
                JObject json = (JObject)JsonConvert.DeserializeObject((string)response.GetValue("data"));

                JArray jArray = (JArray)json["result"];
                totalLabel.Content = $"共{json["lastPageNumber"] ?? ""}条数据";

                if (jArray != null && !jArray.Equals("") && jArray.Count != 0)
                {
                    foreach (JObject returnData in jArray)
                    {
                        CompayQueryListItem item = new CompayQueryListItem();
                        CompanyListNum += 1;
                        item.ListNo = CompanyListNum;

                        item.CompanyID = (string)returnData.GetValue("xybsm");
                        item.CompanyName = (string)returnData.GetValue("qymc");
                        item.USCI = (string)returnData.GetValue("tyshxydm");
                        item.Industry = (string)returnData.GetValue("qylb");
                        CompayQueryListItem.Add(item);
                    }
                    NowTotalLabel.Content = "已经加载" + CompayQueryListItem.Count + "条数据";
                }
                else
                {
                    if (pageNo == 1)
                        PopAlert("未搜索到相应信息", 4);

                    NextPageBorder.Visibility = Visibility.Hidden;
                }
            }
            else
            {
                Content = new HomePage((string)response.GetValue("msg"));
                Pages();
            }
        }


        #endregion

        //提示方法


        private async void PopAlert(string Msg, int time)
        {
            SearchGrid.Visibility = Visibility.Hidden;
            PopTips.Text = Msg;
            POP.Visibility = Visibility.Visible;
            await Task.Delay(TimeSpan.FromSeconds(time));

            POP.Visibility = Visibility.Hidden;
            some();

        }


        //倒计时模块
        private DispatcherTimer pageTimer = null;
        public TimeCount Time = new TimeCount();


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
                FocusManager.SetFocusedElement(this, CompanySearchBox);

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

        //跳转到详情页面（新）
        private void listView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ListView listView = sender as ListView;
            try
            {
                SearchContent = CompayQueryListItem.ElementAt(listView.SelectedIndex).CompanyName.ToString();


                switch (Global.PageType)
                {
                    case "TopoSearch":
                        Content = new CompayQueryDetailPage(SearchContent);
                        Pages();
                        break;
                    case "QingDaoReport":
                        SearchTitleLabel.Content = "请输入企业名称或者统一社会信用代码";
                        break;

                    default:
                        break;
                }
            }
            catch
            {
                listView.SelectedIndex = 0;
            }
        }

        private void NextPage_Click(object sender, RoutedEventArgs e)
        {
            Time.Countdown = 59;
            pageNo += 1;
            Thread worker = new Thread(() => SearchRequests())
            {
                IsBackground = true
            };
            worker.Start();
        }
        //有用的东西
        private void SCManipulationBoundaryFeedback(object sender, ManipulationBoundaryFeedbackEventArgs e)
        {
            e.Handled = true;
        }

        private void TextChanged(object sender, TextChangedEventArgs e)
        {
            Time.Countdown = 30;
        }

        private void DragDown_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;

            CompanySearchBox.Text = button.Tag.ToString();
            border1.Visibility = Visibility.Hidden;
        }

        private void POP_Button(object sender, RoutedEventArgs e)
        {
            POP.Visibility = Visibility.Visible;
        }
    }
}
