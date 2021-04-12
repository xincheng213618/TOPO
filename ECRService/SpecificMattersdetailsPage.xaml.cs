using System;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Threading;
using System.ComponentModel;
using System.Threading;

namespace ECRService
{
    /// <summary>
    /// SpecificMattersdetailsPage.xaml 的交互逻辑
    /// </summary>
    public partial class SpecificMattersdetailsPage : Page, INotifyPropertyChanged
    {

        //秒表倒计时字段
        private const int TIMEOUT = 300;
        private int countdown = TIMEOUT;
        private DispatcherTimer pageTimer = null;

        private bool printing = false;
        private Thread worker_PrinterStatus = null;
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


        public SpecificMattersdetailsPage()
        {
            InitializeComponent();
            hintLabel.Content = "";
            hintBorder.Visibility = Visibility.Hidden;
            //通过Context传值到前台,显示秒数,实现实时变动
            this.DataContext = this;
            pageTimer = new DispatcherTimer()
            {
                IsEnabled = false,
                Interval = TimeSpan.FromSeconds(1),
            };
            pageTimer.Tick += new EventHandler((sender, e) =>
            {
                //时间倒计时
                if (--Countdown <= 0)
                {
                    pageTimer.IsEnabled = false;

                    Dispatcher.BeginInvoke(new Action(() => (Application.Current.MainWindow as MainWindow).frame.Navigate(new HomePage())));
                }
                //timerLabel.Content = countdown.ToString();
            });
            pageTimer.IsEnabled = true;

            this.Loaded += new RoutedEventHandler(MainWindow_Loaded);

        }
        //返回按钮
        private void ReturnIndex_Click(object sender, RoutedEventArgs e)
        {
            sifaName.Height = 0;
            pageTimer.IsEnabled = false;
            Dispatcher.BeginInvoke(new Action(() => (Application.Current.MainWindow as MainWindow).frame.Navigate(new SpecificMattersPage())));
        }



