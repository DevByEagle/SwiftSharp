using System;
#if LINUX
using static SwiftSharp.Glibc;
#elif MACOS
using static SwiftSharp.Darwin;
#endif

namespace Sandbox
{
    internal class Program
    {
        public static void Main()
        {
            
        }
    }
}