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
    /// A delegate for selecting indexed elements.
    /// </summary>
    /// <typeparam name="T">The type of the elements to be indexed.</typeparam>
    /// <typeparam name="TIndexKey">The type of the index keys.</typeparam>
    /// <param name="IndexKey">The index key.</param>
    /// <param name="IndexValues">A set of value for the given index key.</param>
    /// <returns>True if the elements match; False otherwise.</returns>
    public delegate Boolean IndexEvaluator<TIndexKey, T>(TIndexKey IndexKey, ISet<T> IndexValues)
        where T         : IEquatable<T>,         IComparable<T>,         IComparable
        where TIndexKey : IEquatable<TIndexKey>, IComparable<TIndexKey>, IComparable;

}
