/*
 * Copyright (c) 2010-2011, Achim 'ahzf' Friedland <code@ahzf.de>
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

#endregion

namespace de.ahzf.Blueprints.PropertyGraph.InMemory
{

    /// <summary>
    /// A property element index is a data structure that supports the indexing of properties in property graphs.
    /// An index is typically some sort of tree structure that allows for the fast lookup of elements by key/value pairs.
    /// </summary>
    /// <typeparam name="TKey">The type of the elements to be indexed.</typeparam>
    /// <typeparam name="TValue">The type of the index keys.</typeparam>
    public class DictionaryIndex<TKey, TValue> : ILookup<TKey, TValue>
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




        

        public Boolean Add(TKey key, TValue value)
        {
            
            ISet<TValue> _HashSet = null;

            if (Index.TryGetValue(key, out _HashSet))
                _HashSet.Add(value);
            else
                Index.Add(key, new HashSet<TValue>() { value });

            return true;

        }

        public Boolean ContainsKey(TKey key)
        {
            return Index.ContainsKey(key);
        }

        public Boolean ContainsValue(TValue value)
        {
            return (from Value in Values where Value.Equals(value) select true).FirstOrDefault();
        }

        public Boolean Contains(TKey key, TValue value)
        {

            ISet<TValue> _HashSet = null;

            if (Index.TryGetValue(key, out _HashSet))
                return (from Value in _HashSet where Value.Equals(value) select true).FirstOrDefault();

            return false;

        }

        public Boolean Get(TKey key, out ISet<TValue> value)
        {
            return Index.TryGetValue(key, out value);
        }

        public Boolean Remove(TKey key)
        {
            throw new NotImplementedException();
        }

        public Boolean Remove(TKey key, TValue value)
        {
            throw new NotImplementedException();
        }



        public IEnumerator<KeyValuePair<TKey, ISet<TValue>>> GetEnumerator()
        {
            return Index.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return Index.GetEnumerator();
        }



        
    }

}
