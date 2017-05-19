#region Copyright © 2014 Jean-Claude Manoli
/*
 *  This file is licensed to you under the MIT license.
 *  See the LICENSE file in the project root for more information.
 */
#endregion

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manoli.Collections
{
    /// <summary>
    /// Implements a dictionary of dictionary of list.
    /// </summary>
    /// <typeparam name="TOuterKey">The outer dictionary key type.</typeparam>
    /// <typeparam name="TInnerKey">The inner dictionary key type.</typeparam>
    /// <typeparam name="TValue">The list element type.</typeparam>
    public class ListDictionaryDictionary<TOuterKey, TInnerKey, TValue>
        : GenericDictionaryDictionary<TOuterKey, TInnerKey, List<TValue>,
            Dictionary<TOuterKey, ListDictionary<TInnerKey, TValue>>, ListDictionary<TInnerKey, TValue>>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ListDictionaryDictionary{TOuterKey, TInnerKey, TValue}"/> class.
        /// </summary>
        public ListDictionaryDictionary() : base(new Dictionary<TOuterKey, ListDictionary<TInnerKey, TValue>>()) { }
    }

    /// <summary>
    /// Implements a dictionary of dictionary of set.
    /// </summary>
    /// <typeparam name="TOuterKey">The outer dictionary key type.</typeparam>
    /// <typeparam name="TInnerKey">The inner dictionary key type.</typeparam>
    /// <typeparam name="TValue">The set element type.</typeparam>
    public class SetDictionaryDictionary<TOuterKey, TInnerKey, TValue>
        : GenericDictionaryDictionary<TOuterKey, TInnerKey, HashSet<TValue>,
            Dictionary<TOuterKey, SetDictionary<TInnerKey, TValue>>, SetDictionary<TInnerKey, TValue>>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SetDictionaryDictionary{TOuterKey, TInnerKey, TValue}"/> class.
        /// </summary>
        public SetDictionaryDictionary() : base(new Dictionary<TOuterKey, SetDictionary<TInnerKey, TValue>>()) { }
    }

    /// <summary>
    /// Implements a dictionary of dictionary that are both sorted on their keys.
    /// </summary>
    /// <typeparam name="TOuterKey">The outer dictionary key type.</typeparam>
    /// <typeparam name="TInnerKey">The inner dictionary key type.</typeparam>
    /// <typeparam name="TValue">The value element type.</typeparam>
    public class SortedDictionaryDictionary<TOuterKey, TInnerKey, TValue>
        : GenericDictionaryDictionary<TOuterKey, TInnerKey, TValue, SortedDictionary<TOuterKey, SortedDictionary<TInnerKey, TValue>>, SortedDictionary<TInnerKey, TValue>>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SortedDictionaryDictionary{TOuterKey, TInnerKey, TValue}"/> class.
        /// </summary>
        public SortedDictionaryDictionary() : base(new SortedDictionary<TOuterKey, SortedDictionary<TInnerKey, TValue>>()) { }
    }

    /// <summary>
    /// Implements a dictionary of sorted dictionary.
    /// </summary>
    /// <typeparam name="TOuterKey">The outer dictionary key type.</typeparam>
    /// <typeparam name="TInnerKey">The inner dictionary key type.</typeparam>
    /// <typeparam name="TValue">The value element type.</typeparam>
    public class DictionarySortedDictionary<TOuterKey, TInnerKey, TValue>
        : GenericDictionaryDictionary<TOuterKey, TInnerKey, TValue, Dictionary<TOuterKey, SortedDictionary<TInnerKey, TValue>>, SortedDictionary<TInnerKey, TValue>>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DictionarySortedDictionary{TOuterKey, TInnerKey, TValue}"/> class.
        /// </summary>
        public DictionarySortedDictionary() : base(new Dictionary<TOuterKey, SortedDictionary<TInnerKey, TValue>>()) { }
    }

    /// <summary>
    /// Implements a dictionary of dictionary.
    /// </summary>
    /// <typeparam name="TOuterKey">The outer dictionary key type.</typeparam>
    /// <typeparam name="TInnerKey">The inner dictionary key type.</typeparam>
    /// <typeparam name="TValue">The value element type.</typeparam>
    public class DictionaryDictionary<TOuterKey, TInnerKey, TValue>
        : GenericDictionaryDictionary<TOuterKey, TInnerKey, TValue, Dictionary<TOuterKey, Dictionary<TInnerKey, TValue>>, Dictionary<TInnerKey, TValue>>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DictionaryDictionary{TOuterKey, TInnerKey, TValue}"/> class.
        /// </summary>
        public DictionaryDictionary()
            : base(new Dictionary<TOuterKey, Dictionary<TInnerKey, TValue>>()) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="DictionaryDictionary{TOuterKey, TInnerKey, TValue}"/> class 
        /// based on an existing outer dictionary.
        /// </summary>
        /// <param name="outerDictionary">The outer dictionary instance.</param>
        public DictionaryDictionary(Dictionary<TOuterKey, Dictionary<TInnerKey, TValue>> outerDictionary)
            : base(outerDictionary) { }
    }

    /// <summary>
    /// The base class for all dictionaries of dictionary.
    /// </summary>
    /// <typeparam name="TOuterKey">The outer dictionary key type.</typeparam>
    /// <typeparam name="TInnerKey">The inner dictionary key type.</typeparam>
    /// <typeparam name="TValue">The value element type.</typeparam>
    /// <typeparam name="TOuterDictionary">The type of the outer dictionary.</typeparam>
    /// <typeparam name="TInnerDictionary">The type of the inner dictionary.</typeparam>
    public class GenericDictionaryDictionary<TOuterKey, TInnerKey, TValue, TOuterDictionary, TInnerDictionary>
        : IDictionary<TOuterKey, TInnerDictionary>
        where TOuterDictionary : IDictionary<TOuterKey, TInnerDictionary>
        where TInnerDictionary : IDictionary<TInnerKey, TValue>, new()
    {
        private TOuterDictionary outer;

        /// <summary>
        /// Initializes a new instance of the <see cref="GenericDictionaryDictionary{TOuterKey, TInnerKey, TValue, TOuterDictionary, TInnerDictionary}"/> class.
        /// </summary>
        /// <param name="outerDictionary">The outer dictionary instance.</param>
        public GenericDictionaryDictionary(TOuterDictionary outerDictionary)
        {
            this.outer = outerDictionary;
        }

        /// <summary>
        /// Adds an element to the dictionary of dictionary.
        /// </summary>
        /// <param name="outerKey">The outer key of the value.</param>
        /// <param name="innerKey">The inner key of the value.</param>
        /// <param name="value">The value to add.</param>
        public void Add(TOuterKey outerKey, TInnerKey innerKey, TValue value)
        {
            outer[outerKey].Add(innerKey, value);
        }

        /// <summary>
        /// Adds an element with the provided key and dictionary value.
        /// </summary>
        /// <param name="key">The object to use as the key of the element to add.</param>
        /// <param name="value">The dictionary to use as the value of the element to add.</param>
        public void Add(TOuterKey key, TInnerDictionary value)
        {
            outer.Add(key, value);
        }

        /// <summary>
        /// Determines whether the <see cref="T:System.Collections.Generic.IDictionary`2" /> contains an element with the specified key.
        /// </summary>
        /// <param name="key">The key to locate in the <see cref="T:System.Collections.Generic.IDictionary`2" />.</param>
        /// <returns>
        /// true if the <see cref="T:System.Collections.Generic.IDictionary`2" /> contains an element with the key; otherwise, false.
        /// </returns>
        public bool ContainsKey(TOuterKey key)
        {
            return outer.ContainsKey(key);
        }

        /// <summary>
        /// Gets an outer dictionary containing the keys of the <see cref="T:System.Collections.Generic.IDictionary`2" />.
        /// </summary>
        public ICollection<TOuterKey> Keys
        {
            get { return outer.Keys; }
        }

        /// <summary>
        /// Removes the element with the specified key from the outer dictionary.
        /// </summary>
        /// <param name="key">The key of the element to remove.</param>
        /// <returns>
        /// true if the element is successfully removed; otherwise, false.  This method also returns false if <paramref name="key" /> was not found in the original <see cref="T:System.Collections.Generic.IDictionary`2" />.
        /// </returns>
        public bool Remove(TOuterKey key)
        {
            return outer.Remove(key);
        }

        /// <summary>
        /// Removes the specified element from the inner dictionary.
        /// </summary>
        /// <param name="outerKey">The outer key of the element to remove.</param>
        /// <param name="innerKey">The inner key of the element to remove.</param>
        /// <returns>
        /// true if the element is successfully removed; otherwise, false. This method also returns false if either 
        /// <paramref name="outerKey" /> or <paramref name="innerKey"/> were not found in the dictionaries.
        /// </returns>
        public bool Remove(TOuterKey outerKey, TInnerKey innerKey)
        {
            return outer[outerKey].Remove(innerKey);
        }

        /// <summary>
        /// Gets the value associated with the specified key.
        /// </summary>
        /// <param name="key">The key whose value to get.</param>
        /// <param name="value">When this method returns, the value associated with the specified key, 
        /// if the key is found; otherwise, the default value for the type of the <paramref name="value" /> 
        /// parameter. This parameter is passed uninitialized.</param>
        /// <returns>
        /// true if the outer dictionary contains an element with the specified key; otherwise, false.
        /// </returns>
        public bool TryGetValue(TOuterKey key, out TInnerDictionary value)
        {
            return TryGetValue(key, out value);
        }

        /// <summary>
        /// Gets an outer dictionary containing the dictionary 
        /// values in the outer dictionary.
        /// </summary>
        public ICollection<TInnerDictionary> Values
        {
            get { return outer.Values; }
        }

        /// <summary>
        /// Gets or sets the inner dictionary with the specified key.
        /// </summary>
        /// <value>
        /// The inner dictionary value to get or set.
        /// </value>
        /// <param name="key">The key of the element to get or set.</param>
        public TInnerDictionary this[TOuterKey key]
        {
            get
            {
                TInnerDictionary inner;
                if (!outer.TryGetValue(key, out inner))
                {
                    inner = new TInnerDictionary();
                    Add(key, inner);
                }
                return inner;
            }
            set
            {
                outer[key] = value;
            }
        }

        /// <summary>
        /// Adds an item to the outer dictionary key-value pairs.
        /// </summary>
        /// <param name="item">The <see cref="KeyValuePair{TKey, TValue}"/> to add.</param>
        public void Add(KeyValuePair<TOuterKey, TInnerDictionary> item)
        {
            outer.Add(item);
        }

        /// <summary>
        /// Removes all items from the outer dictionary>.
        /// </summary>
        public void Clear()
        {
            outer.Clear();
        }

        /// <summary>
        /// Determines whether the outer dictionary contains a specific key-value pair.
        /// </summary>
        /// <param name="item">The <see cref="KeyValuePair{TKey, TValue}"/> to locate.</param>
        /// <returns>
        /// true if <paramref name="item" /> is found in the outer dictionary; otherwise, false.
        /// </returns>
        public bool Contains(KeyValuePair<TOuterKey, TInnerDictionary> item)
        {
            return outer.Contains(item);
        }

        /// <summary>
        /// Copies the elements of the outer dictionary to an <see cref="T:System.Array" />, starting at a particular <see cref="T:System.Array" /> index.
        /// </summary>
        /// <param name="array">The one-dimensional <see cref="T:System.Array" /> that is the destination of the elements copied from outer dictionary. The <see cref="T:System.Array" /> must have zero-based indexing.</param>
        /// <param name="arrayIndex">The zero-based index in <paramref name="array" /> at which copying begins.</param>
        public void CopyTo(KeyValuePair<TOuterKey, TInnerDictionary>[] array, int arrayIndex)
        {
            outer.CopyTo(array, arrayIndex);
        }

        /// <summary>
        /// Gets the number of elements contained in the outer dictionary.
        /// </summary>
        public int Count
        {
            get { return outer.Count; }
        }

        /// <summary>
        /// Gets a value indicating whether the dictionary is read-only.
        /// </summary>
        public bool IsReadOnly
        {
            get { return outer.IsReadOnly; }
        }

        /// <summary>
        /// Removes the first occurrence of a specific object from the outer dictionary.
        /// </summary>
        /// <param name="item">The object to remove from the outer dictionary.</param>
        /// <returns>
        /// true if <paramref name="item" /> was successfully removed from the outer dictionary; otherwise, false. This method also returns false if <paramref name="item" /> is not found in the original outer dictionary.
        /// </returns>
        public bool Remove(KeyValuePair<TOuterKey, TInnerDictionary> item)
        {
            return outer.Remove(item);
        }

        /// <summary>
        /// Returns an enumerator that iterates through the outer dictionary key-value pairs.
        /// </summary>
        public IEnumerator<KeyValuePair<TOuterKey, TInnerDictionary>> GetEnumerator()
        {
            return outer.GetEnumerator();
        }

        /// <summary>
        /// Returns an enumerator that iterates through the outer dictionary key-value pairs.
        /// </summary>
        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return outer.GetEnumerator();
        }
    }
}