        private void Page_Initialized(object sender, EventArgs e)
        {
            //标题名
            string[] title = null;
            //上个界面传的选择了第几个的值
            string b = Application.Current.Properties["bb"].ToString();
            //上个界面传的选择了什么局的值
            string ju = Application.Current.Properties["aa"].ToString();
            //截取字符串：截取倒数第二位
            char num = Convert.ToChar(b.Substring(b.Length - 2, 1));
            string tmp = "";
            //判断截取倒数第二位是不是数字 ：是就截取2位  不是就截取1位
            if (Char.IsDigit(num))
            {
                tmp = b.Substring(b.Length - 2, 2);
            }
            else
            {
                tmp = b.Substring(b.Length - 1, 1);
            }
            //申请书名字
            string[] applyBooks = null;
            //申请书下标
            int tmpBook = int.Parse(tmp);
           

            if (ju == "Caizhengju")
            {
                //读取标题文本
                title = File.ReadAllLines(@"/ECRServiceLocal/ECRService/事项分类/caizhengju.txt", Encoding.Default);
                //截取标题
                string showTitle = title[int.Parse(tmp)].Substring(3, title[int.Parse(tmp)].Length - 3);
                //显示标题
                lblTitle.Text = showTitle;
                //ListView里要显示的数据
                string[] strs1 = File.ReadAllLines(@"/ECRServiceLocal/ECRService/事项分类/" + "caizheng" + tmp + ".txt", Encoding.Default);
                for (int i = 0; i < strs1.Count(); i++)
                {
                    lstView1.Items.Add(strs1[i]);
                }
                //记录listView的item个数
                Application.Current.Properties["count"] = lstView1.Items.Count;
            }
            else if (ju == "Chengguanju")
            {

                //读取标题文本
                title = File.ReadAllLines(@"/ECRServiceLocal/ECRService/事项分类/chengguanju.txt", Encoding.Default);
                //截取标题
                string showTitle = title[int.Parse(tmp)].Substring(3, title[int.Parse(tmp)].Length - 3);
                //显示标题
                lblTitle.Text = showTitle;
                //ListView里要显示的数据
                string[] strs2 = File.ReadAllLines(@"/ECRServiceLocal/ECRService/事项分类/" + "chengguan" + tmp + ".txt", Encoding.Default);
                for (int i = 0; i < strs2.Count(); i++)
                {
                    lstView1.Items.Add(strs2[i]);
                }
                //记录listView的item个数
                Application.Current.Properties["count"] = lstView1.Items.Count;

            }
            else if (ju == "Danganju")
            {

                //读取标题文本
                title = File.ReadAllLines(@"/ECRServiceLocal/ECRService/事项分类/danganju.txt", Encoding.Default);
                //截取标题
                string showTitle = title[int.Parse(tmp)].Substring(3, title[int.Parse(tmp)].Length - 3);
                //显示标题
                lblTitle.Text = showTitle;
                //ListView里要显示的数据
                string[] strs3 = File.ReadAllLines(@"/ECRServiceLocal/ECRService/事项分类/dangan" + tmp + ".txt", Encoding.Default);
                for (int i = 0; i < strs3.Count(); i++)
                {
                    lstView1.Items.Add(strs3[i]);
                }
                //记录listView的item个数
                Application.Current.Properties["count"] = lstView1.Items.Count;

            }
            else if (ju == "Fagaiju")
            {

                //读取标题文本
                title = File.ReadAllLines(@"/ECRServiceLocal/ECRService/事项分类/fagaiju.txt", Encoding.Default);
                //截取标题
                string showTitle = title[int.Parse(tmp)].Substring(3, title[int.Parse(tmp)].Length - 3);
                //显示标题
                lblTitle.Text = showTitle;
                //ListView里要显示的数据
                string[] strs4 = File.ReadAllLines(@"/ECRServiceLocal/ECRService/事项分类/fagai" + tmp + ".txt", Encoding.Default);
                for (int i = 0; i < strs4.Count(); i++)
                {
                    lstView1.Items.Add(strs4[i]);
                }
                //记录listView的item个数
                Application.Current.Properties["count"] = lstView1.Items.Count;

            }
            else if (ju == "Guihuafenju")
            {

                //读取标题文本
                title = File.ReadAllLines(@"/ECRServiceLocal/ECRService/事项分类/guihuafenju.txt", Encoding.Default);
                //截取标题
                string showTitle = title[int.Parse(tmp)].Substring(3, title[int.Parse(tmp)].Length - 3);
                //显示标题
                lblTitle.Text = showTitle;
                //ListView里要显示的数据
                string[] strs5 = File.ReadAllLines(@"/ECRServiceLocal/ECRService/事项分类/规划分局" + tmp + ".txt", Encoding.Default);
                for (int i = 0; i < strs5.Count(); i++)
                {
                    lstView1.Items.Add(strs5[i]);
                }
                //记录listView的item个数
                Application.Current.Properties["count"] = lstView1.Items.Count;

            }
            else if (ju == "Guoshuifenju")
            {

                //读取标题文本
                title = File.ReadAllLines(@"/ECRServiceLocal/ECRService/事项分类/guoshuifenju.txt", Encoding.Default);
                //截取标题
                string showTitle = title[int.Parse(tmp)].Substring(3, title[int.Parse(tmp)].Length - 3);
                //显示标题
                lblTitle.Text = showTitle;
                //ListView里要显示的数据
                string[] strs6 = File.ReadAllLines(@"/ECRServiceLocal/ECRService/事项分类/国税分局" + tmp + ".txt", Encoding.Default);
                for (int i = 0; i < strs6.Count(); i++)
                {
                    lstView1.Items.Add(strs6[i]);
                }
                //记录listView的item个数
                Application.Current.Properties["count"] = lstView1.Items.Count;


            }
            else if (ju == "Guotufenju")
            {

                //读取标题文本
                title = File.ReadAllLines(@"/ECRServiceLocal/ECRService/事项分类/国土分局.txt", Encoding.Default);
                //截取标题
                string showTitle = title[int.Parse(tmp)].Substring(3, title[int.Parse(tmp)].Length - 3);
                //显示标题
                lblTitle.Text = showTitle;
                //ListView里要显示的数据
                string[] strs1 = File.ReadAllLines(@"/ECRServiceLocal/ECRService/事项分类/国土分局" + tmp + ".txt", Encoding.Default);
                for (int i = 0; i < strs1.Count(); i++)
                {
                    lstView1.Items.Add(strs1[i]);
                }
                //记录listView的item个数
                Application.Current.Properties["count"] = lstView1.Items.Count;

            }
            else if (ju == "Huanbaoju")
            {

                //读取标题文本
                title = File.ReadAllLines(@"/ECRServiceLocal/ECRService/事项分类/huanbaoju.txt", Encoding.Default);
                //截取标题
                string showTitle = title[int.Parse(tmp)].Substring(3, title[int.Parse(tmp)].Length - 3);
                //显示标题
                lblTitle.Text = showTitle;
                //ListView里要显示的数据
                string[] strs7 = File.ReadAllLines(@"/ECRServiceLocal/ECRService/事项分类/huanbao" + tmp + ".txt", Encoding.Default);
                for (int i = 0; i < strs7.Count(); i++)
                {
                    lstView1.Items.Add(strs7[i]);
                }
                //记录listView的item个数
                Application.Current.Properties["count"] = lstView1.Items.Count;


            }
            else if (ju == "Jiaoyunju")
            {

                //读取标题文本
                title = File.ReadAllLines(@"/ECRServiceLocal/ECRService/事项分类/jiaotongyunshuju.txt", Encoding.Default);
                //截取标题
                string showTitle = title[int.Parse(tmp)].Substring(3, title[int.Parse(tmp)].Length - 3);
                //显示标题
                lblTitle.Text = showTitle;
                //ListView里要显示的数据
                string[] strs8 = File.ReadAllLines(@"/ECRServiceLocal/ECRService/事项分类/jiaotongyunshu" + tmp + ".txt", Encoding.Default);
                for (int i = 0; i < strs8.Count(); i++)
                {
                    lstView1.Items.Add(strs8[i]);
                }
                //记录listView的item个数
                Application.Current.Properties["count"] = lstView1.Items.Count;


            }
            else if (ju == "Minzhengju")
            {
                //读取标题文本
                title = File.ReadAllLines(@"/ECRServiceLocal/ECRService/事项分类/minzhengju.txt", Encoding.Default);
                //截取标题
                string showTitle = title[int.Parse(tmp)].Substring(3, title[int.Parse(tmp)].Length - 3);
                //显示标题
                lblTitle.Text = showTitle;
                //ListView里要显示的数据
                string[] strs9 = File.ReadAllLines(@"/ECRServiceLocal/ECRService/事项分类/minzheng" + tmp + ".txt", Encoding.Default);
                for (int i = 0; i < strs9.Count(); i++)
                {
                    lstView1.Items.Add(strs9[i]);
                }
                //记录listView的item个数
                Application.Current.Properties["count"] = lstView1.Items.Count;
            }
            else if (ju == "Renfangban")
            {
                //读取标题文本
                title = File.ReadAllLines(@"/ECRServiceLocal/ECRService/事项分类/renfangban.txt", Encoding.Default);
                //截取标题
                string showTitle = title[int.Parse(tmp)].Substring(3, title[int.Parse(tmp)].Length - 3);
                //显示标题
                lblTitle.Text = showTitle;
                //ListView里要显示的数据
                string[] strs10 = File.ReadAllLines(@"/ECRServiceLocal/ECRService/事项分类/人防办" + tmp + ".txt", Encoding.Default);
                for (int i = 0; i < strs10.Count(); i++)
                {
                    lstView1.Items.Add(strs10[i]);
                }
                //记录listView的item个数
                Application.Current.Properties["count"] = lstView1.Items.Count;
            }
            else if (ju == "Rensheju")
            {
                //读取标题文本
                title = File.ReadAllLines(@"/ECRServiceLocal/ECRService/事项分类/rensheju.txt", Encoding.Default);
                //截取标题
                string showTitle = title[int.Parse(tmp)].Substring(3, title[int.Parse(tmp)].Length - 3);
                //显示标题
                lblTitle.Text = showTitle;
                //ListView里要显示的数据
                string[] strs11 = File.ReadAllLines(@"/ECRServiceLocal/ECRService/事项分类/人社局" + tmp + ".txt", Encoding.Default);
                for (int i = 0; i < strs11.Count(); i++)
                {
                    lstView1.Items.Add(strs11[i]);
                }
                //记录listView的item个数
                Application.Current.Properties["count"] = lstView1.Items.Count;
            }
            else if (ju == "Shangwuju")
            {
                //读取标题文本
                title = File.ReadAllLines(@"/ECRServiceLocal/ECRService/事项分类/shangwuju.txt", Encoding.Default);
                //截取标题
                string showTitle = title[int.Parse(tmp)].Substring(3, title[int.Parse(tmp)].Length - 3);
                //显示标题
                lblTitle.Text = showTitle;
                //ListView里要显示的数据
                string[] strs12 = File.ReadAllLines(@"/ECRServiceLocal/ECRService/事项分类/shangwu" + tmp + ".txt", Encoding.Default);
                for (int i = 0; i < strs12.Count(); i++)
                {
                    lstView1.Items.Add(strs12[i]);
                }
                //记录listView的item个数
                Application.Current.Properties["count"] = lstView1.Items.Count;

            }
            else if (ju == "Shijianguan")
            {

                //读取标题文本
                title = File.ReadAllLines(@"/ECRServiceLocal/ECRService/事项分类/shichangjianguanju.txt", Encoding.Default);
                //截取标题
                string showTitle = title[int.Parse(tmp)].Substring(3, title[int.Parse(tmp)].Length - 3);
                //显示标题
                lblTitle.Text = showTitle;
                //ListView里要显示的数据
                string[] strs13 = File.ReadAllLines(@"/ECRServiceLocal/ECRService/事项分类/shichangjian" + tmp + ".txt", Encoding.Default);
                for (int i = 0; i < strs13.Count(); i++)
                {
                    lstView1.Items.Add(strs13[i]);
                }
                //记录listView的item个数
                Application.Current.Properties["count"] = lstView1.Items.Count;

            }
            else if (ju == "Shuiwuju")
            {
                //读取标题文本
                title = File.ReadAllLines(@"/ECRServiceLocal/ECRService/事项分类/水务局.txt", Encoding.Default);
                //截取标题
                string showTitle = title[int.Parse(tmp)].Substring(3, title[int.Parse(tmp)].Length - 3);
                //显示标题
                lblTitle.Text = showTitle;
                //ListView里要显示的数据
                string[] strs14 = File.ReadAllLines(@"/ECRServiceLocal/ECRService/事项分类/水务" + tmp + ".txt", Encoding.Default);
                for (int i = 0; i < strs14.Count(); i++)
                {
                    lstView1.Items.Add(strs14[i]);
                }
                //记录listView的item个数
                Application.Current.Properties["count"] = lstView1.Items.Count;

            }
            else if (ju == "Sifaju")
            {
                //读取标题文本
                title = File.ReadAllLines(@"/ECRServiceLocal/ECRService/事项分类/司法局.txt", Encoding.Default);
                //截取标题
                string showTitle = title[int.Parse(tmp)].Substring(3, title[int.Parse(tmp)].Length - 3);
                //显示标题
                lblTitle.Text = showTitle;
                //ListView里要显示的数据
                //转成int类型
                if (int.Parse(tmp) == 1)
                {
                    //try
                    //{
                    //List<Image> imagelist = new List<Image>();
                    ////for (int i = 0; i < 10; i++)
                    ////{
                    //    Image image = new Image();
                    //    image.Width = 950;
                    //    image.Height = 800;
                    //    image.Source = new BitmapImage(new Uri(System.Environment.CurrentDirectory + "\\事项分类\\司法局1.png"));
                    //    imagelist.Add(image);

                    //// }
                    //lstView1.ItemsSource= imagelist;
                    Image image = new Image();

                    sifaName.Source = new BitmapImage(new Uri(System.Environment.CurrentDirectory + "\\事项分类\\司法局1.png"));
                    sifaName.Height = 390;
                    slider.Height = 200;
                    slider.Width = 20;


                    //}
                    //catch (Exception ex)
                    //{
                    //    Console.WriteLine(ex.Message);
                    //}
                }
                else
                {
                    string[] strs1 = File.ReadAllLines(@"/ECRServiceLocal/ECRService/事项分类/司法局" + tmp + ".txt", Encoding.Default);
                    for (int i = 0; i < strs1.Count(); i++)
                    {
                        lstView1.Items.Add(strs1[i]);
                    }
                    //记录listView的item个数
                    Application.Current.Properties["count"] = lstView1.Items.Count;
                }
            }
            else if (ju == "Weijiju")
            {
                //读取标题文本
                title = File.ReadAllLines(@"/ECRServiceLocal/ECRService/事项分类/卫计局.txt", Encoding.Default);
                //截取标题
                string showTitle = title[int.Parse(tmp)].Substring(3, title[int.Parse(tmp)].Length - 3);
                //显示标题
                lblTitle.Text = showTitle;
                //ListView里要显示的数据
                string[] strs1 = File.ReadAllLines(@"/ECRServiceLocal/ECRService/事项分类/卫计局" + tmp + ".txt", Encoding.Default);
                for (int i = 0; i < strs1.Count(); i++)
                {
                    lstView1.Items.Add(strs1[i]);
                }
                //记录listView的item个数
                Application.Current.Properties["count"] = lstView1.Items.Count;
            }
            else if (ju == "Wenlvju")
            {
                //读取标题文本
                title = File.ReadAllLines(@"/ECRServiceLocal/ECRService/事项分类/文化旅游局.txt", Encoding.Default);
                //截取标题
                string showTitle = title[int.Parse(tmp)].Substring(3, title[int.Parse(tmp)].Length - 3);
                //显示标题
                lblTitle.Text = showTitle;
                //ListView里要显示的数据
                string[] strs1 = File.ReadAllLines(@"/ECRServiceLocal/ECRService/事项分类/文化旅游局" + tmp + ".txt", Encoding.Default);
                for (int i = 0; i < strs1.Count(); i++)
                {
                    lstView1.Items.Add(strs1[i]);
                }
                //记录listView的item个数
                Application.Current.Properties["count"] = lstView1.Items.Count;
            }
            else if (ju == "Wujiaju")
            {
                //读取标题文本
                title = File.ReadAllLines(@"/ECRServiceLocal/ECRService/事项分类/物价局.txt", Encoding.Default);
                //截取标题
                string showTitle = title[int.Parse(tmp)].Substring(3, title[int.Parse(tmp)].Length - 3);
                //显示标题
                lblTitle.Text = showTitle;
                //ListView里要显示的数据
                string[] strs1 = File.ReadAllLines(@"/ECRServiceLocal/ECRService/事项分类/物价局" + tmp + ".txt", Encoding.Default);
                for (int i = 0; i < strs1.Count(); i++)
                {
                    lstView1.Items.Add(strs1[i]);
                }
                ////记录listView的item个数
                Application.Current.Properties["count"] = lstView1.Items.Count;
            }
            else if (ju == "Yaojianju")
            {
                applyBooks = new string[] { "食品经营许可证补证申请书", "食品经营许可证注销申请书", "食品经营许可证换证申请书", "食品经营许可证申请书", "食品经营许可证变更申请书" };
                if (tmpBook >= 6)
                {
                    Application.Current.Properties["apply"] = applyBooks[tmpBook - 6];
                    //按钮图标显示
                    applyBook.Text = "➤" + applyBooks[tmpBook - 6]; 
                }
                //读取标题文本
                title = File.ReadAllLines(@"/ECRServiceLocal/ECRService/事项分类/药监局.txt", Encoding.Default);
                //截取标题
                string showTitle = title[int.Parse(tmp)].Substring(3, title[int.Parse(tmp)].Length - 3);
                //显示标题
                lblTitle.Text = showTitle;
                //ListView里要显示的数据
                string[] strs1 = File.ReadAllLines(@"/ECRServiceLocal/ECRService/事项分类/药监局" + tmp + ".txt", Encoding.Default);
                for (int i = 0; i < strs1.Count(); i++)
                {
                    lstView1.Items.Add(strs1[i]);
                }
                ////记录listView的item个数
                Application.Current.Properties["count"] = lstView1.Items.Count;
            }
            else if (ju == "Zhijianju")
            {
                //读取标题文本
                title = File.ReadAllLines(@"/ECRServiceLocal/ECRService/事项分类/质监局.txt", Encoding.Default);
                //截取标题
                string showTitle = title[int.Parse(tmp)].Substring(3, title[int.Parse(tmp)].Length - 3);
                //显示标题
                lblTitle.Text = showTitle;
                //ListView里要显示的数据
                string[] strs1 = File.ReadAllLines(@"/ECRServiceLocal/ECRService/事项分类/质监局" + tmp + ".txt", Encoding.Default);
                for (int i = 0; i < strs1.Count(); i++)
                {
                    lstView1.Items.Add(strs1[i]);
                }
                ////记录listView的item个数
                Application.Current.Properties["count"] = lstView1.Items.Count;
            }
            else if (ju == "Zhujianju")
            {
                //读取标题文本
                title = File.ReadAllLines(@"/ECRServiceLocal/ECRService/事项分类/住建局.txt", Encoding.Default);
                //截取标题
                string showTitle = title[int.Parse(tmp)].Substring(3, title[int.Parse(tmp)].Length - 3);
                //显示标题
                lblTitle.Text = showTitle;
                //ListView里要显示的数据
                string[] strs1 = File.ReadAllLines(@"/ECRServiceLocal/ECRService/事项分类/住建局" + tmp + ".txt", Encoding.Default);
                for (int i = 0; i < strs1.Count(); i++)
                {
                    lstView1.Items.Add(strs1[i]);
                }
                //记录listView的item个数
                Application.Current.Properties["count"] = lstView1.Items.Count;
            }



            //string b = Application.Current.Properties["cc"].ToString();
            //string tp = b.Substring(a.Length - 1, 1);
            //string[] strs2 = File.ReadAllLines(@"/ECRServiceLocal/ECRService/事项分类/" + "chengguan" + tp + ".txt", Encoding.Default);
            //for (int k = 0; k < strs2.Count(); k++)
            //{
            //    lstView.Items.Add(strs2[k]);
            //}

        }

