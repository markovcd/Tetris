using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tetris
{
    public struct Size : IEquatable<Size>
    {
        private readonly int _width, _height;

        public int Width { get { return _width; } }
        public int Height { get { return _height; } }

        public Size(int width, int height) : this()
        {
            _width = width;
            _height = height;
        }

        public bool Equals(Size p)
        {
            return p.Width.Equals(Width) && p.Height.Equals(Height);
        }

        public override bool Equals(object obj)
        {
            return Equals((Size)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
	        	var hash = 17;
	
	            hash = hash * 23 + Width.GetHashCode();
	            hash = hash * 23 + Height.GetHashCode();
	
	            return hash;
            }
        }

        public override string ToString()
        {
            return String.Format("Width={0} Height={1}", Width, Height);
        }
    }
}
