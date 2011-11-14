using System;

namespace Survival
{
#if WINDOWS || XBOX
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            using (Survival game = new Survival())
            {
                game.Run();
            }
        }
    }
#endif
}

