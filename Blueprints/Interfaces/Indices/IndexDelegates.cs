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

namespace de.ahzf.Blueprints.Indices
{

    #region IndexTransformation(Element)

    /// <summary>
    /// A delegate for transforming an element into an index key.
    /// </summary>
    /// <typeparam name="TKey">The type of the index keys.</typeparam>
    /// <typeparam name="TValue">The type of the elements to index.</typeparam>
    /// <param name="Element">The element to be indexed.</param>
    /// <returns>An indexkey.</returns>
    public delegate TKey IndexTransformation<TKey, TValue>(TValue Element);

    #endregion

    #region IndexSelector(Element)

    /// <summary>
    /// A delegate for deciding if an element should be indexed or not.
    /// </summary>
    /// <typeparam name="TKey">The type of the index keys.</typeparam>
    /// <typeparam name="TValue">The type of the elements to index.</typeparam>
    /// <param name="Element">The element to be indexed.</param>
    /// <returns>True if the element should be indexed; False otherwise.</returns>
    public delegate Boolean IndexSelector<TKey, TValue>(TValue Element);

    #endregion

    #region IndexNameFilter(IndexName)

    /// <summary>
    /// A delegate for selecting indices by their human-friendly name.
    /// </summary>
    /// <param name="IndexName">The human-friendly name of an index.</param>
    /// <returns>True if the index was selected; False otherwise.</returns>
    public delegate Boolean IndexNameFilter(String IndexName);

    #endregion

    #region IndexEvaluator(Key, Elements)

    /// <summary>
    /// A delegate for selecting indexed elements.
    /// </summary>
    /// <typeparam name="TKey">The type of the index keys.</typeparam>
    /// <typeparam name="TValue">The type of the elements to be indexed.</typeparam>
    /// <param name="Key">The index key.</param>
    /// <param name="Elements">A set of elements associated with the index key.</param>
    /// <returns>True if the elements match; False otherwise.</returns>
    public delegate Boolean IndexEvaluator<TKey, TValue>(TKey Key, ISet<TValue> Elements)
        where TKey   : IEquatable<TKey>,   IComparable<TKey>,   IComparable
        where TValue : IEquatable<TValue>, IComparable<TValue>, IComparable;

    #endregion

}
