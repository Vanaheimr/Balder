/*
 * Copyright (c) 2010, Achim 'ahzf' Friedland <code@ahzf.de>
 * 
 * This file is part of blueprints.NET and licensed
 * as free software under the New BSD License.
 */

#region Usings

using System;
using System.Collections.Generic;

#endregion

namespace de.ahzf.blueprints
{

    /// <summary>
    /// An element is the base class for both vertices and edges.
    /// An element has an identifier that must be unique to its inheriting classes (vertex or edges).
    /// An element can maintain a collection of key/value properties.
    /// Keys are always strings and values can be any object.
    /// Particular implementations can reduce the space of objects that can be used as values.
    /// </summary>
    public interface IElement : IEnumerable<KeyValuePair<String, Object>>, IEquatable<IElement>
    {

        #region Properties

        /// <summary>
        /// An identifier that is unique to its inheriting class.
        /// All vertices and edges of a graph must have unique identifiers.
        /// </summary>
        /// <returns>the identifier of the element</returns>
        IComparable Id { get; }

        #endregion

        #region Methods

        /// <summary>
        /// Assign a key/value property to the element.
        /// If a value already exists for this key, then the previous key/value is overwritten.
        /// </summary>
        /// <param name="myKey">the string key of the property</param>
        /// <param name="myValue">the object value o the property</param>
        void SetProperty(String myKey, Object myValue);


        /// <summary>
        /// Return the object value associated with the provided string key.
        /// </summary>
        /// <param name="myKey">the key of the key/value property</param>
        /// <returns>the object value related to the string key</returns>
        Object GetProperty(String myKey);


        /// <summary>
        /// Allows to return a filtered enumeration of all properties.
        /// </summary>
        /// <param name="myPropertyFilter">A function to filter a property based on its key and value.</param>
        /// <returns>A enumeration of all objects matching the given property filter.</returns>
        IEnumerable<KeyValuePair<String, Object>> GetProperties(Func<String, Object, Boolean> myPropertyFilter = null);


        /// <summary>
        /// Unassigns a key/value property from the element.
        /// The object value of the removed property is returned.
        /// </summary>
        /// <param name="myKey">the key of the property to remove from the element</param>
        /// <returns>the object value associated with that key prior to removal</returns>
        Object RemoveProperty(String myKey);

        #endregion

    }

}
