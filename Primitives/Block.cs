using System;
using System.Drawing;

namespace Tetris
{
    public interface IBlock  
    {
        Point Position { get; }
        Brush Brush { get; }

        IBlock Offset(Point p);
    }

    public interface ITetrisBlock : IBlock
    {
        ITetrisBlock Rotate(PointF pivot, int angle);
        new ITetrisBlock Offset(Point p);
    }

    public class TetrisBlock : Block, ITetrisBlock
    {
        public ITetrisBlock Rotate(PointF pivot, int angle)
        {
            return new TetrisBlock(Brush, Position.Rotate(pivot, angle));
        }

        public new ITetrisBlock Offset(Point p)
        {
            return new TetrisBlock(Brush, Position.Add(p));
        }

        public TetrisBlock(Brush brush, Point position) : base(brush, position) { }
    }

    public class Block : IBlock, IEquatable<IBlock>, ICloneable
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
 
        public bool Equals(IBlock b)
        {
            return b != null && b.Position.Equals(Position);
        }

        public override bool Equals(object obj)
        {
            return Equals((IBlock)obj);
        }

        public override int GetHashCode()
        {
            return Position.GetHashCode();
        }

        public IBlock Clone()
        {
            return new Block((Brush)Brush.Clone(), Position);
        }

        object ICloneable.Clone()
        {
            return Clone();
        }

        
    }
}
