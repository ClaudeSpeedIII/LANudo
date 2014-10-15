using System;

namespace LANudo
{
#if WINDOWS || XBOX
    static class Raiz
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            using (Motor principal = new Motor())
            {
                principal.Run();
            }
        }
    }
#endif
}

