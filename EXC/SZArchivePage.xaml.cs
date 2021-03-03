using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Threading;
using Resources;
using BaseDLL;


namespace EXC
{
    /// <summary>
    /// SZArchivePage.xaml 的交互逻辑
    /// </summary>
    public partial class SZArchivePage : Page
    {
        private IDCardData idcardData;
        private string SZArchiveListPageNo = "1";
        //定义一页有多少条数据
        private string PageSize = "8";
        public SZArchivePage()
        {
            idcardData.Name = IDCardInfo.Name;
            idcardData.IDCardNo = IDCardInfo.IDCardNo;
            InitializeComponent();
        }

        public SZArchivePage(IDCardData idcardData)
        {
            this.idcardData = idcardData;
            InitializeComponent();
        }

        private void Page_Initialized(object sender, EventArgs e)
        {
            DataContext = Time;
            Countdown_timer();
            PopLabel.Content = "正在查询中";
            PopBorder.Visibility = Visibility.Visible;
            Thread worker1 = new Thread(() => ArchiveList());
            worker1.IsBackground = true;
            worker1.Start();
        }

        private void Msg(string mes)
        {
            Content = new HomePage(mes);
            Pages();
        }
        private void ArchiveList()
        {
            string response = Http.RealEstate.SZArchiveList(idcardData.Name, idcardData.IDCardNo, SZArchiveListPageNo, PageSize);
            if (response == null)
            {
                Dispatcher.BeginInvoke(new Action(() => Msg("该接口连接错误")));
                return;
            }
            Dispatcher.BeginInvoke(new Action(() => ParseList(response)));
        }


        private int SZArchiveListNum = 0;
        private ObservableCollection<SZArchiveListItem> SZArchiveListItem = new ObservableCollection<SZArchiveListItem>();

        private void ParseList(string response)
        {
            PopBorder.Visibility = Visibility.Hidden;
            try
            {
                SZArchiveListView.ItemsSource = SZArchiveListItem;
                JObject jsons = (JObject)JsonConvert.DeserializeObject(response);
                string resultCode = jsons["code"].ToString();
                SZArchiveListView.Visibility = Visibility.Visible;
                if (resultCode == "1")
                {
                    Content = new HomePage("每日可查询次数上限");
                    Pages();
                    return;
                }
                if (resultCode == "3")
                {
                    Content = new HomePage("请至档案窗口查询");
                    Pages();
                    return;
                }
                if (resultCode == "2")
                {
                    SZArchiveListMsg.Visibility = Visibility.Visible;
                    return;
                }
                if (resultCode != "0")
                {
                    Content = new HomePage("该接口查询失败:" + response);
                    Pages();
                    return;
                }
                JArray Lists = (JArray)jsons["result"];
                if (Lists.Count > 0)
                {
                    foreach (JObject List in Lists)
                    {
                        SZArchiveListItem item = new SZArchiveListItem();
                        item.ListNo = ++SZArchiveListNum;
                        item.name = List["name"].ToString();
                        item.archivecode = List["archivecode"].ToString();
                        item.beLocated = List["zuoluo"].ToString();
                        item.uniquehouseno = List["zuoluo"].ToString();
                        SZArchiveListItem.Add(item);
                    }
                }
                else
                {
                    SZArchiveListMsg.Visibility = Visibility.Visible;
                }
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
                Dispatcher.BeginInvoke(new Action(() => Msg("该接口解析错误")));
                return;
            }
        }
        private void SZArchiveListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                string Archivecode = SZArchiveListItem.ElementAt(SZArchiveListView.SelectedIndex).archivecode.ToString();
                SZArchiveMenuBorder.Visibility = Visibility.Visible;
                Time.Countdown = 60;
                PopLabel.Content = "正在查询中";
                PopBorder.Visibility = Visibility.Visible;

                string filePath = "Cache\\" + Archivecode + ".pdf";
                if (File.Exists(filePath))
                {
                    Content = new Pdfshow(filePath);
                    Pages();
                    return;
                }

