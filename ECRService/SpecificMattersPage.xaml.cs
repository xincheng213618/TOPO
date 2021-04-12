using System;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;

namespace ECRService
{
    /// <summary>
    /// SpecificMattersPage.xaml 的交互逻辑
    /// </summary>
    public partial class SpecificMattersPage : Page
    {

        //秒表倒计时字段
        private const int TIMEOUT = 60;
        private int countdown = TIMEOUT;
        private DispatcherTimer pageTimer = null;

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

        public SpecificMattersPage()
        {
            InitializeComponent();
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
        }

        private void ReturnIndex_Click(object sender, RoutedEventArgs e)
        {
            pageTimer.IsEnabled = false;
        }

        private void Page_Initialized(object sender, EventArgs e)
        {
           
            string a = Application.Current.Properties["aa"].ToString();
            if (a == "Caizhengju")
            {          
                try
                {
                    //标题text是财政局
                    lblTitle.Text = "财政局";
                    string[] strs1 = File.ReadAllLines(@"/ECRServiceLocal/ECRService/事项分类/caizhengju.txt", Encoding.Default);
                    for (int i = 0; i < strs1.Count(); i++)
                    {
                        lstView1.Items.Add(strs1[i]);
                    }
                    //记录listView的item个数
                    Application.Current.Properties["count"] = lstView1.Items.Count;
                }

                catch (Exception ex)
                {

                }
            }
            #region 其他
            else if (a == "Chengguanju")
            {
       
                lblTitle.Text = "城管局";
                string[] strs2 = File.ReadAllLines(@"/ECRServiceLocal/ECRService/事项分类/chengguanju.txt", Encoding.Default);
                for (int i = 0; i < strs2.Count(); i++)
                {
                    lstView1.Items.Add(strs2[i]);
                }
                //记录listView的item个数
                Application.Current.Properties["count"] = lstView1.Items.Count;

            }
            else if (a == "Danganju")
            {
             
                lblTitle.Text = "档案局";
                string[] strs3 = File.ReadAllLines(@"/ECRServiceLocal/ECRService/事项分类/danganju.txt", Encoding.Default);
                for (int i = 0; i < strs3.Count(); i++)
                {
                    lstView1.Items.Add(strs3[i]);
                }
                //记录listView的item个数
                Application.Current.Properties["count"] = lstView1.Items.Count;

            }
            else if (a == "Fagaiju")
            {

                lblTitle.Text = "发改局";
                string[] strs4 = File.ReadAllLines(@"/ECRServiceLocal/ECRService/事项分类/fagaiju.txt", Encoding.Default);
                for (int i = 0; i < strs4.Count(); i++)
                {
                    lstView1.Items.Add(strs4[i]);
                }
                //记录listView的item个数
                Application.Current.Properties["count"] = lstView1.Items.Count;

            }
            else if (a == "Guihuafenju")
            {

                lblTitle.Text = "规划分局";
                string[] strs5 = File.ReadAllLines(@"/ECRServiceLocal/ECRService/事项分类/guihuafenju.txt", Encoding.Default);
                for (int i = 0; i < strs5.Count(); i++)
                {
                    lstView1.Items.Add(strs5[i]);
                }
                //记录listView的item个数
                Application.Current.Properties["count"] = lstView1.Items.Count;

            }
            else if (a == "Guoshuifenju")
            {

                lblTitle.Text = "国税分局";
                string[] strs6 = File.ReadAllLines(@"/ECRServiceLocal/ECRService/事项分类/guoshuifenju.txt", Encoding.Default);
                for (int i = 0; i < strs6.Count(); i++)
                {
                    lstView1.Items.Add(strs6[i]);
                }
                //记录listView的item个数
                Application.Current.Properties["count"] = lstView1.Items.Count;


            }
            else if (a == "Guotufenju")
            {

                lblTitle.Text = "国土分局";
                string[] strs1 = File.ReadAllLines(@"/ECRServiceLocal/ECRService/事项分类/国土分局.txt", Encoding.Default);
                for (int i = 0; i < strs1.Count(); i++)
                {
                    lstView1.Items.Add(strs1[i]);
                }
                //记录listView的item个数
                Application.Current.Properties["count"] = lstView1.Items.Count;

            }
            else if (a == "Huanbaoju")
            {

                lblTitle.Text = "环保局";
                string[] strs7 = File.ReadAllLines(@"/ECRServiceLocal/ECRService/事项分类/huanbaoju.txt", Encoding.Default);
                for (int i = 0; i < strs7.Count(); i++)
                {
                    lstView1.Items.Add(strs7[i]);
                }
                //记录listView的item个数
                Application.Current.Properties["count"] = lstView1.Items.Count;


            }
            else if (a == "Jiaoyunju")
            {

                lblTitle.Text = "交通运输局";
                string[] strs8 = File.ReadAllLines(@"/ECRServiceLocal/ECRService/事项分类/jiaotongyunshuju.txt", Encoding.Default);
                for (int i = 0; i < strs8.Count(); i++)
                {
                    lstView1.Items.Add(strs8[i]);
                }
                //记录listView的item个数
                Application.Current.Properties["count"] = lstView1.Items.Count;


            }
            else if (a == "Minzhengju")
            {
                lblTitle.Text = "民政局";
                string[] strs9 = File.ReadAllLines(@"/ECRServiceLocal/ECRService/事项分类/minzhengju.txt", Encoding.Default);
                for (int i = 0; i < strs9.Count(); i++)
                {
                    lstView1.Items.Add(strs9[i]);
                }
                //记录listView的item个数
                Application.Current.Properties["count"] = lstView1.Items.Count;
            }
            else if (a == "Renfangban")
            {
                lblTitle.Text = "人防办";
                string[] strs10 = File.ReadAllLines(@"/ECRServiceLocal/ECRService/事项分类/renfangban.txt", Encoding.Default);
                for (int i = 0; i < strs10.Count(); i++)
                {
                    lstView1.Items.Add(strs10[i]);
                }
                //记录listView的item个数
                Application.Current.Properties["count"] = lstView1.Items.Count;
            }
            else if (a == "Rensheju")
            {
                lblTitle.Text = "人社局";
                string[] strs11 = File.ReadAllLines(@"/ECRServiceLocal/ECRService/事项分类/rensheju.txt", Encoding.Default);
                for (int i = 0; i < strs11.Count(); i++)
                {
                    lstView1.Items.Add(strs11[i]);
                }
                //记录listView的item个数
                Application.Current.Properties["count"] = lstView1.Items.Count;
            }
            else if (a == "Shangwuju")
            {
                lblTitle.Text = "商务局";
                string[] strs12 = File.ReadAllLines(@"/ECRServiceLocal/ECRService/事项分类/shangwuju.txt", Encoding.Default);
                for (int i = 0; i < strs12.Count(); i++)
                {
                    lstView1.Items.Add(strs12[i]);
                }
                //记录listView的item个数
                Application.Current.Properties["count"] = lstView1.Items.Count;

            }
            else if (a == "Shijianguan")
            {

                lblTitle.Text = "市场监督管理局";
                string[] strs13 = File.ReadAllLines(@"/ECRServiceLocal/ECRService/事项分类/shichangjianguanju.txt", Encoding.Default);
                for (int i = 0; i < strs13.Count(); i++)
                {
                    lstView1.Items.Add(strs13[i]);
                }
                //记录listView的item个数
                Application.Current.Properties["count"] = lstView1.Items.Count;

            }
            else if (a == "Shuiwuju")
            {
                lblTitle.Text = "水务局";
                string[] strs14 = File.ReadAllLines(@"/ECRServiceLocal/ECRService/事项分类/水务局.txt", Encoding.Default);
                for (int i = 0; i < strs14.Count(); i++)
                {
                    lstView1.Items.Add(strs14[i]);
                }
                //记录listView的item个数
                Application.Current.Properties["count"] = lstView1.Items.Count;

            }
            else if (a == "Sifaju")
            {
                lblTitle.Text = "司法局";
                string[] strs1 = File.ReadAllLines(@"/ECRServiceLocal/ECRService/事项分类/司法局.txt", Encoding.Default);
                for (int i = 0; i < strs1.Count(); i++)
                {
                    lstView1.Items.Add(strs1[i]);
                }
                ////记录listView的item个数
                Application.Current.Properties["count"] = lstView1.Items.Count;

            }
            else if (a == "Weijiju")
            {
                lblTitle.Text = "卫计局";
                string[] strs1 = File.ReadAllLines(@"/ECRServiceLocal/ECRService/事项分类/卫计局.txt", Encoding.Default);
                for (int i = 0; i < strs1.Count(); i++)
                {
                    lstView1.Items.Add(strs1[i]);
                }
                //记录listView的item个数
                Application.Current.Properties["count"] = lstView1.Items.Count;

            }
            else if (a == "Wenlvju")
            {
                lblTitle.Text = "文化旅游局";
                string[] strs1 = File.ReadAllLines(@"/ECRServiceLocal/ECRService/事项分类/文化旅游局.txt", Encoding.Default);
                for (int i = 0; i < strs1.Count(); i++)
                {
                    lstView1.Items.Add(strs1[i]);
                }
                //记录listView的item个数
                Application.Current.Properties["count"] = lstView1.Items.Count;

            }
            else if (a == "Wujiaju")
            {
                lblTitle.Text = "物价局";
                string[] strs1 = File.ReadAllLines(@"/ECRServiceLocal/ECRService/事项分类/物价局.txt", Encoding.Default);
                for (int i = 0; i < strs1.Count(); i++)
                {
                    lstView1.Items.Add(strs1[i]);
                }
                //记录listView的item个数
                Application.Current.Properties["count"] = lstView1.Items.Count;

            }
            else if (a == "Yaojianju")
            {
                lblTitle.Text = "药监局";
                string[] strs1 = File.ReadAllLines(@"/ECRServiceLocal/ECRService/事项分类/药监局.txt", Encoding.Default);
                for (int i = 0; i < strs1.Count(); i++)
                {
                    lstView1.Items.Add(strs1[i]);
                }
                //记录listView的item个数
                Application.Current.Properties["count"] = lstView1.Items.Count;

            }
            else if (a == "Zhijianju")
            {
                lblTitle.Text = "质监局";
                string[] strs1 = File.ReadAllLines(@"/ECRServiceLocal/ECRService/事项分类/质监局.txt", Encoding.Default);
                for (int i = 0; i < strs1.Count(); i++)
                {
                    lstView1.Items.Add(strs1[i]);
                }
                //记录listView的item个数
                Application.Current.Properties["count"] = lstView1.Items.Count;

            }
            else if (a == "Zhujianju")
            {
                lblTitle.Text = "住建局";
                string[] strs1 = File.ReadAllLines(@"/ECRServiceLocal/ECRService/事项分类/住建局.txt", Encoding.Default);
                for (int i = 0; i < strs1.Count(); i++)
                {
                    lstView1.Items.Add(strs1[i]);
                    
                }
                //记录listView的item个数
                Application.Current.Properties["count"] = lstView1.Items.Count;
            }
        }
        #endregion

        private void lstView1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            pageTimer.IsEnabled = false;
            string a = lstView1.SelectedValue.ToString();
            int x = lstView1.SelectedIndex;
            Application.Current.Properties["bb"] = "caizheng:" + x;
            Dispatcher.BeginInvoke(new Action(() => (Application.Current.MainWindow as MainWindow).frame.Navigate(new SpecificMattersdetailsPage())));




        }

        private void lstView1_SelectionChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            //string b = lstView1.SelectedValue.ToString();
            //int q = lstView1.SelectedIndex;
            //Application.Current.Properties["cc"] = "chengguan:" + q;
            //Dispatcher.BeginInvoke(new Action(() => (Application.Current.MainWindow as MainWindow).frame.Navigate(new SpecificMattersdetailsPage())));
            //string c = lstView1.SelectedValue.ToString();
            //int p = lstView1.SelectedIndex;
            //Application.Current.Properties["dd"] = "dangan:" + p;
            //Dispatcher.BeginInvoke(new Action(() => (Application.Current.MainWindow as MainWindow).frame.Navigate(new SpecificMattersdetailsPage())));



        }


    }
}
