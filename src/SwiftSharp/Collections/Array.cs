using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace SwiftSharp.Collections
{
    [StructLayout(LayoutKind.Sequential)]
    public readonly struct Array<T> : IEnumerable<T>
    {
        private readonly List<T> _storage;

        #region Properties

        /// <summary>
        /// The number of elements in the array.
        /// </summary>
        public int Count => _storage.Count;

        /// <summary>
        /// Returns true if the array is empty.
        /// </summary>
        public bool IsEmpty => _storage.Count == 0;

        /// <summary>
        /// Gets or sets the element at the specified index.
        /// </summary>
        public T this[int index]
        {
            get => _storage[index];
            set => _storage[index] = value;
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Creates a new, empty array.
        /// </summary>
        public Array()
        {
            _storage = [];
        }

        /// <summary>
        /// Creates an array from an existing collection.
        /// </summary>
        public Array(IEnumerable<T> collection)
        {
            _storage = [.. collection];
        }

        #endregion

        #region Mutation Methods

        /// <summary>
        /// Adds a new element at the end of the array
        /// </summary>
        /// <param name="newElement">The element to append to the array.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Append(T newElement) => _storage.Add(newElement);

        /// <summary>
        /// Inserts a new element at the specified position.
        /// </summary>
        /// <param name="newElement">The new element to insert into the array.</param>
        /// <param name="at">The position at which to insert the new element.</param>
        public void Insert(T newElement, int at) => _storage.Insert(at, newElement);

        /// <summary>
        /// Removes the element at the specified position.
        /// </summary>
        /// <param name="at">The position of the element to remove.</param>
        public void Remove(int at) => _storage.RemoveAt(at);

        /// <summary>
        /// 
        /// </summary>
        public bool Remove(T element) => _storage.Remove(element);

        /// <summary>
        /// Removes all the elements that satisfy the given predicate.
        /// </summary>
        public int RemoveAll(Predicate<T> match) => _storage.RemoveAll(match);

        /// <summary>
        /// Removes all elements from the array.
        /// </summary>
        public void RemoveAll() => _storage.Clear();

        #endregion

        #region Search & Query Methods

        /// <summary>
        /// Returns a Boolean value indicating whether the sequence contains the given element.
        /// <paramref name="element"/>The element to find in the sequence.
        /// </summary>
        /// <returns><c>true</c> if the element was found in the sequence; otherwise, <c>false</c>.</returns>
        public bool Contains(T element) => _storage.Contains(element);

        #endregion

        #region Transformation Methods

        #endregion

        #region Utility Methods

        /// <summary>
        /// Sorts the collection in place.
        /// </summary>
        public void Sort() => _storage.Sort();

        /// <summary>
        /// 
        /// </summary>
        public void Sort(IComparer<T> comparer) => _storage.Sort(comparer);

        /// <summary>
        /// 
        /// </summary>
        public void Sort(Comparison<T> comparison) => _storage.Sort(comparison);

        public IEnumerator<T> GetEnumerator() => _storage.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public override string ToString() => $"[{string.Join(", ", _storage)}]";

        #endregion
    }
}