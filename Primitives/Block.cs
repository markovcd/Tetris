using System;
using System.Drawing;

namespace Tetris
{
    public interface IBlock
    {
        Point Position { get; }
        Brush Brush { get; }

        IBlock Rotate(PointF pivot, int angle);
        IBlock Offset(Point p);
    }

    public class Block : IBlock
    {
        private readonly Brush _brush;
        private readonly Point _position;

        public Brush Brush { get { return _brush; } }
        public Point Position { get { return _position; } }

        public Block(Brush brush, Point position) 
        {
            _brush = brush;
            _position = position;
        }

        public IBlock Offset(Point p)
        {
            return new Block(Brush, Position.Add(p));
        }
 
        public IBlock Rotate(PointF pivot, int angle)
        {
            return new Block(Brush, Position.Rotate(pivot, angle));
        }

        public bool Equals(IBlock b)
        {
            return b.Position.Equals(Position);
        }

        public override bool Equals(object obj)
        {
            return Equals((IBlock)obj);
        }

        public override int GetHashCode()
        {
            return Position.GetHashCode();
        }
    }
}
