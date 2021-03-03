using System;
using System.Data;
using System.Data.SQLite;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using BaseDLL;
using BaseUtil;

namespace SingleVerification
{
    /// <summary>
    /// SQLWindow.xaml 的交互逻辑
    /// </summary>
    public partial class SQLWindow : Window
    {
        public SQLWindow()
        {
            InitializeComponent();
        }
        private void Window_MouseDrag(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void Window_Initialized(object sender, EventArgs e)
        {

        }

        private void Button_Close_Click(object sender, RoutedEventArgs e)
        {
            GetWindow(this).Close();
        }

        private void Button_Min_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        DataSet dt = new DataSet();
        private void IDcardReadSQL(object sender, RoutedEventArgs e)
        {
            SQLiteConnection db = CSQLite.SQLiteConnection("Database");
            db.Open();  
            string where = null;
            string sql = null;

            sql = $"Select * From IDCardInfo ";
            if (IDcardText.Text != "" || NameText.Text != "" || SexComBox.SelectedIndex != 0 || NationText.Text != "")
            {
                where = "where ";
                bool andbool = false;
                if (IDcardText.Text != "")
                {
                    string and = andbool ? "and" : "";
                    where = where + and + $"IDCardNo like '%{IDcardText.Text}%'";
                    andbool = true;
                }
                if (NameText.Text != "")
                {
                    string and = andbool ? "and" : "";
                    where = where + and + $" Name like '%{NameText.Text}%'";
                    andbool = true;
                }
                if (SexComBox.SelectedIndex != 0)
                {

                    string and = andbool ? "and" : "";
                    where = where + and + $" Sex like '%{SexComBox.Items[SexComBox.SelectedIndex].ToString().Replace("System.Windows.Controls.ComboBoxItem: ", "")}%'";
                    andbool = true;
                }
                if (NationText.Text != "")
                {
                    string and = andbool ? "and" : "";
                    where = where + and + $" Nation like '%{NationText.Text}%'";
                    andbool = true;
                }
            }
            sql = sql + where;
            SQLiteCommand SQLiteCommand = new SQLiteCommand(sql, db);
            SQLiteDataAdapter adap = new SQLiteDataAdapter(SQLiteCommand);
            dt = new DataSet();
            adap.Fill(dt);

            //dt.Tables[0].Rows.RemoveAt(dt.Tables[0].Rows.Count);

            dataGrid.ItemsSource = dt.Tables[0].DefaultView;
            dataGrid.Columns[10].Visibility = Visibility.Hidden;
            dataGrid.Columns[11].Visibility = Visibility.Hidden;
            dataGrid.Columns[12].Visibility = Visibility.Hidden;

            db.Close();
        }
        private BitmapImage image1;
        private BitmapImage image2;

        IDCardData iDCardData = new IDCardData();

        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Single.Visibility = Visibility = Visibility;
            iDCardData.Name = (string)dt.Tables[0].Rows[dataGrid.SelectedIndex]["Name"];
            iDCardData.IDCardNo = (string)dt.Tables[0].Rows[dataGrid.SelectedIndex]["IDCardNo"];
            iDCardData.Address = (string)dt.Tables[0].Rows[dataGrid.SelectedIndex]["Address"];
            iDCardData.Born = (string)dt.Tables[0].Rows[dataGrid.SelectedIndex]["Born"];
            iDCardData.GrantDept = (string)dt.Tables[0].Rows[dataGrid.SelectedIndex]["GrantDept"];
            iDCardData.Sex = (string)dt.Tables[0].Rows[dataGrid.SelectedIndex]["Sex"];
            iDCardData.UserLifeBegin = (string)dt.Tables[0].Rows[dataGrid.SelectedIndex]["UserLifeBegin"];
            iDCardData.UserLifeEnd = (string)dt.Tables[0].Rows[dataGrid.SelectedIndex]["UserLifeEnd"];
            iDCardData.Nation = (string)dt.Tables[0].Rows[dataGrid.SelectedIndex]["Nation"];

            PDFGenerate.Visibility = Visibility.Visible;
            try
            {
                object a = dt.Tables[0].Rows[dataGrid.SelectedIndex]["PhotoFile"];
                object b = dt.Tables[0].Rows[dataGrid.SelectedIndex]["PhotoFile1"];
                if (a.ToString().Length != 0)
                {
                    image1 = Covert.ByteToImage((byte[])a);
                    idcardPicture.Source = image1;
                    idcardPicture.Visibility = Visibility.Visible;
                }
                else
                {
                    image1 = null;
                    idcardPicture.Visibility = Visibility.Hidden;

                }

                if (b.ToString().Length != 0)
                {
                    image2 = Covert.ByteToImage((byte[])b);
                    Picture2.Source = image2;
                    Picture2.Visibility = Visibility.Visible;
                }
                else
                {
                    image2 = null;
                    Picture2.Visibility = Visibility.Hidden;
                }

            }
            catch
            {
                image1 = null;
                image2 = null;
                idcardPicture.Visibility = Visibility.Hidden;
                Picture2.Visibility = Visibility.Hidden;
            }

        }

        private void PictureShow(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            switch (button.Tag.ToString())
            {
                case "PictureBig":
                    PictureBigBorder.Visibility = Visibility.Hidden;
                    break;
                case "Picture1":
                    PictureBig.Source = image1;
                    PictureBigBorder.Visibility = Visibility.Visible;
                    break;
                case "Picture2":
                    PictureBig.Source = image2;
                    PictureBigBorder.Visibility = Visibility.Visible;
                    break;
                default:
                    break;
            }
        }



        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string FilePath = XFile.SaveFileDialog(iDCardData.IDCardNo + "_" + new DateTimeOffset(DateTime.UtcNow).ToUnixTimeSeconds(), "PDF（*.pdf）|*.pdf");
            if (FilePath != null)
            {
                PDF.DrawPDF(FilePath, iDCardData, image1, image2);
            }
        }
    }
}
