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
using System.Collections;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Collections.Generic;
using System.Collections.Specialized;
using de.ahzf.Blueprints.PropertyGraph.ReadOnly;

#endregion

namespace de.ahzf.Blueprints.PropertyGraph.InMemory.ReadOnly
{

    /// <summary>
    /// A generic class maintaining a collection of key/value properties
    /// within the given datastructure.
    /// </summary>
    /// <typeparam name="TId">The type of the ids.</typeparam>
    /// <typeparam name="TRevisionId">The type of the revision identifiers.</typeparam>
    /// <typeparam name="TKey">The type of the property keys.</typeparam>
    /// <typeparam name="TValue">The type of the property values.</typeparam>
    /// <typeparam name="TDatastructure">The type of the datastructure to maintain the key/value pairs.</typeparam>
    public class ReadOnlyProperties<TId, TRevisionId, TKey, TValue, TDatastructure>
                    : IReadOnlyProperties<TKey, TValue, TDatastructure>

        where TKey           : IEquatable<TKey>,        IComparable<TKey>,        IComparable
        where TId            : IEquatable<TId>,         IComparable<TId>,         IComparable, TValue
        where TRevisionId    : IEquatable<TRevisionId>, IComparable<TRevisionId>, IComparable, TValue
        where TDatastructure : IDictionary<TKey, TValue>

