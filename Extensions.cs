
using System;
using System.Drawing;

namespace Tetris
{
	public static class PointExtensions
	{
		public static Point Add(this Point p1, int x, int y)
		{
			return new Point(p1.X + x, p1.Y + y);
		}
		
		public static Point Add(this Point p1, Point p2)
		{
			return new Point(p1.X + p2.X, p1.Y + p2.Y);
		}
		
		public static PointF Add(this PointF p1, Point p2)
		{
			return new PointF(p1.X + p2.X, p1.Y + p2.Y);
		}
		
		public static PointF Add(this PointF p1, PointF p2)
		{
			return new PointF(p1.X + p2.X, p1.Y + p2.Y);
		}
		
		public static bool IsIn(this Point p, Size s)
		{
			return p.X >= 0 && p.X < s.Width && p.Y >= 0 && p.Y < s.Height;
		}

        public static Point Rotate(this Point p, PointF pivot, int angle)
        {
            var rad = angle * Math.PI / 180;

            var x = Math.Cos(rad) * (p.X - pivot.X) - Math.Sin(rad) * (p.Y - pivot.Y) + pivot.X;
            var y = Math.Sin(rad) * (p.X - pivot.X) + Math.Cos(rad) * (p.Y - pivot.Y) + pivot.Y;

            return new Point(Convert.ToInt32(x), Convert.ToInt32(y));
        }
    }
	
	public static class SizeExtensions
	{
		public static Size Multiply(this Size s, int i)
		{
			return new Size(s.Width * i, s.Height * i);
		}
	}
}
