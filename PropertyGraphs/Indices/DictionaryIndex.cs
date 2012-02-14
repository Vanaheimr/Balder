/*
 * Copyright (c) 2010-2012, Achim 'ahzf' Friedland <code@ahzf.de>
 * This file is part of Blueprints.NET <http://www.github.com/ahzf/Blueprints.NET>
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *     http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

#region Usings

using System;
using System.Linq;
using System.Collections.Generic;

using de.ahzf.Illias.Commons;

using de.ahzf.Blueprints.Indices;
using de.ahzf.Blueprints.PropertyGraphs.Indices;

#endregion

namespace de.ahzf.Blueprints.PropertyGraphs.InMemory.Mutable
{

    /// <summary>
    /// A property element index is a data structure that supports the indexing of properties in property graphs.
    /// An index is typically some sort of tree structure that allows for the fast lookup of elements by key/value pairs.
    /// </summary>
    /// <typeparam name="TKey">The type of the index keys.</typeparam>
    /// <typeparam name="TValue">The type of the elements to be indexed.</typeparam>
    public class DictionaryIndex<TKey, TValue> : IIndex<TKey, TValue>
        where TKey   : IEquatable<TKey>,   IComparable<TKey>,   IComparable
        where TValue : IEquatable<TValue>, IComparable<TValue>, IComparable

    {

        #region Data

        /// <summary>
        /// The internal index datastructure.
        /// </summary>
        private readonly IDictionary<TKey, ISet<TValue>> Index;

        #endregion

        #region Properties

        #region Name

        /// <summary>
        /// The name of this index datastructure.
        /// </summary>
        public String Name
        {
            get
            {
                return this.GetType().Name;
            }
        }

        #endregion

        #region SupportsEfficentExactMatchQueries

        /// <summary>
        /// True if the index supports efficient range queries;
        /// False otherwise.
        /// </summary>
        public Boolean SupportsEfficentExactMatchQueries
        {
            get { return true; }
        }

        #endregion

        #region SupportsEfficentRangeQueries

        /// <summary>
        /// True if the index supports efficient range queries;
        /// False otherwise.
        /// </summary>
        public Boolean SupportsEfficentRangeQueries
        {
            get { return false; }
        }

        #endregion

        #endregion

        #region Constructor(s)

        #region DictionaryIndex()

        /// <summary>
        /// Creates a new DictionaryIndex.
        /// (Needed for activating this index datastructure.)
        /// </summary>
        public DictionaryIndex()
        {
            Index = new Dictionary<TKey, ISet<TValue>>();
        }

        #endregion

        #region DictionaryIndex(EqualityComparer)

        /// <summary>
        /// Creates a new DictionaryIndex.
        /// </summary>
        /// <param name="EqualityComparer">An optional equality comparer for the index key.</param>
        public DictionaryIndex(IEqualityComparer<TKey> EqualityComparer)
        {
            Index = new Dictionary<TKey, ISet<TValue>>(EqualityComparer);
        }

        #endregion

        #endregion


        #region IIndex Members

        #region Keys

        /// <summary>
        /// Return all index keys.
        /// </summary>
        public IEnumerable<TKey> Keys
        {
            get
            {
                return Index.Keys;
            }
        }

        #endregion

        #region Sets

        /// <summary>
        /// Return all index sets.
        /// </summary>
        public IEnumerable<ISet<TValue>> Sets
        {
            get
            {
                return Index.Values;
            }
        }

        #endregion

        #region Values

        /// <summary>
        /// Return all index values.
        /// </summary>
        public IEnumerable<TValue> Values
        {
            get
            {
                foreach (var _Set in Index.Values)
                    foreach (var _Value in _Set)
                        yield return _Value;
            }
        }

        #endregion


        #region Set(Key, Value)

        /// <summary>
        /// Adds an element with the provided key and value.
        /// If a value already exists for the given key, then nothing will be changed.
        /// </summary>
        /// <param name="Key">A key.</param>
        /// <param name="Value">A value.</param>
        public IIndex<TKey, TValue> Set(TKey Key, TValue Value)
        {
            
            ISet<TValue> _HashSet = null;

            if (Index.TryGetValue(Key, out _HashSet))
                _HashSet.Add(Value);
            else
                Index.Add(Key, new HashSet<TValue>() { Value });

            return this;

        }

        #endregion


        #region ContainsKey(Key)

        /// <summary>
        /// Determines if the specified key exists.
        /// </summary>
        /// <param name="Key">A key.</param>
        public Boolean ContainsKey(TKey Key)
        {
            return Index.ContainsKey(Key);
        }

        #endregion

        #region ContainsValue(Value)

        /// <summary>
        /// Determines if the specified value exists.
        /// </summary>
        /// <param name="Value">A value.</param>
        public Boolean ContainsValue(TValue Value)
        {
            return (from _Value in Values where Value.Equals(_Value) select true).FirstOrDefault();
        }

        #endregion

        #region Contains(Key, Value)

        /// <summary>
        /// Determines if an KeyValuePair with the specified key and value exists.
        /// </summary>
        /// <param name="Key">A property key.</param>
        /// <param name="Value">A property value.</param>
        public Boolean Contains(TKey Key, TValue Value)
        {

            ISet<TValue> _HashSet = null;

            if (Index.TryGetValue(Key, out _HashSet))
                return (from _Value in _HashSet where _Value.Equals(Value) select true).FirstOrDefault();

            return false;

        }

        #endregion


        #region this[Key]

        /// <summary>
        /// Return the value set associated with the given key.
        /// </summary>
        /// <param name="Key">A key.</param>
        public ISet<TValue> this[TKey Key]
        {
            get
            {
                return Index[Key];
            }
        }

        #endregion

        #region Get(Key, out Values)

        /// <summary>
        /// Return the value set associated with the given key.
        /// </summary>
        /// <param name="Key">A key.</param>
        /// <param name="Values">The associated value set.</param>
        /// <returns>True if the returned value set is valid.</returns>
        public Boolean Get(TKey Key, out ISet<TValue> Values)
        {
            return Index.TryGetValue(Key, out Values);
        }

        #endregion

        #region Get(KeyValueFilter)

        /// <summary>
        /// Return a filtered enumeration of all KeyValuePairs.
        /// </summary>
        /// <param name="KeyValueFilter">A delegate to filter properties based on their keys and values.</param>
        /// <returns>A enumeration of all key/value pairs matching the given KeyValueFilter.</returns>
        public IEnumerable<KeyValuePair<TKey, TValue>> Get(KeyValueFilter<TKey, TValue> KeyValueFilter)
        {
            foreach (var _KeySetPair in this)
                foreach (var _Value in _KeySetPair.Value)
                    if (KeyValueFilter(_KeySetPair.Key, _Value))
                        yield return new KeyValuePair<TKey, TValue>(_KeySetPair.Key, _Value);
        }

        #endregion

        #region Evaluate(IndexEvaluator)

        /// <summary>
        /// Get all elements matching the given index evaluator.
        /// </summary>
        /// <param name="IndexEvaluator">A delegate for selecting indexed elements.</param>
        public IEnumerable<TValue> Evaluate(IndexEvaluator<TKey, TValue> IndexEvaluator)
        {
            foreach (var _KeyValuePair in Index)
            {
                if (IndexEvaluator(_KeyValuePair.Key, _KeyValuePair.Value))
                    foreach (var _Value in _KeyValuePair.Value)
                        yield return _Value;
            }
        }

        #endregion


        #region SmallerThan(IdxKey)

        /// <summary>
        /// Get all elements smaller than the given index key.
        /// </summary>
        /// <param name="IdxKey">An index key.</param>
        public virtual IEnumerable<TValue> SmallerThan(TKey IdxKey)
        {
            return Evaluate((k, v) => k.CompareTo(IdxKey) < 0);
        }

        #endregion

        #region SmallerThanOrEquals(IdxKey)

        /// <summary>
        /// Get all elements smaller or equals the given index key.
        /// </summary>
        /// <param name="IdxKey">An index key.</param>
        public virtual IEnumerable<TValue> SmallerThanOrEquals(TKey IdxKey)
        {
            return Evaluate((k, v) => k.CompareTo(IdxKey) <= 0);
        }

        #endregion

        #region Equals(IdxKey)

        /// <summary>
        /// Get all elements exactly matching the given index key.
        /// </summary>
        /// <param name="IdxKey">An index key.</param>
        public virtual IEnumerable<TValue> Equals(TKey IdxKey)
        {

            ISet<TValue> _Set = null;

            if (Index.TryGetValue(IdxKey, out _Set))
                return _Set;

            return new List<TValue>();

        }

        #endregion

        #region NotEquals(IdxKey)

        /// <summary>
        /// Get all elements not matching the given index key exactly.
        /// </summary>
        /// <param name="IdxKey">An index key.</param>
        public virtual IEnumerable<TValue> NotEquals(TKey IdxKey)
        {
            return Evaluate((k, v) => !k.Equals(IdxKey));
        }

        #endregion

        #region LargerThan(IdxKey)

        /// <summary>
        /// Get all elements larger the given index key.
        /// </summary>
        /// <param name="IdxKey">An index key.</param>
        public virtual IEnumerable<TValue> LargerThan(TKey IdxKey)
        {
            return Evaluate((k, v) => k.CompareTo(IdxKey) > 0);
        }

        #endregion

        #region LargerThanOrEquals(IdxKey)

        /// <summary>
        /// Get all elements larger or equals the given index key.
        /// </summary>
        /// <param name="IdxKey">An index key.</param>
        public virtual IEnumerable<TValue> LargerThanOrEquals(TKey IdxKey)
        {
            return Evaluate((k, v) => k.CompareTo(IdxKey) >= 0);
        }

        #endregion


        #region Remove(Key)

        /// <summary>
        /// Remove all KeyValuePairs associated with the given key.
        /// </summary>
        /// <param name="Key">A key.</param>
        /// <returns>The value set associated with that key prior to the removal.</returns>
        public ISet<TValue> Remove(TKey Key)
        {

            ISet<TValue> _Set = null;

            if (Index.TryGetValue(Key, out _Set))
            {
                if (Index.Remove(Key) == true)
                    return _Set;
            }

            return new HashSet<TValue>();
            
        }

        #endregion

        #region Remove(Key, Value)

        /// <summary>
        /// Remove the given KeyValuePair.
        /// </summary>
        /// <param name="Key">A key.</param>
        /// <param name="Value">A value.</param>
        public Boolean Remove(TKey Key, TValue Value)
        {

            ISet<TValue> _Set = null;

            if (Index.TryGetValue(Key, out _Set))
            {

                // Remove the value from the ISet<TValue>
                if (_Set.Contains(Value))
                    if (_Set.Remove(Value) == false)
                        return false;

                // If the set is now empty remove it
                if (_Set.Count == 0)
                    if (Index.Remove(Key) == false)
                        return false;

            }

            return true;

        }

        #endregion

        #region Remove(KeyValueFilter = null)

        /// <summary>
        /// Remove all KeyValuePairs specified by the given KeyValueFilter.
        /// </summary>
        /// <param name="KeyValueFilter">A delegate to remove properties based on their keys and values.</param>
        /// <returns>A enumeration of all key/value pairs removed by the given KeyValueFilter before their removal.</returns>
        public IEnumerable<KeyValuePair<TKey, TValue>> Remove(KeyValueFilter<TKey, TValue> KeyValueFilter = null)
        {
            throw new NotImplementedException();
        }

        #endregion

        #endregion

        #region IEnumerable Members

        /// <summary>
        /// Returns an enumerator that iterates through the index.
        /// </summary>
        public IEnumerator<KeyValuePair<TKey, ISet<TValue>>> GetEnumerator()
        {
            return Index.GetEnumerator();
        }

        /// <summary>
        /// Returns an enumerator that iterates through the index.
        /// </summary>
        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return Index.GetEnumerator();
        }

        #endregion

    }

}
