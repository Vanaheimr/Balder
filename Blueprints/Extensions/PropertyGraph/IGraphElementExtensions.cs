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
using System.Linq;
using System.Collections.Generic;

#endregion

namespace de.ahzf.Blueprints.PropertyGraph
{

    /// <summary>
    /// Extensions to the IGraphElement interface
    /// </summary>
    public static class IGraphElementExtensions
    {

        #region AsDynamic(this IGraphElement)

        /// <summary>
        /// Converts the given IGraphElement into a dynamic object.
        /// </summary>
        /// <typeparam name="TId">The type of the identifiers.</typeparam>
        /// <typeparam name="TRevisionId">The type of the revision identifiers.</typeparam>
        /// <typeparam name="TKey">The type of the property keys.</typeparam>
        /// <typeparam name="TValue">The type of the property values.</typeparam>
        /// <param name="IGraphElement">An object implementing IGraphElement&lt;...&gt;.</param>
        /// <returns>A dynamic graph element.</returns>
        public static dynamic AsDynamic<TId, TRevisionId, TKey, TValue>(
                                        this IGraphElement<TId, TRevisionId, TKey, TValue> IGraphElement)

            where TKey        : IEquatable<TKey>,        IComparable<TKey>,        IComparable
            where TId         : IEquatable<TId>,         IComparable<TId>,         IComparable, TValue
            where TRevisionId : IEquatable<TRevisionId>, IComparable<TRevisionId>, IComparable, TValue
        {
            return IGraphElement as dynamic;
        }

        #endregion

    }

}
