using System;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace Tetris
{
	public interface IRenderer
    {
    	IBlockCollection Blocks { get; }
        Point Position { get; set; }
        Size Size { get; }
        int BlockSize { get; set; }
    	void Render();
    }

    public interface IGraphicsRenderer : IRenderer
    {
        Graphics Graphics { get; set; }
        Brush Background { get; set; }
        Pen Border { get; set; }
    }

    public interface IBoardRenderer : IGraphicsRenderer
    {
        IBoard Board { get; }
    }

    public interface IPieceRenderer : IGraphicsRenderer
    {
        IPiece Piece { get; set; }
    }

    public interface IGameRenderer : IBoardRenderer
    {
        IScore Score { get; set; }
    }
}
