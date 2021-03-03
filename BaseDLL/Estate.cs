using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BaseDLL
{
	public delegate void Delegate_Msg(object sender, int Code = 0, string data = "");
	public class Estate
    {
		private SerialPort serialPort = new SerialPort();

		public int YES_OR_NO_Paper_ = 1;

		public event Delegate_Msg devMsg;

		public async Task<int> Open(string portname, int baudrate = 115200)
		{
            serialPort = new SerialPort{ PortName = portname,  BaudRate = baudrate};

            try
			{
				serialPort.Open();
				serialPort.Write(new byte[6]{216,160,160,0,1,237}, 0, 6);
			}
			catch
			{
				throw new Exception("打开串口失败。");
			}

			if (await Task<int>.Factory.StartNew(delegate
			{
				for (int i = 0; i < 100; i++)
				{
					Thread.Sleep(10);
					if (serialPort.BytesToRead > 0)
					{
						Thread.Sleep(100);
						byte[] array = new byte[serialPort.BytesToRead];
						serialPort.Read(array, 0, array.Length);
						if (array[0] == 216)
						{
							return 0;
						}
					}
				}
				return -1;
			}) != 0)
			{
				serialPort.Close();
				throw new Exception("握手异常。");  
			}
			serialPort.DataReceived += SerialPort_DataReceived;
			return 0;
		}

		public int Open1(int Port, int BaudRate = 115200)
		{
			try
			{
				serialPort = new SerialPort { PortName = $"COM{Port}", BaudRate = BaudRate };
				serialPort.Open();
				serialPort.Write(new byte[6] { 216, 160, 160, 0, 1, 237 }, 0, 6);

				for (int i = 0; i < 10; i++)
				{
					Thread.Sleep(10);
					if (serialPort.BytesToRead > 0)
					{
						byte[] array = new byte[serialPort.BytesToRead];
						serialPort.Read(array, 0, array.Length);
						if (array[0] == 216)
						{
							serialPort.DataReceived += SerialPort_DataReceived;
							return 0;
                        }
                        else
                        {
							return -1;
						}
                    }
				}
				return -1;
			}
			catch
			{
				return -2;
			}
		}

		public Dictionary<int, string> OpenCode = new Dictionary<int, string>()
		{
			{ 0,"正常" },
			{ -1,"端口异常" },
			{ -2,"端口占用/不存在端口" },
		};


		public void CloseSer()
		{
			serialPort.Close();
			serialPort.Dispose();
		}

		private void SerialPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
		{
			int bytesread = serialPort.BytesToRead;
			byte[] buff = new byte[bytesread];
			serialPort.Read(buff, 0, bytesread);

			string str = "";
			foreach (var a in buff)
			{
				str += a.ToString("x2");
			}

			//devMsg?.Invoke(this,999, buff.Length.ToString() +" " + str);

            if (buff.Length == 6)
            {
                if (buff[4] == 16 && buff[1] == 169)
                {
                    devMsg?.Invoke(this, 10, Event_REL_Code_[10]);//第一页
                }
                if (buff[4] == 16 && buff[0] == 217)
                {
                    YES_OR_NO_Paper_ = 1;
                    devMsg?.Invoke(this, -170, Event_REL_Code_[-170]);
                }
                if (buff[4] == 17 && buff[0] == 217)
                {
                    YES_OR_NO_Paper_ = 0;  
                    devMsg?.Invoke(this, -171, Event_REL_Code_[-171]);
                }
                if (buff[4] == 17 && buff[1] == 225)
                {
                    devMsg?.Invoke(this, -2, Event_REL_Code_[-2]);
                }
                if (buff[4] == 18 && buff[1] == 225)
                {
                    devMsg?.Invoke(this, -3, Event_REL_Code_[-3]);
                }
                if (buff[4] == 19 && buff[1] == 225)
                {
                    devMsg?.Invoke(this, -4, Event_REL_Code_[-4]);
                }
                if (buff[4] == 20 && buff[1] == 225)
                {
                    devMsg?.Invoke(this, -5, Event_REL_Code_[-5]);
                }
                if (buff[4] == 21 && buff[1] == 225)
                {
                    devMsg?.Invoke(this, -6, Event_REL_Code_[-6]);
                }
                if (buff[4] == 23 && buff[1] == 225)
                {
                    devMsg?.Invoke(this, -7, Event_REL_Code_[-7]);
                }
                if (buff[4] == 64 && buff[1] == 225)
                {
                    devMsg?.Invoke(this, -8, Event_REL_Code_[-8]);
                }
                if (buff[4] == 65 && buff[1] == 225)
                {
                    devMsg?.Invoke(this, -9, Event_REL_Code_[-9]);
                }
                if (buff[4] == 66 && buff[1] == 225)
                {
                    devMsg?.Invoke(this, -10, Event_REL_Code_[-10]);
                }
                if (buff[4] == 67 && buff[1] == 225)
                {
                    devMsg?.Invoke(this, -11, Event_REL_Code_[-11]);
                }
                if (buff[4] == 112 && buff[1] == 225)
                {
                    devMsg?.Invoke(this, -12, Event_REL_Code_[-12]);
                }
                if (buff[4] == 113 && buff[1] == 225)
                {
                    devMsg?.Invoke(this, -13, Event_REL_Code_[-13]);
                }
                if (buff[4] == 114 && buff[1] == 225)
                {
                    devMsg?.Invoke(this, -14, Event_REL_Code_[-14]);
                }
                if (buff[4] == 115 && buff[1] == 225)
                {
                    devMsg?.Invoke(this, -21, Event_REL_Code_[-21]);
                }
                if (buff[4] == 144 && buff[1] == 225)
                {
                    devMsg?.Invoke(this, -22, Event_REL_Code_[-22]);
                }


                if (buff[4] == 17 && buff[1] == 169)
                {
                    devMsg?.Invoke(this, 20, Event_REL_Code_[20]);
                }
                if (buff[4] == 18 && buff[1] == 169)
                {
                    devMsg?.Invoke(this, 30, Event_REL_Code_[30]);
                }
                if (buff[4] == 19 && buff[1] == 169)
                {
                    devMsg?.Invoke(this, 40, Event_REL_Code_[40]);
                }
                if (buff[4] == 20 && buff[1] == 169)
                {
                    devMsg?.Invoke(this, 50, Event_REL_Code_[50]);
                }
                if (buff[4] == 21 && buff[1] == 169)
                {
                    devMsg?.Invoke(this, 60, Event_REL_Code_[60]);
                }
                if (buff[4] == 22 && buff[1] == 169)
                {
                    devMsg?.Invoke(this, 70, Event_REL_Code_[70]);
                }
                if (buff[4] == 23 && buff[1] == 169)
                {
                    devMsg?.Invoke(this, 80, Event_REL_Code_[80]);
                }
                if (buff[4] == 24 && buff[1] == 169)
                {
                    devMsg?.Invoke(this, 90, Event_REL_Code_[90]);
                }
                if (buff[4] == 25 && buff[1] == 169)
                {
                    devMsg?.Invoke(this, 100, Event_REL_Code_[100]);
                }
                if (buff[4] == 32 && buff[1] == 169)
                {
                    devMsg?.Invoke(this, 102, Event_REL_Code_[102]);
                }
                if (buff[4] == 33 && buff[1] == 169)
                {
                    devMsg?.Invoke(this, 202, Event_REL_Code_[202]);
                }
                if (buff[4] == 34 && buff[1] == 169)
                {
                    devMsg?.Invoke(this, 302, Event_REL_Code_[302]);
                }
                if (buff[4] == 35 && buff[1] == 169)
                {
                    devMsg?.Invoke(this, 402, Event_REL_Code_[402]);
                }
                if (buff[4] == 36 && buff[1] == 169)
                {
                    devMsg?.Invoke(this, 502, Event_REL_Code_[502]);
                }
                if (buff[4] == 37 && buff[1] == 169)
                {
                    devMsg?.Invoke(this, 602, Event_REL_Code_[602]);
                }
                if (buff[4] == 38 && buff[1] == 169)
                {
                    devMsg?.Invoke(this, 702, Event_REL_Code_[702]);
                }
                if (buff[4] == 39 && buff[1] == 169)
                {
                    devMsg?.Invoke(this, 802, Event_REL_Code_[802]);
                }
                if (buff[4] == 40 && buff[1] == 169)
                {
                    devMsg?.Invoke(this, 902, Event_REL_Code_[902]);
                }
                if (buff[4] == 41 && buff[1] == 169)
                {
                    devMsg?.Invoke(this, 1002, Event_REL_Code_[1002]);
                }
                if (buff[4] == 48 && buff[1] == 169)
                {
                    devMsg?.Invoke(this, 103, Event_REL_Code_[103]);
                }
                if (buff[4] == 49 && buff[1] == 169)
                {
                    devMsg?.Invoke(this, 203, Event_REL_Code_[203]);
                }
                if (buff[4] == 50 && buff[1] == 169)
                {
                    devMsg?.Invoke(this, 303, Event_REL_Code_[303]);
                }
                if (buff[4] == 51 && buff[1] == 169)
                {
                    devMsg?.Invoke(this, 403, Event_REL_Code_[403]);
                }
                if (buff[4] == 52 && buff[1] == 169)
                {
                    devMsg?.Invoke(this, 503, Event_REL_Code_[503]);
                }
                if (buff[4] == 53 && buff[1] == 169)
                {
                    devMsg?.Invoke(this, 603, Event_REL_Code_[603]);
                }
                if (buff[4] == 54 && buff[1] == 169)
                {
                    devMsg?.Invoke(this, 703, Event_REL_Code_[703]);
                }
                if (buff[4] == 55 && buff[1] == 169)
                {
                    devMsg?.Invoke(this, 803, Event_REL_Code_[803]);
                }
                if (buff[4] == 56 && buff[1] == 169)
                {
                    devMsg?.Invoke(this, 903, Event_REL_Code_[903]);
                }
                if (buff[4] == 57 && buff[1] == 169)
                {
                    devMsg?.Invoke(this, 1003, Event_REL_Code_[1003]);
                }
            }

            if (buff.Length > 10)
            {
                if (buff[7] == 169 && buff[10] == 168)
                {
                    devMsg?.Invoke(this, 10, Event_REL_Code_[10]); //第一页
                }
                else if (buff[4] == 16 && buff[1] == 169)
                {
                    devMsg?.Invoke(this, 10, Event_REL_Code_[10]);
                }

                if (buff[10] == 16 && buff[2] == 209 && buff[6] == 217) //有无纸
                {
                    YES_OR_NO_Paper_ = 1;
                    devMsg?.Invoke(this, -170, Event_REL_Code_[-170]);
                }
                if (buff[10] == 17 && buff[2] == 209 && buff[6] == 217)
                {
                    YES_OR_NO_Paper_ = 0;
                    devMsg?.Invoke(this, -171, Event_REL_Code_[-171]);
                }
            }
            if (buff.Length > 10)
            {

            }

            if (buff.Length > 14)
            {
                if (buff[6] == 209)
                {
                    devMsg?.Invoke(this, -15, HexStringToASCII(buff[7]) + HexStringToASCII(buff[8]) + HexStringToASCII(buff[9]) + HexStringToASCII(buff[10]) + HexStringToASCII(buff[11]) + HexStringToASCII(buff[12]) + HexStringToASCII(buff[13]) + HexStringToASCII(buff[14]));
                }
            }


        }

		public static string HexStringToASCII(byte bt)
		{
			string lin = "";
			lin = lin + bt + " ";
			string[] ss = lin.Trim().Split(' ');
			char[] c = new char[ss.Length];
			for (int i = 0; i < c.Length; i++)
			{
				int a = Convert.ToInt32(ss[i]);
				c[i] = Convert.ToChar(a);
			}
			return new string(c);
		}

		public void Process_Print()
		{
			if (YES_OR_NO_Paper_ == 0)
			{
				serialPort.Write(new byte[6]{216,160,162,0,17,237}, 0, 6);
			}
		}

		public void SEL_version()
		{
			serialPort.Write(new byte[6]{216,160,160,0,18,237}, 0, 6);
		}

		public void RESET_()
		{
			serialPort.Write(new byte[6]{216,160,162,0,16,237}, 0, 6);
		}

		public void SEL_P()
		{
			serialPort.Write(new byte[6]{216,161,209,0,20,237}, 0, 6);
		}

		public int Close()
		{
			serialPort.Close();
			serialPort.Dispose();
			serialPort = null;
			return 0;
		}
		public Dictionary<int, string> Event_REL_Code_ = new Dictionary<int, string>()
		{
			{0,"无" },
			{-1,"未知错误" },
			{-2,"纸张未被获起" },
			{-3,"拨杆未进去" },
			{-4,"第1页翻页失败" },
			{-5,"第一页送入打印机失败" },
			{-6,"第一页卡在打印机内" },
			{-7,"盖章不成功" },
			{-8,"第二页抓取失败" },
			{-9,"第二页翻页失败" },
			{-10,"第2页送入打印机失败" },
			{-11,"第2页卡在打印机里" },
			{-12,"第3页抓取失败" },
			{-13,"第3页翻页失败" },
			{-20,"第3页送入打印机失败" }, 
			{-21,"第3页卡在打印机里" },
			{-22,"本子未被取走" },
			{-15 ,"获取版本" },
			{-16,"重启中" },
			{-17,"AIR_P" },
			{-170,"证盒内无证本" },
			{-171,"证盒内有证本" },
			{10,"第一页：进度10%" },
			{20,"第一页：进度20%" },
			{30,"第一页：进度30%" },
			{40,"第一页：进度40%" },
			{50,"第一页：进度50%" },
			{60,"第一页：进度60%" },
			{70,"第一页：进度70%" },
			{80,"第一页：进度80%" },
			{90,"第一页：进度90%" },
			{100,"第一页：进度100%" },
			{102,"第二页：进度10%" },
			{202,"第二页：进度20%" },
			{302,"第二页：进度30%" },
			{402,"第二页：进度40%" },
			{502,"第二页：进度50%" },
			{602,"第二页：进度60%" },
			{702,"第二页：进度70%" },
			{802,"第二页：进度80%" },
			{902,"第二页：进度90%" },
			{1002,"第二页：进度100%" },
			{103,"第三页：进度10%" },
			{203,"第三页：进度20%" },
			{303,"第三页：进度30%" },
			{403,"第三页：进度40%" },
			{503,"第三页：进度50%" },
			{603,"第三页：进度60%" },
			{703,"第三页：进度70%" },
			{803,"第三页：进度80%" },
			{903,"第三页：进度90%" },
			{1003,"第三页：进度100%" }
		};
	}

}
