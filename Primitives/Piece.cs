
using System;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;

namespace Tetris
{
    public interface IPiece : IBlocks, IBlock
    {
        new IPiece Offset(Point p);
        IPiece Rotate(int angle);
    }

    public class Piece : Blocks, IPiece, IEquatable<IPiece>
	{
	    private readonly Brush _brush;
        private readonly PointF _pivot;

		public Brush Brush { get { return _brush; } }
		
		public PointF Pivot { get { return _pivot; } }

		public Piece(PointF pivot, Brush brush, IEnumerable<IBlock> blocks) : base(blocks)
		{
			_pivot = pivot;
			_brush = brush;
		}
		
		public IPiece Rotate(int angle)
		{
            return new Piece(Pivot, Brush, this.Select(b => b.Rotate(Pivot, angle)));
		}

        public IBlock Rotate(PointF pivot, int angle)
        {
            return new Piece(pivot, Brush, this.Select(b => b.Rotate(pivot, angle)));
        }

        IBlock IBlock.Offset(Point p)
        {
            return Offset(p);
        }

        public IPiece Offset(Point p)
	    {
	        return new Piece(Pivot.Add(p) , Brush, this.Select(b => b.Offset(p)));
	    }

	    public IPiece Clone()
		{
			return new Piece(Pivot, Brush, this);
		}
		
	    public override int GetHashCode()
	    {
	        unchecked
	        {
	            return this.Aggregate(17, (current, b) => current*23 + b.GetHashCode());
	        }
	    }

	    public bool Equals(IPiece other)
	    {
	        return GetHashCode().Equals(other.GetHashCode());
	    }

	    public override bool Equals(object obj)
	    {
	        return Equals((IPiece)obj);
	    }
	}
	
 
	
	
}