        //private void lstView1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        //{
        //    Console.Write("点击");
        //}

        //记录点击次数
        //static int click = 1;
        //双击图片放大
        //private void lstView1_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        //{
        //    string a = Application.Current.Properties["bb"].ToString();
        //    string ju = Application.Current.Properties["aa"].ToString();
        //    char num = Convert.ToChar(a.Substring(a.Length - 2, 1));
        //    string tmp = "";

        //    if (Char.IsDigit(num))
        //    {
        //        tmp = a.Substring(a.Length - 2, 2);
        //    }
        //    else
        //    {
        //        tmp = a.Substring(a.Length - 1, 1);
        //    }

        //    if (ju == "Sifaju")
        //    {
        //        if (int.Parse(tmp) == 1)
        //        {
        //            try
        //            {
        //                图片放大
        //                if (click % 2 != 0)
        //                {
        //                    click++;
        //                    Image image = new Image();
        //                    image.Width = 1621;
        //                    image.Height = 1078;
        //                    sifaName.Source = new BitmapImage(new Uri(System.Environment.CurrentDirectory + "\\事项分类\\司法局1.png"));
        //                    slider.Height = 200;
        //                    slider.Width = 20;
        //                }
        //                else
        //                {
        //                    图片缩小
        //                    click++;
        //                    Image image2 = new Image();
        //                    image2.Width = 950;
        //                    image2.Height = 800;
        //                    sifaName.Source = new BitmapImage(new Uri(System.Environment.CurrentDirectory + "\\事项分类\\司法局1.png"));
        //                    slider.Height = 200;
        //                    slider.Width = 20;
        //                }

