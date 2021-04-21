using BaseUtil;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace REC
{
    /// <summary>
    /// Function4.xaml 的交互逻辑
    /// </summary>
    public partial class FunctionPage4 : Page
    {
        public FunctionPage4()
        {
            InitializeComponent();
        }
        private void Page_Initialized(object sender, EventArgs e)
        {
            textBoxes = new List<TextBox> { TexBox0, TexBox1, TexBox2, TexBox3, TexBox4, TexBox5, TexBox6, TexBox7, TexBox8, TexBox9, TexBox10 };
            if (Global.Related.Fix_OCR_Data.Length == 11)
            {
                for (int i = 0; i < 11; i++)
                {
                    textBoxes[i].Text = Global.Related.Fix_OCR_Data.Substring(i,1);
                }
            }
            else
            {
                Global.Related.Fix_OCR_Data = "320";
                for (int i = 0; i < 3; i++)
                {
                    textBoxes[i].Text = Global.Related.Fix_OCR_Data.Substring(i, 1);
                }
            }
            try
            {
                ImageOCR.Source = new BitmapImage(new Uri(Environment.CurrentDirectory+ "\\Temp\\ocr_result1.jpg")); ;
            }
            catch(Exception ex)
            {

            }

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            switch (button.Tag)
            {
                case "BackYinZhiHao":
                    
                    string YinZhihao = "" ;
                    foreach (var item in textBoxes)
                    {
                        YinZhihao += item.Text;
                    }

                    Thread thread1 = new Thread(() => YinZhihao_Upload(YinZhihao))
                    {
                        IsBackground = true
                    };
                    thread1.Start();
                    break;
                case "HomePage":
                    Content = new HomePage();
                    Pages();
                    break;
            }
        }
        private void YinZhihao_Upload(string YinZhihao)
        {
            string response = Http.OCR_Upload(Global.Related.RECData.SLBH, Global.Related.RECData.QLRZJH, DateTime.Now.ToString("yyyyMMdd"), Global.Related.RECData.ZSID, YinZhihao);
            Dispatcher.BeginInvoke(new Action(() => GetPhrase(response)));
        }

        private void GetPhrase(string response)
        {
            if (response != null)
            {
                try
                {
                    JObject RECResponse = (JObject)JsonConvert.DeserializeObject(response);
                    string code = (string)RECResponse.GetValue("code");
                    if (code.Equals("0"))
                    {
                        MessageBox.Show("回填成功");
                    }
                    else
                    {
                        MessageBox.Show("回填失败： " + (string)RECResponse.GetValue("msg"));
                    }
                }
                catch
                {
                    Content = new ErrorPage("印制号回传接口解析错误,请联系C13号窗口工作人员");
                    Pages();
                }
            }
            else
            {
                Content = new ErrorPage("印制号回传接口连接错误，请联系C13号窗口工作人员");
                Pages();
            }
        }



        private void Pages()
        {
            Dispatcher.BeginInvoke(new Action(() => (Application.Current.MainWindow as MainWindow).frame.Navigate(Content)));
        }

        private void TexBox0_FocusableChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            TexBox0.Text = "";
        }


        List<TextBox> textBoxes;
        private void TextChanged(object sender, TextChangedEventArgs e)
        {

            TextBox textBox = sender as TextBox;
            int num = int.Parse(textBox.Tag.ToString())+1;
            if (textBox.Text.Length == 1&& textBoxes != null && num < textBoxes.Count)
            {
                textBoxes[num].Text = "";
                textBoxes[num].Focus();
            }
            if (textBox.Text.Length == 0 && textBoxes != null && num > 1)
            {
                textBoxes[num - 2].Focus();
                //textBoxes[num - 2].Select(1,1);
            }

            if (textBox.Text.Length == 2)
            {
                textBox.Text = textBox.Text.Substring(1, 1);
                textBox.Select(textBox.Text.Length, 0);

                //textBox.Focus();
            }
        }

        private void TexBox0_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            textBox.Select(textBox.Text.Length, 0);
        }
    }
}
