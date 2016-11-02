using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace Tetris
{
    public interface IBlocks : IEnumerable<IBlock>
    {
        Size Size { get; }
        Point Position { get; }
    }

    public abstract class Blocks : IBlocks
    {
        private readonly IDictionary<Point, IBlock> _blocks;

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

        protected void ClearBlocks()
        {
        	_blocks.Clear();
        }

        protected void AddBlock(IBlock b)
        {
            if (_blocks.ContainsKey(b.Position)) _blocks[b.Position] = b;
            else _blocks.Add(b.Position, b);
        }

        protected void AddBlocks(IEnumerable<IBlock> blocks)
        {
            foreach (var b in blocks) AddBlock(b);
        }

        protected bool RemoveBlock(Point p)
        {
            return _blocks.Remove(p);
        }
        
        protected void RemoveBlocks(IEnumerable<IBlock> blocks)
        {
        	foreach (var b in blocks) RemoveBlock(b.Position);
        }

        protected Blocks()
        {
            _blocks = new Dictionary<Point, IBlock>();
        }

        protected Blocks(IEnumerable<IBlock> blocks)
        {
            _blocks = new Dictionary<Point, IBlock>(blocks.ToDictionary(b => b.Position));
        }

        public virtual IEnumerator<IBlock> GetEnumerator()
        {
            return _blocks.Values.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