                Log.Write(Archivecode);
                Thread worker2 = new Thread(() => Archive(Archivecode))
                {
                    IsBackground = true
                };
                worker2.Start();
            }
            catch
            {
                SZArchiveListView.SelectedIndex = 0;
            }

        }

        private void Archive(string Archievcode)
        {
            switch (Global.PageType)
            {
                case "SZWZArchivePages":
                    string response = Http.RealEstate.SZArchiveMenu(idcardData.IDCardNo, Archievcode);
                    Dispatcher.BeginInvoke(new Action(() => WZArchiveParse(response)));
                    break;
                case "SZHQArchivePages":
                    response = Http.RealEstate.SZArchive(idcardData.IDCardNo, Archievcode);
                    
                    Dispatcher.BeginInvoke(new Action(() => HQArchiveParse(response, Archievcode)));
                    break;
            }

        }
        public static Dictionary<string, object> dic;
        private int SZArchiveMenuNum = 0;
        private ObservableCollection<SZArchiveMenuItem> SZArchiveMenuItem;

        private void WZArchiveParse(string response)
        {
            SZArchiveMenuBorder.Visibility = Visibility.Visible;
            SZArchiveMenuItem = new ObservableCollection<SZArchiveMenuItem>();
            SZArchiveMenuListView.ItemsSource = SZArchiveMenuItem;
            List<string> MeanList = new List<string>();
            List<string> MeanFileList = new List<string>();
            try
            {
                JObject json = (JObject)JsonConvert.DeserializeObject(response);


                string daid = (string)json.GetValue("daid");
                string slbhid = (string)json.GetValue("slbhid");
                string tabinfo = (string)json.GetValue("tabinfo");
                string dalx = (string)json.GetValue("dalx");
                string imgif = (string)json.GetValue("imgif");
                if (imgif.Contains("1"))
                {

                    JArray imgtextArray = (JArray)json["imgtext"];
                    if (imgtextArray != null && imgtextArray.Count > 0)
                    {
                        foreach (JObject imgtext in imgtextArray)
                        {
                            MeanList.Add((string)imgtext.GetValue("mlm"));
                        }
                        MeanList.Distinct();
                        for(int i=0; i< MeanList.Count; i++)
                        {
                            SZArchiveMenuItem item = new SZArchiveMenuItem();
                            SZArchiveMenuNum += 1;
                            item.ListNo = SZArchiveMenuNum;
                            item.Category = MeanList[i];
                            string PDFFile = $"Temp\\{idcardData.IDCardNo}_{MeanList[i]}.pdf";
                            item.FileName = PDFFile;


                            List<string> fileNameList = new List<string>();


                            foreach (JObject imgtext in imgtextArray)
                            {
                                string mlm = (string)imgtext.GetValue("mlm");
                                if (mlm == MeanList[i])
                                {
                                    string jpgid = (string)imgtext.GetValue("jpg_id");
                                    string jpgsxh = (string)imgtext.GetValue("sxh");
                                    string jpgsxhz = (string)imgtext.GetValue("sxhz");
                                    dic = new Dictionary<string, object>
                                    {
                                        { "daid", daid },
                                        { "slbhid", slbhid },
                                        { "tabinfo", tabinfo },
                                        { "dalx", dalx },
                                        { "jpgid", jpgid },
                                        { "jpgmlm", mlm},
                                        { "jpgsxh", jpgsxh },
                                        { "jpgsxhz", jpgsxhz }
                                    };
                                    string PicName = $"{ DateTime.Now.ToString("yyyyMMddHHmmss", DateTimeFormatInfo.InvariantInfo)}_{jpgid}.jpg";
                                    Http.RealEstate.ShowImage(dic, PicName);

                                    fileNameList.Add(PicName);
                                }
                                PDF.JPGToPDF(fileNameList.ToArray(), PDFFile);
                                Log.Write("图片转pdf成功");
                                PDF.PDFMark(PDFFile, "苏州市不动产登记中心吴中分中心", DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"));
                                Log.Write("正在添加图片水印");
                                for (int j = 0; j < fileNameList.ToArray().Length; j++)
                                {
                                    try{ File.Delete(@fileNameList.ToArray()[j]);  }
                                    catch  {  }
                                }
                                Log.Write("图片缓存删除成功");
                            }
                            MeanFileList.Add(PDFFile);
                            SZArchiveMenuItem.Add(item);
                        }

                    }
                    else
                    {
                        SZArchiveMenuMsg.Visibility = Visibility.Visible;
                    }

                }
                else
                {
                    SZArchiveMenuMsg.Visibility = Visibility.Visible;
                }

            }
            catch 
            {
                Log.Write(response);
                Content = new HomePage("数据解析错误");
                Pages();
            }

        }
        private void HQArchiveParse(string response,string Archievcode)
        {
            try
            {
                JObject json = (JObject)JsonConvert.DeserializeObject(response);
                string code = json["returncode"] == null ? null : json["returncode"].ToString();
                string msg = json["returnmsg"] == null ? null : json["returnmsg"].ToString();
                if ("0".Equals(code))
                {
                    string recordStr = json["result"] == null ? null : json["result"].ToString();
                    if (recordStr != null && !"".Equals(recordStr))
                    {
                        List<string> caseidList = new List<string>();
                        List<string> fileNameList = new List<string>();
                        JArray recordList = (JArray)json["result"];

                        if (recordList.Count > 0)
                        {
                            int i = 0;
                            foreach (JObject record in recordList)
                            {
                                string pageid = (string)record.GetValue("pageid");
                                string caseid = (string)record.GetValue("caseid");
                                string strPersonCode = (string)record.GetValue("strPersonCode");
                                string imagecontent = (string)record.GetValue("imagecontent");

                                string fileName = DateTime.Now.ToString("yyyyMMddHHmmss", DateTimeFormatInfo.InvariantInfo) + i++ + ".jpg";
                                string filePath = "Temp\\" + fileName;

                                XCovert.Base64ToFile(imagecontent, filePath);
                                caseidList.Add(caseid);
                                fileNameList.Add(filePath);
                                
                            }
                            PDF.JPGToPDF(fileNameList.ToArray(), "Temp\\" + Archievcode + ".pdf");
                            Log.Write("图片转pdf成功");
                            PDF.PDFMark("Cache\\" + Archievcode + ".pdf", "苏州市不动产登记中心虎丘分中心", DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"));
                            Log.Write("正在添加图片水印");
                            Http.RealEstate.AddAction(idcardData.Name, idcardData.IDCardNo, "dayinquanshu");
                            for (int j = 0; j < fileNameList.ToArray().Length; j++)
                            {
                                try
                                {
                                    File.Delete(@fileNameList.ToArray()[j]);
                                }
                                catch 
                                {
                                }
                            }
                            Log.Write("图片缓存删除成功");

                        }
                    }     

                }
                Content = new Pdfshow("Cache\\" + Archievcode + ".pdf");
                Pages();

            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
                Content = new HomePage(ex.Message);
                Pages();
            }
        }


        //List 解析
        private void SCManipulationBoundaryFeedback(object sender, ManipulationBoundaryFeedbackEventArgs e)
        {
            e.Handled = true;
        }

        private void SZArchiveMenuListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                string FilePath = SZArchiveMenuItem.ElementAt(SZArchiveMenuListView.SelectedIndex).FileName.ToString();
                if (File.Exists(FilePath))
                {
                    Content = new Pdfshow(FilePath);
                    Pages();
                    return;
                }
                else
                {
                    Content = new HomePage($"无法找到{FilePath}");
                    Pages();
                }

            }
            catch
            {
                SZArchiveMenuListView.SelectedIndex = 0;
            }
        }


        //倒计时模块
        private DispatcherTimer pageTimer = null;
        public Times Time = new Times();


        private void Countdown_timer()
        {

            pageTimer = new DispatcherTimer() { IsEnabled = true, Interval = TimeSpan.FromSeconds(1) };
            pageTimer.Tick += new EventHandler((sender, e) =>
            {
                if (--Time.Countdown <= 0)
                {
                    Content = new HomePage();
                    Pages();
                }
            });
        }


        private void Pages()
        {
            pageTimer.IsEnabled = false;
            Dispatcher.BeginInvoke(new Action(() => (Application.Current.MainWindow as MainWindow).frame.Navigate(Content)));
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            switch ((string)button.Tag)
            {
                case "Return":
                    SZArchiveMenuBorder.Visibility = Visibility.Hidden;
                    break;
                case "Home":
                    Content = new HomePage();
                    Pages();
                    break;
            }
        }
    }



}
