using System;
using TankGame.GameSystemP;

namespace TankGame
{
#if WINDOWS || XBOX
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            using (GameSystem game = new GameSystem())
            {
                game.Run();
            }
        }
    }
#endif
}

