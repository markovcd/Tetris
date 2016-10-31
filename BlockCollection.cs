using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace Tetris
{
    public abstract class BlockCollection : IEnumerable<Block>
    {
        private readonly Dictionary<Point, Block> _blocks;

        public abstract Size Size { get; }
		
        public virtual Block? this[Point p] 
        {
        	get
        	{
        		Block b;
            	return _blocks.TryGetValue(p, out b) ? b : (Block?)null;
        	}
        }
        
        protected void ClearBlocks()
        {
        	_blocks.Clear();
        }

        protected void AddBlock(Block b)
        {
            if (_blocks.ContainsKey(b.Position)) _blocks[b.Position] = b;
            else _blocks.Add(b.Position, b);
        }

        protected void AddBlocks(IEnumerable<Block> blocks)
        {
            foreach (var b in blocks) AddBlock(b);
        }

        protected bool RemoveBlock(Point p)
        {
            return _blocks.Remove(p);
        }
        
        protected void RemoveBlocks(IEnumerable<Block> blocks)
        {
        	foreach (var b in blocks) RemoveBlock(b.Position);
        }

        protected BlockCollection()
        {
            _blocks = new Dictionary<Point, Block>();
        }

        protected BlockCollection(IEnumerable<Block> blocks)
        {
            _blocks = new Dictionary<Point, Block>(blocks.ToDictionary(b => b.Position));
        }

        public virtual IEnumerator<Block> GetEnumerator()
        {
            return _blocks.Values.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
