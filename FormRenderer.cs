using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tetris;

namespace Tetris
{
    public class PieceBitmapRenderer : BitmapRenderer
    {
        public Piece Piece
        {
            get { return (Piece)Blocks; }
            set { Blocks = value; }
        }

        public override void Render()
        {
            var minPoint = Point.Create(-Blocks.Min(b => b.Position.X), -Blocks.Min(b => b.Position.Y));
            Piece = Piece.Offset(minPoint);

            base.Render();
        }

        public PieceBitmapRenderer(Piece piece, int blockSize = 20) : base(piece, blockSize) { }
    }

    public class BoardBitmapRenderer : BitmapRenderer
    {
        public Board Board
        {
            get { return (Board)Blocks; }
            set { Blocks = value; }
        }

        public override void Render()
        {
            base.Render();
            /*
            var line1 = "Pivot = " + Board.NextPiece.Pivot;
            var line2 = "Size = " + Board.NextPiece.Size;
            var line3 = "Count = " + Board.NextPiece.Count();
            var line4 = "Max = " + Point.Create(Board.NextPiece.Max(b => b.Position.X), Board.NextPiece.Max(b => b.Position.Y));
            var line5 = "Min = " + Point.Create(Board.NextPiece.Min(b => b.Position.X), Board.NextPiece.Min(b => b.Position.Y));

            _graphics.DrawString(line1 + "\n" + line2 + "\n" + line3 + "\n" + line4 + "\n" + line5, SystemFonts.DefaultFont, System.Drawing.Brushes.Black, 5, 5);
            */
        }

        public BoardBitmapRenderer(Board board, int blockSize = 20) : base(board, blockSize) { }
    }

    public class BitmapRenderer : IRenderer, IDisposable
    {
        protected BlockCollection _blocks;
    	protected Graphics _graphics;

        protected readonly IDictionary<long, Brush> _brushes;

        protected int _blockSize;
        protected Bitmap _bitmap;
    	
    	public BlockCollection Blocks 
    	{
    		get { return _blocks; } 
    		set 
    		{
    			_blocks = value;
    			CreateBitmap();
    		}
    	}

        public Bitmap Bitmap { get { return _bitmap; } }
        public IDictionary<long, Brush> Brushes { get { return _brushes; } }
        public int BackgroundBrushKey { get; set; }

        public int BlockSize 
        { 
        	get { return _blockSize; }
        	set 
        	{
        		_blockSize = value;
        		CreateBitmap();
        	}
        }
        
        private void CreateBitmap()
        {
        	if (_blocks == null) return;
            Dispose();
            _bitmap = new Bitmap(Blocks.Size.Width * BlockSize, Blocks.Size.Height * BlockSize);
        	_graphics = Graphics.FromImage(_bitmap);
        }
        
        private IDictionary<long, Brush> DefaultBrushes()
        {
        	var brushes = new Dictionary<long, Brush>();

            brushes.Add(0, System.Drawing.Brushes.White);
            brushes.Add(1, System.Drawing.Brushes.Gold);
        	brushes.Add(2, System.Drawing.Brushes.OrangeRed);
        	brushes.Add(3, System.Drawing.Brushes.Gray);
        	brushes.Add(4, System.Drawing.Brushes.SandyBrown);
        	brushes.Add(5, System.Drawing.Brushes.Indigo);
        	brushes.Add(6, System.Drawing.Brushes.ForestGreen);
        	brushes.Add(7, System.Drawing.Brushes.DodgerBlue);
        	
        	return brushes;
        }
        
        public BitmapRenderer(BlockCollection blocks, int blockSize = 20, IDictionary<long, Brush> brushes = null, int backgroundBrushKey = 0)
        {
        	_brushes = brushes ?? DefaultBrushes();
        	_blocks = blocks;
        	BlockSize = blockSize;
        }
        
    	public virtual void Render()
    	{
    		_graphics.Clear(Color.White);
    		_graphics.FillRectangle(_brushes[BackgroundBrushKey], _graphics.ClipBounds);

            foreach (var b in Blocks)
            {
                _graphics.FillRectangle(_brushes[b.Color], b.Position.X * BlockSize, b.Position.Y * BlockSize, BlockSize, BlockSize);
            }

        }

        public void Dispose()
        {
            if (_graphics != null)_graphics.Dispose();
            if (_bitmap != null) _bitmap.Dispose();
        }
    }
}
