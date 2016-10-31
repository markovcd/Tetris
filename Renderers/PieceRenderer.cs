using System;
using System.Linq;
using System.Drawing;

namespace Tetris
{
	public class PieceRenderer : Renderer
    {
        public Piece Piece
        {
            get { return (Piece)Blocks; }
            set { Blocks = value; }
        }
        
		public override Size Size 
		{
        	get { return new Size(BlockSize * 4, BlockSize * 4); }
		}

        public PieceRenderer(Piece piece, int blockSize = 20) : base(piece, blockSize) { }
    }
}
