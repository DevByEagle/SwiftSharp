using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace SwiftSharp
{
    [StructLayout(LayoutKind.Sequential)]
    public readonly struct Dictionary<Key, Value> where Key : notnull
    {
        #region Fields
        private readonly System.Collections.Generic.Dictionary<Key, Value> _native;
        #endregion

        #region Constructors
        /// <summary>
        /// Creates an empty dictionary.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Dictionary()
        {
            _native = new System.Collections.Generic.Dictionary<Key, Value>();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Dictionary(int minimumCapacity)
        {
            _native = new System.Collections.Generic.Dictionary<Key, Value>(minimumCapacity);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Dictionary(params (Key key, Value value)[] elements)
        {
            _native = new System.Collections.Generic.Dictionary<Key, Value>(elements.Length);
            foreach (var (key, value) in elements)
            {
                _native[key] = value;
            }
        }
        #endregion

        #region Properties
        public int Count => _native.Count;

        public bool IsEmpty => Count == 0;

        public Value? this[Key key]
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => _native.TryGetValue(key, out var value) ? value : default;

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            set => _native[key] = value;
        }

        public IEnumerable<Key> Keys
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return _native.Keys;
            }
        }
        #endregion

        #region Methods
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override int GetHashCode()
        {
            var hash = new HashCode();
            foreach (var kvp in _native)
            {
                hash.Add(kvp.Key);
                hash.Add(kvp.Value);
            }
            return hash.ToHashCode();
        }
        #endregion
    }
}
