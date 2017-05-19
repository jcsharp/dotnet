#region Copyright © 2016 Jean-Claude Manoli
/*
 *  This file is licensed to you under the MIT license.
 *  See the LICENSE file in the project root for more information.
 */
#endregion

using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manoli.Collections
{
    /// <summary>
    /// An implementation of a priority queue using a heap data structure.
    /// </summary>
    /// <typeparam name="TKey">The type of the sort key.</typeparam>
    /// <typeparam name="TValue">The type of value stored in the queue.</typeparam>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1711:IdentifiersShouldNotHaveIncorrectSuffix")]
    [DebuggerDisplay("Count = {Count}")]
    public class PriorityQueue<TKey, TValue> where TKey : IComparable<TKey>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PriorityQueue{TKey, TValue}"/> class.
        /// </summary>
        public PriorityQueue() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="PriorityQueue{TKey, TValue}"/> class.
        /// </summary>
        /// <param name="keySelector">A function that extracts a key from a value instance.</param>
        public PriorityQueue(Func<TValue, TKey> keySelector)
        {
            this.keySelector = keySelector;
        }

        private List<KeyValuePair<TKey, TValue>> heap = new List<KeyValuePair<TKey, TValue>>();
        private Func<TValue, TKey> keySelector;

        /// <summary>
        /// Gets the number of elements contained in the queue.
        /// </summary>
        /// <value>
        /// The number of elements contained in the queue.
        /// </value>
        public int Count { get { return heap.Count; } }

        /// <summary>
        /// Gets a value indicating whether the queue is empty.
        /// </summary>
        /// <value>
        ///   <c>true</c> if the queue is empty; otherwise, <c>false</c>.
        /// </value>
        public bool IsEmpty { get { return heap.Count == 0; } }

        /// <summary>
        /// Adds an object to the end of the queue.
        /// </summary>
        /// <param name="value">The value to add.</param>
        /// <exception cref="System.InvalidOperationException">The key selector is not set.</exception>
        public void Enqueue(TValue value)
        {
            if (keySelector == null)
                throw new InvalidOperationException("Key selector is not set.");

            Enqueue(keySelector(value), value);
        }

        /// <summary>
        /// Adds an object to the end of the queue using the specified key.
        /// </summary>
        /// <param name="key">The sort key of the value.</param>
        /// <param name="value">The value to add.</param>
        public void Enqueue(TKey key, TValue value)
        {
            var kv = new KeyValuePair<TKey, TValue>(key, value);
            heap.Add(kv);

            int pos = heap.Count - 1;
            while (pos > 0)
            {
                int ppos = (pos - 1) / 2;
                var parent = heap[ppos];
                if (parent.Key.CompareTo(key) > 0)
                {
                    heap[pos] = parent;
                    pos = ppos;
                }
                else break;
            }
            heap[pos] = kv;
        }

        /// <summary>
        /// Returns the object at the beginning of the queue without removing it.
        /// </summary>
        /// <returns>The object at the beginning of the queue.</returns>
        /// <exception cref="System.InvalidOperationException">The queue is empty.</exception>
        public TValue Peek()
        {
            if (heap.Count == 0) throw new InvalidOperationException("The queue is empty.");
            return heap[0].Value;
        }

        /// <summary>
        /// Removes and returns the object at the beginning of the queue.
        /// </summary>
        /// <returns>The object that is removed from the beginning of the queue.</returns>
        /// <exception cref="System.InvalidOperationException">The queue is empty.</exception>
        public TValue Dequeue()
        {
            if (heap.Count == 0) throw new InvalidOperationException("The queue is empty.");
            var kv = heap[0];

            int pos = heap.Count - 1;
            heap[0] = heap[pos];
            heap.RemoveAt(pos);

            pos = 0;
            while (true)
            {
                int smallest = pos;
                int left = 2 * pos + 1;
                int right = left + 1;
                if (left < heap.Count && heap[smallest].Key.CompareTo(heap[left].Key) > 0)
                {
                    smallest = left;
                }
                if (right < heap.Count && heap[smallest].Key.CompareTo(heap[right].Key) > 0)
                {
                    smallest = right;
                }
                if (pos == smallest) break;
                Swap(pos, smallest);
                pos = smallest;
            }

            return kv.Value;
        }

        private void Swap(int pos1, int pos2)
        {
            var t = heap[pos2];
            heap[pos2] = heap[pos1];
            heap[pos1] = t;
        }
    }
}
