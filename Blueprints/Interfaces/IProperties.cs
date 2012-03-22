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
using System.ComponentModel;
using System.Linq.Expressions;
using System.Collections.Generic;
using System.Collections.Specialized;

using de.ahzf.Illias.Commons;
using de.ahzf.Illias.Commons.Votes;

#endregion

namespace de.ahzf.Blueprints.PropertyGraphs
{

    // Delegates

    #region PropertiesInitializer

    /// <summary>
    /// A delegate for IProperties initializing.
    /// </summary>
    /// <typeparam name="TKey">The type of the keys.</typeparam>
    /// <typeparam name="TValue">The type of the values.</typeparam>
    public delegate void IPropertiesInitializer<TKey, TValue>(IProperties<TKey, TValue> PropertyVertex)

        where TKey : IEquatable<TKey>, IComparable<TKey>, IComparable;

    #endregion

    #region PropertyAdditionEventHandler<TKey, TValue>

    /// <summary>
    /// An event handler called whenever a property value will be added.
    /// </summary>
    /// <param name="Sender">The sender of this event.</param>
    /// <param name="Key">The key of the property to be added.</param>
    /// <param name="Value">The value of the property to be added.</param>
    /// <param name="VetoVote">A veto vote is a simple way to ask multiple event subscribers if the edge should be added or not.</param>
    public delegate void PropertyAdditionEventHandler<TKey, TValue>(IProperties<TKey, TValue> Sender, TKey Key, TValue Value, VetoVote VetoVote)
        where TKey : IEquatable<TKey>, IComparable<TKey>, IComparable;

    #endregion

    #region PropertyAddedEventHandler<TKey, TValue>

    /// <summary>
    /// An event handler called whenever a property value was added.
    /// </summary>
    /// <param name="Sender">The sender of this event.</param>
    /// <param name="Key">The key of the added property.</param>
    /// <param name="Value">The value of the added property.</param>
    public delegate void PropertyAddedEventHandler<TKey, TValue>(IProperties<TKey, TValue> Sender, TKey Key, TValue Value)
        where TKey : IEquatable<TKey>, IComparable<TKey>, IComparable;

    #endregion

    #region PropertyChangingEventHandler<TKey, TValue>

    /// <summary>
    /// An event handler called whenever a property value will be changed.
    /// </summary>
    /// <param name="Sender">The sender of this event.</param>
    /// <param name="Key">The key of the property to be changed.</param>
    /// <param name="OldValue">The old value of the property to be changed.</param>
    /// <param name="NewValue">The new value of the property to be changed.</param>
    /// <param name="VetoVote">A veto vote is a simple way to ask multiple event subscribers if the edge should be added or not.</param>
    public delegate void PropertyChangingEventHandler<TKey, TValue>(IProperties<TKey, TValue> Sender, TKey Key, TValue OldValue, TValue NewValue, VetoVote VetoVote)
        where TKey : IEquatable<TKey>, IComparable<TKey>, IComparable;

    #endregion

    #region PropertyChangedEventHandler<TKey, TValue>

    /// <summary>
    /// An event handler called whenever a property value was changed.
    /// </summary>
    /// <param name="Sender">The sender of this event.</param>
    /// <param name="Key">The key of the changed property.</param>
    /// <param name="OldValue">The old value of the changed property.</param>
    /// <param name="NewValue">The new value of the changed property.</param>
    public delegate void PropertyChangedEventHandler<TKey, TValue>(IProperties<TKey, TValue> Sender, TKey Key, TValue OldValue, TValue NewValue)
        where TKey : IEquatable<TKey>, IComparable<TKey>, IComparable;

    #endregion

    #region PropertyRemovalEventHandler<TKey, TValue>

    /// <summary>
    /// An event handler called whenever a property will be removed.
    /// </summary>
    /// <param name="Sender">The sender of this event.</param>
    /// <param name="Key">The key of the property to be removed.</param>
    /// <param name="Value">The value of the property to be removed.</param>
    /// <param name="VetoVote">A veto vote is a simple way to ask multiple event subscribers if the edge should be added or not.</param>
    public delegate void PropertyRemovalEventHandler<TKey, TValue>(IProperties<TKey, TValue> Sender, TKey Key, TValue Value, VetoVote VetoVote)
        where TKey : IEquatable<TKey>, IComparable<TKey>, IComparable;

