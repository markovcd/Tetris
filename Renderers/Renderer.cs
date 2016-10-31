using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris
{
    public class Renderer : IRenderer
    {
        private BlockCollection _blocks;

        private int _blockSize;

        public virtual BlockCollection Blocks 
    	{
    		get { return _blocks; } 
    		set { _blocks = value; }
    	}
        
        public virtual bool Border { get; set; }
        
        public virtual Brush Background { get; set; }

        public virtual int BlockSize 
        { 
        	get { return _blockSize; }
        	set { _blockSize = value; }
        }
        
        public virtual Size Size
        {
        	get
        	{
        		var width = _blocks.Size.Width * BlockSize;
        		var height = _blocks.Size.Height * BlockSize;
        		
        		return new Size(width, height);
        	}
        }
        
        protected virtual Point MinPoint()
        {
        	if (Blocks == null || !Blocks.Any()) return default(Point);
        	return new Point(Blocks.Min(b => b.Position.X) * BlockSize, Blocks.Min(b => b.Position.Y)* BlockSize);
        }
        
        protected virtual Point CenterPoint()
        {
        	return new Point((Size.Width - _blocks.Size.Width * BlockSize) / 2, (Size.Height - _blocks.Size.Height * BlockSize) / 2);
        }

        public virtual Graphics Graphics { get; set; }

        public virtual Point Position { get; set; }

        public Renderer(BlockCollection blocks, int blockSize = 20, Brush background = null)
        {

        	_blocks = blocks;
        	BlockSize = blockSize;
            Background = background ?? System.Drawing.Brushes.White;
        }
        
    	public virtual void Render()
    	{
    		var min = MinPoint();
    		var center = CenterPoint();
    		
    		Graphics.TranslateTransform(Position.X, Position.Y);
    		
    		if (_blocks == null) return;

            Graphics.FillRectangle(Background, 0, 0, Size.Width, Size.Height);
			Graphics.TranslateTransform(-min.X + center.X, -min.Y + center.Y);
            
            foreach (var b in Blocks)
            {
                Graphics.FillRectangle(b.Brush, b.Position.X * BlockSize, b.Position.Y * BlockSize, BlockSize, BlockSize);
            }
            
            Graphics.TranslateTransform(min.X - center.X, min.Y - center.Y);
            
            if (Border) Graphics.DrawRectangle(Pens.Black, 0, 0, Size.Width - 1, Size.Height - 1);
            
            Graphics.TranslateTransform(-Position.X , -Position.Y);
        }
        
    }
}
