
using System;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;

namespace Tetris
{
    public interface IPiece : IBlockCollection, IBlock
    {
        new IPiece Offset(Point p);
        IPiece Rotate(int angle);
    }

    public class Piece : BlockCollection, IPiece, IEquatable<IPiece>
	{
	    private readonly Brush _brush;
        private readonly PointF _pivot;

        public override Size Size
	    {
	        get
	        {
	            var position = Position;
                var width = this.Max(b => b.Position.X) - position.X + 1;
                var height = this.Max(b => b.Position.Y) - position.Y + 1;
                return new Size(width, height);
            }
	    }

        public Point Position
        {
            get
            {
                return new Point(this.Min(b => b.Position.X), this.Min(b => b.Position.Y));
            }
        }
		
		public Brush Brush { get { return _brush; } }
		
		public PointF Pivot { get { return _pivot; } }

        public Piece(int width, PointF pivot, Brush brush, string data) : this(pivot, brush, Enumerable.Empty<IBlock>())
		{
			int x = 0, y = 0;
			
			foreach (var c in data) 
			{
				if (c == '1') AddBlock(new Block(brush, new Point(x, y)));
				
				x++;

			    if (x < width) continue;

			    x = 0;
			    y++;
			}
		}

		private Piece(PointF pivot, Brush brush, IEnumerable<IBlock> blocks) : base(blocks)
		{
			_pivot = pivot;
			_brush = brush;
		}
		
		public IPiece Rotate(int angle = 90)
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
	    	var blocks = new HashSet<IBlock>(this.Select(b => b.Offset(p)));
	        return new Piece(Pivot.Add(p) , Brush, blocks);
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
