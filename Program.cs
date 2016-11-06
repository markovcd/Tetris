using System;
using System.Windows.Forms;

namespace Tetris
{
    internal static class Program
    {
        [STAThread]
        private static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(GameFactory.GetGame());
        }

        private static void Test()
        {
            IHeader<IItem> h = new Header<ItemA>();
        }

    }

    interface IHeader<out T> where T : IItem { }
    interface IHeader2<out T> : IHeader<T> where T : IItem { }

    class Header<T> : IHeader<T> where T : IItem { }


    interface IItem { }
    class ItemA : IItem { }
    class ItemB : IItem { }


}
