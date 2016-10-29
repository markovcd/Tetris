using System;

namespace Tetris
{
	public interface IRenderer
    {
    	BlockCollection Blocks { get; }
    	void Render();
    }
}
