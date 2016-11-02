using System;
using System.Linq;
using System.Drawing;

namespace Tetris
{
	public interface IPieceRenderer : IGraphicsRenderer
    {
        IPiece Piece { get; set; }
    }
	
	public class PieceRenderer : GraphicsRenderer, IPieceRenderer
    {
        private const int RendererSize = 4;
		
		public IPiece Piece
        {
            get { return (IPiece)Blocks; }
            set { Blocks = value; }
        }
        
		public override Size Size 
		{
        	get { return new Size(BlockSize * RendererSize, BlockSize * RendererSize); }
		}

        public PieceRenderer(IPiece piece, int blockSize) : base(piece, blockSize) { }
    }
}
