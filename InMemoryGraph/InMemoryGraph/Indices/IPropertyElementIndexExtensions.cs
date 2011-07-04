﻿/*
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
using System.Linq;
using System.Collections.Generic;

#endregion

namespace de.ahzf.Blueprints.PropertyGraph
{

    /// <summary>
    /// Extensions to the IElement interface
    /// </summary>
    public static class IIndexingExtensions
    {

        #region Insert(this IPropertyElementIndex, Elements)

        public static void Insert<T, TIndexKey>(this IPropertyElementIndex<T, TIndexKey> IPropertyElementIndex, IEnumerable<T> Elements)
            where T         : IEquatable<T>,         IComparable<T>,         IComparable
            where TIndexKey : IEquatable<TIndexKey>, IComparable<TIndexKey>, IComparable
        {
            foreach (var Element in Elements)
                IPropertyElementIndex.Insert(Element);
        }

        #endregion

    }

}