        //            }
        //            catch (Exception ex)
        //            {
        //                Console.WriteLine(ex.Message);
        //            }
        //        }
        //    }
        //}

        //图片拖拽
        ScaleTransform st;
        TranslateTransform tt;
        TransformGroup group;
        bool isDrag = false;
        Point startPoint;

        void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            group = (TransformGroup)grdMap.RenderTransform;
            st = group.Children[0] as ScaleTransform;
            tt = group.Children[3] as TranslateTransform;
        }

        private void grdMap_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            var point = e.GetPosition(grdRelative); // 实际点击的点  
            var actualPoint = group.Inverse.Transform(point); // 想要缩放的点  
            slider.Value = slider.Value + (double)e.Delta / 1000;
            tt.X = -((actualPoint.X * (slider.Value - 1))) + point.X - actualPoint.X;
            tt.Y = -((actualPoint.Y * (slider.Value - 1))) + point.Y - actualPoint.Y;
        }

        private void grdMap_MouseDown(object sender, MouseButtonEventArgs e)
        {
            isDrag = true;
            startPoint = e.GetPosition(grdRelative);
        }

        private void grdMap_MouseUp(object sender, MouseButtonEventArgs e)
        {
            isDrag = false;
        }

        private void grdMap_MouseLeave(object sender, MouseEventArgs e)
        {
            isDrag = false;
        }

        private void grdMap_MouseMove(object sender, MouseEventArgs e)
        {
            if (isDrag)
            {
                Point p = e.GetPosition(grdRelative);
                Point topPoint = grdMap.TranslatePoint(new Point(0, 0), grdRelative);
                Point bottomPoint = grdMap.TranslatePoint(new Point(grdMap.ActualWidth, grdMap.ActualHeight), grdRelative);

                double moveX = p.X - startPoint.X;
                double moveY = p.Y - startPoint.Y;

                //向上向下移动条件判断（会有一点点的小偏移，如果想更精确的控制，那么分向上和向下两种情况，并判断边距）  
                if ((moveY < 0 && bottomPoint.Y > grdRelative.ActualHeight) || (moveY > 0 && topPoint.Y < 0))
                {
                    tt.Y += (p.Y - startPoint.Y);
                    startPoint.Y = p.Y;
                }

                //向左向右移动条件判断  
                if ((moveX < 0 && bottomPoint.X > grdRelative.ActualWidth) || (moveX > 0 && topPoint.X < 0))
                {
                    tt.X += (p.X - startPoint.X);
                    startPoint.X = p.X;
                }
            }
        }


    }
}