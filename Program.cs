/*
 * Created by SharpDevelop.
 * User: m25326
 * Date: 2016-10-24
 * Time: 08:18
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Security.Policy;

namespace Tetris
{
	class Program
	{
	    public static void Main(string[] args)
	    {
	    	var board = new Board();

	        var renderer = new ConsoleRenderer(board);
            
	        while (true)
	        {
		        
	            renderer.Render();
                board.Tick();

                var key = Console.ReadKey(true).Key;
		        switch (key)
		        {
		            case ConsoleKey.A:
		                board.MovePiece(-1);
		                break;
		            case ConsoleKey.D:
		                board.MovePiece(1);
		                break;
		            case ConsoleKey.LeftArrow:
		                board.RotatePiece(-90);
		                break;
                    case ConsoleKey.RightArrow:
                        board.RotatePiece(90);
                        break;
                }
	    	}
	    }

	}
}