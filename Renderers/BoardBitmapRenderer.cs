using System;

namespace Tetris
{
	
	public class BoardBitmapRenderer : BitmapRenderer
    {
        public Board Board
        {
            get { return (Board)Blocks; }
            set { Blocks = value; }
        }

        public override void Render()
        {
            base.Render();
            /*
            var line1 = "Pivot = " + Board.NextPiece.Pivot;
            var line2 = "Size = " + Board.NextPiece.Size;
            var line3 = "Count = " + Board.NextPiece.Count();
            var line4 = "Max = " + Point.Create(Board.NextPiece.Max(b => b.Position.X), Board.NextPiece.Max(b => b.Position.Y));
            var line5 = "Min = " + Point.Create(Board.NextPiece.Min(b => b.Position.X), Board.NextPiece.Min(b => b.Position.Y));

            _graphics.DrawString(line1 + "\n" + line2 + "\n" + line3 + "\n" + line4 + "\n" + line5, SystemFonts.DefaultFont, System.Drawing.Brushes.Black, 5, 5);
            */
        }

        public BoardBitmapRenderer(Board board, int blockSize = 20) : base(board, blockSize) { }
    }
}
