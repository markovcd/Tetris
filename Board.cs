using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tetris
{
	public interface IGame
	{
		void Tick();
	}
	
	public class Board : BlockCollection, IGame
    {
  		private readonly Size _size;
  		private Piece _currentPiece, _nextPiece;
		
  		public Point<int> StartingPosition { get; private set; }

        public override Size Size { get { return _size; } }

        public Point<int> Gravity { get; private set; }
  		
		public Piece CurrentPiece 
		{ 
			get { return _currentPiece; }
			private set
			{
				if (_currentPiece != null) RemoveBlocks(_currentPiece);
				if (value != null) AddBlocks(value);
				_currentPiece = value;                                   
			}
		}
		
		public Piece NextPiece { get { return _nextPiece; } }
		
		protected void ClearPiece()
		{
            _currentPiece = null;
		}
		
		protected void MovePiece()
		{
			if (CurrentPiece == null) return;
			CurrentPiece = CurrentPiece.Offset(Gravity);
		}
		
		protected void NewPiece()
		{
			if (_nextPiece != null) CurrentPiece = _nextPiece.Offset(StartingPosition);
			_nextPiece = PieceFactory.GetRandomPiece();
		}
		
		public void MovePiece(int direction = 0)
		{
			if (CurrentPiece == null) return;
			
			var p = direction == 0 ? Gravity : Point.Create(direction, 0);
			
			if (IsCollision(p) || IsEdge(p)) return;
			CurrentPiece = CurrentPiece.Offset(p);
		}
		
		public void RotatePiece(int angle = 90)
        {
        	if (CurrentPiece == null) return;
        	if (IsCollision(angle: angle) || IsEdge(angle: angle)) return;
        	CurrentPiece = CurrentPiece.Rotate(angle);
        }
		
		public bool IsCollision(Point<int> offset = default(Point<int>), int angle = 0)
        {
			if (offset.X == 0 && offset.Y == 0) offset = Gravity;
			
			return this.Except(CurrentPiece).Intersect(CurrentPiece.Offset(offset).Rotate(angle)).Any();
        }
        
        public bool IsEdge(Point<int> p = default(Point<int>), int angle = 0)
        {
        	if (p.X == 0 && p.Y == 0) p = Gravity;
        	
        	return CurrentPiece.Offset(p).Rotate(angle).Any(b => !b.Position.IsIn(Size));
        }
        
        public int RemoveFullRows()
        {
        	var blocks = CurrentPiece == null ? this : this.Except(CurrentPiece);
        	
        	var rows = blocks.GroupBy(k => k.Position.Y)
        		.Where(g => g.Count() == Size.Width).ToList();
        	
		    RemoveBlocks
		    (
		    	rows.SelectMany(g => Enumerable.Range(0, Size.Width - 1)
           				 					   .Select(x => this[Point.Create(x, g.Key)].Value))
        	);
        	
		    var offsetBlocks = (CurrentPiece == null ? this : this.Except(CurrentPiece)).ToList();
		    
		    RemoveBlocks(offsetBlocks);
		    AddBlocks(offsetBlocks.Select(b => b.Offset(Point.Create(0, rows.Count))));
		    
        	return rows.Count;
        }

		public void Tick()
        {
			if (CurrentPiece == null)
            {
            	NewPiece();
            }
            else if (IsCollision() || IsEdge())
            {
            	ClearPiece();
            }
            else
            {
            	MovePiece();
            }
            
            RemoveFullRows();
        }
		
		public Board(Size size = default(Size))
        {
			_size = size.Height + size.Width == 0 ? new Size(10, 22) : size;
            StartingPosition = Point.Create(4, 1);
            Gravity = Point.Create(0, 1);
            NewPiece();
        }
        
    }
}
