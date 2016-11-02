using System;
using System.Drawing;
using System.Collections.Generic;

namespace Tetris
{
	public abstract class AbstractBlockFactory
    {
        public abstract IBlock GetBlock(Brush brush, Point position);
        
        public virtual IEnumerable<IBlock> GetBlocks(int width, PointF pivot, Brush brush, string data)
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
    
	public class BlockFactory : AbstractBlockFactory
	{		
		public override IBlock GetBlock(Brush brush, Point position)
		{
			return new Block(brush, position);
		}
	}
}
