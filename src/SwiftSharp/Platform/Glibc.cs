using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace SwiftSharp
{
#if UNIX
    [StructLayout(LayoutKind.Sequential)]
    public static class Glibc
    {
        private const string LibName = "libc";

        #region System Calls
        [DllImport(LibName, EntryPoint = "printf")]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static extern int Printf(string format);
        #endregion

        #region Process
        #endregion

        #region File APIs
        #endregion

        #region Memory
        [DllImport(LibName, EntryPoint = "malloc")]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static extern IntPtr Malloc(ulong size);
        #endregion

        #region Network
        #endregion

        #region Helpers
        #endregion
    }
#endif
}