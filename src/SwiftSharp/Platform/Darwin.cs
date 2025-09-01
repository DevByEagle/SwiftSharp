using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace SwiftSharp
{
#if MACOS
    public static class Darwin
    {
        private const string LibName = "libSystem.dylib"; 

        #region System Calls
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