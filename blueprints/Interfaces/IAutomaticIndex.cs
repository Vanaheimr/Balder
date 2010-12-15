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
    /// An automatic index will automatically maintain an index of element properties as element properties mutate.
    /// If an element is removed from the graph, then it is also automatically removed from the automatic index.
    /// The key/value pairs that are automatically monitored are element properties and their values.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface AutomaticIndex<T> : IIndex<T>
        where T : IElement
    {

        /// <summary>
        /// Add an element property key that should be indexed.
        /// If null is provided as the key, then all properties are indexed (i.e. null is wildcard)
        /// </summary>
        /// <param name="myKey">the element property key to be indexed</param>
        void AddAutoIndexKey(String myKey);


        /// <summary>
        /// Remove an element property key from being indexed.
        /// </summary>
        /// <param name="myKey">the element property to key to not be indexed</param>
        void RemoveAutoIndexKey(String myKey);


        /// <summary>
        /// Get all the element property keys currently being indexed.
        /// If what is returned is null, then all keys are currently being indexed (i.e. null is wildcard)
        /// </summary>
        IEnumerable<String> AutoIndexKeys { get; }

    }

}
