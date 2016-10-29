using System;
using System.Drawing;

namespace Tetris
{
	public enum Direction { Left = -1, Right = 1}
	
	public class Game : IDisposable
	{
		private readonly Board _board;
	    private readonly BoardBitmapRenderer _boardRenderer;
        private readonly PieceBitmapRenderer _pieceRenderer;
        private readonly Score _score;
        
        private Graphics _graphics;
        private Bitmap _bitmap;
        
        public Score Score { get { return _score; } }
        public Bitmap Bitmap { get { return _bitmap; } }

        
		public Game()
		{
			_board = new Board();
			_score = new Score();
			
			_boardRenderer = new BoardBitmapRenderer(_board);
			_pieceRenderer = new PieceBitmapRenderer(_board.NextPiece);

			_bitmap = new Bitmap(_boardRenderer.Bitmap.Width + 2 + 20 + _pieceRenderer.BlockSize * 4 + 2 + 20, _boardRenderer.Bitmap.Height + 2);
			
			_graphics = Graphics.FromImage(_bitmap);
		}
		
		public void New()
		{
			Score.Clear();
			_board.Clear();
		}
		
		public void DropPiece()
		{
			_board.Tick();
			Score.AddLines(_board.LastClearedRows);
		}
		
		public void MovePiece(Direction direction)
		{
			_board.MovePiece((int)direction);
			Render();
		}
		
		public void RotatePiece(Direction direction)
		{
			_board.RotatePiece((int)direction * 90);
			Render();
		}
		
		public void Render()
		{
			_boardRenderer.Render();
			_pieceRenderer.Blocks = _board.NextPiece;
			_pieceRenderer.Render();
			
			_graphics.Clear(Color.White);
			_graphics.DrawImage(_boardRenderer.Bitmap, 1, 1);
			_graphics.DrawRectangle(Pens.Black, 0, 0, _boardRenderer.Bitmap.Width + 2, _boardRenderer.Bitmap.Height + 2);
			
			_graphics.DrawImage(_pieceRenderer.Bitmap, _boardRenderer.Bitmap.Width + 2 + 20 + 1 + (_pieceRenderer.BlockSize*4-_pieceRenderer.Bitmap.Width)/2, 1 + (_pieceRenderer.BlockSize * 4-_pieceRenderer.Bitmap.Height)/2);
			_graphics.DrawRectangle(Pens.Black, _boardRenderer.Bitmap.Width + 2 + 20, 0, _pieceRenderer.BlockSize * 4 + 2, _pieceRenderer.BlockSize * 4 + 2);
			
			_graphics.DrawString(Score.ToString(), SystemFonts.CaptionFont, Brushes.Black, _boardRenderer.Bitmap.Width + 2 + 20, _pieceRenderer.BlockSize * 5 + 2);
		}
		
		public void Tick()
		{
			DropPiece();
			Render();
		}

		public void Dispose()
		{
			_pieceRenderer.Dispose();
			_boardRenderer.Dispose();

			_graphics.Dispose();
			_bitmap.Dispose();
		}
	}
}
