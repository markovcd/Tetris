using System;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;

namespace Tetris
{
    public abstract class AbstractPieceFactory
    {
    	private readonly AbstractBlockFactory<ITetrisBlock> _factory;
    	
    	public virtual IEnumerable<IPiece> Pieces { get; set; }
    	
    	public Random Random { get; set; }
    	
    	public virtual int RandomAngle()
		{
			int[] angles = { 0, 90, 180, 270 };
			return angles.ElementAt(Random.Next(0, 4));
		}
    	
    	public virtual IPiece GetRandomPiece()
    	{
    		var p = Pieces.ElementAt(Random.Next(0, Pieces.Count()));

    	    return p.Rotate(RandomAngle());
    	}
    	
    	public abstract IPiece GetPiece(PointF pivot, Brush brush, IEnumerable<ITetrisBlock> blocks);
    	
    	public IPiece GetPiece(int width, PointF pivot, Brush brush, string data)
		{
    		return GetPiece(pivot, brush, _factory.GetBlocks(width, pivot, brush, data));
		}
    	
    	public AbstractPieceFactory(AbstractBlockFactory<ITetrisBlock> factory)
    	{
    		Random = new Random();
    		_factory = factory;
    	}
    }
    
	public class PieceFactory : AbstractPieceFactory
	{
		public readonly IPiece LPiece;
		public readonly IPiece JPiece;
		public readonly IPiece OPiece;	
		public readonly IPiece IPiece;
		public readonly IPiece TPiece;
		public readonly IPiece SPiece;
		public readonly IPiece ZPiece;
		
		public PieceFactory(AbstractBlockFactory<ITetrisBlock> factory) : base(factory)
		{
			LPiece = GetPiece(2, new PointF(0.0f, 1.0f), Brushes.Gold, "101011");
			JPiece = GetPiece(2, new PointF(1.0f, 1.0f), Brushes.OrangeRed, "010111");
			OPiece = GetPiece(2, new PointF(0.5f, 0.5f), Brushes.Gray, "1111");	
			IPiece = GetPiece(4, new PointF(1.5f, 0.5f), Brushes.SandyBrown, "1111");
			TPiece = GetPiece(3, new PointF(1.0f, 1.0f), Brushes.Indigo, "010111");
			SPiece = GetPiece(2, new PointF(0.0f, 1.0f), Brushes.ForestGreen, "101101");
			ZPiece = GetPiece(2, new PointF(0.0f, 1.0f), Brushes.DodgerBlue, "011110");
			
			Pieces = new List<IPiece>
			{
                OPiece, IPiece, TPiece, ZPiece, SPiece, LPiece, JPiece 
			};
		}
		
		public override IPiece GetPiece(PointF pivot, Brush brush, IEnumerable<ITetrisBlock> blocks)
		{
			return new Piece(pivot, brush, blocks);
		}
		
	}
}
