using System;
using System.IO;
using System.Collections.ObjectModel;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;
using System.Windows.Input;
using System.Windows.Media;
using System.Xml;
using ECRService.ReportServiceReference;
using System.ComponentModel;
using Microsoft.Ink;
using System.Runtime.InteropServices;

namespace ECRService
{
    public class NaturalPersonListItem
    {
        public int xh { get; set; }
        public string grid { get; set; }
        public string xm { get; set; }
        public string sfzjhm { get; set; }
    }

    /// <summary>
    /// NaturalPersonQueryPage.xaml 的交互逻辑
    /// </summary>
    public partial class NaturalPersonQueryPage : Page, INotifyPropertyChanged
    {
        private const int TIMEOUT = 90;
        private int countdown = TIMEOUT;
        private DispatcherTimer pageTimer = null;
        private CancellationTokenSource tokenSource = null;

        private string[] queryTypeName = new string[13] { "执业药师", "价格鉴证师", "工程建设领域执业人员", "执业律师", "信用管理师", "食品检验员", "公证员", "注册房地产估价师", "注册房地产经纪人", "监理工程师", "信息系统工程监理资质工程师", "计算机信息系统集成高级项目经理", "计算机信息系统集成项目经理"};
        private string[] queryType = new string[13] { "ZYYS", "JGJZ", "GCJS", "ZYLS", "XYGL", "SQJY", "GZY", "FDCGJ", "FDCJJ", "JLGC", "XXXTGCJL", "XTJCGJXMJL", "XTJCXMJL" };

        private bool querying = false;
        private bool queried = false;
        private string name = null;
        private string certificate = null;
        private ObservableCollection<NaturalPersonListItem> listItems = null;

