using BaseDLL;
using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace EXC
{
    /// <summary>
    /// PrintUtilPage.xaml 的交互逻辑
    /// </summary>
    public partial class PrintUtilWindow : Window
    {
        private int PrintPageNo;
        public PrintUtilWindow(int PrintPageNo)
        {
           
            this.PrintPageNo = PrintPageNo;
            InitializeComponent();
        }

        private void Page_Initialized(object sender, EventArgs e)
        {
            timer = new Timer(_ => Dispatcher.BeginInvoke(new Action(async () => await TimeRunAsync(PrintPageNo, true))), null, 0, 500);
        }

        Timer timer; //打印用
        int Ostatue = 0;
        bool Print = false;
        int PrintTimeNo = 0;//防止出现什么意外导致迟迟卡死在此页面，一定次数之后直接判定超时

        static PrintDocument print = new PrintDocument();

        private async Task TimeRunAsync(int PageAllNum, bool PrintMode = false)
        {
            int Nstatue = PrintStatus.PrinterStatue(print.PrinterSettings.PrinterName);
            Print = Ostatue != 0 && Nstatue == 0;
            Ostatue = Ostatue == Nstatue ? Ostatue : Nstatue;
            PopTips.Text = PrintStatus.PrinterStatusCodes.ContainsKey(Nstatue) ? PrintStatus.PrinterStatusCodes[Nstatue].ToString() : $"{Nstatue}";
            PrintTimeNo += 1;


            if (Print)
            {
                timer.Dispose();
                PopTips.Text = "PDF已经发送到打印机";
                await Task.Delay(2 * 1000);
                PopTips.Text = "打印机正在打印";
                await Task.Delay(PageAllNum * 1000);
                PopTips.Text = "打印完成";
                await Task.Delay(3 * 1000);
                PopTips.Text = "";
                Print = false;
                Ostatue = 0;
                Close();
            }


            if (PrintTimeNo > 20+PageAllNum)
            {
                Ostatue = 0;
                Close();
            }
        }
    }
}
