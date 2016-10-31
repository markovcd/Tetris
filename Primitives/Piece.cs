
using System;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;

namespace Tetris
{
	public class Piece : BlockCollection, IEquatable<Piece>
	{
	    private readonly Brush _brush;
        private readonly PointF _pivot;

        public override Size Size
	    {
	        get
	        {
	            var width = this.Max(b => b.Position.X) - this.Min(b => b.Position.X) + 1;
                var height = this.Max(b => b.Position.Y) - this.Min(b => b.Position.Y) + 1;
                return new Size(width, height);
            }
	    }
		
		public Brush Brush { get { return _brush; } }
		
		public PointF Pivot { get { return _pivot; } }

        public Piece(int width, PointF pivot, Brush brush, string data) : this(pivot, brush, Enumerable.Empty<Block>())
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

		private Piece(PointF pivot, Brush brush, IEnumerable<Block> blocks) : base(blocks)
		{
			_pivot = pivot;
			_brush = brush;
		}
		
		public Piece Rotate(int angle = 90)
		{
            return new Piece(Pivot, Brush, this.Select(b => b.Rotate(Pivot, angle)));
		}

	    public Piece Offset(Point p)
	    {
	    	var blocks = new HashSet<Block>(this.Select(b => b.Offset(p)));
	        return new Piece(Pivot.Add(p) , Brush, blocks);
	    }

	    public Piece Clone()
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

	    public bool Equals(Piece other)
	    {
	        return GetHashCode().Equals(other.GetHashCode());
	    }

	    public override bool Equals(object obj)
	    {
	        return Equals((Piece)obj);
	    }
	}
	
 
	
	
}
