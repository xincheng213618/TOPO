using BaseUtil;
using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace BaseDLL
{
    public delegate void DelegateMsg(int Code = 0, string data = "");
    public static class ESerialPort
    {
        //串行端口1
        private static SerialPort serialPort1 = new SerialPort();
        //串行端口2
        private static SerialPort serialPort2 = new SerialPort();

        public static event DelegateMsg DevMsg;


        private static DispatcherTimer pageTimer = null;
        private static int Countdown = 59;
        private static void Countdown_timer()
        {
            pageTimer = new DispatcherTimer() { IsEnabled = false, Interval = TimeSpan.FromSeconds(1) };
            pageTimer.Tick += new EventHandler((sender, e) =>
            {
                if (--Countdown <= 0)
                {
                    pageTimer.IsEnabled = false;
                    DevMsg?.Invoke(-1, EventCode[-1]);
                }
            });
        }


        public static int Open1(int Port, int BaudRate = 9600)
        {
            try
            {
                serialPort1 = new SerialPort { PortName = $"COM{Port}", BaudRate = BaudRate };
                serialPort1.Open();
                serialPort1.Write(new byte[7] { 0xFE, 0x01, 0x05, 0x97, 0xBC, 0xCC, 0xE3 }, 0, 7);

                for (int i = 0; i < 16; i++)
                {
                    Thread.Sleep(16);
                    if (serialPort1.BytesToRead > 0)
                    {
                        Countdown_timer();
                        serialPort1.DataReceived += new SerialDataReceivedEventHandler(post_DataReceived);
                        DevMsg += ESerialPort_DevMsg;
                        return 0;
                    }
                }
                return -1;
            }
            catch
            {
                return -2;
            }
        }



        //判断是否是命令超时
        private static void ESerialPort_DevMsg(int Code = 0, string data = "")
        {
            Countdown = 59;

            if (Code == 13)
            {
                pageTimer.IsEnabled = false;
            }
            else
            {
                pageTimer.IsEnabled = Code > 0 ? true : false;
            }
        }


        public static Dictionary<int, string> OpenCode = new Dictionary<int, string>()
        {
            { 0,"正常" },
            { -1,"端口异常" },
            { -2,"端口占用/不存在端口" },
        };


        //打开第二个端口
        public static int Open2(int Port, int BaudRate = 9600)
        {
            try
            {
                serialPort2 = new SerialPort { PortName = $"COM{Port}", BaudRate = BaudRate };
                serialPort2.Open();
                serialPort2.Write(new byte[7] { 0xFE, 0x01, 0x05, 0x97, 0xBC, 0xCC, 0xE3 }, 0, 7);

                for (int i = 0; i < 16; i++)
                {
                    Thread.Sleep(16);
                    if (serialPort2.BytesToRead > 0)
                    {
                        serialPort2.DataReceived += new SerialDataReceivedEventHandler(post_DataReceived1);
                        return 0;
                    }
                }
                return -1;
            }
            catch
            {
                return -2;
            }
        }

        public static void RunStart()
        {
            if (serialPort1.IsOpen)
            {
                Countdown = 59;
                pageTimer.IsEnabled = true;
                process = 0;
                serialPort1.Write(new byte[7] { 0xFE, 0x01, 0x05, 0x97, 0xB2, 0x11, 0x30 }, 0, 7);
            }
        }



        public static void Run1()
        {
            if (serialPort1.IsOpen)
            {
                process = 0;
                serialPort1.Write(new byte[7] { 0xFE, 0x01, 0x05, 0x97, 0xB2, 0x11, 0x30 }, 0, 7);
            }
        }

        public static void Run2()
        {
            if (serialPort2.IsOpen)
                serialPort2.Write(new byte[7] { 0xFE, 0x01, 0x05, 0x97, 0xB2, 0x22, 0x03 }, 0, 7);
        }



        public static int CheckDevice1()
        {
            if (serialPort1.IsOpen)
            {
                serialPort1.Write(new byte[7] { 0xFE, 0x01, 0x05, 0x97, 0xBC, 0xCC, 0xE3 }, 0, 7);
                Thread.Sleep(100);


                if (n1 && n3 && n3)
                {
                    return 0;
                }
                else
                {
                    Countdown = 59;
                    pageTimer.IsEnabled = false;
                    if (!n1 && n2 & n3)
                    {
                        return -1;
                    }
                    else if (n1 && !n2 & n3)
                    {
                        return -2;
                    }
                    else if (n1 && n2 & !n3)
                    {
                        return -3;
                    }
                    else if (!n1 && !n2 & n3)
                    {
                        return -4;
                    }
                    else if (!n1 && n2 & !n3)
                    {
                        return -5;
                    }
                    else if (n1 && !n2 & !n3)
                    {
                        return -6;
                    }
                    else
                    {
                        return -7;
                    }
                }
            }
            else
            {
                return -9;
            }
        }

        public static int CheckDevice2()
        {
            if (serialPort2.IsOpen)
            {
                serialPort2.Write(new byte[7] { 0xFE, 0x01, 0x05, 0x97, 0xBC, 0xCC, 0xE3 }, 0, 7);
                Thread.Sleep(100);

                if (n4 && n5)
                {
                    return 0;
                }
                else
                {
                    Countdown = 59;
                    pageTimer.IsEnabled = false;

                    if (!n4 && n5)
                    {
                        return -11;
                    }
                    else if (n4 && !n5)
                    {
                        return -12;
                    }
                    else
                    {
                        return -13;
                    }
                }
            }
            else
            {
                return -19;
            }
        }


        /// <summary>
        ///状态异常返回  { 0,"正常" };,
        /// { -1,"缺证" },
        ///{ -2,"卡证" },
        ///{ -3,"故障" },
        ///{ -4,"缺证+卡证" },
        ///{ -5,"缺证+故障" },
        ///{ -6,"卡证+故障" },
        ///{ -7,"卡证+故障" },
        ///{ -9,"未连接"},
        ///{ -10,"正常" },
        ///{ -11,"卡纸"},
        ///{ -12,"故障"},
        ///{ -13,"卡纸+故障"},
        ///{ -19,"未连接"} 
        /// </summary>
        public static Dictionary<int, string> CheckDeviceCode = new Dictionary<int, string>()
        {
            { 0,"正常" },
            { -1,"缺证" },
            { -2,"卡证" },
            { -3,"故障" },
            { -4,"缺证+卡证" },
            { -5,"缺证+故障" },
            { -6,"卡证+故障" },
            { -7,"卡证+故障" },
            { -9,"未连接"},
            { -10,"正常" },
            { -11,"卡纸"},
            { -12,"故障"},
            { -13,"卡纸+故障"},
            { -19,"未连接"},
        };


        //状态判定
        private static bool n1 = true;
        private static bool n2 = true;
        private static bool n3 = true;

        private static bool n4 = true;
        private static bool n5 = true;
        private static int process = 0;




        /// <summary>
        /// 数据接收
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void post_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            SerialPort serialPort = sender as SerialPort;
            Thread.Sleep(50);
            int bytesread = serialPort.BytesToRead;
            byte[] buff = new byte[bytesread];
            serialPort.Read(buff, 0, bytesread);
            string str = "";
            foreach (var a in buff)
            {
                str += a.ToString("x2");
            }

            //DevMsg?.Invoke(0, str);
            //状态
            if (buff.Length >= 7 && buff[2] == 7)
            {
                n1 = buff[5] == 0;
                n2 = buff[6] == 0;
                n3 = buff[7] == 0;
            }
            if (buff.Length >= 7 && buff[2] == 6)
            {
                n4 = buff[5] == 0;
                n5 = buff[6] == 0;
            }

            if (buff.Length >= 7 && buff[2] == 5 && buff[3] == 1)
            {
                if (buff[5] == 136)
                {
                    DevMsg?.Invoke(1, EventCode[1]);
                }
                else if (buff[5] == 102)
                {
                    DevMsg?.Invoke(10 + process, EventCode[10 + process]);//返回状态码判断，多次返回增加计数。
                    process += 1;

                }
                else if (buff[5] == 21)
                {
                    DevMsg?.Invoke(3, EventCode[3]);
                }
                else if (buff[5] == 85)
                {
                    DevMsg?.Invoke(4, EventCode[4]);
                }
            }
        }


        private static void post_DataReceived1(object sender, SerialDataReceivedEventArgs e)
        {
            SerialPort serialPort = sender as SerialPort;
            Thread.Sleep(50);
            int bytesread = serialPort.BytesToRead;
            byte[] buff = new byte[bytesread];
            serialPort.Read(buff, 0, bytesread);
            string str = "";
            foreach (var a in buff)
            {
                str += a.ToString("x2");
            }
            //DevMsg?.Invoke(0, str);
            //状态
            if (buff.Length >= 7 && buff[2] == 7)
            {
                n1 = buff[5] == 0;
                n2 = buff[6] == 0;
                n3 = buff[7] == 0;
            }
            if (buff.Length >= 7 && buff[2] == 6)
            {
                n4 = buff[5] == 0;
                n5 = buff[6] == 0;
            }

            if (buff.Length >= 7 && buff[2] == 5 && buff[3] == 1)
            {
                if (buff[5] == 136)
                {
                    DevMsg?.Invoke(1, EventCode[1]);
                }
                else if (buff[5] == 102)
                {
                    DevMsg?.Invoke(10 + process, EventCode[10 + process]);//返回状态码判断，多次返回增加计数。
                    process += 1;

                }
                else if (buff[5] == 17)
                {
                    DevMsg?.Invoke(3, EventCode[3]);
                }
                else if (buff[5] == 85)
                {
                    DevMsg?.Invoke(4, EventCode[4]);
                }
            }
        }
        /// <summary>
        ///打印状态码__________________                     
        ///{ 1,"上证成功" },
        ///{ 2,"证本出" },
        ///{ 3,"请求拍照" },
        ///{ 4,"盖章完成" },
        ///{ 5,"纸张送出" },
        ///{ 10,"第三页打印完毕" },
        ///{ 11,"第二页打印完毕" },
        ///{ 12,"第一页打印完毕" },
        ///{ 13,"证书打印完毕" },
        ///{ -1,"打印异常，请联系工作人员" }
        /// </summary>
        public static Dictionary<int, string> EventCode = new Dictionary<int, string>()
        {
            {1,"上证成功" },
            {2,"证本出" },
            {3,"请求拍照" },
            {4,"盖章完成" },
            {5,"纸张送出" },
            {10,"第三页打印完毕" },
            {11,"第二页打印完毕" },
            {12,"第一页打印完毕" },
            {13,"证书打印完毕" },
            {-1,"打印异常，请联系工作人员" }
        };




    }
}
