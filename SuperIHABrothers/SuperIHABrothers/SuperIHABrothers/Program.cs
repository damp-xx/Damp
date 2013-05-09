using System;

namespace SuperIHABrothers
{
#if WINDOWS || XBOX
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            
            /****************** ONLY FOR TESTING **************************/
            if (args.Length < 1)
            {
                args = new string[2];
                args[0] = "NOPIPE";
                args[1] = "NOPIPE";
            }
            /***************************************************************/

            using (Game1 game = new Game1(args[0], args[1]))
            {
                game.Run();
            }
        }
    }
#endif
}

