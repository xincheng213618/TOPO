using BaseDLL;
using BaseUtil;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.IO;
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
            idcardData.Name = Global.IDCardInfo.Name;
            idcardData.IDCardNo = Global.IDCardInfo.IDCardNo;
            InitializeComponent();
        }

        public SZArchivePage(IDCardData idcardData)
        {
            this.idcardData = idcardData;
            Global.IDCardInfo.Name = idcardData.Name;
            Global.IDCardInfo.IDCardNo = idcardData.IDCardNo;
            InitializeComponent();
        }

        private void Page_Initialized(object sender, EventArgs e)
        {
            DataContext = Time;
            Time.Countdown = 120;
            Countdown_timer();

            WaitShow.Visibility = Visibility.Visible;
            Thread thread = new Thread(() => ArchiveList());
            thread.IsBackground = true;
            thread.Start();
        }

        private void ArchiveList()
        {
            string response = Http.SZArchiveList(idcardData.Name, idcardData.IDCardNo, SZArchiveListPageNo, PageSize);
            Dispatcher.BeginInvoke(new Action(() => ParseList(response)));
        }

        private int SZArchiveListNum = 0;
        private ObservableCollection<SZArchiveListItem> SZArchiveListItem = new ObservableCollection<SZArchiveListItem>();

        private void ParseList(string response)
        {    
            WaitShow.Visibility = Visibility.Hidden;
            if (response != null)
            {
                try
                {
                    SZArchiveListView.ItemsSource = SZArchiveListItem;
                    JObject jsons = (JObject)JsonConvert.DeserializeObject(response);
                    string resultCode = jsons["code"].ToString();
                    SZArchiveListView.Visibility = Visibility.Visible;
                    if (resultCode == "0")
                    {
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
                    else if (resultCode == "3")
                    {
                        Content = new HomePage("请至档案窗口查询");
                        Pages();
                    }
                    else if (resultCode == "2")
                    {
                        SZArchiveListMsg.Visibility = Visibility.Visible;
                    }
                    else if (resultCode == "1")
                    {
                        Content = new HomePage("每日可查询次数上限");
                        Pages();
                    }
                    else
                    {
                        Content = new HomePage("该接口查询失败:" + response);
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



        private void SZArchiveListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (SZArchiveListView.SelectedIndex >= 0)
            {
                Time.Countdown = 60;
                string Archivecode = SZArchiveListItem.ElementAt(SZArchiveListView.SelectedIndex).archivecode.ToString();
                switch (Global.PageType)
                {
                    case "SZHQArchivePages":
                        WaitShow.Visibility = Visibility.Visible;
                        Thread tread2 = new Thread(() => Archive(Archivecode))
                        {
                            IsBackground = true
                        };
                        tread2.Start();
                        break;
                    case "SZWZArchivePages":
                        TotalLabel.Content = "详细列表";
                        Log.Write(Archivecode);
                        WaitShow.Visibility = Visibility.Visible;
                        Thread tread1 = new Thread(() => Archive(Archivecode))
                        {
                            IsBackground = true
                        };
                        tread1.Start();
                        break;
                    default:
                        MessageBox.Show("PageType为空，请重启机器");
                        break;
                }
            }
        }

        private void Archive(string Archievcode)
        {
            switch (Global.PageType)
            {

                case "SZHQArchivePages":
                    string response = Http.SZArchive(idcardData.IDCardNo, Archievcode);
                    Dispatcher.BeginInvoke(new Action(() => HQArchiveParse(response, Archievcode)));
                    break;
                case "SZWZArchivePages":
                    response = Http.SZArchiveMenu(idcardData.IDCardNo, Archievcode);
                    Dispatcher.BeginInvoke(new Action(() => WZArchiveParse(response)));
                    break;
            }
        }
        private void HQArchiveParse(string response, string Archievcode)
        {
            if (response != null)
            {
                try
                {
                    JObject @object = (JObject)JsonConvert.DeserializeObject(response);
                    string code = @object["returncode"]?.ToString();
                    string msg = @object["returnmsg"]?.ToString();
                    if ("0".Equals(code))
                    {
                        string recordStr = @object["result"]?.ToString();
                        if (recordStr != null && !"".Equals(recordStr))
                        {
                            List<string> caseidList = new List<string>();
                            List<string> fileNameList = new List<string>();
                            JArray recordList = (JArray)@object["result"];

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

                                    Covert.Base64ToFile(imagecontent, filePath);
                                    caseidList.Add(caseid);
                                    fileNameList.Add(filePath);
                                }
                                string FileName = "Temp\\" + Archievcode + DateTime.Now.ToString("yyyyMMddHHmmss") + ".pdf";

                                PDF.JPGToPDF(fileNameList.ToArray(), FileName);
                                Log.Write("图片转pdf成功");
                                PDF.PDFMark(FileName, "苏州市不动产登记中心虎丘分中心", DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"));
                                Log.Write("正在添加图片水印");
                                Http.AddAction(idcardData.Name, idcardData.IDCardNo, "dayinquanshu");
                                for (int j = 0; j < fileNameList.ToArray().Length; j++)
                                    File.Delete(@fileNameList.ToArray()[j]);

                                Log.Write("图片缓存删除成功");
                                Content = new Pdfshow(FileName);
                                Pages();
                            }
                        }
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


        private int SZArchiveMenuNum = 0;
        private ObservableCollection<SZArchiveMenuItem> SZArchiveMenuItems;

        private void download(string response)
        {
            List<string> MeanList = new List<string>();

            JObject jObject = (JObject)JsonConvert.DeserializeObject(response);

            string a = jObject["imgtext"][0].ToString();

            JObject json = (JObject)JsonConvert.DeserializeObject(a);


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
                    MeanList = MeanList.Distinct().ToList();

                    for (int i = 0; i < MeanList.Count; i++)
                    {
                        SZArchiveMenuItem item = new SZArchiveMenuItem();
                        SZArchiveMenuNum += 1;
                        item.ListNo = SZArchiveMenuNum;
                        item.Category = MeanList[i];
                        string PDFFile = "Temp\\"+DateTime.Now.ToString("MMddHHmmss", DateTimeFormatInfo.InvariantInfo)+ $"{MeanList[i]}.pdf";
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
                                Dictionary<string, object> dic = new Dictionary<string, object>
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
                                string PicName = $"Temp\\{jpgid}.jpg";
                                Http.ShowImage(dic, PicName);
                                //下载图片
                                fileNameList.Add(PicName);
                            }           
                        }
                        PDF.JPGToPDF(fileNameList.ToArray(), PDFFile);
                        Log.Write("图片转pdf成功");
                        PDF.PDFMark(PDFFile, "苏州市不动产登记中心吴中分中心", DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"));
                        Log.Write("正在添加图片水印");
                        for (int j = 0; j < fileNameList.ToArray().Length; j++)
                            File.Delete(@fileNameList.ToArray()[j]);

                        Log.Write("图片缓存删除成功");
                        Dispatcher.BeginInvoke(new Action(() => SZArchiveMenuItems.Add(item)));
                    }
                    Dispatcher.BeginInvoke(new Action(() => WaitShow.Visibility = Visibility.Hidden));
                }
                else
                {
                    Dispatcher.BeginInvoke(new Action(() => SZArchiveMenuMsg.Visibility = Visibility.Visible));

                }

            }
            else
            {
                Dispatcher.BeginInvoke(new Action(() => SZArchiveMenuMsg.Visibility = Visibility.Visible));
            }


        }


        private void WZArchiveParse(string response)
        {
            SZArchiveListGrid.Visibility = Visibility.Hidden;
            SZArchiveMenuGrid.Visibility = Visibility.Visible;
            SZArchiveMenuItems = new ObservableCollection<SZArchiveMenuItem>();
            SZArchiveMenuListView.ItemsSource = SZArchiveMenuItems;
            if (response != null)
            {
                try
                {
                    PopTips.Text = "正在下载图片";

                    Thread thread1 = new Thread(() => download( response));
                    thread1.IsBackground = true;
                    thread1.Start();


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


        //List 解析
        private void SCManipulationBoundaryFeedback(object sender, ManipulationBoundaryFeedbackEventArgs e)
        {
            e.Handled = true;
        }

        private void SZArchiveMenuListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (SZArchiveMenuListView.SelectedIndex > -1)
            {
                string FilePath = SZArchiveMenuItems.ElementAt(SZArchiveMenuListView.SelectedIndex).FileName.ToString();
                if (File.Exists(FilePath))
                {
                    Content = new Pdfshow(FilePath);
                    Pages();
                }
                else
                {
                    Content = new HomePage($"无法找到{FilePath}");
                    Pages();
                }
            }
        }


        //倒计时模块
        private DispatcherTimer pageTimer = null;
        public TimeCount Time = new TimeCount();


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
                    Content = new HomePage();
                    Pages();
                    break;
                case "Home":
                    Content = new HomePage();
                    Pages();
                    break;
            }
        }
    }
}