using System;
using System.Collections.Generic;
using System.Linq;

namespace Tetris
{
	/// <summary>
	/// Description of PieceFactory.
	/// </summary>
	public static class PieceFactory
	{
		public static IEnumerable<Piece> Pieces { get; private set; }
		
		public static Random Random { get; private set; }
		
		public readonly static Piece LPiece = new Piece(2, Point.Create(0.0, 1.0), 1, "101011");
		public readonly static Piece JPiece = new Piece(2, Point.Create(1.0, 1.0), 2, "010111");
		public readonly static Piece OPiece = new Piece(2, Point.Create(0.5, 0.5), 3, "1111");	
		public readonly static Piece IPiece = new Piece(1, Point.Create(0.0, 1.0), 4, "1111");
		public readonly static Piece TPiece = new Piece(3, Point.Create(1.0, 1.0), 5, "010111");
		public readonly static Piece SPiece = new Piece(2, Point.Create(0.0, 1.0), 6, "101101");
		public readonly static Piece ZPiece = new Piece(2, Point.Create(1.0, 1.0), 7, "011110");
		
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
			int[] angles = { 0, 90, 180, 360 };
			return angles.ElementAt(Random.Next(0, 4));
		}
		
		public static Piece GetRandomPiece(bool randomAngle = true, Point<int> offset = default(Point<int>))
		{
			var p = Pieces.ElementAt(Random.Next(0, Pieces.Count()));
			
			return randomAngle ? p.Rotate(RandomAngle()).Offset(offset) : p.Offset(offset);
		}
	}
}
