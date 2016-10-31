using System;
using System.Drawing;
using System.Windows.Forms;

namespace Tetris
{
	public enum Direction { Left = -1, Right = 1}
	
	public class Game : Form
	{
		private readonly Board _board;
	    private readonly GameRenderer _renderer;
        private readonly Score _score;
        private readonly Timer _timer;
        
        public Score Score { get { return _score; } }
 
		public Game()
		{
			_timer = new Timer();
			_board = new Board();
			_score = new Score();
			
			_board.LinesCleared += delegate { Score.AddLines(_board.LastClearedRows); };
			_board.GameEnd += delegate { New(); };
			_score.NewLevel += delegate { _timer.Interval = _score.TimerInterval; };
			
			_renderer = new GameRenderer(_board);
			_renderer.Border = true;
            _renderer.Score = _score;
			
            _timer.Tick += new EventHandler(_timer_Tick);
            _timer.Interval = _score.InitialInterval;
            _timer.Enabled = true;
            
            ClientSize = new System.Drawing.Size(_renderer.Size.Width, _renderer.Size.Height);
            DoubleBuffered = true;
			KeyPreview = true;
			MaximizeBox = false;
			FormBorderStyle = FormBorderStyle.FixedSingle;
			Text = "Tetris";
		}

		void _timer_Tick(object sender, EventArgs e)
		{
			DropPiece();
			Invalidate();
		}
		
		protected override void OnPaint(PaintEventArgs e)
		{
			base.OnPaint(e);
			
			_renderer.Graphics = e.Graphics;
			_renderer.Render();     
		}
		
		protected override void OnKeyDown(KeyEventArgs e)
		{
			base.OnKeyDown(e);
			
			if (e.KeyCode == Keys.A) MovePiece(Direction.Left);
			if (e.KeyCode == Keys.D) MovePiece(Direction.Right);
			if (e.KeyCode == Keys.W) RotatePiece(Direction.Left);
			if (e.KeyCode == Keys.S) DropPiece();
			
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
