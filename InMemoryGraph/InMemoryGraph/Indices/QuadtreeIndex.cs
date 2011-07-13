//*
// * Copyright (c) 2010-2011, Achim 'ahzf' Friedland <code@ahzf.de>
// * This file is part of Blueprints.NET <http://www.github.com/ahzf/Blueprints.NET>
// *
// * Licensed under the Apache License, Version 2.0 (the "License");
// * you may not use this file except in compliance with the License.
// * You may obtain a copy of the License at
// *
// *     http://www.apache.org/licenses/LICENSE-2.0
// *
// * Unless required by applicable law or agreed to in writing, software
// * distributed under the License is distributed on an "AS IS" BASIS,
// * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// * See the License for the specific language governing permissions and
// * limitations under the License.
// */

#region Usings

using System;
using System.Linq;
using System.Collections.Generic;

using de.ahzf.Blueprints.Indices;
using de.ahzf.Blueprints.PropertyGraph.Indices;

#endregion

namespace de.ahzf.Blueprints.PropertyGraph.InMemory
{

    //public class IndexPixel<TKey, TValue> : Pixel<TKey>,
    //                                        IEquatable<IndexPixel<TKey, TValue>>, IComparable<IndexPixel<TKey, TValue>>, IComparable
    //    where TKey : IEquatable<TKey>, IComparable<TKey>, IComparable
    //{

    //    public ISet<TValue> Set
    //    {
    //        get;
    //        set;
    //    }

    //    public IndexPixel(TKey x, TKey y)
    //        : base(x, y)
    //    {
    //        this.Set = new HashSet<TValue>();
    //    }


    //    public bool Equals(IndexPixel<TKey, TValue> other)
    //    {
    //        return base.Equals(other);
    //    }

    //    public int CompareTo(IndexPixel<TKey, TValue> other)
    //    {
    //        if (base.Equals(other))
    //            return 0;
    //        return 1;
    //    }

    //    public int CompareTo(object obj)
    //    {
    //        if (base.Equals(obj))
    //            return 0;
    //        return 1;
    //    }

    //}


    /// <summary>
    /// A property element index is a data structure that supports the indexing of properties in property graphs.
    /// An index is typically some sort of tree structure that allows for the fast lookup of elements by key/value pairs.
    /// </summary>
    /// <typeparam name="TKey">The type of the index keys.</typeparam>
    /// <typeparam name="TValue">The type of the elements to be indexed.</typeparam>
    public class QuadtreeIndex<TKey, T, TValue> : IIndex2D<TKey, T, TValue>
        where TKey : IPixel<T>, IEquatable<TKey>, IComparable<TKey>, IComparable
        where T : IEquatable<T>, IComparable<T>, IComparable
        where TValue : IEquatable<TValue>, IComparable<TValue>, IComparable
    {

        #region Data

        /// <summary>
        /// The internal index datastructure.
        /// </summary>
        private readonly Quadtree<T> Quadtree;

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

        #region QuadtreeIndex()

        /// <summary>
        /// Creates a new DictionaryIndex.
        /// (Needed for activating this index datastructure.)
        /// </summary>
        public QuadtreeIndex()
        {
            Quadtree = new Quadtree<T>(default(T), default(T), default(T), default(T));
        }

        #endregion

        //#region DictionaryIndex(EqualityComparer)

        ///// <summary>
        ///// Creates a new DictionaryIndex.
        ///// </summary>
        ///// <param name="EqualityComparer">An optional equality comparer for the index key.</param>
        //public DictionaryIndex(IEqualityComparer<TKey> EqualityComparer)
        //{
        //    Index = new Dictionary<TKey, ISet<TValue>>(EqualityComparer);
        //}

        //#endregion

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
                foreach (var x in Quadtree)
                    yield return (TKey) x;
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
                throw new NotImplementedException();
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
                throw new NotImplementedException();
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
        public IIndex2D<TKey, T, TValue> Set(TKey Key, TValue Value)
        {
            Quadtree.Add(new PixelValuePair<T, TValue>(Key.X, Key.Y, Value));
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
            throw new NotImplementedException();
        }

        #endregion

        #region ContainsValue(Value)

        /// <summary>
        /// Determines if the specified value exists.
        /// </summary>
        /// <param name="Value">A value.</param>
        public Boolean ContainsValue(TValue Value)
        {
            //return (from _Value in Values where Value.Equals(_Value) select true).FirstOrDefault();
            throw new NotImplementedException();
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
            throw new NotImplementedException();
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
                throw new NotImplementedException();
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
            throw new NotImplementedException();
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
            throw new NotImplementedException();
        }

        #endregion


        #region SmallerThan(IdxKey)

        /// <summary>
        /// Get all elements smaller than the given index key.
        /// </summary>
        /// <param name="IdxKey">An index key.</param>
        public virtual IEnumerable<TValue> SmallerThan(TKey IdxKey)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region SmallerThanOrEquals(IdxKey)

        /// <summary>
        /// Get all elements smaller or equals the given index key.
        /// </summary>
        /// <param name="IdxKey">An index key.</param>
        public virtual IEnumerable<TValue> SmallerThanOrEquals(TKey IdxKey)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region Equals(IdxKey)

        /// <summary>
        /// Get all elements exactly matching the given index key.
        /// </summary>
        /// <param name="IdxKey">An index key.</param>
        public virtual IEnumerable<TValue> Equals(TKey IdxKey)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region NotEquals(IdxKey)

        /// <summary>
        /// Get all elements not matching the given index key exactly.
        /// </summary>
        /// <param name="IdxKey">An index key.</param>
        public virtual IEnumerable<TValue> NotEquals(TKey IdxKey)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region LargerThan(IdxKey)

        /// <summary>
        /// Get all elements larger the given index key.
        /// </summary>
        /// <param name="IdxKey">An index key.</param>
        public virtual IEnumerable<TValue> LargerThan(TKey IdxKey)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region LargerThanOrEquals(IdxKey)

        /// <summary>
        /// Get all elements larger or equals the given index key.
        /// </summary>
        /// <param name="IdxKey">An index key.</param>
        public virtual IEnumerable<TValue> LargerThanOrEquals(TKey IdxKey)
        {
            throw new NotImplementedException();
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
            throw new NotImplementedException();
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
            throw new NotImplementedException();
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
            throw new NotImplementedException();
        }

        /// <summary>
        /// Returns an enumerator that iterates through the index.
        /// </summary>
        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }

        #endregion



    }

}
