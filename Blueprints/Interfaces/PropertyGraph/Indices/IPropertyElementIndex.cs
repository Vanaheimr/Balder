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
using System.Collections.Generic;

#endregion

namespace de.ahzf.Blueprints.PropertyGraph
{

    /// <summary>
    /// A property element index is a data structure that supports the indexing of properties in property graphs.
    /// An index is typically some sort of tree structure that allows for the fast lookup of elements by key/value pairs.
    /// </summary>
    public interface IPropertyElementIndex<T, TIndexKey>
        where T         : IEquatable<T>,         IComparable<T>,         IComparable
        where TIndexKey : IEquatable<TIndexKey>, IComparable<TIndexKey>, IComparable

    {

        /// <summary>
        /// A human-friendly name of this index.
        /// </summary>
        String IndexName { get; }

        /// <summary>
        /// Inserts an element into the index.
        /// </summary>
        /// <param name="Element">An element to index.</param>
        void Insert(T Element);


        IEnumerable<T> Get(TIndexKey key);

        
        void Remove(T Element);

    }

}
