using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;

namespace SwiftSharp
{
    [StructLayout(LayoutKind.Sequential)]
    public readonly struct Array<T> : IEnumerable<T>
    {
        private readonly List<T> storage;
        private readonly ReaderWriterLockSlim _lock = new(LockRecursionPolicy.SupportsRecursion);

        #region Constructors

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Array()
        {
            storage = new List<T>();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Array(IEnumerable<T> elements)
        {
            if (elements == null)
                throw new ArgumentNullException(nameof(elements));
            storage = [.. elements];
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Array(T repeatedValue, int count)
        {
            if (count < 0)
                throw new ArgumentOutOfRangeException(nameof(count));
            storage = new List<T>(count);
            for (int i = 0; i < count; i++)
                storage.Add(repeatedValue);
        }

        #endregion

        #region Properties

        public int Count
        {
            get
            {
                _lock.EnterReadLock();
                try
                { return storage.Count; }
                finally { _lock.ExitReadLock(); }
            }
        }

        #endregion

        #region Operators

        public T this[int index]
        {
            get
            {
                _lock.EnterReadLock();
                try
                { return storage[index]; }
                finally { _lock.ExitReadLock(); }
            }
            set
            {
                _lock.EnterWriteLock();
                try
                { storage[index] = value; }
                finally { _lock.ExitWriteLock(); }
            }
        }

        #endregion

        #region Methods

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Append(T newElement)
        {
            _lock.EnterWriteLock();
            try
            { storage.Add(newElement); }
            finally { _lock.ExitWriteLock(); }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Insert(T newElement, int at)
        {
            _lock.EnterWriteLock();
            try
            { storage.Insert(at, newElement); }
            finally { _lock.ExitWriteLock(); }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public T Remove(int at)
        {
            _lock.EnterWriteLock();
            try
            {
                var item = storage[at];
                storage.RemoveAt(at);
                return item;
            }
            finally { _lock.ExitWriteLock(); }
        }

        #endregion

        #region Utility Methods

        public IEnumerator<T> GetEnumerator()
        {
            _lock.EnterReadLock();
            try
            { return new List<T>(storage).GetEnumerator(); }
            finally { _lock.ExitReadLock(); }
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public override string ToString()
        {
            _lock.EnterReadLock();
            try
            {
                return "[" + string.Join(", ", storage) + "]";
            }
            finally
            {
                _lock.ExitReadLock();
            }
        }

        #endregion
    }
}