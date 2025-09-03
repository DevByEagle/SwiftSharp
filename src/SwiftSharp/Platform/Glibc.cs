using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace SwiftSharp
{
#if LINUX
    [StructLayout(LayoutKind.Sequential)]
    public static class Glibc
    {
        #region Lib Names
        private const string LIBC = "libc";
        #endregion

        #region System Calls
        #endregion

        #region Process
        [DllImport(LIBC, EntryPoint = "getpid")]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static extern int GetPid();
        #endregion

        #region File APIs
        #endregion

        #region Memory
        [DllImport(LIBC, EntryPoint = "malloc")]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static extern IntPtr Malloc(ulong size);

        [DllImport(LIBC, EntryPoint = "malloc")]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static extern void Free(IntPtr ptr);
        #endregion

        #region Network
        #endregion

        #region C Runtime
        [DllImport(LIBC, EntryPoint = "printf", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static extern int Printf(string format);
        #endregion

        #region Helpers
        public static void Printf(string format, params object[] args)
        {
            string formatted = string.Format(format, args);
            Printf(formatted);
        }
        #endregion
    }
#endif
}