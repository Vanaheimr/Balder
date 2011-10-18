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

    // http://msdn.microsoft.com/en-us/library/hh136548(v=vs.110).aspx

    /// <summary>
    /// Provides a generic immutable map/dictionary.
    /// </summary>
    /// <typeparam name="TKey">The type of the keys.</typeparam>
    /// <typeparam name="TValue">The type of the values.</typeparam>
    public interface IImmutableMap<TKey, TValue> : IEnumerable<KeyValuePair<TKey, TValue>>
        where TKey : IEquatable<TKey>, IComparable<TKey>, IComparable
    {

        IEnumerable<TKey>    Keys          (ItemFilter<TKey>   KeyFilter   = null);
        UInt64               KeyCount      (ItemFilter<TKey>   KeyFilter   = null);

        IEnumerable<TValue>  Values        (ItemFilter<TValue> ValueFilter = null);
        UInt64               ValueCount    (ItemFilter<TValue> ValueFilter = null);

        Boolean              ContainsKey   (TKey   Key);
        Boolean              ContainsValue (TValue Value);
        Boolean              Contains      (KeyValuePair<TKey, TValue> KeyValuePair);

        Boolean              TryGetValue   (TKey Key, out TValue Value);

        //TValue this[TKey key] { get; }

    }

}
