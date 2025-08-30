using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace SwiftSharp
{
#if UNIX
    [StructLayout(LayoutKind.Sequential)]
    public static class Glibc
    {
        [DllImport("libc", EntryPoint = "printf")] public static extern int Printf(string format);
    }
#endif
}