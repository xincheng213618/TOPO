﻿using BaseDLL;
using BaseUtil;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing.Printing;
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

namespace RECSuzhou
{
    /// <summary>
    /// OwnerShipPages.xaml 的交互逻辑
    /// </summary>
    public partial class OwnerShipPages : Page
    {
        public OwnerShipPages()
        {
            idcardData.IDCardNo = Global.IDCardInfo.IDCardNo;
            idcardData.Name = Global.IDCardInfo.Name;
            InitializeComponent();
        }
        private IDCardData idcardData;
        string FileName;
        public OwnerShipPages(IDCardData idcardData)
        {
            this.idcardData = idcardData;
            Global.IDCardInfo.Name = idcardData.Name;
            Global.IDCardInfo.IDCardNo = idcardData.IDCardNo;
            InitializeComponent();
        }

        private void Page_Initialized(object sender, EventArgs e)
        {
            //AcrobatHelper.pdfControl.BeginInit();
            //formsHost.Child = AcrobatHelper.pdfControl;
            //AcrobatHelper.pdfControl.EndInit();
            TotalLabel.Content = idcardData.Name + TotalLabel.Content;
            Countdown_timer();
            PopLabel.Content = "正在查询中";
            PopBorder.Visibility = Visibility.Visible;
            Thread thread = new Thread(() => OwnerShip())
            {
                IsBackground = true
            };
            thread.Start();

        }
        private void OwnerShip()
        {
            string response = Http.OwnerShip(idcardData.Name.Trim(), idcardData.IDCardNo.Trim());
            Dispatcher.BeginInvoke(new Action(() => Parse(response)));
        }

        private int OwnerShipNum = 0;
        private ObservableCollection<HouseItem> HouseItem = new ObservableCollection<HouseItem>();
        private void Parse(string response)
        {
            if (response != null)
            {
                PopBorder.Visibility = Visibility.Hidden;
                try
                {
                    OwnerShipListView.ItemsSource = HouseItem;
                    JObject jsons = (JObject)JsonConvert.DeserializeObject(response);
                    if (jsons["code"].ToString() == "0")
                    {
                        JArray houselist = (JArray)jsons["houselist"];
                        if (houselist.Count > 0)
                        {
                            foreach (JObject house in houselist)
                            {
                                HouseItem item = new HouseItem();
                                item.ListNo = ++OwnerShipNum;
                                item.Location = (string)house.GetValue("zuoluo");
                                item.HouseID = (string)house.GetValue("bianhao");
                                item.Name = (string)house.GetValue("name");
                                item.FilePath = Environment.CurrentDirectory + "\\Temp\\" + (string)house.GetValue("filename");
                                string reportcontent = (string)house.GetValue("report");
                                item.Visible = reportcontent != null ? "Visible" : "Hidden";
                                if (reportcontent != null)
                                {
                                    Covert.Base64ToFile(reportcontent, item.FilePath);
                                }
                                item.FilePage = (string)house.GetValue("pageNumber");
                                HouseItem.Add(item);
                            }
                        }
                        else
                        {
                            OwnerShipMsg.Visibility = Visibility.Visible;
                        }
                    }
                    else
                    {
                        Content = new HomePage(jsons["Message"].ToString());
                        Pages();
                    }
                }
                catch
                {
                    Content = new HomePage("该接口解析错误");
                    Pages();

                }
            }
            else
            {
                Content = new HomePage("该接口连接错误");
                Pages();
            }


        }

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
                if (--Time.Countdown <= 0)
                {
                    Content = new HomePage();
                    Pages();
                }
            });
        }
        private void SCManipulationBoundaryFeedback(object sender, ManipulationBoundaryFeedbackEventArgs e)
        {
            e.Handled = true;
        }

        //页面转跳
        private void Pages()
        {
            pageTimer.IsEnabled = false;
            Dispatcher.BeginInvoke(new Action(() => (Application.Current.MainWindow as MainWindow).frame.Navigate(Content)));
        }
        private void OwnerShipListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (OwnerShipListView.SelectedIndex > -1) 
            {
                if (HouseItem.ElementAt(OwnerShipListView.SelectedIndex).Visible == "Visible")
                {
                    Http.AddAction(idcardData.Name, idcardData.IDCardNo, "dayinquanshu");

                    FileName = HouseItem.ElementAt(OwnerShipListView.SelectedIndex).FilePath.ToString();
                    Global.IDCardInfo.Name = idcardData.Name;
                    Global.IDCardInfo.IDCardNo = idcardData.IDCardNo;

                    WaitShow.Visibility = Visibility.Visible;
                    PopTips.Text = "正在打印";
                    pageTimer.IsEnabled = false;
                    Time.ButtonClor = "#60d0ff";
                    //AcrobatHelper.pdfControl.LoadFile(FileName);
                    //AcrobatHelper.pdfControl.printAll();

                   // timer = new Timer(_ => Dispatcher.BeginInvoke(new Action(async () => await TimeRunAsync(1))), null, 0, 500);
                    Content = new Pdfshow(FileName);
                    Pages();

                }
                else
                {
                    MessageBox.Show("暂无此报告的PDF,请至房屋所属区域不动产档案中心窗口打印");
                }
            }

        }
       
        Timer timer; //打印用
        int Ostatue = 0;
        bool PrintSucess = false;
        int PrintTimeNo = 0;//防止出现什么意外导致迟迟卡死在此页面，一定次数之后直接判定超时

        static PrintDocument print = new PrintDocument();

        private async Task TimeRunAsync(int PageAllNum)
        {
            int Nstatue = PrintStatus.PrinterStatue(print.PrinterSettings.PrinterName);
            PrintSucess = Ostatue != 0 && Nstatue == 0;
            Ostatue = Ostatue == Nstatue ? Ostatue : Nstatue;
            PopTips.Text = PrintStatus.PrinterStatusCodes.ContainsKey(Nstatue) ? PrintStatus.PrinterStatusCodes[Nstatue].ToString() : $"{Nstatue}";
            PrintTimeNo += 1;

            if (PrintSucess || (PrintTimeNo < 20 + PageAllNum))
            {
                timer.Dispose();
                PopTips.Text = "PDF已经发送到打印机";
                await Task.Delay(2 * 1000);
                PopTips.Text = "打印机正在打印";
                await Task.Delay(PageAllNum * 1000);
                PopTips.Text = "打印完成";
                await Task.Delay(3 * 1000);
                PopTips.Text = "";                
                Ostatue = 0;
                WaitShow.Visibility = Visibility.Hidden;
            }
            else
            {
                timer.Dispose();
                Ostatue = 0;

                Content = new HomePage("打印失败，请联系维修人员");
                Pages();
            }
        }
            private void Home_Click(object sender, RoutedEventArgs e)
        {
            Content = new HomePage();
            Pages();
        }
    }
}