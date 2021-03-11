// MultiMediaTechnology / FHS
// MultiMediaProjekt 1
// Programming, Art & Music - Hofer Thomas
// Co-Music Producer - Veltman Bob

using System;

namespace SG_01
{
#if WINDOWS || LINUX
    /// <summary>
    /// The main class.
    /// </summary>
    public static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            
            using (var game = new Game1())
                game.Run();
            
        }
    }
#endif
}
