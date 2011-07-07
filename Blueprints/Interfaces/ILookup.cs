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

    /// <summary>
    /// A generic interface for all lookup datastructures.
    /// </summary>
    /// <typeparam name="TKey">The lookup key.</typeparam>
    /// <typeparam name="TValue">The lookup values.</typeparam>
    public interface ILookup<TKey, TValue> : IEnumerable<KeyValuePair<TKey, ISet<TValue>>>
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


        Boolean TryAddValue(TKey key, TValue value);

        Boolean ContainsKey(TKey key);
        Boolean ContainsValue(TValue value);
        Boolean Contains(TKey key, TValue value);

        Boolean TryGetValue(TKey key, out ISet<TValue> value);

        Boolean TryRemove(TKey key);
        Boolean TryRemove(TKey key, TValue value);

    }

}
