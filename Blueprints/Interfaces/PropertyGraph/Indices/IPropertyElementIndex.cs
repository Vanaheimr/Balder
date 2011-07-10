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

#endregion

namespace de.ahzf.Blueprints.PropertyGraph.Indices
{

    #region IPropertyElementIndex

    /// <summary>
    /// A property element index is a data structure that supports the indexing of properties in property graphs.
    /// An index is typically some sort of tree structure that allows for the fast lookup of elements by key/value pairs.
    /// </summary>
    public interface IPropertyElementIndex
    {

        /// <summary>
        /// A human-friendly name of this index.
        /// </summary>
        String Name { get; }


        /// <summary>
        /// The type of the index keys.
        /// </summary>
        Type KeyType { get; }


        /// <summary>
        /// Is this index maintained by the database or by the user?
        /// </summary>
        Boolean IsAutomaticIndex { get; }


        /// <summary>
        /// True if the index supports efficient range queries;
        /// False otherwise.
        /// </summary>
        Boolean SupportsEfficentExactMatchQueries { get; }

        /// <summary>
        /// True if the index supports efficient range queries;
        /// False otherwise.
        /// </summary>
        Boolean SupportsEfficentRangeQueries { get; }

    }

    #endregion

    #region IPropertyElementIndex<T>

    /// <summary>
    /// A property element index is a data structure that supports the indexing of properties in property graphs.
    /// An index is typically some sort of tree structure that allows for the fast lookup of elements by key/value pairs.
    /// </summary>
    /// <typeparam name="TValue">The type of the elements to be indexed.</typeparam>
    public interface IPropertyElementIndex<TValue> : IPropertyElementIndex
        where TValue : IEquatable<TValue>, IComparable<TValue>, IComparable
    {

        #region Insert, Remove

        /// <summary>
        /// Inserts an element into the index.
        /// </summary>
        /// <param name="Element">An element for indexing.</param>
        void Insert(TValue Element);

        /// <summary>
        /// Remove an element from the index.
        /// </summary>
        /// <param name="Element">An element for indexing.</param>
        void Remove(TValue Element);

        #endregion


        #region <, <=

        /// <summary>
        /// Get all elements smaller than the given index key.
        /// </summary>
        /// <typeparam name="TIdxKey">The type of the index key which must be at least ICompare/IEquatable.</typeparam>
        /// <param name="IdxKey">An index key.</param>
        IEnumerable<TValue> SmallerThan<TIdxKey>(TIdxKey IdxKey)
            where TIdxKey : IEquatable<TIdxKey>, IComparable<TIdxKey>, IComparable;


        /// <summary>
        /// Get all elements smaller or equals the given index key.
        /// </summary>
        /// <typeparam name="TIdxKey">The type of the index key which must be at least ICompare/IEquatable.</typeparam>
        /// <param name="IdxKey">An index key.</param>
        IEnumerable<TValue> SmallerThanOrEquals<TIdxKey>(TIdxKey IdxKey)
            where TIdxKey : IEquatable<TIdxKey>, IComparable<TIdxKey>, IComparable;

        #endregion

        #region ==, !=

        /// <summary>
        /// Get all elements exactly matching the given index key.
        /// </summary>
        /// <typeparam name="TIdxKey">The type of the index key which must be at least ICompare/IEquatable.</typeparam>
        /// <param name="IdxKey">An index key.</param>
        IEnumerable<TValue> Equals<TIdxKey>(TIdxKey IdxKey)
            where TIdxKey : IEquatable<TIdxKey>, IComparable<TIdxKey>, IComparable;


        /// <summary>
        /// Get the indexed elements for the given typed index key
        /// exactly matching the given index key.
        /// </summary>
        /// <typeparam name="TIdxKey">The type of the index key which must be at least ICompare/IEquatable.</typeparam>
        /// <param name="IdxKey">An index key.</param>
        IEnumerable<TValue> NotEquals<TIdxKey>(TIdxKey IdxKey)
            where TIdxKey : IEquatable<TIdxKey>, IComparable<TIdxKey>, IComparable;

        #endregion

        #region >, >=

        /// <summary>
        /// Get the indexed elements for the given typed index key
        /// exactly matching the given index key.
        /// </summary>
        /// <typeparam name="TIdxKey">The type of the index key which must be at least ICompare/IEquatable.</typeparam>
        /// <param name="IdxKey">An index key.</param>
        IEnumerable<TValue> LargerThan<TIdxKey>(TIdxKey IdxKey)
            where TIdxKey : IEquatable<TIdxKey>, IComparable<TIdxKey>, IComparable;


        /// <summary>
        /// Get the indexed elements for the given typed index key
        /// exactly matching the given index key.
        /// </summary>
        /// <typeparam name="TIdxKey">The type of the index key which must be at least ICompare/IEquatable.</typeparam>
        /// <param name="IdxKey">An index key.</param>
        IEnumerable<TValue> LargerThanOrEquals<TIdxKey>(TIdxKey IdxKey)
            where TIdxKey : IEquatable<TIdxKey>, IComparable<TIdxKey>, IComparable;

        #endregion


        /// <summary>
        /// Downcast this index to an IPropertyElementIndex&lt;TIdxKey, TValue&gt; index.
        /// </summary>
        /// <typeparam name="TKey">The type of the index keys.</typeparam>
        IPropertyElementIndex<TKey, TValue> CastTo<TKey>()
            where TKey : IEquatable<TKey>, IComparable<TKey>, IComparable;


    }

