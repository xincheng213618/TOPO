using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Media.Imaging;

namespace BaseUtil
{

    /// <summary>
    /// 一些逻辑转换行的代码
    /// </summary>  
    public class Covert
    {
        /// <summary>
        /// BitmapSource To Byte
        /// </summary>
        /// <param name="bitmapSource"></param>
        /// <returns></returns>
        public static byte[] BitmapSourceToByte(BitmapSource bitmapSource)
        {
            byte[] data;
            JpegBitmapEncoder encoder = new JpegBitmapEncoder();
            encoder.Frames.Add(BitmapFrame.Create(bitmapSource));
            using (MemoryStream ms = new MemoryStream())
            {
                encoder.Save(ms);
                data = ms.ToArray();
            }
            return data;
        }
        /// <summary>
        /// BitMap to BitMap
        /// </summary>
        /// <param name="BmpBack"></param>
        /// <returns></returns>
        public static Bitmap BitmapMakeTransparent(Bitmap BmpBack)
        {
            Color TempColor;
            for (int x = 0; x < BmpBack.Width; x++)
            {
                for (int y = 0; y < BmpBack.Height; y++)
                {
                    TempColor = BmpBack.GetPixel(x, y);

                    if (TempColor.R > 210 && TempColor.B > 210 && TempColor.G > 210)//清洗颜色
                    {
                        BmpBack.MakeTransparent(TempColor);
                    }
                }
            }
            return BmpBack;
        }

        /// <summary>
        /// BitmapMakeTransparent
        /// </summary>
        /// <param name="BipBack"></param>
        /// <returns></returns>
        public static BitmapSource BitmapMakeTransparent(BitmapSource BipBack)
        {
            Bitmap BmpBack = GetBitmap(BipBack);
            Color TempColor;
            for (int x = 0; x < BmpBack.Width; x++)
            {
                for (int y = 0; y < BmpBack.Height; y++)
                {
                    TempColor = BmpBack.GetPixel(x, y);

                    if (TempColor.R > 210 && TempColor.B > 210 && TempColor.G > 210)//清洗颜色
                    {
                        BmpBack.MakeTransparent(TempColor);
                    }
                }
            }

            return GetBitmapSource(BmpBack);
        }

        /// <summary>
        /// BitMap
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static Bitmap GetBitmap(BitmapSource source)
        {
            Bitmap bmp = new Bitmap(source.PixelWidth, source.PixelHeight, PixelFormat.Format32bppPArgb);
            BitmapData data = bmp.LockBits(new Rectangle(System.Drawing.Point.Empty, bmp.Size), ImageLockMode.WriteOnly, PixelFormat.Format32bppPArgb);
            source.CopyPixels(Int32Rect.Empty, data.Scan0, data.Height * data.Stride, data.Stride);
            bmp.UnlockBits(data);
            return bmp;
        }
        /// <summary>
        /// Bitmap To BitmapSource
        /// </summary>
        /// <param name="bitmap"></param>
        /// <returns></returns>
        public static BitmapSource GetBitmapSource(Bitmap bitmap)
        {
            return System.Windows.Interop.Imaging.CreateBitmapSourceFromHBitmap(bitmap.GetHbitmap(), IntPtr.Zero, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());
        }


        public static string DicAndUrl(string url, Dictionary<string, object> dic)
        {
            string param = "";
            if (dic != null)
                for (int count = 0; count < dic.Count; count++)
                    param += dic.ElementAt(count).Key + "=" + dic.ElementAt(count).Value.ToString() + "&";
            return url += "?" + param;
        }

        public static BitmapImage ByteToImage(byte[] byData)
        {
            BitmapImage bitmapImage = new BitmapImage();
            bitmapImage.BeginInit();
            bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
            bitmapImage.StreamSource = new MemoryStream(byData);
            bitmapImage.EndInit();
            return bitmapImage;
        }

        public static BitmapImage BitmapToBitmapImage(Bitmap bitmap)
        {
            BitmapImage bitmapImage = new BitmapImage();
            try
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    bitmap.Save(ms, bitmap.RawFormat);
                    bitmapImage.BeginInit();
                    bitmapImage.StreamSource = ms;
                    bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                    bitmapImage.EndInit();
                    bitmapImage.Freeze();
                }
            }
            catch
            {

            }

            return bitmapImage;
        }

        public static bool Base64ToFile(string Base64, string FilePath)
        {
            try
            {
                FileStream fs = new FileStream(FilePath, FileMode.Create);
                byte[] bt = Convert.FromBase64String(Base64);
                fs.Write(bt, 0, bt.Length);
                fs.Close();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static bool Base64ToJpeg(string Base64, string Filepath)
        {
            try
            {
                MemoryStream stream = new MemoryStream(Convert.FromBase64String(Base64));
                System.Drawing.Bitmap Bitmap = new System.Drawing.Bitmap(stream);
                Bitmap.Save(Filepath, System.Drawing.Imaging.ImageFormat.Jpeg);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static BitmapImage FileToImage(string filename)
        {
            BitmapImage bitmapImage = null;
            try
            {
                using (var stream = File.Open(filename, FileMode.Open))
                {
                    byte[] buffer = new byte[stream.Length];
                    stream.Read(buffer, 0, (int)stream.Length);
                    bitmapImage = new BitmapImage();
                    bitmapImage.BeginInit();
                    bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                    bitmapImage.StreamSource = new MemoryStream(buffer);
                    bitmapImage.EndInit();
                }
                return bitmapImage;
            }
            catch
            {
                return null;
            }
        }
        public static string FileToBase64(string FilePath)
        {
            return Convert.ToBase64String(File.ReadAllBytes(FilePath));
        }
        public static byte[] FileToByte(string FilePath)
        {
            return File.ReadAllBytes(FilePath);
        }
    }
}
