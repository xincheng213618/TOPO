
using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Threading;
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

        /// <summary>
        /// 增加一个状态，是否已经拍照
        /// </summary>
        public static bool Shoot = false;

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
                if (Countdown < 45 && process == 3 && Shoot==false)
                {
                    DevMsg?.Invoke(3, EventCode[3]);//请求拍照
                    Shoot = true;
                    process = 3;
                }
            });
        }

        //打开翻页机
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
                        serialPort1.DataReceived += new SerialDataReceivedEventHandler(Post_DataReceived);
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


        //判断是否是命令超时 每一次返回都加一次校研
        private static void ESerialPort_DevMsg(int Code = 0, string data = "")
        {
            Countdown = 59;
            if (Code == 13)
            {
                pageTimer.IsEnabled = false;
            }
            else
            {
                pageTimer.IsEnabled = Code > 0;
            }
        }


        public static Dictionary<int, string> OpenCode = new Dictionary<int, string>()
        {
            { 0,"正常" },
            { -1,"端口异常" },
            { -2,"端口占用/不存在端口" },
        };


        //打开盖章机
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
                        serialPort2.DataReceived += new SerialDataReceivedEventHandler(Post_DataReceived1);
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
                Shoot = false;
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
        /// 错误代码
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
        /// <summary>
        /// 流程状态
        /// </summary>
        private static int process = 0; 


        private static void Post_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            SerialPort serialPort = sender as SerialPort;
            Thread.Sleep(50);
            int bytesread = serialPort.BytesToRead;
            byte[] buff = new byte[bytesread];
            serialPort.Read(buff, 0, bytesread);
            //string str = "";
            //foreach (var a in buff)
            //    str += a.ToString("x2");

            //DevMsg?.Invoke(0, str);
            //状态

            if (buff.Length >= 7)
            {
                if (buff[2] == 6)
                {
                    //故障判定 盖章机
                    n4 = buff[5] == 0;
                    n5 = buff[6] == 0;
                }
                else if (buff[2] == 7)
                {
                    //故障判定，翻页机
                    n1 = buff[5] == 0;
                    n2 = buff[6] == 0;
                    n3 = buff[7] == 0;
                }
                else if (buff[2] == 5 && buff[3] == 1)
                {

                    if (buff[5] == 136)
                    {
                        DevMsg?.Invoke(1, EventCode[1]);//上证成功
                    }
                    else if (buff[5] == 102)
                    {
                        DevMsg?.Invoke(10 + process, EventCode[10 + process]);//返回状态码判断，多次返回增加计数。
                        process += 1;

                    }
                    else if (buff[5] == 17)
                    {
                        Shoot = true;
                        DevMsg?.Invoke(3, EventCode[3]);//请求拍照
                        process = 3;
                    }
                    else if (buff[5] == 85)
                    {
                        DevMsg?.Invoke(4, EventCode[4]);//盖章完成
                    }
                }
            }
        }


        private static void Post_DataReceived1(object sender, SerialDataReceivedEventArgs e)
        {
            SerialPort serialPort = sender as SerialPort;
            Thread.Sleep(50);
            int bytesread = serialPort.BytesToRead;
            byte[] buff = new byte[bytesread];
            serialPort.Read(buff, 0, bytesread);
            //string str = "";
            //foreach (var a in buff)
            //    str += a.ToString("x2");

            //DevMsg?.Invoke(0, str);

            //状态
            if (buff.Length >= 7)
            {
                if (buff[2] == 6)
                {
                    //故障判定 盖章机
                    n4 = buff[5] == 0;
                    n5 = buff[6] == 0;
                }
                else if (buff[2] == 7)
                {
                    //故障判定，翻页机
                    n1 = buff[5] == 0;
                    n2 = buff[6] == 0;
                    n3 = buff[7] == 0;
                }
                else if (buff[2] == 5 && buff[3] == 1)
                {
                    if (buff[5] == 136)
                    {
                        DevMsg?.Invoke(1, EventCode[1]);//上证成功
                    }
                    else if (buff[5] == 102)
                    {
                        DevMsg?.Invoke(10 + process, EventCode[10 + process]);//返回状态码判断，多次返回增加计数。
                        process += 1;
                    }
                    else if (buff[5] == 17)
                    {
                        Shoot = true;
                        process = 3;
                        DevMsg?.Invoke(3, EventCode[3]);//请求拍照
                    }
                    else if (buff[5] == 85)
                    {
                        DevMsg?.Invoke(4, EventCode[4]);//盖章完成
                    }
                }
            }
        }

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
