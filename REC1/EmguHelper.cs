using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Util;
using Emgu.CV.Structure;

namespace REC
{
    public static class EmguHelper
    {
        public static void PreOCR(string Filename)
        {
            Mat mat = new Mat(@Filename, ImreadModes.Color);
            Rectangle rectangle = new Rectangle(int.Parse(Global.OcrRegion[0]), int.Parse(Global.OcrRegion[1]), int.Parse(Global.OcrRegion[2]), int.Parse(Global.OcrRegion[3]));

            Mat mat1 = Rotate(new Mat(mat, rectangle), -90);  //旋转加切割
            mat1.Save("Temp\\mat1.jpg");

            //纯色不做通道分离
            ////通道分离
            VectorOfMat channels = new VectorOfMat();
            CvInvoke.Split(mat1, channels);
            InputOutputArray mix_channel = channels.GetInputOutputArray(); //获得数组

            Mat r = mix_channel.GetMat(2);
            //r.Save("r.jpg");

            Mat theredshold = new Mat();
            //二值化处理
            CvInvoke.Threshold(r, theredshold, 135, 255, ThresholdType.Binary);

            //theredshold.Save("Temp\\thereshold.jpg");
            //去除噪点
            Mat r2 = new Mat();
            CvInvoke.MedianBlur(theredshold, r2, 3);


            //腐蚀处理
            Mat r3 = new Mat();
            Mat element = CvInvoke.GetStructuringElement(ElementShape.Rectangle, new Size(2, 2), new Point(-1, -1));
            CvInvoke.Erode(r2, r3, element, new Point(-1, -1), 1, BorderType.Default, new MCvScalar(128, 128, 128));

            r3.Save("Temp\\ocr_result.jpg");

        }


        public static Mat Rotate(Mat mat, double degree)
        {
            Matrix<float> rotate_matrix = new Matrix<float>(2, 3);
            double angle = degree * Math.PI / 180; // 弧度  
            int width = mat.Width;
            int height = mat.Height;
            int width_rotate = Convert.ToInt32(height * Math.Abs(Math.Sin(angle)) + width * Math.Abs(Math.Cos(angle)));
            int height_rotate = Convert.ToInt32(width * Math.Abs(Math.Sin(angle)) + height * Math.Abs(Math.Cos(angle)));

            CvInvoke.GetRotationMatrix2D(new PointF(mat.Width / 2, mat.Height / 2), degree, 1, rotate_matrix);//以特定的参数获取旋转矩阵。

            rotate_matrix[0, 2] += (width_rotate - width) / 2;
            rotate_matrix[1, 2] += (height_rotate - height) / 2;

            Mat rotate_mat = new Mat();//创建矩阵， 用于存储目标图像（处理后的图像）。

            CvInvoke.WarpAffine(mat, rotate_mat, rotate_matrix, new System.Drawing.Size(width_rotate, height_rotate), Inter.Linear, Warp.Default, BorderType.Transparent, new MCvScalar(0d, 0d, 0d, 0d));// 采 用 仿射获取目标图像。

            return rotate_mat;
        }
    }
}
