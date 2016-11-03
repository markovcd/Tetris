using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace Tetris
{
    public interface IBlocks : IList<IBlock>
    {
        Size Size { get; }
        Point Position { get; }
    }

    public abstract class Blocks : List<IBlock>, IBlocks
    {
        public virtual Size Size
	    {
	        get
	        {
	            var position = Position;
                var width = this.Max(b => b.Position.X) - position.X + 1;
                var height = this.Max(b => b.Position.Y) - position.Y + 1;
                return new Size(width, height);
            }
	    }

        public virtual Point Position
        {
            get
            {
                return new Point(this.Min(b => b.Position.X), this.Min(b => b.Position.Y));
            }
        }

        public void Add(IEnumerable<IBlock> blocks)
        {
            foreach (var b in blocks) Add(b);
        }

        public void Remove(IEnumerable<IBlock> blocks)
        {
            foreach (var b in blocks) Remove(b);
        }

        protected Blocks(IEnumerable<IBlock> b) : base(b) { }
        protected Blocks() { }

    }
}