    #endregion

    #region PropertyRemovedEventHandler<TKey, TValue>

    /// <summary>
    /// An event handler called whenever a property was removed.
    /// </summary>
    /// <param name="Sender">The sender of this event.</param>
    /// <param name="Key">The key of the removed property.</param>
    /// <param name="Value">The value of the removed property.</param>
    public delegate void PropertyRemovedEventHandler<TKey, TValue>(IProperties<TKey, TValue> Sender, TKey Key, TValue Value)
        where TKey : IEquatable<TKey>, IComparable<TKey>, IComparable;

    #endregion


    // Interface

    #region IProperties<TKey, TValue>

    /// <summary>
    /// A generic interface maintaining a collection of key/value properties
    /// within the given datastructure.
    /// </summary>
    /// <typeparam name="TKey">The type of the keys.</typeparam>
    /// <typeparam name="TValue">The type of the values.</typeparam>
    public interface IProperties<TKey, TValue> : IEnumerable<KeyValuePair<TKey, TValue>>

        where TKey : IEquatable<TKey>, IComparable<TKey>, IComparable
    {

        #region Events

        /// <summary>
        /// Called whenever a property value will be added.
        /// </summary>
        event PropertyAdditionEventHandler<TKey, TValue> OnPropertyAddition;

        /// <summary>
        /// Called whenever a property value was added.
        /// </summary>
        event PropertyAddedEventHandler<TKey, TValue> OnPropertyAdded;


        /// <summary>
        /// Called whenever a property value will be changed.
        /// </summary>
        event PropertyChangingEventHandler<TKey, TValue> OnPropertyChanging;

        /// <summary>
        /// Called whenever a property value was changed.
        /// </summary>
        event PropertyChangedEventHandler<TKey, TValue> OnPropertyChanged;


        /// <summary>
        /// Called whenever a property value will be removed.
        /// </summary>
        event PropertyRemovalEventHandler<TKey, TValue> OnPropertyRemoval;

        /// <summary>
        /// Called whenever a property value was removed.
        /// </summary>
        event PropertyRemovedEventHandler<TKey, TValue> OnPropertyRemoved;

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


        #region Well-known properties

        #region IdKey

        /// <summary>
        /// The property key of the identification.
        /// </summary>
        TKey IdKey { get; }

        #endregion

        #region RevIdKey

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

        #region Contains...

        /// <summary>
        /// Determines if the given key exists.
        /// </summary>
        /// <param name="Key">A key.</param>
        Boolean ContainsKey(TKey Key);

        /// <summary>
        /// Determines if the given value exists.
        /// </summary>
        /// <param name="Value">A value.</param>
        Boolean ContainsValue(TValue Value);

        /// <summary>
        /// Determines if the given key and value exists.
        /// </summary>
        /// <param name="Key">A key.</param>
        /// <param name="Value">A value.</param>
        Boolean Contains(TKey Key, TValue Value);

        /// <summary>
        /// Determines if the given KeyValuePair exists.
        /// </summary>
        /// <param name="KeyValuePair">A KeyValuePair.</param>
        Boolean Contains(KeyValuePair<TKey, TValue> KeyValuePair);

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
        Boolean TryGetProperty(TKey Key, out TValue Value);

        /// <summary>
        /// Return the value associated with the given key.
        /// </summary>
        /// <param name="Key">A key.</param>
        /// <param name="Value">The associated value.</param>
        /// <typeparam name="T">Check if the value is of the given type.</typeparam>
        /// <returns>True if the returned value is valid. False otherwise.</returns>
        Boolean TryGetProperty<T>(TKey Key, out T Value);

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
        /// Remove the given key and value pair.
        /// </summary>
        /// <param name="Key">A key.</param>
        /// <param name="Value">A value.</param>
        /// <returns>The value associated with that key prior to the removal.</returns>
        TValue Remove(TKey Key, TValue Value);

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
