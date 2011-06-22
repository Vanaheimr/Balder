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

namespace de.ahzf.Blueprints.QuadStore
{

    /// <summary>
    /// Extensions to the IQuadStore interface
    /// </summary>
    public static class IQuadStoreExtensions
    {

        #region GetQuads<TValue>(this QuadStore, QuadIds)

        /// <summary>
        /// Returns a enumeration of quads having the given QuadIds.
        /// </summary>
        /// <typeparam name="T">The type of the subjects, predicates and objects of the stored quads.</typeparam>
        /// <param name="QuadStore">A QuadStore.</param>
        /// <param name="QuadIds">An enumeration of QuadIds.</param>
        public static IEnumerable<IQuad<T>> GetQuads<T>(this IQuadStore<T> QuadStore, IEnumerable<T> QuadIds)
            where T : IEquatable<T>, IComparable, IComparable<T>
        {
            return from _QuadId in QuadIds select QuadStore.GetQuad(_QuadId);
        }

        #endregion

    }

}
