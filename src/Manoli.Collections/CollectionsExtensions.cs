#region Copyright © 2016 Jean-Claude Manoli
/*
 *  This file is licensed to you under the MIT license.
 *  See the LICENSE file in the project root for more information.
 */
#endregion

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manoli.Collections
{
    /// <summary>
    /// A set of extension methods for collections.
    /// </summary>
    public static class CollectionsExtensions
    {
        /// <summary>
        /// Merges the content of two dictionaries into a new dictionary.
        /// </summary>
        /// <typeparam name="TKey">The type of keys in the dictionaries.</typeparam>
        /// <typeparam name="TValue">The type of values in the dictionaries.</typeparam>
        /// <param name="first">The first dictionary to merge.</param>
        /// <param name="second">The second dictionary to merge.</param>
        /// <returns>A new dictionary with merge values.</returns>
        public static IReadOnlyDictionary<TKey, TValue> Merge<TKey, TValue>(
            this IReadOnlyDictionary<TKey, TValue> first,
            IReadOnlyDictionary<TKey, TValue> second)
        {
            if (first == null || first.Count == 0) return second;
            if (second == null || second.Count == 0) return first;
            var result = first.ToDictionary(d => d.Key, d => d.Value);
            foreach (var item in second)
            {
                result[item.Key] = item.Value;
            }
            return result;
        }

        /// <summary>
        /// Merges the content of a dictionary with another dictionary.
        /// </summary>
        /// <typeparam name="TKey">The type of keys in the dictionaries.</typeparam>
        /// <typeparam name="TValue">The type of values in the dictionaries.</typeparam>
        /// <param name="target">The first dictionary to merge.</param>
        /// <param name="other">The second dictionary to merge.</param>
        /// <exception cref="System.ArgumentNullException">
        /// <paramref name="target"/> is null.
        /// </exception>
        public static void Merge<TKey, TValue>(
            this IDictionary<TKey, TValue> target,
            IEnumerable<KeyValuePair<TKey, TValue>> other)
        {
            if (target == null) throw new ArgumentNullException(nameof(target));
            if (other != null)
            {
                foreach (var item in other)
                {
                    target[item.Key] = item.Value;
                }
            }
        }

        /// <summary>
        /// Determines whether the specified dictionary is equal to the current dictionary.
        /// </summary>
        /// <param name="current">The current dictionary.</param>
        /// <param name="other">The dictionary to copare with.</param>
        /// <returns>
        /// <c>true</c> if the specified dictionary keys and values are equal 
        /// to the current dictionary keys and values; otherwise, <c>false</c>.
        /// </returns>
        public static bool EquivalentTo(IDictionary current, IDictionary other)
        {
            if (current == null || other == null) return (current == other);
            return EquivalentTo(current.Values, other.Values)
                && EquivalentTo(current.Keys, other.Keys);
        }

        /// <summary>
        /// Determines whether the specified collection is equal to the current collection.
        /// </summary>
        /// <param name="current">The current collection.</param>
        /// <param name="other">The collection to copare with.</param>
        /// <returns>
        /// <c>true</c> if the specified collection values are equal to the 
        /// current collection values; otherwise, <c>false</c>.
        /// </returns>
        public static bool EquivalentTo(ICollection current, ICollection other)
        {
            if (current == null || other == null) return (current == other);
            if (current.Count != other.Count) return false;

            var ea = current.GetEnumerator();
            var eb = other.GetEnumerator();
            while (ea.MoveNext() && eb.MoveNext())
            {
                if (ea.Current != eb.Current &&
                    ea.Current.ToString() != eb.Current.ToString())
                    return false;
            }
            return true;
        }
    }
}
