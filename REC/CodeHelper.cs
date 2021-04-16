using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZXing;
using ZXing.QrCode;

namespace REC
{
    public class CodeHelper
    {

        //生成二维码流，不保存成文件
        public static Stream QRcode(string text)
        {
            QrCodeEncodingOptions options = new QrCodeEncodingOptions
            {
                DisableECI = true,
                //设置内容编码
                CharacterSet = "UTF-8",
                QrVersion = 9,
                //将传来的值赋给二维码的宽度和高度
                //options.Width = Convert.ToInt32(width);
                //options.Height = Convert.ToInt32(height);
                Width = 270,
                Height = 270,
                //设置二维码的边距,单位不是固定像素
                Margin = 1
            };
            BarcodeWriter writer = new BarcodeWriter
            {
                Format = BarcodeFormat.QR_CODE,
                Options = options
            };

            //Bitmap map = writer.Write(text);
            //string di = text + DateTime.Now.ToString("yyyyMMddHHmmss") + ".png";
            //二维码会显示在桌面(你也想显示在桌面的话, 要改一下路径)
            //map.Save(path, ImageFormat.Png);
            //map.Dispose();

            Stream stream = new MemoryStream();
            writer.Write(text).Save(stream, ImageFormat.Png);
            stream.Position = 0;

            return stream;
        }
    }

}
