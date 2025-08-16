using System;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;

namespace SwiftSharp.Memory
{
    public unsafe struct UnsafePointer<T> where T : unmanaged
    {
        #region Fields
        #endregion

        #region Constructors
        #endregion

        #region Properties
        #endregion

        #region Methods

        /// <summary>
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Deallocate()
        {

        }

        #endregion
    }
}