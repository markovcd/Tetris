using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace Tetris
{
    public interface IBlockCollection : IEnumerable<IBlock>
    {
        Size Size { get; }
    }

    public abstract class BlockCollection : IBlockCollection
    {
        private readonly Dictionary<Point, IBlock> _blocks;

        public abstract Size Size { get; }
		
        public virtual IBlock this[Point p] 
        {
        	get
        	{
        		IBlock b;
            	return _blocks.TryGetValue(p, out b) ? b : null;
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

        protected BlockCollection()
        {
            _blocks = new Dictionary<Point, IBlock>();
        }

        protected BlockCollection(IEnumerable<IBlock> blocks)
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
