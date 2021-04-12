using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ECRLibrary;
using System.Security.Cryptography;

namespace ECRService
{
    /// <summary>
    /// UserInfoPage.xaml 的交互逻辑
    /// </summary>
    public partial class UserInfoPage : Page
    {
        private const int TIMEOUT = 3;
        private int countdown = TIMEOUT;
        private DispatcherTimer pageTimer = null;

        public UserInfoPage()
        {
            InitializeComponent();
            pageTimer = new DispatcherTimer()
            {
                IsEnabled = false,
                Interval = TimeSpan.FromSeconds(1),
            };
            pageTimer.Tick += new EventHandler((sender, e) =>
            {
                if (--countdown <= 0)
                {
                    pageTimer.IsEnabled = false;
                    Dispatcher.BeginInvoke(new Action(() => (Application.Current.MainWindow as MainWindow).frame.Navigate(new CameraPage())));
                }
            });
            pageTimer.IsEnabled = true;
        }

        private bool IDCardIsValid(IDCardInfo idCardInfo)
        {
            string date = DateTime.Now.Date.ToString("yyyyMMdd");
            if (date.CompareTo(idCardInfo.valid_begin) < 0 || date.CompareTo(idCardInfo.valid_end) > 0)
                throw new Exception("身份证已过期。");
            return true;
        }

        private BitmapImage MemoryStreamBitmapImage(string filename)
        {
            BitmapImage bitmapImage = null;

            try
            {
                using (var stream = File.Open(filename, FileMode.Open))
                {
                    byte[] buffer = new byte[stream.Length];
                    stream.Read(buffer, 0, (int)stream.Length);
                    bitmapImage = new BitmapImage();
                    bitmapImage.BeginInit();
                    bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                    bitmapImage.StreamSource = new MemoryStream(buffer);
                    bitmapImage.EndInit();
                }
            }
            catch (Exception ex)
            {
                bitmapImage = null;
            }

            return bitmapImage;
        }

        private void Page_Initialized(object sender, EventArgs e)
        {
            /*
            IDCardInfo card = new IDCardInfo();
            card.name = "楼姗姗";
            card.birth = "1983年12月12日";
            card.sex = "2";
            card.no = "322123456709876789";
            card.org = "南京公安局";
            card.valid_begin = "2012年12月12日";
            card.valid_end = "2032年12月12日";
            ServiceData.idCardInfo = card;
            */

            byte[] buffer;
            using (var stream = File.Open($"{ServiceData.idCardInfo.no}.jpeg", FileMode.Open))
            {
                buffer = new byte[stream.Length];
                stream.Read(buffer, 0, (int)stream.Length);
            }
            string pic = Encoding.Default.GetString(buffer);
            string content = GetMD5Str(ServiceData.idCardInfo.name+","+ ServiceData.idCardInfo.sex+","+ ServiceData.idCardInfo.no+"," + pic);


            
            name.Content = "*" + ServiceData.idCardInfo.name.Substring(1);
            cardNo.Content = ServiceData.idCardInfo.no.Substring(0, 10) + "******" + ServiceData.idCardInfo.no.Substring(16);
            idcardPicture.Source = MemoryStreamBitmapImage($"{ServiceData.idCardInfo.no}.jpeg");
            sex.Content = ServiceData.idCardInfo.sex.Equals("2") ? "女" : "男";
            bir.Content = ServiceData.idCardInfo.birth;
            placesOfIssue.Content = ServiceData.idCardInfo.org;
            validDate.Content = ServiceData.idCardInfo.valid_begin + " - " + ServiceData.idCardInfo.valid_end;
            
            media.Source = new Uri(Directory.GetCurrentDirectory() + "/Media/7、取走身份证和报告.mp3", UriKind.Absolute);
            media.Position = TimeSpan.Zero;
            media.Play();
        }


        public static string GetMD5Str(string toCryString)
        {
            MD5CryptoServiceProvider hashmd5;
            hashmd5 = new MD5CryptoServiceProvider();
            return BitConverter.ToString(hashmd5.ComputeHash(Encoding.Default.GetBytes(toCryString)));
        }
    }
}
