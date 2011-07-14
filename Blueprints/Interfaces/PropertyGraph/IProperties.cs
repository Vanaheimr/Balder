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
using System.ComponentModel;
using System.Linq.Expressions;
using System.Collections.Generic;
using System.Collections.Specialized;

#endregion

namespace de.ahzf.Blueprints.PropertyGraph
{

    #region IProperties<TKey, TValue>

    /// <summary>
    /// A generic interface maintaining a collection of key/value properties
    /// within the given datastructure.
    /// </summary>
    /// <typeparam name="TKey">The type of the keys.</typeparam>
    /// <typeparam name="TValue">The type of the values.</typeparam>
    public interface IProperties<TKey, TValue>
                        : IEnumerable<KeyValuePair<TKey, TValue>>,
                          IPropertyNotifications<TKey, TValue>

        where TKey : IEquatable<TKey>, IComparable<TKey>, IComparable

    {

        #region Properties

        #region IdKey

        /// <summary>
        /// The property key of the identification.
        /// </summary>
        TKey IdKey         { get; }

        #endregion

        #region RevisionIdKey

        /// <summary>
        /// The property key of the revision identification.
        /// </summary>
        TKey RevIdKey { get; }

        #endregion

        #endregion

        #region Keys/Values

        /// <summary>
        /// An enumeration of all property keys.
        /// </summary>
        IEnumerable<TKey> Keys { get; }

        /// <summary>
        /// An enumeration of all property values.
        /// </summary>
        IEnumerable<TValue> Values { get; }

        #endregion

        #region SetProperty(Key, Value)

        /// <summary>
        /// Add a KeyValuePair to the graph element.
        /// If a value already exists for the given key, then the previous value is overwritten.
        /// </summary>
        /// <param name="Key">A key.</param>
        /// <param name="Value">A value.</param>
        IProperties<TKey, TValue> SetProperty(TKey Key, TValue Value);

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
        /// <param name="Key">A key.</param>
        /// <param name="Value">A value.</param>
        Boolean Contains(TKey Key, TValue Value);

        #endregion

        #region Get...

        /// <summary>
        /// Return the value associated with the given key.
        /// </summary>
        /// <param name="Key">A key.</param>
        TValue this[TKey Key] { get; }

        /// <summary>
        /// Return the value associated with the given key.
        /// </summary>
        /// <param name="Key">A key.</param>
        /// <param name="Value">The associated value.</param>
        /// <returns>True if the returned value is valid. False otherwise.</returns>
        Boolean GetProperty(TKey Key, out TValue Value);

        /// <summary>
        /// Return a filtered enumeration of all KeyValuePairs.
        /// </summary>
        /// <param name="KeyValueFilter">A delegate to filter properties based on their keys and values.</param>
        /// <returns>A enumeration of all key/value pairs matching the given KeyValueFilter.</returns>
        IEnumerable<KeyValuePair<TKey, TValue>> GetProperties(KeyValueFilter<TKey, TValue> KeyValueFilter = null);

        #endregion

        #region Remove...

        /// <summary>
        /// Removes all KeyValuePairs associated with the given key.
        /// </summary>
        /// <param name="Key">A key.</param>
        /// <returns>The value associated with that key prior to the removal.</returns>
        TValue Remove(TKey Key);

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

    #region IProperties<TKey, TValue, TDatastructure>

    /// <summary>
    /// This generic interface maintaining a collection of key/value properties
    /// within the given datastructure.
    /// </summary>
    /// <typeparam name="TKey">The type of the property keys.</typeparam>
    /// <typeparam name="TValue">The type of the property values.</typeparam>
    /// <typeparam name="TDatastructure">The type of the datastructure to maintain the key/value pairs.</typeparam>
    public interface IProperties<TKey, TValue, TDatastructure>
                        : IProperties<TKey, TValue>

        where TKey           : IEquatable<TKey>, IComparable<TKey>, IComparable
        where TDatastructure : IDictionary<TKey, TValue>

    { }

    #endregion

}
