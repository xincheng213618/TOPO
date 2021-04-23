using System;
using System.Collections.ObjectModel;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using BaseDLL;
using BaseUtil;

namespace EXC
{
    /// <summary>
    /// PersonalCertificate.xaml 的交互逻辑
    /// </summary>
    public partial class PersonalCertificate : Page
    {
        IDCardData IDCardData;
        public PersonalCertificate()
        {
            InitializeComponent();
        }
        public PersonalCertificate( IDCardData IDCardData)
        {
            this.IDCardData = IDCardData;
            InitializeComponent();
        }
        private void Page_Initialized(object sender, EventArgs e)
        {
            WaitShow.Visibility = Visibility.Visible;
            Thread worker = new Thread(() => RequestUrl());
            worker.IsBackground = true;
            worker.Start();
        }
        private void RequestUrl()
        {
            string response = Http.HeFei.GettypeConfigId();
            Dispatcher.BeginInvoke(new Action(() => Pharse(response)));
        }
        private ObservableCollection<HeiFeiPeopleTypeItem> PeopleTypeItem = new ObservableCollection<HeiFeiPeopleTypeItem>();
        int ListNo = 0;
        private void Pharse(string response)
        {
            WaitShow.Visibility = Visibility.Hidden;
            try
            {
                JObject @object = (JObject)JsonConvert.DeserializeObject(response);
                bool falg = (bool)@object.GetValue("flag");
                if (falg)
                {
                    JArray jArray = (JArray)@object["rows"];
                    if (jArray != null && !jArray.Equals("") && jArray.Count != 0)
                    {
                        foreach (JObject result in jArray)
                        {
                            HeiFeiPeopleTypeItem item = new HeiFeiPeopleTypeItem();
                            item.ListNo = ++ListNo;
                            item.TypeConfigId = (string)result.GetValue("typeConfigId");
                            item.TypeName = (string)result.GetValue("chineseName");
                            PeopleTypeItem.Add(item);
                        }
                    }
                    else
                    {
                        Content = new HomePage("接口内容为空");
                        Pages();
                    }
                }
                else
                {
                    string Msg = (string)@object.GetValue("msg");
                    Content = new HomePage(Msg);
                }

            }
            catch
            {

                Content = new HomePage("接口解析错误，请联系开发人员");
                Pages();
            }
        }

        private void Pages()
        {
            Dispatcher.BeginInvoke(new Action(() => (Application.Current.MainWindow as MainWindow).frame.Navigate(Content)));
        }

        private void Home_Click(object sender, RoutedEventArgs e)
        {
            Content = new HomePage();
            Pages();
        }

        private void PageButton_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            Content = new SearchHeiFei(PeopleTypeItem[int.Parse(button.Tag.ToString())].TypeConfigId);
            Pages();
        }
    }

    public class HeiFeiPeopleTypeItem : ListItem
    {
        public string TypeConfigId { get; set; }
        public string TypeName { get; set; }

    }
}
