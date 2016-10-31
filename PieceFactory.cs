using System;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace Tetris
{
	/// <summary>
	/// Description of PieceFactory.
	/// </summary>
	public static class PieceFactory
	{
		public static IEnumerable<Piece> Pieces { get; private set; }
		
		public static Random Random { get; private set; }
		
		public readonly static Piece LPiece = new Piece(2, new PointF(0.0f, 1.0f), Brushes.Gold, "101011");
		public readonly static Piece JPiece = new Piece(2, new PointF(1.0f, 1.0f), Brushes.OrangeRed, "010111");
		public readonly static Piece OPiece = new Piece(2, new PointF(0.5f, 0.5f), Brushes.Gray, "1111");	
		public readonly static Piece IPiece = new Piece(4, new PointF(1.5f, 0.5f), Brushes.SandyBrown, "1111");
		public readonly static Piece TPiece = new Piece(3, new PointF(1.0f, 1.0f), Brushes.Indigo, "010111");
		public readonly static Piece SPiece = new Piece(2, new PointF(0.0f, 1.0f), Brushes.ForestGreen, "101101");
		public readonly static Piece ZPiece = new Piece(2, new PointF(0.0f, 1.0f), Brushes.DodgerBlue, "011110");
		
		static PieceFactory()
		{
			Random = new Random();
			
			Pieces = new List<Piece>
			{
                OPiece, IPiece, TPiece, ZPiece, SPiece, LPiece, JPiece 
			};
		}
		
		public static int RandomAngle()
		{
			int[] angles = { 0, 90, 180, 270 };
			return angles.ElementAt(Random.Next(0, 4));
		}
		
		public static Piece GetRandomPiece(bool randomAngle = true, Point offset = default(Point))
		{
			var p = Pieces.ElementAt(Random.Next(0, Pieces.Count()));
			
			return randomAngle ? p.Rotate(RandomAngle()).Offset(offset) : p.Offset(offset);
		}
	}
}
