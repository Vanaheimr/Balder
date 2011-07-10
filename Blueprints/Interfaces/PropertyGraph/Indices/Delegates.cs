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

namespace de.ahzf.Blueprints.PropertyGraph.Indices
{

    #region IndexFilter<T>(IPropertyElementIndex)

    /// <summary>
    /// A delegate for selecting indices.
    /// </summary>
    /// <typeparam name="TValue">The type of the elements to be indexed.</typeparam>
    /// <param name="IPropertyElementIndex">An IPropertyElementIndex.</param>
    /// <returns>True if the index was selected; False otherwise.</returns>
    public delegate Boolean IndexFilter<TValue>(IPropertyElementIndex<TValue> IPropertyElementIndex)
        where TValue : IEquatable<TValue>, IComparable<TValue>, IComparable;

    #endregion

}