        public int Countdown
        {
            get { return countdown; }
            set { countdown = value; OnPropertyChanged("Countdown"); }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        private int Kinds = 0;
        public NaturalPersonQueryPage(int Kinds)
        {
            this.Kinds = Kinds;
            InitializeComponent();
            this.DataContext = this;

            tokenSource = new CancellationTokenSource();
            pageTimer = new DispatcherTimer()
            {
                IsEnabled = true,
                Interval = TimeSpan.FromSeconds(1),
            };
            pageTimer.Tick += new EventHandler((sender, e) =>
            {
                if (--Countdown <= 0)
                {
                    pageTimer.IsEnabled = false;
                    tokenSource.Cancel();
                    Dispatcher.BeginInvoke(new Action(() => (Application.Current.MainWindow as MainWindow).frame.Navigate(new HomePage())));
                }
            });

            title.Content = queryTypeName[Kinds - 1] + "信息查询";

            media.Source = new Uri(Directory.GetCurrentDirectory() + "/Media/3、放置身份证.mp3", UriKind.Absolute);
            media.Position = TimeSpan.Zero;
            //media.Play();
        }

        private void Return_Click(object sender, RoutedEventArgs e)
        {
            pageTimer.IsEnabled = false;
            tokenSource.Cancel();
            Dispatcher.BeginInvoke(new Action(() => (Application.Current.MainWindow as MainWindow).frame.Navigate(Kinds <= 8 ? (Page)new NaturalPersonQueryIndexPage() : (Page)new NaturalPersonQueryIndexPage())));
        }

        private void GetNaturalPersonListCompleted(string response)
        {
            querying = false;
            queried = true;
            hintLabel.Content = "";
            hintBorder.Visibility = Visibility.Hidden;
            border.Visibility = Visibility.Hidden;

            try
            {
                XmlDocument document = new XmlDocument();
                document.LoadXml(response);

                string returncode = document.SelectSingleNode("//data/returncode").InnerText;
                string returnmsg = document.SelectSingleNode("//data/returnmsg").InnerText;

                if (returncode.Equals("1"))
                {
                    int i = 0;
                    listItems = new ObservableCollection<NaturalPersonListItem>();
                    listView.ItemsSource = listItems;

                    XmlNodeList xnList = document.SelectNodes("//data/result/record");
                    foreach (XmlNode xn in xnList)
                    {
                        NaturalPersonListItem item = new NaturalPersonListItem();

                        item.xh = ++i;
                        item.grid = xn["grid"].InnerText;
                        item.xm = xn["xm"].InnerText;
                        item.sfzjhm = xn["sfzjhm"].InnerText;
                        listItems.Add(item);
                    }

                    listViewHeader.Visibility = Visibility.Visible;
                    listView.Visibility = Visibility.Visible;
                }
                else if (returncode.Equals("0"))
                {
                    listViewHeader.Visibility = Visibility.Hidden;
                    listView.Visibility = Visibility.Hidden;
                    hintBorder.Visibility = Visibility.Visible;
                    //border.Visibility = Visibility.Visible;
                    hintLabel.Content = returnmsg;
                }
                else if (returncode.Equals("e"))
                {
                    throw new Exception(returnmsg);
                }
            }
            catch (Exception ex)
            {
                pageTimer.IsEnabled = false;
            }

        }

        private void GetNaturalPersonListProc(CancellationToken token)
        {
            try
            {
                
                System.ServiceModel.BasicHttpBinding binding = new System.ServiceModel.BasicHttpBinding();
                binding.MaxReceivedMessageSize = 16777216;
                binding.SendTimeout = TimeSpan.FromSeconds(30);
                binding.ReceiveTimeout = TimeSpan.FromSeconds(30);
                System.ServiceModel.EndpointAddress endpointAddress = new System.ServiceModel.EndpointAddress(Global.Config.ServerURL);
                ReportService queryService = new ReportServiceClient(binding, endpointAddress);
                string response = queryService.getZrrxyxxList(Global.Config.LoginName, Global.Config.LoginPassword, name, certificate, queryType[Kinds - 1]);

                /*
                string response = "<?xml version=\"1.0\" encoding=\"UTF - 8\"?><data><returncode>1</returncode><returnmsg>调用成功</returnmsg><result>"
               + "<record><grid>1</grid><xm>张三</xm><sfzjhm>330231234567898767</sfzjhm></record>"
               + "<record><grid>1</grid><xm>张三</xm><sfzjhm>330231234567898767</sfzjhm></record></result></data>"; */
                if (!token.IsCancellationRequested)
                {
                    Dispatcher.BeginInvoke(new Action(() => GetNaturalPersonListCompleted(response)));
                }
            }
            catch (Exception ex)
            {

            }
        }

        private void Query_Click(object sender, RoutedEventArgs e)
        {
            if (nameTextBox.Text.Length == 0)
            {
                hintBorder.Visibility = Visibility.Visible;
                hintLabel.Content = "请输入姓名！";
                return;
            }

            if (querying)
                return;


            querying = true;
            name = nameTextBox.Text;
            certificate = certificateTextBox.Text;

            hintBorder.Visibility = Visibility.Visible;
            border.Visibility = Visibility.Visible;
            hintLabel.Content = "正在查询" + queryTypeName[Kinds - 1] + "信息，请稍候！";

            Thread worker = new Thread(() => GetNaturalPersonListProc(tokenSource.Token));
            worker.Start();
        }

        public void ResetTimer()
        {
            Countdown = TIMEOUT;
            pageTimer.IsEnabled = true;

            FocusManager.SetFocusedElement(this, null);
        }

        private void ListView_ManipulationBoundaryFeedback(object sender, ManipulationBoundaryFeedbackEventArgs e)
        {
            e.Handled = true;
        }

        private void ListView_MouseLeftButtonDown(object sender, RoutedEventArgs e)
        {
            NaturalPersonListItem item = (NaturalPersonListItem)((ListViewItem)sender).Content;

            pageTimer.IsEnabled = false;
            Dispatcher.BeginInvoke(new Action(() => (Application.Current.MainWindow as MainWindow).frame.Navigate(new NaturalPersonDetailPage(this, item.grid,Kinds))));
        }

        private void NameTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            hintLabel.Content = "";
            hintBorder.Visibility = Visibility.Hidden;
            border.Visibility = Visibility.Hidden;

            listViewHeader.Visibility = Visibility.Hidden;
            listView.Visibility = Visibility.Hidden;

            inkPanel.Visibility = Visibility.Visible;
        }

        private void NameTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            inkPanel.Visibility = Visibility.Hidden;

            if (queried)
            {
                listViewHeader.Visibility = Visibility.Visible;
                listView.Visibility = Visibility.Visible;
            }
        }

