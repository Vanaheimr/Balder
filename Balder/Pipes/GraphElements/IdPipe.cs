/*
 * Copyright (c) 2010-2013, Achim 'ahzf' Friedland <achim@graph-database.org>
 * This file is part of Balder <http://www.github.com/Vanaheimr/Balder>
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

using eu.Vanaheimr.Illias.Commons;
using eu.Vanaheimr.Styx;

#endregion

namespace eu.Vanaheimr.Balder
{

    #region IdPipeExtensions

    /// <summary>
    /// Extension methods for the IdPipe.
    /// </summary>
    public static class IdPipeExtensions
    {

        #region Ids<TId>(this IEnumerable)

        /// <summary>
        /// Emits the identifications of the given identifiable objects.
        /// </summary>
        /// <typeparam name="TId">The type of the identifications.</typeparam>
        /// <param name="IEnumerable">An enumeration of identifiable objects.</param>
        /// <returns>An enumeration of identifications.</returns>
        public static IdPipe<TId> Ids<TId>(this IEnumerable<IIdentifier<TId>> IEnumerable)
            where TId : IEquatable<TId>, IComparable<TId>, IComparable
        {
            return new IdPipe<TId>(IEnumerable);
        }

        #endregion

    }

    #endregion

    #region IdPipe<TId>

    /// <summary>
    /// Emits the identifications of the given identifiable objects.
    /// </summary>
    /// <typeparam name="TId">The type of the identifications.</typeparam>
    public class IdPipe<TId> : FuncPipe<IIdentifier<TId>, TId>
        where TId : IEquatable<TId>, IComparable<TId>, IComparable
    {

        #region IdPipe(IEnumerable = null, IEnumerator = null)

        /// <summary>
        /// Emits the identifications of the given identifiable objects.
        /// </summary>
        /// <param name="IEnumerable">An optional IEnumerable&lt;IIdentifier&lt;TId&gt;&gt; as element source.</param>
        /// <param name="IEnumerator">An optional IEnumerator&lt;IIdentifier&lt;TId&gt;&gt; as element source.</param>
        public IdPipe(IEnumerable<IIdentifier<TId>> IEnumerable = null,
                      IEnumerator<IIdentifier<TId>> IEnumerator = null)
            : base(Object => (Object != null) ? Object.Id : default(TId), IEnumerable, IEnumerator)
        { }

        #endregion

    }

    #endregion

}
