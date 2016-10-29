using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tetris;

namespace Tetris
{
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
            BackgroundBrushKey = backgroundBrushKey;
        }
        
    	public virtual void Render()
    	{
    		_graphics.Clear((Brushes[BackgroundBrushKey] as SolidBrush).Color);
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
