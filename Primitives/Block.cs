using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace Tetris
{
    public struct Block
    {
        private readonly Brush _brush;
        private readonly Point _position;

        public Brush Brush { get { return _brush; } }
        public Point Position { get { return _position; } }

        public Block(Brush brush, Point position) : this()
        {
            _brush = brush;
            _position = position;
        }

        public Block Offset(Point p)
        {
            return new Block(Brush, Position.Add(p));
        }
 
        public Block Rotate(PointF pivot, int angle = 90)
        {
            var rad = angle * Math.PI / 180;

            var x = Math.Cos(rad) * (Position.X - pivot.X) - Math.Sin(rad) * (Position.Y - pivot.Y) + pivot.X;
            var y = Math.Sin(rad) * (Position.X - pivot.X) + Math.Cos(rad) * (Position.Y - pivot.Y) + pivot.Y;

            return new Block(Brush, new Point(Convert.ToInt32(x), Convert.ToInt32(y)));
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
