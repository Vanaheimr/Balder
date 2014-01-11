/*
 * Copyright (c) 2010-2014, Achim 'ahzf' Friedland <achim@graphdefined.org>
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

        /// <summary>
        /// Emits the revision identifications of the given revisioned objects.
        /// </summary>
        /// <typeparam name="TRevId">The type of the revision identifications.</typeparam>
        /// <param name="IEnumerable">An enumeration of revisioned objects.</param>
        /// <returns>An enumeration of revision identifications.</returns>
        public static RevIdPipe<TRevId> RevIds<TRevId>(this IEndPipe<IRevisionId<TRevId>> SourcePipe)
            where TRevId : IEquatable<TRevId>, IComparable<TRevId>, IComparable
        {
            return new RevIdPipe<TRevId>(SourcePipe);
        }

    }

    #endregion

    #region RevIdPipe<TRevId>

    /// <summary>
    /// Emits the revision identifications of the given revisioned objects.
    /// </summary>
    /// <typeparam name="TRevId">The type of the revision identifications.</typeparam>
    public class RevIdPipe<TRevId> : SelectPipe<IRevisionId<TRevId>, TRevId>
        where TRevId : IEquatable<TRevId>, IComparable<TRevId>, IComparable
    {

        /// <summary>
        /// Emits the revision identifications of the given revisioned objects.
        /// </summary>
        public RevIdPipe(IEndPipe<IRevisionId<TRevId>> SourcePipe)
            : base(SourcePipe, Object => Object.RevId)
        { }

    }

    #endregion

}
