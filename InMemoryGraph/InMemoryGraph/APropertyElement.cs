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

namespace de.ahzf.Blueprints.PropertyGraph.InMemory
{

    /// <summary>
    /// A property element is the base class for all property graph elements
    /// (vertices, edges, hyperedges).
    /// </summary>
    /// <typeparam name="TId">The graph element identification.</typeparam>
    /// <typeparam name="TRevisionId">The graph element revision identification.</typeparam>
    /// <typeparam name="TKey">The type of the graph element property keys.</typeparam>
    /// <typeparam name="TValue">The type of the graph element property values.</typeparam>
    /// <typeparam name="TDatastructure">A datastructure for storing all properties.</typeparam>
    public abstract class APropertyElement<TId, TRevisionId, TKey, TValue, TDatastructure>
                              : IProperties<TKey, TValue>

        where TKey           : IEquatable<TKey>,        IComparable<TKey>,        IComparable
        where TId            : IEquatable<TId>,         IComparable<TId>,         IComparable, TValue
        where TRevisionId    : IEquatable<TRevisionId>, IComparable<TRevisionId>, IComparable, TValue
        where TDatastructure : IDictionary<TKey, TValue>

    {

        #region Data

        /// <summary>
        /// The property key for storing the Id.
        /// </summary>
        protected readonly TKey _IdKey;

        /// <summary>
        /// The property key for storing the RevisionId.
        /// </summary>
        protected readonly TKey _RevisonIdKey;

        #endregion


        #region Id

        /// <summary>
        /// The Identification of this property graph element.
        /// </summary>
        public TId Id
        {
            get
            {
                return (TId) _Data.GetProperty(_IdKey);
            }
        }

        #endregion

        #region RevisionId

        /// <summary>
        /// The revision identification of this property graph element.
        /// </summary>
        public TRevisionId RevisionId
        {
            get
            {
                return (TRevisionId) _Data.GetProperty(_RevisonIdKey);
            }
        }

        #endregion

        #region Data

        protected readonly IProperties<TKey, TValue> _Data;

        public IProperties<TKey, TValue> Data
        {
            get
            {
                return _Data;
            }
        }

        #endregion

        #region Properties

        public IProperties<TKey, TValue> Properties
        {
            get
            {
                return _Data;
            }
        }

        #endregion


        #region SetProperty(myKey, myValue)

        /// <summary>
        /// Set a property.
        /// </summary>
        /// <param name="myKey">The property key.</param>
        /// <param name="myValue">The property value.</param>
        public IProperties<TKey, TValue> SetProperty(TKey myKey, TValue myValue)
        {
            return _Data.SetProperty(myKey, myValue);
        }

        #endregion

        #region Contains(myKey)

        /// <summary>
        /// Checks if the given property key is assigned.
        /// </summary>
        /// <param name="myKey">A property key.</param>
        public Boolean Contains(TKey myKey)
        {
            return _Data.Contains(myKey);
        }

        #endregion

        #region Contains(myKey, myValue)

        /// <summary>
        /// Checks if the given key/value pair is assigned.
        /// </summary>
        /// <param name="myKey">A property key.</param>
        /// <param name="myValue">A property value.</param>
        public Boolean Contains(TKey myKey, TValue myValue)
        {
            return _Data.Contains(myKey, myValue);
        }

        #endregion

        #region Contains(myKeyValuePair)

        /// <summary>
        /// Checks if the given key/value pair is assigned.
        /// </summary>
        /// <param name="myKeyValuePair">A KeyValuePair.</param>
        public Boolean Contains(KeyValuePair<TKey, TValue> myKeyValuePair)
        {
            return _Data.Contains(myKeyValuePair);
        }

        #endregion

        #region GetProperty(myKey)

        /// <summary>
        /// Returns the value stored using the given property key.
        /// </summary>
        /// <param name="myKey">A property key.</param>
        public TValue GetProperty(TKey myKey)
        {
            return _Data.GetProperty(myKey);
        }

        #endregion

        #region TryGetProperty(myKey, out myValue)

        /// <summary>
        /// Tries to get the value of the given property key.
        /// </summary>
        /// <param name="myKey">The property key.</param>
        /// <param name="myValue">The property value.</param>
        /// <returns>True of success; else false.</returns>
        public Boolean TryGetProperty(TKey myKey, out TValue myValue)
        {
            return _Data.TryGetProperty(myKey, out myValue);
        }

        #endregion

        #region GetProperties(myPropertyFilter = null)

        /// <summary>
        /// Return an enumeration of all key-value pairs stored.
        /// </summary>
        /// <param name="myPropertyFilter">An optional property filter.</param>
        public IEnumerable<KeyValuePair<TKey, TValue>> GetProperties(Func<TKey, TValue, Boolean> myPropertyFilter = null)
        {
            return _Data.GetProperties(myPropertyFilter);
        }

        #endregion

        #region RemoveProperty(myKey)

        /// <summary>
        /// Remove the given property key.
        /// </summary>
        /// <param name="myKey">The property key.</param>
        /// <returns>The last value stored before removing it.</returns>
        public TValue RemoveProperty(TKey myKey)
        {
            return _Data.RemoveProperty(myKey);
        }

        #endregion

        #region PropertyKeys

        /// <summary>
        /// An enumeration of all property keys.
        /// </summary>
        public IEnumerable<TKey> PropertyKeys
        {
            get
            {
                return _Data.PropertyKeys;
            }
        }

        #endregion

        #region GetEnumerator()

        /// <summary>
        /// An enumerator of all key-value pairs stored.
        /// </summary>
        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
        {
            return _Data.GetEnumerator();
        }

        /// <summary>
        /// An enumerator of all key-value pairs stored.
        /// </summary>
        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return _Data.GetEnumerator();
        }

