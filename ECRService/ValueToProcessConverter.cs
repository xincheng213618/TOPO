using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;


namespace ECRService
{
    class ValueToProcessConverter : IValueConverter
    {
        private const double Thickness = 8;
        private const double Padding = 0;
        private const double Offset = 4;
        private const double WarnValue = 10;
        private const int TextFontSize = 26;
        private static readonly Typeface TextTypeface = new Typeface(new FontFamily("微软雅黑"), new FontStyle(), new FontWeight(), new FontStretch());
        private static readonly SolidColorBrush TextBrush = new SolidColorBrush(Color.FromRgb(0xff, 0xff, 0xff));
        private static readonly SolidColorBrush EllipseBrush = new SolidColorBrush(Color.FromRgb(0xc5, 0xc5, 0xc5));
        private static readonly SolidColorBrush PlentyBrush = new SolidColorBrush(Color.FromRgb(0x3e, 0xb5, 0xf6));
        private static readonly SolidColorBrush NormalBrush = new SolidColorBrush(Color.FromRgb(0xff, 0xff, 0xff));
        private static readonly SolidColorBrush RedBrush = new SolidColorBrush(Color.FromRgb(0xff, 0x00, 0x00));
        private static readonly SolidColorBrush WarnBrush = new SolidColorBrush(Color.FromRgb(0xff, 0xff, 0xff));

        private Point centerPoint;
        private double radius;

        public ValueToProcessConverter()
        {
        }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is double && !string.IsNullOrEmpty((string)parameter))
            {
                double arg = (double)value;
                string[] parameters = ((string)parameter).Split(':');
                double width = double.Parse((string)parameter);
                radius = width / 2;
                centerPoint = new Point(radius, radius);

                return DrawBrush(arg, radius, radius, Thickness, Padding);
            }
            else
            {
                throw new ArgumentException();
            }
            throw new NotImplementedException();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        private Point GetPointByAngel(Point CenterPoint, double r, double angel)
        {
            Point p = new Point();
            p.X = Math.Sin(angel * Math.PI / 180) * r + CenterPoint.X;
            p.Y = CenterPoint.Y - Math.Cos(angel * Math.PI / 180) * r;

            return p;
        }

        private Geometry DrawingArcGeometry(Point bigFirstPoint, Point bigSecondPoint, Point smallFirstPoint, Point smallSecondPoint, double bigRadius, double smallRadius, bool isLargeArc)
        {
            PathFigure pathFigure = new PathFigure { IsClosed = true };
            pathFigure.StartPoint = bigFirstPoint;
            pathFigure.Segments.Add(
              new ArcSegment
              {
                  Point = bigSecondPoint,
                  IsLargeArc = isLargeArc,
                  Size = new Size(bigRadius, bigRadius),
                  SweepDirection = SweepDirection.Clockwise
              });
            pathFigure.Segments.Add(new LineSegment { Point = smallSecondPoint });
            pathFigure.Segments.Add(
             new ArcSegment
             {
                 Point = smallFirstPoint,
                 IsLargeArc = isLargeArc,
                 Size = new Size(smallRadius, smallRadius),
                 SweepDirection = SweepDirection.Counterclockwise
             });
            PathGeometry pathGeometry = new PathGeometry();
            pathGeometry.Figures.Add(pathFigure);

            return pathGeometry;
        }

        private Geometry GetGeometry(double value, double radiusX, double radiusY, double thickness, double padding)
        {
            bool isLargeArc = false;
            double percent = value > 60 ? 1 : value / 60;
            double angel = percent * 360D;
            if (angel > 180) isLargeArc = true;
            double bigR = radiusX;
            double smallR = radiusX - thickness + padding;
            Point firstpoint = GetPointByAngel(centerPoint, bigR, 0);
            Point secondpoint = GetPointByAngel(centerPoint, bigR, angel);
            Point thirdpoint = GetPointByAngel(centerPoint, smallR, 0);
            Point fourpoint = GetPointByAngel(centerPoint, smallR, angel);
            return DrawingArcGeometry(firstpoint, secondpoint, thirdpoint, fourpoint, bigR, smallR, isLargeArc);
        }

        private void DrawingGeometry(DrawingContext drawingContext, double value, double radiusX, double radiusY, double thickness, double padding)
        {
            if (value < 60)
            {
                SolidColorBrush brush = value < WarnValue ? WarnBrush : NormalBrush;
                drawingContext.DrawEllipse(RedBrush, new Pen(EllipseBrush, thickness), centerPoint, radiusX, radiusY);
                drawingContext.DrawGeometry(brush, new Pen(), GetGeometry(value, radiusX + Offset, radiusY + Offset, thickness, padding));
                FormattedText formatWords = new FormattedText(value.ToString() + "s",
                    CultureInfo.CurrentCulture,
                    FlowDirection.LeftToRight,
                    TextTypeface,
                    TextFontSize,
                    TextBrush);
                Point startPoint = new Point(centerPoint.X - formatWords.Width / 2, centerPoint.Y - formatWords.Height / 2);
                drawingContext.DrawText(formatWords, startPoint);
            }
            else
            {
                drawingContext.DrawEllipse(RedBrush, new Pen(EllipseBrush, thickness), centerPoint, radiusX, radiusY);
                drawingContext.DrawGeometry(NormalBrush, new Pen(), GetGeometry(value - 60, radiusX + Offset, radiusY + Offset, thickness, padding));
                FormattedText formatWords = new FormattedText(value.ToString() + "s",
                    CultureInfo.CurrentCulture,
                    FlowDirection.LeftToRight,
                    TextTypeface,
                    TextFontSize,
                    TextBrush);
                Point startPoint = new Point(centerPoint.X - formatWords.Width / 2, centerPoint.Y - formatWords.Height / 2);
                drawingContext.DrawText(formatWords, startPoint);
            }

            drawingContext.Close();
        }

        private Brush DrawBrush(double value, double radiusX, double radiusY, double thickness, double padding)
        {
            DrawingGroup drawingGroup = new DrawingGroup();
            DrawingContext drawingContext = drawingGroup.Open();

            DrawingGeometry(drawingContext, value, radiusX, radiusY, thickness, padding);

            DrawingBrush brush = new DrawingBrush(drawingGroup);

            return brush;
        }
    }
}
