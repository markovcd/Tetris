using System;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace Tetris
{
	public class GameFactory
	{
		public static Game GetGame()
		{
			var pieceFactory = new PieceFactory(new TetrisBlockFactory());
			var score = new Score(10, 1000, 100, Score.DefaultScoreMultiplier());
			var board = new Board(pieceFactory, new Size(10, 22), new Point(4, 1));
			var renderer = new GameRenderer(board, score, 20);
			
			renderer.Border = new Pen(new LinearGradientBrush(new Point(renderer.Size), new Point(0, 0), Color.DarkGray, Color.FloralWhite));
		    renderer.Background = new LinearGradientBrush(new Point(0, 0), new Point(renderer.Size), Color.DarkGray, Color.FloralWhite );
			renderer.Font = SystemFonts.DefaultFont;
			renderer.FontBrush = Brushes.Black;
			
			var game = new Game(renderer);
			
			return game;
		}
	}
}
