using System;
using System.Linq;

namespace Tetris
{
	public class PieceBitmapRenderer : BitmapRenderer
    {
        public Piece Piece
        {
            get { return (Piece)Blocks; }
            set { Blocks = value; }
        }

        public override void Render()
        {
            var minPoint = Point.Create(-Blocks.Min(b => b.Position.X), -Blocks.Min(b => b.Position.Y));
            Piece = Piece.Offset(minPoint);

            base.Render();
        }

        public PieceBitmapRenderer(Piece piece, int blockSize = 20) : base(piece, blockSize) { }
    }
}
