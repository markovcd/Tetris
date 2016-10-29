using System;
using System.Collections.Generic;
using System.Linq;

namespace Tetris
{
	public interface IBoard
	{
		void Tick();
	}
	
	public class Board : BlockCollection, IBoard
    {
  		private readonly Size _size;
  		private readonly Point<int> _gravity; 
  		private Piece _currentPiece, _nextPiece;
		
  		public Point<int> StartingPosition { get; private set; }

        public override Size Size { get { return _size; } }
  		
        public int LastClearedRows { get; set; }
        
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
		
		public void Clear()
		{
			ClearPiece();
			ClearBlocks();
			_nextPiece = null;
		}
		
		protected void NewPiece()
		{
			if (_nextPiece != null) CurrentPiece = _nextPiece.Offset(StartingPosition);
			_nextPiece = PieceFactory.GetRandomPiece();
		}
		
		public bool MovePiece(int direction = 0)
		{
			if (CurrentPiece == null) return false;
			
			var p = direction == 0 ? _gravity : Point.Create(direction, 0);
			
			if (IsCollision(p) || IsEdge(p)) return false;
			CurrentPiece = CurrentPiece.Offset(p);

		    return true;
		}
		
		public void RotatePiece(int angle = 90)
        {
        	if (CurrentPiece == null) return;
        	if (IsCollision(angle: angle) || IsEdge(angle: angle)) return;
        	CurrentPiece = CurrentPiece.Rotate(angle);
        }
		
		protected bool IsCollision(Point<int> offset = default(Point<int>), int angle = 0)
        {
			if (offset.X == 0 && offset.Y == 0) offset = _gravity;
			
			return this.Except(CurrentPiece).Intersect(CurrentPiece.Offset(offset).Rotate(angle)).Any();
        }
        
        protected bool IsEdge(Point<int> p = default(Point<int>), int angle = 0)
        {
        	if (p.X == 0 && p.Y == 0) p = _gravity;
        	
        	return CurrentPiece.Offset(p).Rotate(angle).Any(b => !b.Position.IsIn(Size));
        }

	    protected bool IsFullRow(int rowIndex, out IList<Block> row)
	    {
	        row = this.Where(b => b.Position.Y.Equals(rowIndex)).ToList();

            return row.Count.Equals(Size.Width);
	    }
        
        protected int RemoveFullRows(int rowIndex = 0)
        {
            var rows = 0;
            IList<Block> currentRow;

            if (IsFullRow(rowIndex, out currentRow))
            {
                RemoveBlocks(currentRow);
                var offsetBlocks = this.Where(b => b.Position.Y < rowIndex).ToList();
                RemoveBlocks(offsetBlocks);
                AddBlocks(offsetBlocks.Select(b => b.Offset(Point.Create(0, 1))));

                rows++;
            }

            if (rowIndex < Size.Height - 1) rows += RemoveFullRows(rowIndex + 1);
            
        	return rows;
        }

		public void Tick()
        {
			if (CurrentPiece == null)
            {
            	NewPiece();
            }
            else if (!MovePiece())
            {
                ClearPiece();
                LastClearedRows = RemoveFullRows();
            }
        }
		
		public Board(Size size = default(Size))
        {
			_size = size.Height + size.Width == 0 ? new Size(10, 22) : size;
            StartingPosition = Point.Create(4, 1);
            _gravity = Point.Create(0, 1);
            NewPiece();
        }
        
    }
}
