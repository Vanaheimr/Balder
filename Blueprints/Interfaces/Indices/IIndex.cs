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
using System.Collections.Generic;

#endregion

namespace de.ahzf.Blueprints.Indices
{

    #region IIndex

    /// <summary>
    /// An interface for all index datastructures.
    /// </summary>
    public interface IIndex
    {

        /// <summary>
        /// The name of this index datastructure.
        /// </summary>
        String Name { get; }

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

    #region IIndex<TKey, TValue>

    /// <summary>
    /// A generic interface for all index datastructures.
    /// </summary>
    /// <typeparam name="TKey">The type of the lookup keys.</typeparam>
    /// <typeparam name="TValue">The type of the elements to be looked up.</typeparam>
    public interface IIndex<TKey, TValue> : IIndex, IEnumerable<KeyValuePair<TKey, ISet<TValue>>>
        where TKey   : IEquatable<TKey>,   IComparable<TKey>,   IComparable
        where TValue : IEquatable<TValue>, IComparable<TValue>, IComparable
    {

        #region Keys/Sets/Values

        /// <summary>
        /// An enumeration of all index keys.
        /// </summary>
        IEnumerable<TKey>         Keys   { get; }

        /// <summary>
        /// An enumeration of all index sets.
        /// </summary>
        IEnumerable<ISet<TValue>> Sets   { get; }

        /// <summary>
        /// An enumeration of all indexed values.
        /// </summary>
        IEnumerable<TValue>       Values { get; }

        #endregion

        #region Set(Key, Value)

        /// <summary>
        /// Adds an element with the provided key and value.
        /// If a value already exists for the given key, then nothing will be changed.
        /// </summary>
        /// <param name="Key">A key.</param>
        /// <param name="Value">A value.</param>
        IIndex<TKey, TValue> Set(TKey Key, TValue Value);

        #endregion

        #region Contains...

        /// <summary>
        /// Determines if the specified key exists.
        /// </summary>
        /// <param name="Key">A key.</param>
        Boolean ContainsKey(TKey Key);

        /// <summary>
        /// Determines if the specified value exists.
        /// </summary>
        /// <param name="Value">A value.</param>
        Boolean ContainsValue(TValue Value);

        /// <summary>
        /// Determines if an KeyValuePair with the specified key and value exists.
        /// </summary>
        /// <param name="Key">A property key.</param>
        /// <param name="Value">A property value.</param>
        Boolean Contains(TKey Key, TValue Value);

        #endregion

        #region Get...

        /// <summary>
        /// Return the value set associated with the given key.
        /// </summary>
        /// <param name="Key">A key.</param>
        ISet<TValue> this[TKey Key] { get; }

        /// <summary>
        /// Return the value set associated with the given key.
        /// </summary>
        /// <param name="Key">A key.</param>
        /// <param name="Values">The associated value set.</param>
        /// <returns>True if the returned value set is valid.</returns>
        Boolean Get(TKey Key, out ISet<TValue> Values);

        /// <summary>
        /// Return a filtered enumeration of all KeyValuePairs.
        /// </summary>
        /// <param name="KeyValueFilter">A delegate to filter properties based on their keys and values.</param>
        /// <returns>A enumeration of all key/value pairs matching the given KeyValueFilter.</returns>
        IEnumerable<KeyValuePair<TKey, TValue>> Get(KeyValueFilter<TKey, TValue> KeyValueFilter);

        /// <summary>
        /// Get all elements matching the given index evaluator.
        /// </summary>
        /// <param name="IndexEvaluator">A delegate for selecting indexed elements.</param>
        IEnumerable<TValue> Evaluate(IndexEvaluator<TKey, TValue> IndexEvaluator);

        #endregion

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

        #region Remove...

        /// <summary>
        /// Remove all KeyValuePairs associated with the given key.
        /// </summary>
        /// <param name="Key">A key.</param>
        /// <returns>The value set associated with that key prior to the removal.</returns>
        ISet<TValue> Remove(TKey Key);

        /// <summary>
        /// Remove the given KeyValuePair.
        /// </summary>
        /// <param name="Key">A key.</param>
        /// <param name="Value">A value.</param>
        Boolean Remove(TKey Key, TValue Value);

        /// <summary>
        /// Remove all KeyValuePairs specified by the given KeyValueFilter.
        /// </summary>
        /// <param name="KeyValueFilter">A delegate to remove properties based on their keys and values.</param>
        /// <returns>A enumeration of all key/value pairs removed by the given KeyValueFilter before their removal.</returns>
        IEnumerable<KeyValuePair<TKey, TValue>> Remove(KeyValueFilter<TKey, TValue> KeyValueFilter = null);

        #endregion

    }

    #endregion

}
