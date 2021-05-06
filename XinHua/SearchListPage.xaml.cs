using BaseInk;
using BaseUtil;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace XinHua
{
    /// <summary>
    /// SearchListPage.xaml 的交互逻辑
    /// </summary>
    public partial class SearchListPage : Page
    {
        private string SearchContent;
        public SearchListPage(string SearchContent)
        {
            this.SearchContent = SearchContent;
            InitializeComponent();
        }
        Thread thread;
        private void Page_Initialized(object sender, EventArgs e)
        {
            //InkPut.delegates();
            //App.InkWindows.Hide();
            WaitShow.Visibility = Visibility.Visible;
            Countdown_timer();
            thread = new Thread(() => SearchRequests())
            {
                IsBackground = true
            };
            thread.Start();
        }

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

        private int pageNo = 1;
        private void SearchRequests()
        {
            string response = null;
            string companyName = SearchContent;
            switch (Global.Related.PageType)
            {
                case "QiYeXinXi":
                    response = Http.XinHuaSearch(companyName, pageNo);
                    Dispatcher.BeginInvoke(new Action(() => SearchPharse(response)));
                    break;
                case "NaShuiXinYongA":
                    response = Http.XinHuaTaxA(companyName, pageNo);
                    Dispatcher.BeginInvoke(new Action(() => XinHuaTaxAPharse(response)));
                    break;
                case "ShuiShouWeiFa":
                    response = Http.XinHuaTaxV(companyName, pageNo);
                    Dispatcher.BeginInvoke(new Action(() => XinHuaTaxVPharse(response)));
                    break;
                case "ShiXinRen":
                    response = Http.XinHuaDishonestyPeople(companyName, pageNo);
                    Dispatcher.BeginInvoke(new Action(() => XinHuaTaxAPharse(response)));
                    break;
                default:
                    break;
            }
        }
        private int CompanyListNum = 0;
        private readonly ObservableCollection<CompayQueryListItem> CompayQueryListItem = new ObservableCollection<CompayQueryListItem>();

        private void SearchPharse(string response)
        {
            WaitShow.Visibility = Visibility.Hidden;
            ListGrid.Visibility = Visibility.Visible;
            list.Visibility = Visibility.Visible;
            list1.Visibility = Visibility.Hidden;
            listView.ItemsSource = CompayQueryListItem;
            try
            {
                JObject compayList = (JObject)JsonConvert.DeserializeObject(response);
                string resultCode = compayList["error_code"].ToString();
                string resultMsg = compayList["reason"].ToString();

                listView.ItemsSource = CompayQueryListItem;
                if (CompayQueryListItem.Count > 0)
                    listView.ScrollIntoView(listView.Items[CompayQueryListItem.Count - 1]);

                if (resultCode.Equals("200"))
                {
                    JObject data = (JObject)compayList["result"];

                    totalLabel.Content = $"共{data["total"] ?? ""}条数据";
                    totalLabel.Visibility = Visibility.Visible;

                    if (!data.Equals(""))
                    {
                        JArray returnDataArray = (JArray)data["items"];

                        if (returnDataArray != null && !returnDataArray.Equals("") && returnDataArray.Count != 0)
                        {
                            foreach (JObject returnData in returnDataArray)
                            {
                                CompayQueryListItem item = new CompayQueryListItem();
                                CompanyListNum += 1;
                                item.ListNo = CompanyListNum;

                                item.CompanyID = (string)returnData.GetValue("keyNo");
                                item.CompanyName = (string)returnData.GetValue("name");
                                item.USCI = (string)returnData.GetValue("creditCode");

                                try { item.Establishment = "成立时间：" + Convert.ToDateTime((string)returnData.GetValue("termStart")).ToString("yyyy-MM-dd"); }
                                catch { item.Establishment = "成立时间：" + (string)returnData.GetValue("termStart"); }
                                item.BusinessStatus = (string)returnData.GetValue("status");
                                item.Phone = (string)returnData.GetValue("phone");
                                item.Repname = "法人代表：" + (string)returnData.GetValue("operName");

                                item.RegisteredCapital = "注册资本：" + (string)returnData.GetValue("registCapi");
                                item.Industry = "所属行业：" + (string)returnData.GetValue("subIndustry");
                                CompayQueryListItem.Add(item);
                            }

                            NextPageBorder.Visibility = CompayQueryListItem.Count < pageNo * 10 ? Visibility.Hidden : Visibility.Visible;
                            NowTotalLabel.Content = "已经加载" + CompayQueryListItem.Count + "条数据";
                            NowTotalLabel.Visibility = Visibility.Visible;
                        }
                        else
                        {
                            if (pageNo == 1)
                            {
                                Content = new SearchPage("未搜索到相关信息");
                                Pages();
                            }
                            NextPageBorder.Visibility = Visibility.Hidden;
                        }
                    }
                    else
                    {
                        if (pageNo == 1)
                        {
                            Content = new SearchPage("未搜索到相关信息");
                            Pages();
                        }
                        NextPageBorder.Visibility = Visibility.Hidden;
                    }

                }
                else
                {
                    if (pageNo == 1)
                    {
                        Content = new SearchPage(resultMsg);
                        Pages();
                    }
                    NextPageBorder.Visibility = Visibility.Hidden;
                }
            }
            catch
            {
                Content = new HomePage("数据解析错误，联系管理人员");
                Pages();
            }
        }


        private void XinHuaTaxAPharse(string response)
        {
            WaitShow.Visibility = Visibility.Hidden;
            list.Visibility = Visibility.Hidden;
            list1.Visibility = Visibility.Visible;
            listView1.ItemsSource = CompayQueryListItem;
            if (CompayQueryListItem.Count > 0)
                listView1.ScrollIntoView(listView.Items[CompayQueryListItem.Count - 1]);
            try
            {
                JObject jObject = (JObject)JsonConvert.DeserializeObject(response);

                string stateCode = jObject["stateCode"].ToString();
                if (stateCode.Equals("1"))
                {
                    JObject data = (JObject)jObject["data"];

                    totalLabel.Content = $"共{data["totalRecords"] ?? ""}条数据";
                    totalLabel.Visibility = Visibility.Visible;
                    JArray returnDataArray = (JArray)data["list"];
                    if (returnDataArray != null && !returnDataArray.Equals("") && returnDataArray.Count != 0)
                    {
                        foreach (JObject temp in returnDataArray)
                        {
                            CompayQueryListItem item = new CompayQueryListItem();
                            CompanyListNum += 1;
                            item.ListNo = CompanyListNum;

                            item.CompanyID = (string)temp.GetValue("COMPANY_ID");
                            item.USCI = (string)temp.GetValue("DISCERN_NUMBER");
                            item.CompanyName = (string)temp.GetValue("NAME");
                            item.TaxANum = (string)temp.GetValue("NUMS");

                            CompayQueryListItem.Add(item);
                        }
                        NextPageBorder.Visibility = CompayQueryListItem.Count < pageNo * 10 ? Visibility.Hidden : Visibility.Visible;

                        NowTotalLabel.Content = "已经加载" + CompayQueryListItem.Count + "条数据";
                        listView1.ScrollIntoView(CompayQueryListItem.Count - 1);

                        NowTotalLabel.Visibility = Visibility.Visible;

                    }
                    else
                    {
                        if (pageNo == 1)
                        {
                            Content = new SearchPage("未搜索到相关信息");
                            Pages();

                        }
                        NextPageBorder.Visibility = Visibility.Hidden;
                    }
                }
                else
                {
                    Content = new SearchPage("获取信息失败");
                    Pages();
                }
            }
            catch
            {
                Content = new HomePage("数据解析错误，联系管理人员");
                Pages();
            }
        }

        private void XinHuaTaxVPharse(string response)
        {
            WaitShow.Visibility = Visibility.Hidden;
            ListGrid.Visibility = Visibility.Visible;
            list1.Visibility = Visibility;
            listView1.ItemsSource = CompayQueryListItem;
            try
            {
                JObject jObject = (JObject)JsonConvert.DeserializeObject(response);

                string stateCode = jObject["stateCode"].ToString();
                if (stateCode.Equals("1"))
                {
                    JObject data = (JObject)jObject["data"];

                    totalLabel.Content = $"共{data["totalRecords"] ?? ""}条数据";
                    totalLabel.Visibility = Visibility.Visible;

                    JArray returnDataArray = (JArray)data["list"];
                    if (returnDataArray != null && !returnDataArray.Equals("") && returnDataArray.Count != 0)
                    {
                        foreach (JObject temp in returnDataArray)
                        {
                            CompayQueryListItem item = new CompayQueryListItem();
                            CompanyListNum += 1;
                            item.ListNo = CompanyListNum;

                            item.CompanyID = (string)temp.GetValue("COMPANY_ID");
                            item.USCI = (string)temp.GetValue("ORGANIZATION_CODE");
                            item.CompanyName = (string)temp.GetValue("COMPANY_NAME");
                            item.TaxANum = (string)temp.GetValue("NUMS");
                            CompayQueryListItem.Add(item);
                        }
                        NextPageBorder.Visibility = CompayQueryListItem.Count < pageNo * 10 ? Visibility.Hidden : Visibility.Visible;

                        NowTotalLabel.Content = "已经加载" + CompayQueryListItem.Count + "条数据";
                        NowTotalLabel.Visibility = Visibility.Visible;

                    }
                    else
                    {
                        if (pageNo == 1)
                        {
                            Content = new SearchPage("未搜索到相关信息");
                            Pages();
                        }
                        NextPageBorder.Visibility = Visibility.Hidden;
                    }

                }
                else
                {
                    Content = new SearchPage("获取到信息失败请重试");
                    Pages();
                }
            }
            catch
            {
                Content = new HomePage("数据解析错误，联系管理人员");
                Pages();
            }
        }

        //页面转换
        private void Pages()
        {
            pageTimer.IsEnabled = false;
            Dispatcher.BeginInvoke(new Action(() => (Application.Current.MainWindow as MainWindow).frame.Navigate(Content)));
        }

        private void NextPage_Click(object sender, RoutedEventArgs e)
        {
            pageNo += 1;
            thread = new Thread(() => SearchRequests())
            {
                IsBackground = true
            };
            thread.Start();
        }
        private void SCManipulationBoundaryFeedback(object sender, ManipulationBoundaryFeedbackEventArgs e)
        {
            e.Handled = true;
        }

        private void listView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ListView listView = sender as ListView;
            if (listView.SelectedIndex > -1)
            {
                string companyName = CompayQueryListItem.ElementAt(listView.SelectedIndex).CompanyName.ToString();
                string CompanyID = CompayQueryListItem.ElementAt(listView.SelectedIndex).CompanyID.ToString();
                switch (Global.Related.PageType)
                {
                    case "QiYeXinXi":
                        Content = new XinHuaCompanyDetailPage(CompanyID);
                        Pages();
                        break;
                    case "NaShuiXinYongA":
                        string Uniscid = CompayQueryListItem.ElementAt(listView.SelectedIndex).USCI.ToString();
                        Content = new InfoShowPage(CompanyName: companyName, Uniscid: Uniscid);
                        Pages();
                        break;
                    case "ShuiShouWeiFa":
                    case "ShiXinRen":
                        Content = new InfoShowPage(CompanyName: companyName, CompanyID: CompanyID);
                        Pages();
                        break;
                    default:
                        break;
                }
            }
 
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            switch (button.Tag)
            {
                case "Home":
                    Content = new HomePage();
                    Pages();
                    break;
                case "Return":
                    Content = new SearchPage();
                    Pages();
                    break;
                default:
                    break;
            }
        }
    }
}
