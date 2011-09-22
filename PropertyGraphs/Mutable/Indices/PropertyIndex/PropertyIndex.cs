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
using System.Collections.Generic;

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
    public class PropertyIndex<TKey, TValue> : IPropertyElementIndex<TKey, TValue>
        where TKey   : IEquatable<TKey>,   IComparable<TKey>,   IComparable
        where TValue : IEquatable<TValue>, IComparable<TValue>, IComparable

    {

        #region Data

        /// <summary>
        /// The internal index datastructure.
        /// </summary>
        protected readonly IIndex<TKey, TValue> IndexDatastructure;

        /// <summary>
        /// A delegate for deciding if an element should be indexed or not.
        /// </summary>
        protected readonly IndexSelector<TKey, TValue> Selector;

        /// <summary>
        /// A transformation from an incoming element to an index key.
        /// </summary>
        protected readonly IndexTransformation<TKey, TValue> Transformation;

        #endregion

        #region Properties

        #region Name

        /// <summary>
        /// A human-friendly name of this index.
        /// </summary>
        public String Name { get; protected set; }

        #endregion

        #region KeyType

        /// <summary>
        /// The type of the index keys.
        /// </summary>
        public Type KeyType { get; private set; }

        #endregion

        #region IsAutomaticIndex

        /// <summary>
        /// Is this index maintained by the database or by the user?
        /// </summary>
        public Boolean IsAutomaticIndex { get; private set; }

        #endregion

        #region SupportsEfficentExactMatchQueries

        /// <summary>
        /// True if the index supports efficient range queries;
        /// False otherwise.
        /// </summary>
        public Boolean SupportsEfficentExactMatchQueries
        {
            get
            {
                return IndexDatastructure.SupportsEfficentExactMatchQueries;
            }
        }

        #endregion

        #region SupportsEfficentRangeQueries

        /// <summary>
        /// True if the index supports efficient range queries;
        /// False otherwise.
        /// </summary>
        public Boolean SupportsEfficentRangeQueries
        {
            get
            {
                return IndexDatastructure.SupportsEfficentRangeQueries;
            }
        }

        #endregion

        #endregion

        #region Constructor(s)

        #region PropertyIndex(Name, Selector, Transformation, Datastructure = null, IsAutomaticIndex = false, SupportsEfficentExactMatchQueries = false, SupportsEfficentRangeQueries = false)

        /// <summary>
        /// Creates a new index for indexing PropertyElements like vertices, edges or hyperedges.
        /// </summary>
        /// <param name="Name">A human-friendly name for this index.</param>
        /// <param name="Transformation">A delegate for transforming a PropertyElement into an index key.</param>
        /// <param name="Selector">An optional delegate for deciding if a PropertyElement should be indexed or not.</param>
        /// <param name="IndexDatastructure">A datastructure for maintaining the index.</param>
        /// <param name="IsAutomaticIndex">Should this index be maintained by the database or by the user?</param>
        /// <param name="SupportsEfficentExactMatchQueries">True if the index supports efficient range queries; False otherwise.</param>
        /// <param name="SupportsEfficentRangeQueries">True if the index supports efficient range queries; False otherwise.</param>
        public PropertyIndex(String                            Name,
                             IIndex<TKey, TValue>              IndexDatastructure,
                             IndexTransformation<TKey, TValue> Transformation,
                             IndexSelector<TKey, TValue>       Selector                          = null,
                             Boolean                           IsAutomaticIndex                  = false,
                             Boolean                           SupportsEfficentExactMatchQueries = false,
                             Boolean                           SupportsEfficentRangeQueries      = false)
        {

            if (IndexDatastructure == null)
                throw new ArgumentNullException("The given Datastructure must not be null!");

            this.Name               = Name;
            this.IndexDatastructure = IndexDatastructure;
            this.Transformation     = Transformation;
            this.Selector           = Selector;
            this.IsAutomaticIndex   = IsAutomaticIndex;
            this.KeyType            = typeof(TKey);

        }

        #endregion

        #endregion


        #region Insert(Element)

        /// <summary>
        /// Inserts an element into the index.
        /// </summary>
        /// <param name="Element">An element to index.</param>
        public void Insert(TValue Element)
        {
            if (this.Selector(Element))
                IndexDatastructure.Set(Transformation(Element), Element);
        }

        #endregion


        #region Evaluate(IndexEvaluator)

        /// <summary>
        /// Get all elements matching the given index evaluator.
        /// </summary>
        /// <param name="IndexEvaluator">A delegate for selecting indexed elements.</param>
        public IEnumerable<TValue> Evaluate(IndexEvaluator<TKey, TValue> IndexEvaluator)
        {
            foreach (var _KeyValuePair in IndexDatastructure)
            {
                if (IndexEvaluator(_KeyValuePair.Key, _KeyValuePair.Value))
                    foreach (var _Value in _KeyValuePair.Value)
                        yield return _Value;
            }
        }

        #endregion


        // <, <=

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

        #region SmallerThan<TIdxKey>(IdxKey)

        /// <summary>
        /// Get all elements smaller than the given index key.
        /// </summary>
        /// <typeparam name="TIdxKey">The type of the index key which must be at least ICompare/IEquatable.</typeparam>
        /// <param name="IdxKey">An index key.</param>
        public virtual IEnumerable<TValue> SmallerThan<TIdxKey>(TIdxKey IdxKey)
            where TIdxKey : IEquatable<TIdxKey>, IComparable<TIdxKey>, IComparable
        {
            try
            {
                return SmallerThan((TKey) (Object) IdxKey);
            }
            catch (Exception)
            {
                return new List<TValue>();
            }
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

        #region SmallerThanOrEquals<TIdxKey>(IdxKey)

        /// <summary>
        /// Get all elements smaller or equals the given index key.
        /// </summary>
        /// <typeparam name="TIdxKey">The type of the index key which must be at least ICompare/IEquatable.</typeparam>
        /// <param name="IdxKey">An index key.</param>
        public virtual IEnumerable<TValue> SmallerThanOrEquals<TIdxKey>(TIdxKey IdxKey)
            where TIdxKey : IEquatable<TIdxKey>, IComparable<TIdxKey>, IComparable
        {
            try
            {
                return SmallerThanOrEquals((TKey) (Object) IdxKey);
            }
            catch (Exception)
            {
                return new List<TValue>();
            }
        }

        #endregion

        
        // ==, !=

        #region Equals(IdxKey)

        /// <summary>
        /// Get all elements exactly matching the given index key.
        /// </summary>
        /// <param name="IdxKey">An index key.</param>
        public virtual IEnumerable<TValue> Equals(TKey IdxKey)
        {
            
            ISet<TValue> _Set = null;

            if (IndexDatastructure.Get(IdxKey, out _Set))
                return _Set;

            return new List<TValue>();

        }

        #endregion

        #region Equals<TIdxKey>(IdxKey)

        /// <summary>
        /// Get all elements exactly matching the given index key.
        /// </summary>
        /// <typeparam name="TIdxKey">The type of the index key which must be at least ICompare/IEquatable.</typeparam>
        /// <param name="IdxKey">An index key.</param>
        public virtual IEnumerable<TValue> Equals<TIdxKey>(TIdxKey IdxKey)
            where TIdxKey : IEquatable<TIdxKey>, IComparable<TIdxKey>, IComparable
        {
            try
            {
                return Equals((TKey) (Object) IdxKey);
            }
            catch (Exception)
            {
                return new List<TValue>();
            }
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

        #region NotEquals<TIdxKey>(IdxKey)

        /// <summary>
        /// Get all elements not matching the given index key exactly.
        /// </summary>
        /// <typeparam name="TIdxKey">The type of the index key which must be at least ICompare/IEquatable.</typeparam>
        /// <param name="IdxKey">An index key.</param>
        public virtual IEnumerable<TValue> NotEquals<TIdxKey>(TIdxKey IdxKey)
            where TIdxKey : IEquatable<TIdxKey>, IComparable<TIdxKey>, IComparable
        {
            try
            {
                return NotEquals((TKey) (Object) IdxKey);
            }
            catch (Exception)
            {
                return new List<TValue>();
            }
        }

        #endregion
        

        // >, >=

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

        #region LargerThan<TIdxKey>(IdxKey)

        /// <summary>
        /// Get all elements larger the given index key.
        /// </summary>
        /// <typeparam name="TIdxKey">The type of the index key which must be at least ICompare/IEquatable.</typeparam>
        /// <param name="IdxKey">An index key.</param>
        public virtual IEnumerable<TValue> LargerThan<TIdxKey>(TIdxKey IdxKey)
            where TIdxKey : IEquatable<TIdxKey>, IComparable<TIdxKey>, IComparable
        {
            try
            {
                return LargerThan((TKey) (Object) IdxKey);
            }
            catch (Exception)
            {
                return new List<TValue>();
            }
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

        #region LargerThanOrEquals<TIdxKey>(IdxKey)

        /// <summary>
        /// Get all elements larger the given index key.
        /// </summary>
        /// <typeparam name="TIdxKey">The type of the index key which must be at least ICompare/IEquatable.</typeparam>
        /// <param name="IdxKey">An index key.</param>
        public virtual IEnumerable<TValue> LargerThanOrEquals<TIdxKey>(TIdxKey IdxKey)
            where TIdxKey : IEquatable<TIdxKey>, IComparable<TIdxKey>, IComparable
        {
            try
            {
                return LargerThanOrEquals((TKey) (Object) IdxKey);
            }
            catch (Exception)
            {
                return new List<TValue>();
            }
        }

        #endregion


        #region Remove(Element)

        /// <summary>
        /// Remove an element from the index.
        /// </summary>
        /// <param name="Element">An element for indexing.</param>
        public void Remove(TValue Element)
        {
            throw new NotImplementedException();
        }

        #endregion


        #region CastTo<TKey>()

        /// <summary>
        /// Downcast this index to an IPropertyElementIndex&lt;TIdxKey, TValue&gt; index.
        /// </summary>
        /// <typeparam name="TIdxKey">The type of the index keys.</typeparam>
        public IPropertyElementIndex<TIdxKey, TValue> CastTo<TIdxKey>()
            where TIdxKey : IEquatable<TIdxKey>, IComparable<TIdxKey>, IComparable
        {
            return (IPropertyElementIndex<TIdxKey, TValue>) this;
        }

        #endregion


    }

}
