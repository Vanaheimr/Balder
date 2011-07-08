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

#endregion

namespace de.ahzf.Blueprints
{

    #region ILookup

    /// <summary>
    /// An interface for all lookup datastructures.
    /// </summary>
    public interface ILookup
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

    #region ILookup<TKey, TValue>

    /// <summary>
    /// A generic interface for all lookup datastructures.
    /// </summary>
    /// <typeparam name="TKey">The lookup key.</typeparam>
    /// <typeparam name="TValue">The lookup values.</typeparam>
    public interface ILookup<TKey, TValue> : ILookup, IEnumerable<KeyValuePair<TKey, ISet<TValue>>>
        where TKey   : IEquatable<TKey>,   IComparable<TKey>,   IComparable
        where TValue : IEquatable<TValue>, IComparable<TValue>, IComparable
    {

        #region Properties

        /// <summary>
        /// Return all index keys.
        /// </summary>
        IEnumerable<TKey>         Keys   { get; }

        /// <summary>
        /// Return all index sets.
        /// </summary>
        IEnumerable<ISet<TValue>> Sets   { get; }

        /// <summary>
        /// Return all index values.
        /// </summary>
        IEnumerable<TValue>       Values { get; }

        #endregion


        Boolean Add(TKey key, TValue value);

        Boolean ContainsKey(TKey key);
        Boolean ContainsValue(TValue value);
        Boolean Contains(TKey key, TValue value);

        Boolean Get(TKey key, out ISet<TValue> value);

        Boolean Remove(TKey key);
        Boolean Remove(TKey key, TValue value);

    }

    #endregion

}
