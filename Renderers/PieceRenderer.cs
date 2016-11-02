using System;
using System.Linq;
using System.Drawing;

namespace Tetris
{
	public class PieceRenderer : GraphicsRenderer, IPieceRenderer
    {
        public IPiece Piece
        {
            get { return (IPiece)Blocks; }
            set { Blocks = value; }
        }
        
		public override Size Size 
		{
        	get { return new Size(BlockSize * 4, BlockSize * 4); }
		}

        public PieceRenderer(IPiece piece, int blockSize = 20) : base(piece, blockSize) { }
    }
}
