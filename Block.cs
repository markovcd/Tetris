using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tetris
{
    public struct Block
    {
        private readonly long _color;
        private readonly Point<int> _position;

        public long Color { get { return _color; } }
        public Point<int> Position { get { return _position; } }

        public Block(long color, Point<int> position) : this()
        {
            _color = color;
            _position = position;
        }

        public Block Offset(Point<int> p)
        {
            return new Block(Color, Position.Offset(p));
        }
 
        public Block Rotate(Point<double> pivot, int angle = 90)
        {
            var rad = angle * Math.PI / 180;

            var x = Math.Cos(rad) * (Position.X - pivot.X) - Math.Sin(rad) * (Position.Y - pivot.Y) + pivot.X;
            var y = Math.Sin(rad) * (Position.X - pivot.X) + Math.Cos(rad) * (Position.Y - pivot.Y) + pivot.Y;

            return new Block(Color, Point.Create(Convert.ToInt32(x), Convert.ToInt32(y)));
        }

        public bool Equals(Block b)
        {
            return b.Position.Equals(Position);
        }

        public override bool Equals(object obj)
        {
            return Equals((Block)obj);
        }

        public override int GetHashCode()
        {
            return Position.GetHashCode();
        }
    }
}
