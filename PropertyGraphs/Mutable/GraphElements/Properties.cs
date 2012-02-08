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
using System.Collections.Generic;

using de.ahzf.Illias.Commons.Votes;

#endregion

namespace de.ahzf.Blueprints.PropertyGraphs.InMemory.Mutable
{

    /// <summary>
    /// A generic class maintaining a collection of key/value properties
    /// within the given datastructure.
    /// </summary>
    /// <typeparam name="TId">The type of the ids.</typeparam>
    /// <typeparam name="TRevisionId">The type of the revision identifiers.</typeparam>
    /// <typeparam name="TKey">The type of the property keys.</typeparam>
    /// <typeparam name="TValue">The type of the property values.</typeparam>
    public class Properties<TId, TRevisionId, TKey, TValue>
                    : IProperties<TKey, TValue>

        where TKey           : IEquatable<TKey>,        IComparable<TKey>,        IComparable
        where TId            : IEquatable<TId>,         IComparable<TId>,         IComparable, TValue
        where TRevisionId    : IEquatable<TRevisionId>, IComparable<TRevisionId>, IComparable, TValue

    {

        #region Data

        /// <summary>
        /// The datastructure holding all graph properties.
        /// </summary>
        protected readonly IDictionary<TKey, TValue> PropertyData;

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

        #region Events

        #region OnPropertyAddition

        /// <summary>
        /// Called when a property value will be added.
        /// </summary>
        public event PropertyAdditionEventHandler<TKey, TValue> OnPropertyAddition;

        #endregion

        #region OnPropertyAdded

        /// <summary>
        /// Called whenever a property value was added.
        /// </summary>
        public event PropertyAddedEventHandler   <TKey, TValue> OnPropertyAdded;

        #endregion

        #region OnPropertyChanging

        /// <summary>
        /// Called whenever a property value will be changed.
        /// </summary>
        public event PropertyChangingEventHandler<TKey, TValue> OnPropertyChanging;

        #endregion

        #region OnPropertyChanged

        /// <summary>
        /// Called whenever a property value was changed.
        /// </summary>
        public event PropertyChangedEventHandler <TKey, TValue> OnPropertyChanged;

        #endregion

        #region OnPropertyRemoval

        /// <summary>
        /// Called whenever a property value will be removed.
        /// </summary>
        public event PropertyRemovalEventHandler <TKey, TValue> OnPropertyRemoval;

        #endregion

        #region OnPropertyRemoved

        /// <summary>
        /// Called whenever a property value was removed.
        /// </summary>
        public event PropertyRemovedEventHandler <TKey, TValue> OnPropertyRemoved;
        
        #endregion

        #endregion

        #region (internal) Send[...]Notifications(...)

        #region (internal) SendPropertyAdditionNotification(Key, Value)

        /// <summary>
        /// Notify about a property to be added.
        /// </summary>
        /// <param name="Key">The key of the property to be added.</param>
        /// <param name="Value">The value of the property to be added.</param>
        internal Boolean SendPropertyAdditionNotification(TKey Key, TValue Value)
        {

            var _VetoVote = new VetoVote();

            if (OnPropertyAddition != null)
                OnPropertyAddition(this, Key, Value, _VetoVote);

            return _VetoVote.Result;

        }

        #endregion

        #region (internal) SendPropertyAddedNotification(Key, Value)

        /// <summary>
        /// Notify about an added property.
        /// </summary>
        /// <param name="Key">The key of the added property.</param>
        /// <param name="Value">The value of the added property.</param>
        internal void SendPropertyAddedNotification(TKey Key, TValue Value)
        {
            if (OnPropertyAdded != null)
                OnPropertyAdded(this, Key, Value);
        }

        #endregion

        #region (internal) SendPropertyChangingNotification(Key, OldValue, NewValue)

        /// <summary>
        /// Notify about a property to be changed.
        /// </summary>
        /// <param name="Key">The key of the property to be changed.</param>
        /// <param name="OldValue">The old value of the property to be changed.</param>
        /// <param name="NewValue">The new value of the property to be changed.</param>
        internal Boolean SendPropertyChangingNotification(TKey Key, TValue OldValue, TValue NewValue)
        {

            var _VetoVote = new VetoVote();

            if (OnPropertyChanging != null)
                OnPropertyChanging(this, Key, OldValue, NewValue, _VetoVote);

            return _VetoVote.Result;

        }

        #endregion

        #region (internal) SendPropertyChangedNotification(Key, OldValue, NewValue)

        /// <summary>
        /// Notify about a changed property.
        /// </summary>
        /// <param name="Key">The key of the changed property.</param>
        /// <param name="OldValue">The old value of the changed property.</param>
        /// <param name="NewValue">The new value of the changed property.</param>
        internal void SendPropertyChangedNotification(TKey Key, TValue OldValue, TValue NewValue)
        {
            if (OnPropertyChanged != null)
                OnPropertyChanged(this, Key, OldValue, NewValue);
        }

        #endregion

        #region (internal) SendPropertyRemovalNotification(Key, Value)

        /// <summary>
        /// Notify about a property to be removed.
        /// </summary>
        /// <param name="Key">The key of the property to be removed.</param>
        /// <param name="Value">The value of the property to be removed.</param>
        internal Boolean SendPropertyRemovalNotification(TKey Key, TValue Value)
        {

            var _VetoVote = new VetoVote();

            if (OnPropertyRemoval != null)
                OnPropertyRemoval(this, Key, Value, _VetoVote);

            return _VetoVote.Result;

        }

        #endregion

        #region (internal) SendPropertyRemovedNotification(Key, Value)

        /// <summary>
        /// Notify about a removed property.
        /// </summary>
        /// <param name="Key">The key of the removed property.</param>
        /// <param name="Value">The value of the removed property.</param>
        internal void SendPropertyRemovedNotification(TKey Key, TValue Value)
        {
            if (OnPropertyRemoved != null)
                OnPropertyRemoved(this, Key, Value);
        }

        #endregion

        #endregion

        #region Constructor(s)

        #region Properties(IdKey, Id, RevisionIdKey, RevisionId, TDatastructureInitializer = null)

        /// <summary>
        /// Creates a new collection of key/value properties.
        /// </summary>
        /// <param name="IdKey">The key to access the identification of the properties.</param>
        /// <param name="Id">The identification of the properties.</param>
        /// <param name="RevisionIdKey">The key to access the revision identification of the properties.</param>
        /// <param name="RevisionId">The revision identification of the properties.</param>
        /// <param name="DatastructureInitializer">A delegate to initialize the datastructure of the the properties.</param>
        public Properties(TKey IdKey, TId Id, TKey RevisionIdKey, TRevisionId RevisionId, Func<IDictionary<TKey, TValue>> DatastructureInitializer)
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


        #region SetProperty(Key, Value)

        /// <summary>
        /// Add a KeyValuePair to the graph element.
        /// If a value already exists for the given key, then the previous value is overwritten.
        /// </summary>
        /// <param name="Key">A key.</param>
        /// <param name="Value">A value.</param>
        public virtual IProperties<TKey, TValue> SetProperty(TKey Key, TValue Value)
        {

            #region Initial Checks

            if (Key.Equals(IdKey))
                throw new IdentificationChangeException();

            if (Key.Equals(RevIdKey))
                throw new RevisionIdentificationChangeException();

            #endregion

            TValue _OldValue;

            if (PropertyData.TryGetValue(Key, out _OldValue))
            {
                SendPropertyChangingNotification(Key, _OldValue, Value);
                PropertyData[Key] = Value;
                SendPropertyChangedNotification (Key, _OldValue, Value);
            }

            else
            {
                SendPropertyAdditionNotification(Key, Value);
                PropertyData.Add(Key, Value);
                SendPropertyAddedNotification(Key, Value);
            }

            return this;

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


        #region Remove(Key)

        /// <summary>
        /// Removes all KeyValuePairs associated with the given key.
        /// </summary>
        /// <param name="Key">A key.</param>
        /// <returns>The value associated with that key prior to the removal.</returns>
        public virtual TValue Remove(TKey Key)
        {

            #region Initial Checks

            if (Key.Equals(IdKey))
                throw new ArgumentException("Removing the Id property is not allowed!");

            if (Key.Equals(RevIdKey))
                throw new ArgumentException("Removing the RevisionId property is not allowed!");

            #endregion

            TValue _Value;

            if (PropertyData.TryGetValue(Key, out _Value))
            {
                SendPropertyRemovalNotification(Key, _Value);
                PropertyData.Remove(Key);
                SendPropertyRemovedNotification(Key, _Value);
            }

            return _Value;

        }

        #endregion

        #region Remove(Key, Value)

        /// <summary>
        /// Remove the given KeyValuePair.
        /// </summary>
        /// <param name="Key">A key.</param>
        /// <param name="Value">A value.</param>
        public Boolean Remove(TKey Key, TValue Value)
        {

            #region Initial Checks

            if (Key.Equals(IdKey))
                throw new ArgumentException("Removing the Id property is not allowed!");

            if (Key.Equals(RevIdKey))
                throw new ArgumentException("Removing the RevisionId property is not allowed!");

            #endregion

            TValue _Value;

            if (GetProperty(Key, out _Value))
            {
                if (_Value.Equals(Value))
                {
                    SendPropertyRemovalNotification(Key, _Value);
                    Remove(Key);
                    SendPropertyRemovedNotification(Key, _Value);
                    return true;
                }
            }

            return false;
            
        }

        #endregion

        #region Remove(KeyValueFilter = null)

        /// <summary>
        /// Remove all KeyValuePairs specified by the given KeyValueFilter.
        /// </summary>
        /// <param name="KeyValueFilter">A delegate to remove properties based on their keys and values.</param>
        /// <returns>A enumeration of all key/value pairs removed by the given KeyValueFilter before their removal.</returns>
        public IEnumerable<KeyValuePair<TKey, TValue>> Remove(KeyValueFilter<TKey, TValue> KeyValueFilter = null)
        {
            throw new NotImplementedException();
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
