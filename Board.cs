using System;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;

namespace Tetris
{
	public interface IBoard : IBlockCollection
    {
        event EventHandler<LinesEventArgs> LinesCleared;
        event EventHandler PieceCreated;
        event EventHandler GameEnd;
	    IPiece NextPiece { get; }

	    void Tick();
	    bool MovePiece(int direction);
	    void Clear();
	    bool RotatePiece(int angle);
    }

    public class LinesEventArgs : EventArgs
    {
        private readonly int _lines;
        public int Lines { get { return _lines; } }

        public LinesEventArgs(int lines) { _lines = lines; }
    }
	
	public class Board : BlockCollection, IBoard
	{
	    private readonly AbstractPieceFactory _factory;
        private readonly Size _size;
  		private readonly Point _gravity; 
  		private IPiece _currentPiece, _nextPiece;
		
  		public event EventHandler<LinesEventArgs> LinesCleared;
  		public event EventHandler PieceCreated;
  		public event EventHandler GameEnd;
  		
  		public Point StartingPosition { get; private set; }

        public override Size Size { get { return _size; } }
  		
		public IPiece CurrentPiece 
		{ 
			get { return _currentPiece; }
			private set
			{
				if (_currentPiece != null) RemoveBlocks(_currentPiece);
				if (value != null) AddBlocks(value);
				_currentPiece = value;                                   
			}
		}
		
		public IPiece NextPiece { get { return _nextPiece; } }
		
		protected void ClearPiece()
		{
            _currentPiece = null;
		}
		
		public void Clear()
		{
			ClearPiece();
			ClearBlocks();
			_nextPiece = _factory.GetRandomPiece();
		}
		
		protected bool NewPiece()
		{
			var piece = _nextPiece.Offset(StartingPosition);
			
			if (IsCollision(piece)) return false;
			
			CurrentPiece = piece;
			_nextPiece = _factory.GetRandomPiece();
			
			return true;
		}
		
		public bool MovePiece(int direction = 0)
		{
			if (CurrentPiece == null) return false;
			
			var p = direction == 0 ? _gravity : new Point(direction, 0);
			
			if (IsCollision(p) || IsEdge(p)) return false;
			CurrentPiece = CurrentPiece.Offset(p);

		    return true;
		}
		
		public bool RotatePiece(int angle = 90)
        {
        	if (CurrentPiece == null) return false;
        	if (IsCollision(angle: angle) || IsEdge(angle: angle)) return false;
        	CurrentPiece = CurrentPiece.Rotate(angle);
        	return true;
        }
		
		protected bool IsCollision(IPiece piece)
		{
			if (CurrentPiece == null) return this.Intersect(piece).Any();
			
			return this.Except(CurrentPiece).Intersect(piece).Any();
		}
		
		protected bool IsCollision(Point offset = default(Point), int angle = 0)
        {
			if (offset.X == 0 && offset.Y == 0) offset = _gravity;
			
			return IsCollision(CurrentPiece.Offset(offset).Rotate(angle));
        }
        
        protected bool IsEdge(Point p = default(Point), int angle = 0)
        {
        	if (p.X == 0 && p.Y == 0) p = _gravity;
        	
        	return CurrentPiece.Offset(p).Rotate(angle).Any(b => !b.Position.IsIn(Size));
        }

	    protected bool IsFullRow(int rowIndex, out IList<IBlock> row)
	    {
	        row = this.Where(b => b.Position.Y.Equals(rowIndex)).ToList();

            return row.Count.Equals(Size.Width);
	    }
        
        protected int RemoveFullRows(int rowIndex = 0)
        {
            var rows = 0;
            IList<IBlock> currentRow;

            if (IsFullRow(rowIndex, out currentRow))
            {
                RemoveBlocks(currentRow);
                var offsetBlocks = this.Where(b => b.Position.Y < rowIndex).ToList();
                RemoveBlocks(offsetBlocks);
                AddBlocks(offsetBlocks.Select(b => b.Offset(new Point(0, 1))));

                rows++;
            }

            if (rowIndex < Size.Height - 1) rows += RemoveFullRows(rowIndex + 1);
            
        	return rows;
        }

		public void Tick()
        {
		
			if (CurrentPiece == null)
            {
				if (NewPiece()) OnPieceCreated(new EventArgs());
				else OnGameEnd(new EventArgs());
            }
            else if (!MovePiece())
            {
                ClearPiece();
                var cleared = RemoveFullRows();
                if (cleared > 0) OnLinesCleared(new LinesEventArgs(cleared));
            }
        }
		
		public Board(Size size = default(Size)) : this(new PieceFactory())
        {
			_size = size == default(Size) ? new Size(10, 22) : size;
            StartingPosition = new Point(4, 1);
            _gravity = new Point(0, 1);
            _nextPiece = _factory.GetRandomPiece();
        }

	    public Board(AbstractPieceFactory factory)
	    {
	        _factory = factory;
	    }
        
		protected virtual void OnLinesCleared(LinesEventArgs e)
		{
			if (LinesCleared != null) LinesCleared.Invoke(this, e);
		}
		
		protected virtual void OnPieceCreated(EventArgs e)
		{
			if (PieceCreated != null) PieceCreated.Invoke(this, e);
		}
		
		protected virtual void OnGameEnd(EventArgs e)
		{
			if (GameEnd != null) GameEnd.Invoke(this, e);
		}
    }
}
