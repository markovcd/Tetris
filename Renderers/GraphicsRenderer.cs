using System;
using System.Drawing;
using System.Linq;

namespace Tetris
{
    public class GraphicsRenderer : IGraphicsRenderer
    {
        private IBlockCollection _blocks;

        private int _blockSize;

        public virtual IBlockCollection Blocks 
    	{
    		get { return _blocks; } 
    		set { _blocks = value; }
    	}
        
        public virtual Pen Border { get; set; }
        
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

        public GraphicsRenderer(IBlockCollection blocks, int blockSize = 20)
        {

        	_blocks = blocks;
        	BlockSize = blockSize;
        }
        
    	public virtual void Render()
    	{
    		var min = MinPoint();
    		var center = CenterPoint();
    		
    		Graphics.TranslateTransform(Position.X, Position.Y);
    		
    		if (_blocks == null) return;

            if (Background != null) Graphics.FillRectangle(Background, 0, 0, Size.Width, Size.Height);
			Graphics.TranslateTransform(-min.X + center.X, -min.Y + center.Y);
            
            foreach (var b in Blocks)
            {
                Graphics.FillRectangle(b.Brush, b.Position.X * BlockSize, b.Position.Y * BlockSize, BlockSize, BlockSize);
            }
            
            Graphics.TranslateTransform(min.X - center.X, min.Y - center.Y);
            
            if (Border != null) Graphics.DrawRectangle(Border, 0, 0, Size.Width - 1, Size.Height - 1);
            
            Graphics.TranslateTransform(-Position.X , -Position.Y);
        }
        
    }
}
