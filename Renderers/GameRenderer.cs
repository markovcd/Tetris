using System;
using System.Drawing;

namespace Tetris
{
	public class GameRenderer : BoardRenderer
	{
		private readonly PieceRenderer _pieceRenderer;
		
		public override Point Position {
			get { return base.Position; }
			set 
			{ 
				base.Position = value;
				_pieceRenderer.Position = value.Add(base.Size.Width + BlockSize, 0);
			}
		}
		
		public override Graphics Graphics {
			get { return base.Graphics; }
			set 
			{ 
				base.Graphics = value; 
				_pieceRenderer.Graphics = value;
			}
		} 
		
		public override int BlockSize 
		{
			get { return base.BlockSize; }
			set 
			{ 
				base.BlockSize = value; 
				if (_pieceRenderer != null) _pieceRenderer.BlockSize = value;
			}
		}
		
		public override bool Border 
		{
			get { return base.Border; }
			set 
			{ 
				base.Border = value;
				_pieceRenderer.Border = value;				
			}
		}
		
		public new Size Size 
		{
			get 
			{ 
				var p1 = base.Position;
				var p2 = _pieceRenderer.Position;
				var s1 = base.Size;
				var s2 = _pieceRenderer.Size;
				
				return new Size(Math.Max(p1.X + s1.Width, p2.X + s2.Width) + BlockSize, Math.Max(p1.Y + s1.Height, p2.Y + s2.Height));
			}
		}
		
		public Size BoardSize { get { return base.Size; } }
		public Size PieceSize { get { return _pieceRenderer.Size; } }
		
		public Score Score { get; set; }
		public Point ScorePosition 
		{
			get
			{
				return Position.Add(BoardSize.Width + BlockSize, PieceSize.Height + BlockSize);
			}
		}
		
		public GameRenderer(Board board, int blockSize = 20) : base(board, blockSize) 
		{
			_pieceRenderer = new PieceRenderer(board.NextPiece, blockSize);
			Position = default(Point);
		}
		
		public override void Render()
		{
			_pieceRenderer.Piece = Board.NextPiece;
			base.Render();
			_pieceRenderer.Render();
			
			if (Score == null) return;
			
			Graphics.DrawString(Score.ToString(), SystemFonts.CaptionFont, System.Drawing.Brushes.Black, ScorePosition.X, ScorePosition.Y);
		}

	}
}
