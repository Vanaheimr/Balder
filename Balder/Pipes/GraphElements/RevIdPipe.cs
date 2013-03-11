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

    #region RevIdPipeExtensions

    /// <summary>
    /// Extension methods for the RevIdPipe.
    /// </summary>
    public static class RevIdPipeExtensions
    {

        #region RevIds<TRevId>(this IEnumerable)

        /// <summary>
        /// Emits the revision identifications of the given revisioned objects.
        /// </summary>
        /// <typeparam name="TRevId">The type of the revision identifications.</typeparam>
        /// <param name="IEnumerable">An enumeration of revisioned objects.</param>
        /// <returns>An enumeration of revision identifications.</returns>
        public static RevIdPipe<TRevId> RevIds<TRevId>(this IEnumerable<IRevisionId<TRevId>> IEnumerable)
            where TRevId : IEquatable<TRevId>, IComparable<TRevId>, IComparable
        {
            return new RevIdPipe<TRevId>(IEnumerable);
        }

        #endregion

    }

    #endregion

    #region RevIdPipe<TRevId>

    /// <summary>
    /// Emits the revision identifications of the given revisioned objects.
    /// </summary>
    /// <typeparam name="TRevId">The type of the revision identifications.</typeparam>
    public class RevIdPipe<TRevId> : FuncPipe<IRevisionId<TRevId>, TRevId>
        where TRevId : IEquatable<TRevId>, IComparable<TRevId>, IComparable
    {

        #region RevIdPipe(IEnumerable = null, IEnumerator = null)

        /// <summary>
        /// Emits the revision identifications of the given revisioned objects.
        /// </summary>
        /// <param name="IEnumerable">An optional IEnumerable&lt;IRevIdentifier&lt;TRevId&gt;&gt; as element source.</param>
        /// <param name="IEnumerator">An optional IEnumerator&lt;IRevIdentifier&lt;TRevId&gt;&gt; as element source.</param>
        public RevIdPipe(IEnumerable<IRevisionId<TRevId>> IEnumerable = null,
                      IEnumerator<IRevisionId<TRevId>> IEnumerator = null)
            : base(Object => (Object != null) ? Object.RevId : default(TRevId), IEnumerable, IEnumerator)
        { }

        #endregion

    }

    #endregion

}
