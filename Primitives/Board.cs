using System;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;

namespace Tetris
{
	public interface IBoard : IBlocks<IBlock>
    {
        event EventHandler<LinesEventArgs> LinesCleared;
        event EventHandler PieceCreated;
        event EventHandler GameEnd;
	    IPiece NextPiece { get; }

	    void Tick();
	    void Clear();
	    bool MovePiece(Point direction);
	    bool RotatePiece(int angle);
    }

    public class LinesEventArgs : EventArgs
    {
        private readonly int _lines;
        public int Lines { get { return _lines; } }

        public LinesEventArgs(int lines) { _lines = lines; }
    }
	
	public class Board : Blocks<IBlock>, IBoard
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
				if (_currentPiece != null) Remove(_currentPiece);
				if (value != null) Add(value);
				_currentPiece = value;                                   
			}
		}
		
		public IPiece NextPiece { get { return _nextPiece; } }
		
		protected void ClearPiece()
		{
		    if (_currentPiece == null) return;

            Remove(_currentPiece);
            AddRange(_currentPiece);
            _currentPiece = null;
		}
		
		public new void Clear()
		{
			ClearPiece();
			base.Clear();
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
		
		public bool MovePiece(Point direction)
		{
			if (CurrentPiece == null) return false;

		    var piece = CurrentPiece.Offset(direction);

			if (IsCollision(piece) || IsEdge(piece)) return false;
			CurrentPiece = piece;

		    return true;
		}
		
		public bool RotatePiece(int angle)
        {
        	if (CurrentPiece == null) return false;

        	var piece = CurrentPiece.Rotate(angle);
		    if (IsCollision(piece) || IsEdge(piece)) return false;
		    CurrentPiece = piece;
		    return true;
        }
		
		protected bool IsCollision(IPiece piece)
		{
            return this.Except(new [] { CurrentPiece }).Intersect(piece).Any();
		}

	    protected bool IsEdge(IPiece piece)
	    {
            return piece.Any(b => !b.Position.IsIn(Size));
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
                RemoveRange(currentRow);
                var offsetBlocks = this.Where(b => b.Position.Y < rowIndex).ToList();
                RemoveRange(offsetBlocks);
                AddRange(offsetBlocks.Select(b => b.Offset(new Point(0, 1))));

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
			else if (!MovePiece(_gravity))
            {
                ClearPiece();
                var cleared = RemoveFullRows();
                if (cleared > 0) OnLinesCleared(new LinesEventArgs(cleared));
            }
        }
		
		public Board(AbstractPieceFactory factory, Size size, Point startingPosition)
        {
			_factory = factory;
			_size = size; 
            StartingPosition = startingPosition;
            _gravity = new Point(0, 1);
            _nextPiece = _factory.GetRandomPiece();
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
