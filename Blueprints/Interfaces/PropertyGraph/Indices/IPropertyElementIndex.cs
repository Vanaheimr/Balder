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

    #region IPropertyElementIndex

    /// <summary>
    /// A property element index is a data structure that supports the indexing of properties in property graphs.
    /// An index is typically some sort of tree structure that allows for the fast lookup of elements by key/value pairs.
    /// </summary>
    public interface IPropertyElementIndex
    {

        /// <summary>
        /// A human-friendly name of this index.
        /// </summary>
        String Name { get; }


        /// <summary>
        /// The type of the index keys.
        /// </summary>
        Type KeyType { get; }

    }

    #endregion

    #region IPropertyElementIndex<T>

    /// <summary>
    /// A property element index is a data structure that supports the indexing of properties in property graphs.
    /// An index is typically some sort of tree structure that allows for the fast lookup of elements by key/value pairs.
    /// </summary>
    /// <typeparam name="T">The type of the elements to be indexed.</typeparam>
    public interface IPropertyElementIndex<T> : IPropertyElementIndex
        where T : IEquatable<T>, IComparable<T>, IComparable
    {

        /// <summary>
        /// Inserts an element into the index.
        /// </summary>
        /// <param name="Element">An element for indexing.</param>
        void Insert(T Element);


        /// <summary>
        /// Get the indexed elements for the given typed index key.
        /// </summary>
        /// <typeparam name="TIdxKey">The type of the index key which must be at least ICompare/IEquatable.</typeparam>
        /// <param name="IdxKey">An index key.</param>
        IEnumerable<T> Get<TIdxKey>(TIdxKey IdxKey)
            where TIdxKey : IEquatable<TIdxKey>, IComparable<TIdxKey>, IComparable;


        /// <summary>
        /// Remove an element from the index.
        /// </summary>
        /// <param name="Element">An element for indexing.</param>
        void Remove(T Element);



        /// <summary>
        /// Downcast this index to an IPropertyElementIndex&lt;T, TIndexKey&gt; index.
        /// </summary>
        /// <typeparam name="TIdxKey">The type of the index keys.</typeparam>
        IPropertyElementIndex<T, TIdxKey> CastTo<TIdxKey>()
            where TIdxKey : IEquatable<TIdxKey>, IComparable<TIdxKey>, IComparable;


    }

    #endregion

    #region IPropertyElementIndex<T, TIndexKey>

    /// <summary>
    /// A property element index is a data structure that supports the indexing of properties in property graphs.
    /// An index is typically some sort of tree structure that allows for the fast lookup of elements by key/value pairs.
    /// </summary>
    /// <typeparam name="T">The type of the elements to be indexed.</typeparam>
    /// <typeparam name="TIndexKey">The type of the index keys.</typeparam>
    public interface IPropertyElementIndex<T, TIndexKey> : IPropertyElementIndex<T>
        where T         : IEquatable<T>,         IComparable<T>,         IComparable
        where TIndexKey : IEquatable<TIndexKey>, IComparable<TIndexKey>, IComparable
    {

        /// <summary>
        /// Get the indexed elements for the given typed index key.
        /// </summary>
        /// <param name="IdxKey">An index key.</param>
        IEnumerable<T> Get(TIndexKey IdxKey);

    }

    #endregion

}