        private void CertificateTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            hintLabel.Content = "";
            hintBorder.Visibility = Visibility.Hidden;
            border.Visibility = Visibility.Hidden;

            listViewHeader.Visibility = Visibility.Hidden;
            listView.Visibility = Visibility.Hidden;

            keyboardPanel.Visibility = Visibility.Visible;
        }

        private void CertificateTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            keyboardPanel.Visibility = Visibility.Hidden;

            if (queried)
            {
                listViewHeader.Visibility = Visibility.Visible;
                listView.Visibility = Visibility.Visible;
            }
        }

        private Timer timer = null;
        private void InkCanvas_StrokeCollected(object sender, InkCanvasStrokeCollectedEventArgs e)
        {
            inkCanvas.Background = new SolidColorBrush(Colors.White);
            
            if (timer != null)
            {
                timer.Dispose();
                timer = null;
            }

            timer = new Timer(_ => Dispatcher.BeginInvoke(new Action(() => HandWriting_Recognize())), null, TimeSpan.FromMilliseconds(100), Timeout.InfiniteTimeSpan);
        }

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
                        if (ink.Strokes.Count > 0)
                        {
                            context.Strokes = ink.Strokes;
                            RecognitionStatus status;

                            var result = context.Recognize(out status);

                            if (status == RecognitionStatus.NoError)
                            {
                                RecognitionAlternates selections = result.GetAlternatesFromSelection();

                                Dispatcher.BeginInvoke(new Action(() =>
                                {
                                    select0.Content = selections[0];
                                    select1.Content = selections[1];
                                    select2.Content = selections[2];
                                    select3.Content = selections[3];
                                    select4.Content = selections[4];
                                }));
                            }
                        }
                    }
                }).Start();
            }

        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Clear_Click(object sender, RoutedEventArgs e)
        {
            inkCanvas.Strokes.Clear();

            select0.Content = null;
            select1.Content = null;
            select2.Content = null;
            select3.Content = null;
            select4.Content = null;
        }


        private void Page_Initialized(object sender, EventArgs e)
        {
            //nameTextBox.Focus();
            FocusManager.SetFocusedElement(this, nameTextBox);
        }

        private void Recognizer_Select(object sender, RoutedEventArgs e)
        {
            RecognitionAlternate alternate = (RecognitionAlternate)((Button)sender).Content;

            if (alternate == null)
                return;

            TextBox focus = FocusManager.GetFocusedElement(this) as TextBox;

            if (focus != null)
            {
                int cursor = focus.SelectionStart;
                focus.Text = focus.Text.Insert(cursor, alternate.ToString());
                focus.Select(cursor + alternate.ToString().Length, 0);
            }

            inkCanvas.Strokes.Clear();

            select0.Content = null;
            select1.Content = null;
            select2.Content = null;
            select3.Content = null;
            select4.Content = null;
        }

        private void Keyboard_SendKeyPress(char ch)
        {

        }

        private void Keyboard_1_Click(object sender, RoutedEventArgs e)
        {
            Keyboard_SendKeyPress('1');
        }

        private void Keyboard_2_Click(object sender, RoutedEventArgs e)
        {
            Keyboard_SendKeyPress('2');
        }

        private void Keyboard_3_Click(object sender, RoutedEventArgs e)
        {
            Keyboard_SendKeyPress('3');
        }

        private void Keyboard_4_Click(object sender, RoutedEventArgs e)
        {
            Keyboard_SendKeyPress('4');
        }

        private void Keyboard_5_Click(object sender, RoutedEventArgs e)
        {
            Keyboard_SendKeyPress('5');
        }

        private void Keyboard_6_Click(object sender, RoutedEventArgs e)
        {
            Keyboard_SendKeyPress('6');
        }

        private void Keyboard_7_Click(object sender, RoutedEventArgs e)
        {
            Keyboard_SendKeyPress('7');
        }

        private void Keyboard_8_Click(object sender, RoutedEventArgs e)
        {
            Keyboard_SendKeyPress('8');
        }

        private void Keyboard_9_Click(object sender, RoutedEventArgs e)
        {
            Keyboard_SendKeyPress('9');
        }

        private void Keyboard_0_Click(object sender, RoutedEventArgs e)
        {
            Keyboard_SendKeyPress('0');

        }

        private void Keyboard_Q_Click(object sender, RoutedEventArgs e)
        {
            Keyboard_SendKeyPress('Q');
        }

        private void Keyboard_W_Click(object sender, RoutedEventArgs e)
        {
            Keyboard_SendKeyPress('W');
        }

        private void Keyboard_E_Click(object sender, RoutedEventArgs e)
        {
            Keyboard_SendKeyPress('E');
        }

        private void Keyboard_R_Click(object sender, RoutedEventArgs e)
        {
            Keyboard_SendKeyPress('R');
        }

        private void Keyboard_T_Click(object sender, RoutedEventArgs e)
        {
            Keyboard_SendKeyPress('T');
        }

        private void Keyboard_Y_Click(object sender, RoutedEventArgs e)
        {
            Keyboard_SendKeyPress('Y');
        }

        private void Keyboard_U_Click(object sender, RoutedEventArgs e)
        {
            Keyboard_SendKeyPress('U');
        }

        private void Keyboard_I_Click(object sender, RoutedEventArgs e)
        {
            Keyboard_SendKeyPress('I');
        }

        private void Keyboard_O_Click(object sender, RoutedEventArgs e)
        {
            Keyboard_SendKeyPress('O');
        }

        private void Keyboard_P_Click(object sender, RoutedEventArgs e)
        {
            Keyboard_SendKeyPress('P');
        }

        private void Keyboard_A_Click(object sender, RoutedEventArgs e)
        {
            Keyboard_SendKeyPress('A');
        }

        private void Keyboard_S_Click(object sender, RoutedEventArgs e)
        {
            Keyboard_SendKeyPress('S');
        }

        private void Keyboard_D_Click(object sender, RoutedEventArgs e)
        {
            Keyboard_SendKeyPress('D');
        }

        private void Keyboard_F_Click(object sender, RoutedEventArgs e)
        {
            Keyboard_SendKeyPress('F');
        }

        private void Keyboard_G_Click(object sender, RoutedEventArgs e)
        {
            Keyboard_SendKeyPress('G');
        }

        private void Keyboard_H_Click(object sender, RoutedEventArgs e)
        {
            Keyboard_SendKeyPress('H');
        }

        private void Keyboard_J_Click(object sender, RoutedEventArgs e)
        {
            Keyboard_SendKeyPress('J');
        }

        private void Keyboard_K_Click(object sender, RoutedEventArgs e)
        {
            Keyboard_SendKeyPress('K');
        }

        private void Keyboard_L_Click(object sender, RoutedEventArgs e)
        {
            Keyboard_SendKeyPress('L');
        }

        private void Keyboard_Clear_Click(object sender, RoutedEventArgs e)
        {
            TextBox focus = FocusManager.GetFocusedElement(this) as TextBox;

            if (focus != null)
            {
                focus.Text = "";
            }
        }

        private void Keyboard_Z_Click(object sender, RoutedEventArgs e)
        {
            Keyboard_SendKeyPress('Z');
        }

        private void Keyboard_X_Click(object sender, RoutedEventArgs e)
        {
            Keyboard_SendKeyPress('X');
        }

        private void Keyboard_C_Click(object sender, RoutedEventArgs e)
        {
            Keyboard_SendKeyPress('C');
        }

        private void Keyboard_V_Click(object sender, RoutedEventArgs e)
        {
            Keyboard_SendKeyPress('V');
        }

        private void Keyboard_B_Click(object sender, RoutedEventArgs e)
        {
            Keyboard_SendKeyPress('B');
        }

        private void Keyboard_N_Click(object sender, RoutedEventArgs e)
        {
            Keyboard_SendKeyPress('N');
        }

        private void Keyboard_M_Click(object sender, RoutedEventArgs e)
        {
            Keyboard_SendKeyPress('M');
        }

        private void Keyboard_Backspace_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Home_Click(object sender, RoutedEventArgs e)
        {
            pageTimer.IsEnabled = false;
            tokenSource.Cancel();
            Dispatcher.BeginInvoke(new Action(() => (Application.Current.MainWindow as MainWindow).frame.Navigate((Page)new HomePage())));
        }
    }
}
