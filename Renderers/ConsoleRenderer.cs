using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tetris
{
    public class ConsoleRenderer : IRenderer
    {
        private readonly BlockCollection _blocks;
    	
        public BlockCollection Blocks { get { return _blocks; } }
        
        public ConsoleRenderer(BlockCollection blocks)
        {
        	_blocks = blocks;
        }
        
    	public void Render()
        {
            Console.Clear();

            for (var y = 0; y < _blocks.Size.Height; y++)
            {
                for (var x = 0; x < _blocks.Size.Width; x++)
                {
                	var b = _blocks[Point.Create(x, y)];
                	Console.Write(b.HasValue ? b.Value.Color : 0);
                }

                Console.WriteLine();
            }
        }
    }

    
}
