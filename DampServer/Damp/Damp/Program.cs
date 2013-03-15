#region

using System;

#endregion

namespace Damp
{
    public class Program
    {
        private static void Main(string[] args)
        {
            //SocketHandler s = new SocketHandler();

            var gameHandler = new GameHandler("cs11.zip");


            Console.Read();
        }
    }
}