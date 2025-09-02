using System;
using System.Collections;
using System.Collections.Generic; // TODO: Replace the current collection with a custom KeyValuePairs implementation for better performance or type safety.
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace SwiftSharp
{
    [StructLayout(LayoutKind.Sequential)]
    public readonly struct Dictionary<Key, Value> : IEnumerable<KeyValuePair<Key, Value>> where Key : notnull
    {
        private readonly System.Collections.Generic.Dictionary<Key, Value> _storage;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal Dictionary(System.Collections.Generic.Dictionary<Key, Value> _native)
        {
            _storage = new System.Collections.Generic.Dictionary<Key, Value>(_native);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        /// <summary>
        /// Creates an empty dictionary.
        /// </summary>
        public Dictionary() : this(Array.Empty<(Key key, Value value)>()) { }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Dictionary(params (Key key, Value value)[] elements)
        {
            var native = new System.Collections.Generic.Dictionary<Key, Value>(elements.Length);

            foreach (var (key, value) in elements)
            {
                if (native.ContainsKey(key))
                    throw new ArgumentException("Dictionary literal contains duplicate keys");

                native[key] = value;
            }

            _storage = native;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerator<KeyValuePair<Key, Value>> GetEnumerator() => _storage.GetEnumerator();

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public int Count
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => _storage.Count;
        }

        public bool IsEmpty
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => Count == 0;
        }

        public Value? this[Key key]
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => _storage.TryGetValue(key, out var val) ? val : default;
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            set
            {
                if (value is not null)
                    _storage[key] = value;
                else
                    _storage.Remove(key);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Value? UpdateValue(Value value, Key key)
        {
            if (_storage.TryGetValue(key, out var oldValue))
            {
                _storage[key] = value;
                return oldValue;
            }
            else
            {
                _storage[key] = value;
                return default;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override int GetHashCode()
        {
            int commutativeHash = 0;

            foreach (var kvp in this)
            {
                var elementHasher = new HashCode();
                elementHasher.Add(kvp.Key);
                elementHasher.Add(kvp.Value);

                commutativeHash ^= elementHasher.ToHashCode();
            }

            var finalHasher = new HashCode();
            finalHasher.Add(commutativeHash);
            return finalHasher.ToHashCode();
        }
    }
}