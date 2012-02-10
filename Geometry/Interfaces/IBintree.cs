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
using System.Collections.Generic;

#endregion

namespace de.ahzf.Blueprints
{

    /// <summary>
    /// A bintree is an indexing structure for 1-dimensional spartial data.
    /// It stores the given maximum number of elements and forkes itself
    /// into two subtrees if this number becomes larger.
    /// Note: This datastructure is not self-balancing!
    /// </summary>
    /// <typeparam name="T">The internal datatype of the bintree.</typeparam>
    public interface IBintree<T> : IEnumerable<T>
        where T : IEquatable<T>, IComparable<T>, IComparable
    {

        #region Events

        /// <summary>
        /// An event to notify about an bintree split happening.
        /// </summary>
        event BintreeSplitEventHandler<T> OnTreeSplit;

        #endregion

        #region Properties

        /// <summary>
        /// The maximum number of embedded elements before
        /// four child node will be created.
        /// </summary>
        UInt32 MaxNumberOfEmbeddedElements { get; }

        /// <summary>
        /// Return the number of embedded pixels
        /// stored within this Bintree(Node).
        /// </summary>
        UInt64 EmbeddedCount{ get; }

        /// <summary>
        /// Return the number of pixels stored
        /// within the entire bintree.
        /// </summary>
        UInt64 Count { get; }

        #endregion

        #region Methods

        /// <summary>
        /// Add an element to the bintree.
        /// </summary>
        /// <param name="Element">An element of type T.</param>
        void Add(T Element);

        /// <summary>
        /// Return all elements matching the given elementselector delegate.
        /// </summary>
        /// <param name="ElementSelector">A delegate selecting which elements to return.</param>
        IEnumerable<T> Get(ElementSelector<T> ElementSelector);

        /// <summary>
        /// Return all elements within the given line.
        /// </summary>
        /// <param name="Line">A line selecting which elements to return.</param>
        IEnumerable<T> Get(ILine1D<T> Line);

        /// <summary>
        /// Remove a element from the bintree.
        /// </summary>
        /// <param name="Element">An element of type T.</param>
        void Remove(T Element);

        /// <summary>
        /// Remove all elements located within the given line.
        /// </summary>
        /// <param name="Line">A line selecting which elements to remove.</param>
        void Remove(ILine1D<T> Line);

        #endregion

    }

}
