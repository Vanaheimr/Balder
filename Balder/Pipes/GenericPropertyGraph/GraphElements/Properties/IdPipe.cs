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

using de.ahzf.Illias.Commons;
using de.ahzf.Vanaheimr.Styx;

#endregion

namespace de.ahzf.Vanaheimr.Balder
{

    #region IdPipeExtensions

    /// <summary>
    /// Extension methods for the IdPipe.
    /// </summary>
    public static class IdPipeExtensions
    {

        #region Ids<TId>(this IIdentifier)

        /// <summary>
        /// Emits the identification of the given identifiable object.
        /// </summary>
        /// <typeparam name="TId">The type of the identifications.</typeparam>
        /// <param name="IIdentifier">An identifiable object.</param>
        /// <returns>An identification.</returns>
        public static IdPipe<TId> Ids<TId>(this IIdentifier<TId> IIdentifier)

            where TId : IEquatable<TId>, IComparable<TId>, IComparable

        {
            return new IdPipe<TId>(new IIdentifier<TId>[1] { IIdentifier });
        }

        #endregion

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
    public class IdPipe<TId> : AbstractPipe<IIdentifier<TId>, TId>
        
        where TId : IEquatable<TId>, IComparable<TId>, IComparable

    {

        #region Constructor(s)

        #region IdPipe(IEnumerable = null, IEnumerator = null)

        /// <summary>
        /// Emits the identifications of the given identifiable objects.
        /// </summary>
        /// <param name="IEnumerable">An optional IEnumerable&lt;IIdentifier&lt;TId&gt;&gt; as element source.</param>
        /// <param name="IEnumerator">An optional IEnumerator&lt;IIdentifier&lt;TId&gt;&gt; as element source.</param>
        public IdPipe(IEnumerable<IIdentifier<TId>> IEnumerable = null,
                      IEnumerator<IIdentifier<TId>> IEnumerator = null)

            : base(IEnumerable, IEnumerator)

        { }

        #endregion

        #endregion

        #region MoveNext()

        /// <summary>
        /// Advances the enumerator to the next element of the collection.
        /// </summary>
        /// <returns>
        /// True if the enumerator was successfully advanced to the next
        /// element; false if the enumerator has passed the end of the
        /// collection.
        /// </returns>
        public override Boolean MoveNext()
        {

            if (_InputEnumerator == null)
                return false;

            if (_InputEnumerator.MoveNext())
            {
                _CurrentElement = _InputEnumerator.Current.Id;
                return true;
            }

            else
                return false;

        }

        #endregion

    }

    #endregion

}
