/*
 * Copyright (c) 2010-2011, Achim 'ahzf' Friedland <code@ahzf.de>
 * This file is part of Blueprints.NET
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
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq.Expressions;

#endregion

namespace de.ahzf.blueprints
{

    #region IProperties<TKey, TValue>

    /// <summary>
    /// This generic interface maintains a collection of key/value properties
    /// within the given datastructure.
    /// </summary>
    /// <typeparam name="TKey">The type of the property keys.</typeparam>
    /// <typeparam name="TValue">The type of the property values.</typeparam>
    /// <typeparam name="TDatastructure">The type of the datastructure to maintain the key/value pairs.</typeparam>
    public interface IProperties<TKey, TValue>
                        : IEnumerable<KeyValuePair<TKey, TValue>>,
                          INotifyCollectionChanged,
                          INotifyPropertyChanging,
                          INotifyPropertyChanged

        where TKey : IEquatable<TKey>, IComparable<TKey>, IComparable

    {

        #region Events

        void OnCollectionChanged(NotifyCollectionChangedEventArgs myNotifyCollectionChangedEventArgs);

        void OnPropertyChanging(String myPropertyName);
        void OnPropertyChanging<TResult>(Expression<Func<TResult>> myPropertyExpression);

        void OnPropertyChanged(String myPropertyName);
        void OnPropertyChanged<TResult>(Expression<Func<TResult>> myPropertyExpression);

        #endregion


        /// <summary>
        /// Assign a key/value property to the element.
        /// If a value already exists for this key, then the previous key/value is overwritten.
        /// </summary>
        /// <param name="myKey">The property key.</param>
        /// <param name="myValue">The property value.</param>
        IProperties<TKey, TValue> SetProperty(TKey myKey, TValue myValue);


        /// <summary>
        /// Return the property value associated with the given property key.
        /// </summary>
        /// <param name="myKey">The key of the key/value property.</param>
        /// <returns>The property value related to the string key.</returns>
        TValue GetProperty(TKey myKey);


        /// <summary>
        /// Try to return the property value associated with the given property key.
        /// </summary>
        /// <param name="myKey">The key of the key/value property.</param>
        /// <param name="myValue">The value of the key/value property.</param>
        /// <returns>True if the returned value is valid.</returns>
        Boolean TryGetProperty(TKey myKey, out TValue myValue);


        /// <summary>
        /// Allows to return a filtered enumeration of all properties.
        /// </summary>
        /// <param name="myPropertyFilter">A function to filter a property based on its key and value.</param>
        /// <returns>A enumeration of all key/value pairs matching the given property filter.</returns>
        IEnumerable<KeyValuePair<TKey, TValue>> GetProperties(Func<TKey, TValue, Boolean> myPropertyFilter = null);


        /// <summary>
        /// Removes a key/value property from the element.
        /// </summary>
        /// <param name="myKey">The key of the property to remove.</param>
        /// <returns>The property value associated with that key prior to removal.</returns>
        TValue RemoveProperty(TKey myKey);


        /// <summary>
        /// Return all property keys.
        /// </summary>
        IEnumerable<TKey> PropertyKeys { get; }

    }

    #endregion

    #region IProperties<TKey, TValue, TDatastructure>

    /// <summary>
    /// This generic interface maintains a collection of key/value properties
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
