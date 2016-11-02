using System;
using System.Drawing;

namespace Tetris
{
	public class BoardRenderer : GraphicsRenderer, IBoardRenderer
    {	
		public IBoard Board
        {
            get { return (IBoard)Blocks; }
            set { Blocks = value; }
        }
		
		public override Size Size 
		{
			get { return Board.Size.Multiply(BlockSize); }
		}
		
		protected override Point MinPoint()
		{
			return default(Point);
		}
		
		protected override Point CenterPoint()
		{
			return default(Point);
		}
		
        public BoardRenderer(IBoard board, int blockSize = 20) : base(board, blockSize) { }
       
    }
}
