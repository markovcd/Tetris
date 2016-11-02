using System;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;

namespace Tetris
{
    public abstract class AbstractPieceFactory
    {
        public abstract IPiece GetRandomPiece(bool randomAngle = true, Point offset = default(Point));
    }
    
    /// <summary>
	/// Description of PieceFactory.
	/// </summary>
	public class PieceFactory : AbstractPieceFactory
	{
		public static IEnumerable<IPiece> Pieces { get; private set; }
		
		public static Random Random { get; private set; }
		
		public static readonly IPiece LPiece = new Piece(2, new PointF(0.0f, 1.0f), Brushes.Gold, "101011");
		public static readonly IPiece JPiece = new Piece(2, new PointF(1.0f, 1.0f), Brushes.OrangeRed, "010111");
		public static readonly IPiece OPiece = new Piece(2, new PointF(0.5f, 0.5f), Brushes.Gray, "1111");	
		public static readonly IPiece IPiece = new Piece(4, new PointF(1.5f, 0.5f), Brushes.SandyBrown, "1111");
		public static readonly IPiece TPiece = new Piece(3, new PointF(1.0f, 1.0f), Brushes.Indigo, "010111");
		public static readonly IPiece SPiece = new Piece(2, new PointF(0.0f, 1.0f), Brushes.ForestGreen, "101101");
		public static readonly IPiece ZPiece = new Piece(2, new PointF(0.0f, 1.0f), Brushes.DodgerBlue, "011110");
		
		static PieceFactory()
		{
			Random = new Random();
			
			Pieces = new List<IPiece>
			{
                OPiece, IPiece, TPiece, ZPiece, SPiece, LPiece, JPiece 
			};
		}
		
		public static int RandomAngle()
		{
			int[] angles = { 0, 90, 180, 270 };
			return angles.ElementAt(Random.Next(0, 4));
		}
		
		public override IPiece GetRandomPiece(bool randomAngle = true, Point offset = default(Point))
		{
			var p = Pieces.ElementAt(Random.Next(0, Pieces.Count()));
			
			return randomAngle ? p.Rotate(RandomAngle()).Offset(offset) : p.Offset(offset);
		}
	}
}