    {

        #region Data

        /// <summary>
        /// The datastructure holding all graph properties.
        /// </summary>
        protected readonly TDatastructure PropertyData;

        #endregion

        #region Properties

        #region IdKey

        /// <summary>
        /// The property key of the identification.
        /// </summary>
        public TKey IdKey { get; private set; }

        #endregion

        #region Id

        /// <summary>
        /// An identifier that is unique to its inheriting class.
        /// All vertices of a graph must have unique identifiers.
        /// All edges of a graph must have unique identifiers.
        /// </summary>
        public TId Id
        {
            get
            {

                TValue _TValue;

                if (PropertyData.TryGetValue(IdKey, out _TValue))
                    return (TId) _TValue;

                return default(TId);

            }
        }

        #endregion

        #region RevisionIdKey

        /// <summary>
        /// The property key of the revision identification.
        /// </summary>
        public TKey RevIdKey { get; private set; }

        #endregion

        #region RevisionId

        /// <summary>
        /// The RevisionId extends the Id to identify multiple revisions of
        /// an element during the lifetime of a graph. A RevisionId should
        /// additionally be unique among all elements of a graph.
        /// </summary>
        public TRevisionId RevisionId
        {
            get
            {

                TValue _TValue;

                if (PropertyData.TryGetValue(RevIdKey, out _TValue))
                    return (TRevisionId) (Object) _TValue;

                return default(TRevisionId);

            }
        }

        #endregion

        #endregion

        #region Constructor(s)

        #region ReadOnlyProperties(IdKey, Id, RevisionIdKey, RevisionId, TDatastructureInitializer = null)

        /// <summary>
        /// Creates a new collection of key/value properties.
        /// </summary>
        /// <param name="IdKey">The key to access the identification of the properties.</param>
        /// <param name="Id">The identification of the properties.</param>
        /// <param name="RevisionIdKey">The key to access the revision identification of the properties.</param>
        /// <param name="RevisionId">The revision identification of the properties.</param>
        /// <param name="DatastructureInitializer">A delegate to initialize the datastructure of the the properties.</param>
        public ReadOnlyProperties(TKey IdKey, TId Id, TKey RevisionIdKey, TRevisionId RevisionId, Func<TDatastructure> DatastructureInitializer)
        {

            #region Initial checks

            if (IdKey == null)
                throw new ArgumentNullException("The given IdKey must not be null!");

            if (Id == null)
                throw new ArgumentNullException("The given Id must not be null!");

            if (RevisionIdKey == null)
                throw new ArgumentNullException("The given RevisionIdKey must not be null!");

            if (DatastructureInitializer == null)
                throw new ArgumentNullException("The given DatastructureInitializer must not be null!");

            #endregion

            this.IdKey         = IdKey;
            this.RevIdKey = RevisionIdKey;
            this.PropertyData  = DatastructureInitializer();
            this.PropertyData.Add(IdKey,         Id);
            this.PropertyData.Add(RevisionIdKey, RevisionId);

        }

        #endregion

        #endregion


        #region IProperties Members

        #region Keys

        /// <summary>
        /// An enumeration of all property keys.
        /// </summary>
        public IEnumerable<TKey> Keys
        {
            get
            {
                return PropertyData.Keys;
            }
        }

        #endregion

        #region Values

        /// <summary>
        /// An enumeration of all property values.
        /// </summary>
        public IEnumerable<TValue> Values
        {
            get
            {
                return PropertyData.Values;
            }
        }

        #endregion


        #region ContainsKey(Key)

        /// <summary>
        /// Determines if the specified key exists.
        /// </summary>
        /// <param name="Key">A key.</param>
        public Boolean ContainsKey(TKey Key)
        {
            return PropertyData.ContainsKey(Key);
        }

        #endregion

        #region ContainsValue(Value)

        /// <summary>
        /// Determines if the specified value exists.
        /// </summary>
        /// <param name="Value">A value.</param>
        public Boolean ContainsValue(TValue Value)
        {

            foreach (var _Value in PropertyData.Values)
                if (_Value.Equals(Value))
                    return true;

            return false;

        }

        #endregion

        #region Contains(Key, Value)

        /// <summary>
        /// Determines if an KeyValuePair with the specified key and value exists.
        /// </summary>
        /// <param name="Key">A key.</param>
        /// <param name="Value">A value.</param>
        public Boolean Contains(TKey Key, TValue Value)
        {
            return PropertyData.Contains(new KeyValuePair<TKey,TValue>(Key, Value));
        }

        #endregion


        #region this[Key]

        /// <summary>
        /// Return the value associated with the given key.
        /// </summary>
        /// <param name="Key">A key.</param>
        public virtual TValue this[TKey Key]
        {
            get
            {

                TValue _Object;

                PropertyData.TryGetValue(Key, out _Object);

                return _Object;

            }
        }

        #endregion

        #region Get(Key, out Value)

        /// <summary>
        /// Return the value associated with the given key.
        /// </summary>
        /// <param name="Key">A key.</param>
        /// <param name="Value">The associated value.</param>
        /// <returns>True if the returned value is valid. False otherwise.</returns>
        public virtual Boolean GetProperty(TKey Key, out TValue Value)
        {
            return PropertyData.TryGetValue(Key, out Value);
        }

        #endregion

        #region Get(KeyValueFilter = null)

        /// <summary>
        /// Return a filtered enumeration of all KeyValuePairs.
        /// </summary>
        /// <param name="KeyValueFilter">A delegate to filter properties based on their keys and values.</param>
        /// <returns>A enumeration of all key/value pairs matching the given KeyValueFilter.</returns>
        public virtual IEnumerable<KeyValuePair<TKey, TValue>> GetProperties(KeyValueFilter<TKey, TValue> KeyValueFilter = null)
        {

            if (KeyValueFilter == null)
            {
                foreach (var _KeyValuePair in PropertyData)
                    if (_KeyValuePair.Value != null)
                        yield return _KeyValuePair;
            }

            else
            {
                foreach (var _KeyValuePair in PropertyData)
                    if (_KeyValuePair.Value != null)
                        if (KeyValueFilter(_KeyValuePair.Key, _KeyValuePair.Value))
                            yield return _KeyValuePair;
            }

        }

        #endregion

        #endregion

        #region IEnumerable Members

        /// <summary>
        /// Return an enumeration of all properties within this element.
        /// </summary>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return PropertyData.GetEnumerator();
        }

        /// <summary>
        /// Return an enumeration of all properties within this element.
        /// </summary>
        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
        {
            return PropertyData.GetEnumerator();
        }

        #endregion


        #region GetHashCode()

        /// <summary>
        /// Return the HashCode of this object.
        /// </summary>
        /// <returns>The HashCode of this object.</returns>
        public override Int32 GetHashCode()
        {

            if (Id == null)
                return 0;

            return Id.GetHashCode();

        }

        #endregion

        #region ToString()

        /// <summary>
        /// Returns a string representation of this object.
        /// </summary>
        public override String ToString()
        {

            var IdString = (Id == null) ? "<null>" : Id.ToString();

            return this.GetType().Name + "(Id = " + IdString + ")";

        }

        #endregion

    }

}
