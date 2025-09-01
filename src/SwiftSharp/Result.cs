using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace SwiftSharp
{
    [StructLayout(LayoutKind.Sequential)]
    public readonly struct Result<TSuccess, TFailure> where TFailure : Exception
    {
        #region Fields
        private readonly TSuccess success;
        private readonly TFailure failure;
        #endregion

        #region Constructors
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private Result(TSuccess value, TFailure error, bool isSuccess)
        {
            success = value;
            failure = error;
            IsSuccess = isSuccess;
        }
        #endregion

        #region Properties
        public bool IsSuccess { get; }
        #endregion

        #region Methods
        public static Result<TSuccess, TFailure> Success(TSuccess value) => new(value, default!, true);
        public static Result<TSuccess, TFailure> Failure(TFailure error) => new(default!, error, false);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public TSuccess Get()
        {
            if (IsSuccess)
                return success;
            throw failure;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Result<NewSuccess, TFailure> Map<NewSuccess>(Func<TSuccess, NewSuccess> transform)
        {
            if (IsSuccess)
                return Result<NewSuccess, TFailure>.Success(transform(success));
            return Result<NewSuccess, TFailure>.Failure(failure);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Result<TSuccess, NewFailure> MapError<NewFailure>(Func<TFailure, NewFailure> transform) where NewFailure : Exception
        {
            if (!IsSuccess)
                return Result<TSuccess, NewFailure>.Failure(transform(failure));
            return Result<TSuccess, NewFailure>.Success(success);
        }

        public static bool operator ==(Result<TSuccess, TFailure> a, Result<TSuccess, TFailure> b)
        {
            if (a.IsSuccess && b.IsSuccess)
                return a.success!.Equals(b.success);
            if (!a.IsSuccess && !b.IsSuccess)
                return a.failure.Equals(b.failure);
            return false;
        }

        public static bool operator !=(Result<TSuccess, TFailure> lhs, Result<TSuccess, TFailure> rhs) => !(lhs == rhs);

        public override bool Equals([NotNullWhen(true)] object? obj)
        {
            if (obj is Result<TSuccess, TFailure> other)
                return this == other;
            return false;
        }

        public override int GetHashCode()
        {
            return IsSuccess ? success!.GetHashCode() : failure.GetHashCode();
        }
        #endregion
    }
}