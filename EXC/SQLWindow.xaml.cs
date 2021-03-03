using GemBox.Spreadsheet;
using System;
using System.Data;
using System.Data.SQLite;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using EXCResources;
using Background;
using System.Windows.Media;

namespace EXC
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
            //cmysql.IDcardRead();
            SQLiteConnection db = CSQLite.SQLiteConnection("Database");
            db.Open();
            string where = null;
            string sql = null;

            sql = $"Select id,CreaTime,Name,Sex,IDCardNo,Sucessed,Similarity,Nation,Address,GrantDept,UserLifeBegin,UserLifeEnd,PhotoFile,PhotoFile1,PhotoFile2 From IDCardInfo ";
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

        private void dataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {

                object a = dt.Tables[0].Rows[dataGrid.SelectedIndex]["PhotoFile"];
                object b = dt.Tables[0].Rows[dataGrid.SelectedIndex]["PhotoFile1"];
                if (a.ToString().Length!=0)
                {
                    image1 = XCovert.ByteToImage((byte[])a);
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
                    image2 = XCovert.ByteToImage((byte[])b);
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

        Window UserControl1;
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
                    UserControl1 = new PhotoWindows(new ImageBrush(image1));
                    UserControl1.Show();
                    PictureBigBorder.Visibility = Visibility.Visible;
                    break;
                case "Picture2":
                    PictureBig.Source = image2;
                    UserControl1 = new PhotoWindows(new ImageBrush(image2));
                    UserControl1.Show();
                    PictureBigBorder.Visibility = Visibility.Visible;
                    break;
                default:
                    break;
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            try
            {
                SpreadsheetInfo.SetLicense("FREE-LIMITED-KEY");
                var workbook = new ExcelFile();
                var worksheet = workbook.Worksheets.Add("IDCardInfo");
                //int columns = dt.Tables[0].Columns.Count - 1;
                int columns = 11;
                int rows = dt.Tables[0].Rows.Count;

                for (int j = 0; j < columns; j++)
                {
                    worksheet.Cells[0, j].Value = dataGrid.Columns[j].Header;
                }

                for (int i = 1; i < rows; i++)
                {
                    for (int j = 0; j < columns; j++)
                    {
                        worksheet.Cells[i, j].Value = dt.Tables[0].Rows[i].ItemArray[j];
                    }
                }
                workbook.Save(XFile.SaveFileDialog($"{DateTime.Now.ToString("yyyy年MM月dd日")}.xlsx"));
                MessageBox.Show("数据成功保存");
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
                MessageBox.Show(ex.Message);
            }
        }
    }
}
