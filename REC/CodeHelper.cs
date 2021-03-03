using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZXing;
using ZXing.QrCode;

namespace REC
{
    public class CodeHelper
    {

        //生成二维码
        public static bool QRcode(string text,string path)
        {
            try
            {
                BarcodeWriter writer = new BarcodeWriter();
                writer.Format = BarcodeFormat.QR_CODE;
                QrCodeEncodingOptions options = new QrCodeEncodingOptions();
                options.DisableECI = true;
                //设置内容编码
                options.CharacterSet = "UTF-8";
                options.QrVersion = 9;
                //将传来的值赋给二维码的宽度和高度
                //options.Width = Convert.ToInt32(width);
                //options.Height = Convert.ToInt32(height);
                options.Width = 270;
                options.Height = 270;
                //设置二维码的边距,单位不是固定像素
                options.Margin = 1;
                writer.Options = options;

                Bitmap map = writer.Write(text);
                string di = text + DateTime.Now.ToString("yyyyMMddHHmmss") + ".png";
                //二维码会显示在桌面(你也想显示在桌面的话,要改一下路径)
                map.Save(path, ImageFormat.Png);
                map.Dispose();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }

}
