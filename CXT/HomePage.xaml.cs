using Background;
using BaseUtil;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace XinHua
{
    /// <summary>
    /// HomePage.xaml 的交互逻辑
    /// </summary>
    public partial class HomePage : Page
    {
        public HomePage()
        {
            InitializeComponent();
        }

        public HomePage(string Msg)
        {
            InitializeComponent();
            PopAlert(Msg);
        }
        private async void PopAlert(string Msg)
        {
            Log.Write("HomePage:" + Msg);
            PopTips.Text = Msg;
            Pop.Visibility = Visibility.Visible;
            await Task.Delay(4000);
            Pop.Visibility = Visibility.Hidden;
        }
        private void Page_Initialized(object sender, EventArgs e)
        {

            Global.PageType = null;
            List<Border> List = new List<Border>() { };
            for (int i = 0; i < List.Count; i++)
                List[i].Visibility = Visibility.Hidden;


            BackgroundItem.Kind = false;
            BackgroundItem.Video.Files = Directory.GetFiles(Directory.GetCurrentDirectory() + "\\Background\\");

            BackgroundItem.Picture.Files = Directory.GetFiles(Directory.GetCurrentDirectory() + "\\Background\\");
            BackgroundItem.Picture.Auto = true;
            BackgroundItem.Picture.Intervaltime = 5000;// 千分秒
            App.backgroundWindow.Updated();

            CompanyInfo.Initialized();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            Global.PageType = button.Tag.ToString();
            Log.Write(Global.PageType);
            switch (Global.PageType)
            {
                case "QiYeXinXi":              
                    Content = new SearchPage();
                    break;
                case "NaShuiXinYongA":
                    Content = new SearchPage();
                    //PopAlert("暂未开放此功能", 3);
                    break;
                case "ShuiShouWeiFa":
                    //PopAlert("暂未开放此功能", 3);
                    Content = new SearchPage();
                    break;
                case "ShiXinRen":
                    //PopAlert("暂未开放此功能", 3);
                    Content = new SearchPage();
                    break;
                case "NanJingReport":
                    Content = new Report();
                    break;
                case "NanJingGRReport":
                    Content = new Report();
                    break;
                default:
                    break;
            }
            Pages();
        }

        private void POP_Button(object sender, RoutedEventArgs e)
        {
            Pop.Visibility = Visibility.Hidden;
        }
        private void Pages()
        {
            Dispatcher.BeginInvoke(new Action(() => (Application.Current.MainWindow as MainWindow).frame.Navigate(Content)));
        }
        ///
        /// 完成缓冲效果
        ///
        /// 起始位置
        /// 目标位置
        /// 加速加速度
        /// 减速加速度
        /// 持续时间
        private void DoMove(DependencyProperty dp, double to, double ar, double dr, double duration)
        {
            DoubleAnimation doubleAnimation = new DoubleAnimation();//创建双精度动画对象

            doubleAnimation.To = to;//设置动画的结束值
            doubleAnimation.Duration = TimeSpan.FromSeconds(duration);//设置动画时间线长度
            doubleAnimation.AccelerationRatio = ar;//动画加速
            doubleAnimation.DecelerationRatio = dr;//动画减速
            doubleAnimation.FillBehavior = FillBehavior.HoldEnd;//设置动画完成后执行的操作

            grdTransfer.BeginAnimation(dp, doubleAnimation);//设置动画应用的属性并启动动画
        }



        private double pressedX;

        ///
        /// 点击鼠标，记录鼠标单击的位置
        ///
        ///
        ///
        private void grdTest_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            //获得鼠标点击的X坐标
            pressedX = e.GetPosition(cvsGround).X;

        }



        //鼠标释放时的操作

        private void grdTest_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            double transferLeft = Convert.ToDouble(grdTransfer.GetValue(Canvas.LeftProperty));

            if (transferLeft > 0)
            {
                transferLeft = 0;
            }
            if (this.Width - transferLeft > cvsGround.Width)
            {
                transferLeft = this.Width - cvsGround.Width;
            }

            //获得鼠标释放时的位置


            double releasedX = e.GetPosition(cvsGround).X;

            //获得距离间隔
            double interval = releasedX - pressedX;
            pressedX = 0;
            //计算出传送带要的目标位置
            double to = transferLeft + interval;
            //移动



            // btn1.Content = transferLeft.ToString() + " " + to.ToString();
            DoMove(Canvas.LeftProperty, to, 0.1, 0.5, 0.5);
        }

        private void grdTransfer_PreviewMouseMove(object sender, MouseEventArgs e)
        {

            //double pressedX1 = e.GetPosition(cvsGround).X;
            ////计算相对位置
            //double diffOffsetX = pressedX1 - pressedX;
            //DoMove(Canvas.LeftProperty, diffOffsetX, 0.1, 0.5, 0.5);
        }
    }
}
