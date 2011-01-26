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
using de.ahzf.blueprints.Datastructures;

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
        ElementId Id { get; }

        #endregion

        #region Methods

        /// <summary>
        /// Assign a key/value property to the element.
        /// If a value already exists for this key, then the previous key/value is overwritten.
        /// </summary>
        /// <param name="myKey">The property key.</param>
        /// <param name="myValue">The property value.</param>
        IElement SetProperty(String myKey, Object myValue);


        /// <summary>
        /// Return the property value associated with the given property key.
        /// </summary>
        /// <param name="myKey">The key of the key/value property.</param>
        /// <returns>The property value related to the string key.</returns>
        Object GetProperty(String myKey);


        /// <summary>
        /// Allows to return a filtered enumeration of all properties.
        /// </summary>
        /// <param name="myPropertyFilter">A function to filter a property based on its key and value.</param>
        /// <returns>A enumeration of all key/value pairs matching the given property filter.</returns>
        IEnumerable<KeyValuePair<String, Object>> GetProperties(Func<String, Object, Boolean> myPropertyFilter = null);


        /// <summary>
        /// Removes a key/value property from the element.
        /// </summary>
        /// <param name="myKey">The key of the property to remove.</param>
        /// <returns>The property value associated with that key prior to removal.</returns>
        Object RemoveProperty(String myKey);


        /// <summary>
        /// Return all property keys.
        /// </summary>
        IEnumerable<String> PropertyKeys { get; }

        #endregion

    }

}