    #endregion

    #region IPropertyElementIndex<TKey, TValue>

    /// <summary>
    /// A property element index is a data structure that supports the indexing of properties in property graphs.
    /// An index is typically some sort of tree structure that allows for the fast lookup of elements by key/value pairs.
    /// </summary>
    /// <typeparam name="TKey">The type of the index keys.</typeparam>
    /// <typeparam name="TValue">The type of the elements to be indexed.</typeparam>
    public interface IPropertyElementIndex<TKey, TValue> : IPropertyElementIndex<TValue>
        where TKey   : IEquatable<TKey>,   IComparable<TKey>,   IComparable
        where TValue : IEquatable<TValue>, IComparable<TValue>, IComparable
    {

        /// <summary>
        /// Get all elements matching the given index evaluator.
        /// </summary>
        /// <param name="IndexEvaluator">A delegate for selecting indexed elements.</param>
        IEnumerable<TValue> Evaluate(IndexEvaluator<TKey, TValue> IndexEvaluator);

        #region <, <=

        /// <summary>
        /// Get all elements smaller than the given index key.
        /// </summary>
        /// <param name="IdxKey">An index key.</param>
        IEnumerable<TValue> SmallerThan(TKey IdxKey);


        /// <summary>
        /// Get all elements smaller or equals the given index key.
        /// </summary>
        /// <param name="IdxKey">An index key.</param>
        IEnumerable<TValue> SmallerThanOrEquals(TKey IdxKey);

        #endregion

        #region ==, !=

        /// <summary>
        /// Get all elements exactly matching the given index key.
        /// </summary>
        /// <param name="IdxKey">An index key.</param>
        IEnumerable<TValue> Equals(TKey IdxKey);


        /// <summary>
        /// Get all elements not matching the given index key exactly.
        /// </summary>
        /// <param name="IdxKey">An index key.</param>
        IEnumerable<TValue> NotEquals(TKey IdxKey);

        #endregion

        #region >, >=

        /// <summary>
        /// Get the indexed elements for the given typed index key
        /// exactly matching the given index key.
        /// </summary>
        /// <param name="IdxKey">An index key.</param>
        IEnumerable<TValue> LargerThan(TKey IdxKey);


        /// <summary>
        /// Get the indexed elements for the given typed index key
        /// exactly matching the given index key.
        /// </summary>
        /// <param name="IdxKey">An index key.</param>
        IEnumerable<TValue> LargerThanOrEquals(TKey IdxKey);

        #endregion

    }

    #endregion

}
