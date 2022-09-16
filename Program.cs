using System;

namespace Duelyst
{
#if WINDOWS || XBOX
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            using (DuelystArena game = new DuelystArena())
            {
                game.Run();
            }
        }
    }
#endif
}

