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

using org.GraphDefined.Vanaheimr.Illias;
using org.GraphDefined.Vanaheimr.Styx;

#endregion

namespace org.GraphDefined.Vanaheimr.Balder
{

    #region IdPipeExtensions

    /// <summary>
    /// Extension methods for the IdPipe.
    /// </summary>
    public static class IdPipeExtensions
    {

        /// <summary>
        /// Emits the identifications of the given identifiable objects.
        /// </summary>
        /// <typeparam name="TId">The type of the identifications.</typeparam>
        /// <param name="SourcePipe">An IEndPipe&lt;IIdentifier&lt;TId&gt;&gt; as element source.</param>
        /// <returns>An enumeration of identifications.</returns>
        public static IdPipe<TId> Ids<TId>(this IEndPipe<IIdentifier<TId>> SourcePipe)
            where TId : IEquatable<TId>, IComparable<TId>, IComparable
        {
            return new IdPipe<TId>(SourcePipe);
        }

    }

    #endregion

    #region IdPipe<TId>

    /// <summary>
    /// Emits the identifications of the given identifiable objects.
    /// </summary>
    /// <typeparam name="TId">The type of the identifications.</typeparam>
    public class IdPipe<TId> : SelectPipe<IIdentifier<TId>, TId>
        where TId : IEquatable<TId>, IComparable<TId>, IComparable
    {

        /// <summary>
        /// Emits the identifications of the given identifiable objects.
        /// </summary>
        /// <param name="SourcePipe">An IEndPipe&lt;IIdentifier&lt;TId&gt;&gt; as element source.</param>
        public IdPipe(IEndPipe<IIdentifier<TId>> SourcePipe)
            : base(SourcePipe, Object => Object.Id)
        { }

    }

    #endregion

}
