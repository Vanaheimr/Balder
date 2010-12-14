/*
 * Copyright (c) 2010, Achim 'ahzf' Friedland
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
    public interface IElement
    {

        /// <summary>
        /// Return the object value associated with the provided string key.
        /// </summary>
        /// <param name="myKey">the key of the key/value property</param>
        /// <returns>the object value related to the string key</returns>
        Object getProperty(String myKey);


        /// <summary>
        /// Return the object value of type TValue associated with the provided string key.
        /// </summary>
        /// <typeparam name="TValue">the type the property</typeparam>
        /// <param name="myKey">the key of the key/value property</param>
        /// <returns>the object value related to the string key</returns>
        TValue getProperty<TValue>(String myKey);


        /// <summary>
        /// Assign a key/value property to the element.
        /// If a value already exists for this key, then the previous key/value is overwritten.
        /// </summary>
        /// <param name="myKey">the string key of the property</param>
        /// <param name="myValue">the object value o the property</param>
        void setProperty(String myKey, Object myValue);


        /// <summary>
        /// Return all the keys associated with the element.
        /// </summary>
        /// <returns>the set of all string keys associated with the element</returns>
        IEnumerable<String> Keys { get; }


        /// <summary>
        /// Unassigns a key/value property from the element.
        /// The object value of the removed property is returned.
        /// </summary>
        /// <param name="myKey">the key of the property to remove from the element</param>
        /// <returns>the object value associated with that key prior to removal</returns>
        Object removeProperty(String myKey);


        /// <summary>
        /// An identifier that is unique to its inheriting class.
        /// All vertices of a graph must have unique identifiers.
        /// All edges of a graph must have unique identifiers.
        /// </summary>
        /// <returns>the identifier of the element</returns>
        Object Id { get; }


    }

}
