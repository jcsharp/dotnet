#region Copyright © 2014 Jean-Claude Manoli
/*
 *  This file is licensed to you under the MIT license.
 *  See the LICENSE file in the project root for more information.
 */
#endregion

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manoli.Collections
{
    /// <summary>
    /// A generic collection of key/<see cref="List{TValue}"/> pairs.
    /// </summary>
    /// <typeparam name="TKey">The type of keys in the dictionary.</typeparam>
    /// <typeparam name="TValue">The type of values in the dictionary lists.</typeparam>
    public class ListDictionary<TKey, TValue> : CollectionDictionary<TKey, TValue, Dictionary<TKey, List<TValue>>, List<TValue>>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ListDictionary{TKey, TValue}"/> class.
        /// </summary>
        public ListDictionary() : base(new Dictionary<TKey, List<TValue>>()) { }
    }

    /// <summary>
    /// A generic collection of key/<see cref="HashSet{TValue}"/> pairs.
    /// </summary>
    /// <typeparam name="TKey">The type of keys in the dictionary.</typeparam>
    /// <typeparam name="TValue">The type of values in the dictionary sets.</typeparam>
    public class SortedSetDictionary<TKey, TValue> : CollectionDictionary<TKey, TValue, SortedDictionary<TKey, SortedSet<TValue>>, SortedSet<TValue>>
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="SetDictionary{TKey, TValue}"/> class.
        /// </summary>
        public SortedSetDictionary() : base(new SortedDictionary<TKey, SortedSet<TValue>>()) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="SetDictionary{TKey, TValue}"/> class.
        /// </summary>
        /// <param name="comparer">The comparer to use for the keys.</param>
        public SortedSetDictionary(IComparer<TValue> comparer)
            : base(new SortedDictionary<TKey, SortedSet<TValue>>(), () => new SortedSet<TValue>(comparer)) { }
    }

    /// <summary>
    /// A generic collection of key/<see cref="HashSet{TValue}"/> pairs.
    /// </summary>
    /// <typeparam name="TKey">The type of keys in the dictionary.</typeparam>
    /// <typeparam name="TValue">The type of values in the dictionary sets.</typeparam>
    public class SetDictionary<TKey, TValue> : CollectionDictionary<TKey, TValue, Dictionary<TKey, HashSet<TValue>>, HashSet<TValue>>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SetDictionary{TKey, TValue}"/> class.
        /// </summary>
        public SetDictionary() : base(new Dictionary<TKey, HashSet<TValue>>()) { }
    }

    /// <summary>
    /// A generic collection of key/collection pairs.
    /// </summary>
    /// <typeparam name="TKey">The type of keys in the dictionary.</typeparam>
    /// <typeparam name="TValue">The type of values in the dictionary.</typeparam>
    /// <typeparam name="TDictionary">The type of the dictionary.</typeparam>
    /// <typeparam name="TCollection">The type of the collection.</typeparam>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1005:AvoidExcessiveParametersOnGenericTypes")]
    [DebuggerDisplay("Count = {Count}")]
    public class CollectionDictionary<TKey, TValue, TDictionary, TCollection> : IDictionary<TKey, TCollection>
        where TDictionary : IDictionary<TKey, TCollection>
        where TCollection : ICollection<TValue>, new()
    {
        private Func<TCollection> collectionFactory;
        private TDictionary dictionary;

        /// <summary>
        /// Initializes a new instance of the <see cref="CollectionDictionary{TKey, TValue, TDictionary, TCollection}"/> class.
        /// </summary>
        /// <param name="dictionary">The internal dictionary instance to use.</param>
        public CollectionDictionary(TDictionary dictionary) : this(dictionary, null) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="CollectionDictionary{TKey, TValue, TDictionary, TCollection}"/> class.
        /// </summary>
        /// <param name="dictionary">The internal dictionary instance to use.</param>
        /// <param name="collectionFactory">A function used to create new instances of the list values.</param>
        public CollectionDictionary(TDictionary dictionary, Func<TCollection> collectionFactory)
        {
            this.dictionary = dictionary;
            this.collectionFactory = collectionFactory;
        }

        /// <summary>
        /// Adds an element with the provided key and value to the dictionary.
        /// </summary>
        /// <param name="key">The object to use as the key of the element to add.</param>
        /// <param name="value">The object to use as the value of the element to add.</param>
        /// <exception cref="System.ArgumentNullException">key is null.</exception>
        /// <exception cref="System.NotSupportedException">The dictionary is read-only.</exception>
        public void Add(TKey key, TValue value)
        {
            this[key].Add(value);
        }

        /// <summary>
        /// Adds an element with the provided key and value to the <see cref="T:System.Collections.Generic.IDictionary`2" />.
        /// </summary>
        /// <param name="key">The object to use as the key of the element to add.</param>
        /// <param name="value">The object to use as the value of the element to add.</param>
        /// <exception cref="System.ArgumentNullException">key is null.</exception>
        /// <exception cref="System.ArgumentException">An element with the same key already exists in the dictionary.</exception>
        /// <exception cref="System.NotSupportedException">The dictionary is read-only.</exception>
        public void Add(TKey key, TCollection value)
        {
            dictionary.Add(key, value);
        }

        /// <summary>
        /// Determines whether the <see cref="T:System.Collections.Generic.IDictionary`2" /> contains an element with the specified key.
        /// </summary>
        /// <param name="key">The key to locate in the <see cref="T:System.Collections.Generic.IDictionary`2" />.</param>
        /// <returns>
        /// true if the <see cref="T:System.Collections.Generic.IDictionary`2" /> contains an element with the key; otherwise, false.
        /// </returns>
        public bool ContainsKey(TKey key)
        {
            return dictionary.ContainsKey(key);
        }

        /// <summary>
        /// Gets an <see cref="T:System.Collections.Generic.ICollection`1" /> containing the keys of the <see cref="T:System.Collections.Generic.IDictionary`2" />.
        /// </summary>
        /// <returns>An <see cref="T:System.Collections.Generic.ICollection`1" /> containing the keys of the object that implements <see cref="T:System.Collections.Generic.IDictionary`2" />.</returns>
        public ICollection<TKey> Keys
        {
            get { return dictionary.Keys; }
        }

        /// <summary>
        /// Removes the element with the specified key from the <see cref="T:System.Collections.Generic.IDictionary`2" />.
        /// </summary>
        /// <param name="key">The key of the element to remove.</param>
        /// <returns>
        /// true if the element is successfully removed; otherwise, false.  This method also returns false if <paramref name="key" /> was not found in the original <see cref="T:System.Collections.Generic.IDictionary`2" />.
        /// </returns>
        public bool Remove(TKey key)
        {
            return dictionary.Remove(key);
        }

        /// <summary>
        /// Gets the value associated with the specified key.
        /// </summary>
        /// <param name="key">The key whose value to get.</param>
        /// <param name="value">When this method returns, the value associated with the specified key, if the key is found; otherwise, the default value for the type of the <paramref name="value" /> parameter. This parameter is passed uninitialized.</param>
        /// <returns>
        /// true if the object that implements <see cref="T:System.Collections.Generic.IDictionary`2" /> contains an element with the specified key; otherwise, false.
        /// </returns>
        public bool TryGetValue(TKey key, out TCollection value)
        {
            return dictionary.TryGetValue(key, out value);
        }

        /// <summary>
        /// Gets an <see cref="T:System.Collections.Generic.ICollection`1" /> containing the values in the <see cref="T:System.Collections.Generic.IDictionary`2" />.
        /// </summary>
        /// <returns>An <see cref="T:System.Collections.Generic.ICollection`1" /> containing the values in the object that implements <see cref="T:System.Collections.Generic.IDictionary`2" />.</returns>
        public ICollection<TCollection> Values
        {
            get { return dictionary.Values; }
        }

        /// <summary>
        /// Gets or sets the element with the specified key.
        /// </summary>
        /// <param name="key">The key of the element to get or set.</param>
        /// <returns>The collection with the specified key.</returns>
        /// <exception cref="ArgumentNullException">key is null.</exception>
        /// <exception cref="NotSupportedException">The dictionary is read-only.</exception>
        public TCollection this[TKey key]
        {
            get
            {
                TCollection col;
                if (!dictionary.TryGetValue(key, out col))
                {
                    col = (collectionFactory == null) ? new TCollection() : collectionFactory();
                    Add(key, col);
                }
                return col;
            }
            set
            {
                dictionary[key] = value;
            }
        }

        /// <summary>
        /// If the collection
        /// </summary>
        /// <param name="key">The key of the collection to initialize.</param>
        public void InitializeCollection(TKey key)
        {
            var col = (collectionFactory == null) ? new TCollection() : collectionFactory();
            Add(key, col);
        }

        /// <summary>
        /// Adds an item to the dictionary.
        /// </summary>
        /// <param name="item">The object to add to the dictionary.</param>
        public void Add(KeyValuePair<TKey, TCollection> item)
        {
            dictionary.Add(item);
        }

        /// <summary>
        /// Removes all items from the dictionary.
        /// </summary>
        public void Clear()
        {
            dictionary.Clear();
        }

        /// <summary>
        /// Determines whether the dictionary contains a specific value.
        /// </summary>
        /// <param name="item">The object to locate in the dictionary.</param>
        /// <returns>
        /// true if <paramref name="item" /> is found in the dictionary; otherwise, false.
        /// </returns>
        public bool Contains(KeyValuePair<TKey, TCollection> item)
        {
            return dictionary.Contains(item);
        }

        /// <summary>
        /// Copies the collections of the dictionary to an
        /// <see cref="System.Array"/>, starting at a particular index.
        /// </summary>
        /// <param name="array">The one-dimensional <see cref="System.Array"/> that is the destination of the elements
        /// copied from the dictionary. The array must have zero-based indexing.</param>
        /// <param name="arrayIndex">Index of the array.</param>
        public void CopyTo(KeyValuePair<TKey, TCollection>[] array, int arrayIndex)
        {
            dictionary.CopyTo(array, arrayIndex);
        }

        /// <summary>
        /// Gets the number of collections contained in the dictionary.
        /// </summary>
        /// <returns>The number of elements contained in the dictionary.</returns>
        public int Count
        {
            get { return dictionary.Count; }
        }

        /// <summary>
        /// Gets a value indicating whether the dictionary is read-only.
        /// </summary>
        /// <returns>true if the dictionary is read-only; otherwise, false.</returns>
        public bool IsReadOnly
        {
            get { return dictionary.IsReadOnly; }
        }

        /// <summary>
        /// Removes the first occurrence of a specific object from the dictionary.
        /// </summary>
        /// <param name="item">The object to remove from the dictionary.</param>
        /// <returns>
        /// true if <paramref name="item" /> was successfully removed from the dictionary; otherwise, false. This method also returns false if <paramref name="item" /> is not found in the original dictionary.
        /// </returns>
        public bool Remove(KeyValuePair<TKey, TCollection> item)
        {
            return dictionary.Remove(item);
        }

        /// <summary>
        /// Returns an enumerator that iterates through the collection.
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.Collections.Generic.IEnumerator`1" /> that can be used to iterate through the collection.
        /// </returns>
        public IEnumerator<KeyValuePair<TKey, TCollection>> GetEnumerator()
        {
            return dictionary.GetEnumerator();
        }

        /// <summary>
        /// Returns an enumerator that iterates through a collection.
        /// </summary>
        /// <returns>
        /// An <see cref="T:System.Collections.IEnumerator" /> object that can be used to iterate through the collection.
        /// </returns>
        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return dictionary.GetEnumerator();
        }
    }
}
