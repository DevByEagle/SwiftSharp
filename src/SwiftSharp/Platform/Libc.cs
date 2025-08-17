using System;
using System.Runtime.InteropServices;

namespace SwiftSharp
{
#if WINDOWS
    internal static class Libc
    {
        // Memory
        [DllImport("msvcrt.dll")] public static extern IntPtr Malloc(ulong size);
        [DllImport("msvcrt.dll")] public static extern void Free(IntPtr ptr);
    }
#elif LINUX || OSX
    internal static class Libc
    {
        // Memory
        [DllImport("libc")] public static extern IntPtr Malloc(ulong size);
        [DllImport("libc")] public static extern void Free(IntPtr ptr);
    }
#endif
}