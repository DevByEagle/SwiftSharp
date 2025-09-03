using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace SwiftSharp
{
#if WINDOWS
    public static class WinSDK
    {
        #region Lib Names
        public const string KERNEL32 = "kernel32.dll";
        public const string USER32 = "user32.dll";
        #endregion

        #region Constants
        public const int STD_OUTPUT_HANDLE = -11;
        #endregion

        #region System Calls
        #endregion

        #region Process
        #endregion

        #region File APIs
        #endregion

        #region Memory
        #endregion

        #region Network
        #endregion

        #region Others
        [DllImport(KERNEL32, SetLastError = true)]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static extern IntPtr GetStdHandle(int nStdHandle);
        #endregion

        #region Helpers
        #endregion
    }
#endif
}