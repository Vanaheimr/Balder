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

using de.ahzf.Vanaheimr.Styx;
using de.ahzf.Vanaheimr.Blueprints;

#endregion

namespace de.ahzf.Vanaheimr.Balder
{

    #region LabelPipeExtensions

    /// <summary>
    /// Extension methods for the LabelPipe.
    /// </summary>
    public static class LabelPipeExtensions
    {

        #region Label(this IReadOnlyGraphElement<...>)

        /// <summary>
        /// Emits the label of the given graph element.
        /// </summary>
        /// <typeparam name="TId">The type of the identifiers.</typeparam>
        /// <typeparam name="TRevId">The type of the revision identifiers.</typeparam>
        /// <typeparam name="TLabel">The taype of the labels.</typeparam>
        /// <typeparam name="TKey">The type of the property keys.</typeparam>
        /// <typeparam name="TValue">The type of the property values.</typeparam>
        /// <param name="IEnumerable">An enumeration of graph elements.</param>
        /// <returns>The the labels of the given graph elements.</returns>
        public static LabelPipe<TId, TRevId, TLabel, TKey, TValue>

                          Label<TId, TRevId, TLabel, TKey, TValue>(this IReadOnlyGraphElement<TId, TRevId, TLabel, TKey, TValue> IReadOnlyGraphElement)

            where TId     : IEquatable<TId>,    IComparable<TId>,    IComparable, TValue
            where TRevId  : IEquatable<TRevId>, IComparable<TRevId>, IComparable, TValue
            where TLabel  : IEquatable<TLabel>, IComparable<TLabel>, IComparable
            where TKey    : IEquatable<TKey>,   IComparable<TKey>,   IComparable

        {

            return new LabelPipe<TId, TRevId, TLabel, TKey, TValue>(
                new IReadOnlyGraphElement<TId, TRevId, TLabel, TKey, TValue>[1] { IReadOnlyGraphElement });

        }

        #endregion

        #region Label(this IEnumerable<IReadOnlyGraphElement<...>>)

        /// <summary>
        /// Emits the labels of the given graph elements.
        /// </summary>
        /// <typeparam name="TId">The type of the identifiers.</typeparam>
        /// <typeparam name="TRevId">The type of the revision identifiers.</typeparam>
        /// <typeparam name="TLabel">The taype of the labels.</typeparam>
        /// <typeparam name="TKey">The type of the property keys.</typeparam>
        /// <typeparam name="TValue">The type of the property values.</typeparam>
        /// <param name="IEnumerable">An enumeration of graph elements.</param>
        /// <returns>The the labels of the given graph elements.</returns>
        public static LabelPipe<TId, TRevId, TLabel, TKey, TValue>

                          Label<TId, TRevId, TLabel, TKey, TValue>(this IEnumerable<IReadOnlyGraphElement<TId, TRevId, TLabel, TKey, TValue>> IEnumerable)

            where TId     : IEquatable<TId>,    IComparable<TId>,    IComparable, TValue
            where TRevId  : IEquatable<TRevId>, IComparable<TRevId>, IComparable, TValue
            where TLabel  : IEquatable<TLabel>, IComparable<TLabel>, IComparable
            where TKey    : IEquatable<TKey>,   IComparable<TKey>,   IComparable

        {
            return new LabelPipe<TId, TRevId, TLabel, TKey, TValue>(IEnumerable);
        }

        #endregion

    }

    #endregion

    #region LabelPipe()

    /// <summary>
    /// Emits the labels of the given graph elements.
    /// </summary>
    /// <typeparam name="TId">The type of the identifiers.</typeparam>
    /// <typeparam name="TRevId">The type of the revision identifiers.</typeparam>
    /// <typeparam name="TLabel">The taype of the labels.</typeparam>
    /// <typeparam name="TKey">The type of the property keys.</typeparam>
    /// <typeparam name="TValue">The type of the property values.</typeparam>
    public class LabelPipe<TId, TRevId, TLabel, TKey, TValue>
                     : AbstractPipe<IReadOnlyGraphElement<TId, TRevId, TLabel, TKey, TValue>,
                                    TLabel>

        where TId     : IEquatable<TId>,    IComparable<TId>,    IComparable, TValue
        where TRevId  : IEquatable<TRevId>, IComparable<TRevId>, IComparable, TValue
        where TLabel  : IEquatable<TLabel>, IComparable<TLabel>, IComparable
        where TKey    : IEquatable<TKey>,   IComparable<TKey>,   IComparable

    {

        #region Constructor(s)

        #region LabelPipe(IEnumerable = null, IEnumerator = null)

        /// <summary>
        /// Emits the labels of the given graph elements.
        /// </summary>
        /// <param name="IEnumerable">An optional IEnumerable&lt;...&gt; as element source.</param>
        /// <param name="IEnumerator">An optional IEnumerator&lt;...&gt; as element source.</param>
        public LabelPipe(IEnumerable<IReadOnlyGraphElement<TId, TRevId, TLabel, TKey, TValue>> IEnumerable = null,
                         IEnumerator<IReadOnlyGraphElement<TId, TRevId, TLabel, TKey, TValue>> IEnumerator = null)

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

            while (_InputEnumerator.MoveNext())
            {
                _CurrentElement = _InputEnumerator.Current.Label;
                return true;
            }

            return false;

        }

        #endregion

    }

    #endregion

}
