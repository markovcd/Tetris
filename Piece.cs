
using System;
using System.Collections.Generic;
using System.Linq;

namespace Tetris
{
	/// <summary>
	/// Description of Piece.
	/// </summary>
	public class Piece : BlockCollection, IEquatable<Piece>
	{
	    private readonly long _color;
        private readonly Point<double> _pivot;

        public override Size Size
	    {
	        get
	        {
	            var width = this.Max(b => b.Position.X) - this.Min(b => b.Position.X) + 1;
                var height = this.Max(b => b.Position.Y) - this.Min(b => b.Position.Y) + 1;
                return new Size(width, height);
            }
	    }
		
		public long Color { get { return _color; } }
		
		public Point<double> Pivot { get { return _pivot; } }

        public Piece(int width, Point<double> pivot, long color, string data) : this(pivot, color, Enumerable.Empty<Block>())
		{
			int x = 0, y = 0;
			
			foreach (var c in data) 
			{
				if (c == '1') AddBlock(new Block(color, Point.Create(x, y)));
				
				x++;

			    if (x < width) continue;

			    x = 0;
			    y++;
			}
		}

		private Piece(Point<double> pivot, long color, IEnumerable<Block> blocks) : base(blocks)
		{
			_pivot = pivot;
			_color = color;
		}
		
		public Piece Rotate(int angle = 90)
		{
            return new Piece(Pivot, Color, this.Select(b => b.Rotate(Pivot, angle)));
		}

	    public Piece Offset(Point<int> p)
	    {
	        var blocks = new HashSet<Block>(this.Select(b => b.Offset(p)));
	        var p2 = Point.Create(Convert.ToDouble(p.X), Convert.ToDouble(p.Y));
	        return new Piece(Pivot.Offset(p2), Color, blocks);
	    }

	    public Piece Clone()
		{
			return new Piece(Pivot, Color, this);
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
