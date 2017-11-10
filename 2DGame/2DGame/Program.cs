using System;

namespace Intro2DGame
{
    /// <summary>
    /// The main class.
    /// </summary>
    public static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        private static void Main(params String[] args)
        {
            using (var game = new Game.Game(args))
                game.Run();
        }
    }
}