        #endregion


        #region Events

        #region CollectionChanged/OnCollectionChanged(...)

        public event NotifyCollectionChangedEventHandler CollectionChanged
        {

            add
            {
                Data.CollectionChanged += value;
            }

            remove
            {
                Data.CollectionChanged -= value;
            }

        }

        public void OnCollectionChanged(NotifyCollectionChangedEventArgs myNotifyCollectionChangedEventArgs)
        {
            Data.OnCollectionChanged(myNotifyCollectionChangedEventArgs);
        }

        #endregion

        #region PropertyChanging/OnPropertyChanging(...)

        public event PropertyChangingEventHandler PropertyChanging
        {

            add
            {
                Data.PropertyChanging += value;
            }

            remove
            {
                Data.PropertyChanging -= value;
            }

        }

        public void OnPropertyChanging(String myPropertyName)
        {
            Data.OnPropertyChanging(myPropertyName);
        }

        public void OnPropertyChanging<TResult>(Expression<Func<TResult>> myPropertyExpression)
        {
            Data.OnPropertyChanging(myPropertyExpression);
        }

        #endregion

        #region PropertyChanged/OnPropertyChanged(...)

        public event PropertyChangedEventHandler PropertyChanged
        {

            add
            {
                Data.PropertyChanged += value;
            }

            remove
            {
                Data.PropertyChanged -= value;
            }

        }

        public void OnPropertyChanged(String myPropertyName)
        {
            Data.OnPropertyChanged(myPropertyName);
        }

        public void OnPropertyChanged<TResult>(Expression<Func<TResult>> myPropertyExpression)
        {
            Data.OnPropertyChanged(myPropertyExpression);
        }

        #endregion

        #endregion

        #region Constructor(s)

        #region APropertyElement(myId, myIdKey, myRevisonIdKey, myDataInitializer, myEdgeInitializer = null)

        /// <summary>
        /// Creates a new APropertyElement.
        /// </summary>
        /// <param name="myId">The Id of this APropertyElement.</param>
        /// <param name="myIdKey">The key to access the Id of this APropertyElement.</param>
        /// <param name="myRevisonIdKey">The key to access the RevisionId of this APropertyElement.</param>
        /// <param name="myDataInitializer">A func to initialize the datastructure of the this APropertyElement.</param>
        /// <param name="myPropertyElementInitializer">An action to add some properties.</param>
        internal protected APropertyElement(TId                               myId,
                                            TKey                              myIdKey,
                                            TKey                              myRevisonIdKey,
                                            Func<TDatastructure>              myDataInitializer,
                                            Action<IProperties<TKey, TValue>> myPropertyElementInitializer = null)
        {

            _IdKey        = myIdKey;
            _RevisonIdKey = myRevisonIdKey;

            _Data         = new Properties<TId, TRevisionId, TKey, TValue, TDatastructure>
                                          (myId, myIdKey, myRevisonIdKey, myDataInitializer);

            if (myPropertyElementInitializer != null)
                myPropertyElementInitializer(Data);

        }

        #endregion

        #endregion

        #region GetHashCode()

        /// <summary>
        /// Return the HashCode of this object.
        /// </summary>
        /// <returns>The HashCode of this object.</returns>
        public override Int32 GetHashCode()
        {
            return Id.GetHashCode();
        }

        #endregion

    }

}
