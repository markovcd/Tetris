using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tetris
{
	public static class Point
	{
		public static Point<T> Create<T>(T x, T y) where T : IConvertible, IEquatable<T>, IComparable<T>
		{ 
			return new Point<T>(x, y); 
		}
	}
	
	public struct Point<T> : IEquatable<Point<T>> where T : IConvertible, IEquatable<T>, IComparable<T>
	{
	    private readonly T _x, _y;

        public T X { get { return _x; } }
        public T Y { get { return _y; } }

        public Point<T> Offset(Point<T> p)
        {
        	var x = (T)Convert.ChangeType(Convert.ToDouble(p.X) + Convert.ToDouble(X), typeof(T));
        	var y = (T)Convert.ChangeType(Convert.ToDouble(p.Y) + Convert.ToDouble(Y), typeof(T));
        	
        	return new Point<T>(x, y);
        }

        public bool IsIn(Size size)
        {
        	var x = Convert.ToInt32(X);
        	var y = Convert.ToInt32(Y);
        	
        	return x >= 0 && x < size.Width && y >= 0 && y < size.Height;
        }

        public Point(T x, T y) : this()
        {
            _x = x;
            _y = y;
        }

        public bool Equals(Point<T> p)
        {
            return p.X.Equals(X) && p.Y.Equals(Y);
        }

        public override bool Equals(object obj)
        {
            return Equals((Point<T>)obj);
        }

        public override int GetHashCode()
        {
            unchecked 
            {
	        	var hash = 17;
	
	            hash = hash * 23 + X.GetHashCode();
	            hash = hash * 23 + Y.GetHashCode();
	
	            return hash;
            }
        }

	    public override string ToString()
	    {
	        return String.Format("X={0} Y={1}", X, Y);
	    }
	}
}
