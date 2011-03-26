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
//        where T : IElement
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
