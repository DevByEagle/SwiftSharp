using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace SwiftSharp
{
#if MACOS
    public static class Darwin
    {
        #region Lib Names
        private const string LIBSYSTEM = "libSystem.dylib";
        #endregion

        #region System Calls
        #endregion

        #region Process
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [DllImport(LIBSYSTEM, EntryPoint = "malloc")]
        public static extern int GetPid();
        #endregion

        #region File APIs
        #endregion

        #region Memory
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [DllImport(LIBSYSTEM, EntryPoint = "malloc")]
        public static extern IntPtr Malloc(UIntPtr size);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [DllImport(LIBSYSTEM, EntryPoint = "free")]
        public static extern void Free(IntPtr ptr);
        #endregion

        #region Network
        #endregion

        #region Helpers
        #endregion
    }
#endif
}