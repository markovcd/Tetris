using System;
using System.Drawing;
using System.Collections.Generic;

namespace Tetris
{
	public abstract class AbstractBlockFactory<TBlock> where TBlock : IBlock
    {
        public abstract TBlock GetBlock(Brush brush, Point position);
        
        public virtual IEnumerable<TBlock> GetBlocks(int width, PointF pivot, Brush brush, string data)
        {
        	int x = 0, y = 0;
			
			foreach (var c in data) 
			{
				if (c == '1') yield return GetBlock(brush, new Point(x, y));
				
				x++;

			    if (x < width) continue;

			    x = 0;
			    y++;
			}
        }         
    }
    
	public class BlockFactory : AbstractBlockFactory<IBlock>
	{		
		public override IBlock GetBlock(Brush brush, Point position)
		{
			return new Block(brush, position);
		}
	}

    public class TetrisBlockFactory : AbstractBlockFactory<ITetrisBlock>
    {
        public override ITetrisBlock GetBlock(Brush brush, Point position)
        {
            return new TetrisBlock(brush, position);
        }
    }

}
