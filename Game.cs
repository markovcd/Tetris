using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace Tetris
{
	public enum Direction { Left = -1, Right = 1}
	
	public sealed class Game : Form
	{
		private readonly IBoard _board;
	    private readonly IGameRenderer _renderer;
        private readonly IScore _score;
        private readonly Timer _timer;
        
        public IScore Score { get { return _score; } }
 
		public Game()  
		{
			_timer = new Timer();
			_board = new Board();
			_score = new Score();

            _board.LinesCleared += delegate(object sender, LinesEventArgs args) { Score.AddLines(args.Lines); };
            _board.GameEnd += delegate { New(); };
			_score.NewLevel += delegate { _timer.Interval = _score.TimerInterval; };
			
			_renderer = new GameRenderer(_board);
			_renderer.Border = new Pen(new LinearGradientBrush(new Point(_renderer.Size), new Point(0, 0), Color.DarkGray, Color.FloralWhite));
            _renderer.Score = _score;
		    _renderer.Background = new LinearGradientBrush(new Point(0,0), new Point(_renderer.Size), Color.DarkGray, Color.FloralWhite );
			
            _timer.Tick += new EventHandler(_timer_Tick);
            _timer.Interval = _score.InitialInterval;
            _timer.Enabled = true;
            
            ClientSize = new Size(_renderer.Size.Width, _renderer.Size.Height);
            DoubleBuffered = true;
			KeyPreview = true;
			MaximizeBox = false;
			FormBorderStyle = FormBorderStyle.FixedSingle;
			Text = "Tetris";
		}

	    public Game(IGameRenderer renderer)
	    {
	        _renderer = renderer;
	    }

		void _timer_Tick(object sender, EventArgs e)
		{
			DropPiece();
			Invalidate();
		}
		
		protected override void OnPaint(PaintEventArgs e)
		{
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
