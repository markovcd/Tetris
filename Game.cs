using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using System.Collections.Generic;

namespace Tetris
{
	public enum Direction { Left = -1, Right = 1}
	
	public sealed class Game : Form
	{
		private readonly IBoard _board;
	    private readonly IGameRenderer _renderer;
        private readonly IScore _score;
        private readonly Timer _timer;
        
        private readonly IDictionary<Keys, Action> _keyBindings;
        
        public IScore Score { get { return _score; } }
 
		public Game(IGameRenderer renderer)  
		{
			_renderer = renderer;
			_timer = new Timer();
			_board = renderer.Board;
			_score = renderer.Score;

            _board.LinesCleared += delegate(object sender, LinesEventArgs args) { _score.AddLines(args.Lines); };
            _board.GameEnd += delegate { New(); };
			_score.NewLevel += delegate { _timer.Interval = _score.TimerInterval; };
			
			_timer.Tick += delegate { DropPiece(); Invalidate(); };
            _timer.Interval = _score.InitialInterval;
            _timer.Enabled = true;
            
            _keyBindings = CreateKeyBindings();
            
            ClientSize = _renderer.Size;
            DoubleBuffered = true;
			KeyPreview = true;
			MaximizeBox = false;
			FormBorderStyle = FormBorderStyle.FixedSingle;
			Text = "Tetris";
		}
		
		private IDictionary<Keys, Action> CreateKeyBindings()
		{
			var d = new Dictionary<Keys, Action>();
			
            d.Add(Keys.A, () => MovePiece(Direction.Left));
            d.Add(Keys.D, () => MovePiece(Direction.Right));
            d.Add(Keys.W, () => RotatePiece(Direction.Right));
            d.Add(Keys.S, DropPiece);
            
            return d;
		}
		
		protected override void OnPaint(PaintEventArgs e)
		{
			_renderer.Graphics = e.Graphics;
			_renderer.Render();     
		}
		
		protected override void OnKeyDown(KeyEventArgs e)
		{
			Action action;
			if (_keyBindings.TryGetValue(e.KeyCode, out action)) action();
			
			Invalidate();
		}
		
		public void New()
		{
			Score.Clear();
			_board.Clear();
		}
		
		public void DropPiece()
		{
			_board.Tick();
		}
		
		public void MovePiece(Direction direction)
		{
			_board.MovePiece((int)direction);
		}
		
		public void RotatePiece(Direction direction)
		{
			_board.RotatePiece((int)direction * 90);
		}
	
	}
}
