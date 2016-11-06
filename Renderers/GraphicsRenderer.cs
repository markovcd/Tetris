using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace Tetris
{
    public interface IRenderer
    {
    	IBlocks<IBlock> Blocks { get; }
    	void Render();
    }
	
	public interface IGraphicsRenderer : IRenderer
    {
        Graphics Graphics { get; set; }
        Brush Background { get; set; }
        Pen Border { get; set; }
        Point Position { get; set; }
        Size Size { get; }
        int BlockSize { get; set; }
    }
	
	public class GraphicsRenderer : IGraphicsRenderer
    {
        private IBlocks<IBlock> _blocks;

        private int _blockSize;

        public virtual IBlocks<IBlock> Blocks 
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

        public GraphicsRenderer(IBlocks<IBlock> blocks, int blockSize)
        {

        	_blocks = blocks;
        	BlockSize = blockSize;
        }

	    private void RenderBlocks(IEnumerable<IBlock> blocks)
	    {
            foreach (var b in blocks)
            {
                if (b is IPiece) RenderBlocks(b as IPiece);
                else Graphics.FillRectangle(b.Brush, b.Position.X * BlockSize, b.Position.Y * BlockSize, BlockSize, BlockSize);
            }
        }
        
        protected virtual void RenderBlocks()
        {
            RenderBlocks(Blocks);
        }
        
        protected virtual void RenderBackground()
        {
        	if (Background != null) Graphics.FillRectangle(Background, 0, 0, Size.Width, Size.Height);
        }
        
        protected virtual void RenderBorder()
        {
        	if (Border != null) Graphics.DrawRectangle(Border, 0, 0, Size.Width - 1, Size.Height - 1);
        }
        
    	public virtual void Render()
    	{
    		var min = MinPoint();
    		var center = CenterPoint();
    		
    		Graphics.TranslateTransform(Position.X, Position.Y);
            RenderBackground();
            
			Graphics.TranslateTransform(-min.X + center.X, -min.Y + center.Y);
			RenderBlocks();
            
            Graphics.TranslateTransform(min.X - center.X, min.Y - center.Y);       
            RenderBorder();
            
            Graphics.TranslateTransform(-Position.X , -Position.Y);
        }
        
    }
}
