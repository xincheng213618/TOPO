using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;
using System.Windows.Media.Imaging;

namespace ECRService
{
    internal class RecognitionException : Exception
    {
        public RecognitionException()
        {

        }
    }

    /// <summary>
    /// RecognitionPage.xaml 的交互逻辑
    /// </summary>
    public partial class RecognitionPage : Page
    {
        public RecognitionPage()
        {
            InitializeComponent();
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
                Console.Write(ex);
                bitmapImage = null;
            }

            return bitmapImage;
        }

        public float FaceRecognition(string fileName)
        {
            float score = 0, highScore = 0;

            for (int i = 0; i < GlobalData.configData.NumberOfPhotos; i++)
            {
                //score = FaceControlKeeper.GetFaceControlKeeper().FaceControl.Verify(Directory.GetCurrentDirectory() + $"\\{i}_1.jpg", Directory.GetCurrentDirectory() + "\\" + fileName);
                // Logging.Log(LOGLEVEL.INFO, "照片路径：" + Directory.GetCurrentDirectory() + $"\\{i}_1.jpg");
                //Logging.Log(LOGLEVEL.INFO, "身份证路径：" + Directory.GetCurrentDirectory() + "\\" + fileName);
                score = AmLivingBodyApi.AmVerify(Directory.GetCurrentDirectory() + $"\\{i}_1.jpg", Directory.GetCurrentDirectory() + "\\" + fileName);
                // Logging.Log(LOGLEVEL.INFO, "比对分数_"+ i+ ":" + score);
                if (score < 0)
                    throw new Exception("照片比对失败：" + score);

                if (score > highScore)
                    highScore = score;
            }

            return highScore;
        }



        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            idcardPicture.Source = MemoryStreamBitmapImage($"{ServiceData.idCardInfo.no}.jpeg");
            scenePicture.Source = MemoryStreamBitmapImage($"{GlobalData.configData.NumberOfPhotos - 1}_1.jpg");

            Dispatcher.BeginInvoke(new Action(() =>
            {
                try
                {
                    float score = FaceRecognition($"{ServiceData.idCardInfo.no}.bmp");
                    if (score * 100 < GlobalData.configData.AdmissibleScore)
                    {
                        throw new RecognitionException();
                    }

                    if (ServiceData.type == 1)
                    {
                        Dispatcher.BeginInvoke(new Action(() => (Application.Current.MainWindow as MainWindow).frame.Navigate(new ReportListPage())));
                    }
                    else if (ServiceData.type == 2)
                    {
                        Dispatcher.BeginInvoke(new Action(() => (Application.Current.MainWindow as MainWindow).frame.Navigate(new QuarantineCreditQueryPage())));
                    }
                    else if (ServiceData.type == 3)
                    {
                        Dispatcher.BeginInvoke(new Action(() => (Application.Current.MainWindow as MainWindow).frame.Navigate(new RecordViewPage())));
                    }
                }
                catch (RecognitionException ex)
                {
                    if (++ServiceData.authenticationCount < GlobalData.configData.FaceRecognitionTimes)
                    {
                        hintLabel.Content = "比对失败，请重试！";
                        DispatcherTimer timer = new DispatcherTimer()
                        {
                            IsEnabled = false,
                            Interval = TimeSpan.FromSeconds(3),
                        };
                        timer.Tick += new EventHandler((s, ev) =>
                        {
                            timer.IsEnabled = false;
                            Dispatcher.BeginInvoke(new Action(() => (Application.Current.MainWindow as MainWindow).frame.Navigate(new CameraPage())));
                        });
                        timer.IsEnabled = true;
                    }
                    else
                    {
                        Dispatcher.BeginInvoke(new Action(() => (Application.Current.MainWindow as MainWindow).frame.Navigate(new MessagePage("身份验证失败。"))));
                    }
                }
                catch (Exception ex)
                {

                    Dispatcher.BeginInvoke(new Action(() => (Application.Current.MainWindow as MainWindow).frame.Navigate(new MessagePage(ex.Message))));
                }
            }));

            media.Source = new Uri(Directory.GetCurrentDirectory() + "/Media/5、比对中.mp3", UriKind.Absolute);
            media.Position = TimeSpan.Zero;
            media.Play();
        }
    }
}
