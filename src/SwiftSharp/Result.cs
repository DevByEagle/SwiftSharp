using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace SwiftSharp
{
    [StructLayout(LayoutKind.Sequential)]
    public readonly struct Result<TSuccess, TFailure> where TFailure : Exception
    {
        private readonly TSuccess _value;
        private readonly TFailure _error;

        #region Constructors

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Result(Func<TSuccess> body)
        {
            try
            {
                _value = body();
                _error = default!;
            }
            catch (Exception ex)
            {
                _error = (TFailure)(object)ex;
                _value = default!;
            }
        }

        #endregion

        #region Properties



        #endregion

        #region Methods

        #endregion

        #region Utility Methods

        #endregion
    }
}