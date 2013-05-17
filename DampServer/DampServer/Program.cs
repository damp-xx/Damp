#region

using System;

#endregion

namespace DampServer
{
    public class Program
    {
        private static void Main(string[] args)
        {
            var notify = new Notifier();
             var s = new SocketHandler();

           // var gameHandler = new GameHandler("cs11.zip");


            Console.Read();
        }
    }
}