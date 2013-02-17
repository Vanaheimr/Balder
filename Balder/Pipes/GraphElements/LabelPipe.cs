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

    #region LabelPipeExtensions

    /// <summary>
    /// Extension methods for the LabelPipe.
    /// </summary>
    public static class LabelPipeExtensions
    {

        #region Labels<TLabel>(this IEnumerable)

        /// <summary>
        /// Emits the labels of the given labeled objects.
        /// </summary>
        /// <typeparam name="TLabel">The type of the labels.</typeparam>
        /// <param name="IEnumerable">An enumeration of labeled objects.</param>
        /// <returns>An enumeration of labels.</returns>
        public static LabelPipe<TLabel> Labels<TLabel>(this IEnumerable<ILabel<TLabel>> IEnumerable)
            where TLabel : IEquatable<TLabel>, IComparable<TLabel>, IComparable
        {
            return new LabelPipe<TLabel>(IEnumerable);
        }

        #endregion

    }

    #endregion

    #region LabelPipe<TLabel>

    /// <summary>
    /// Emits the labels of the given labeled objects.
    /// </summary>
    /// <typeparam name="TLabel">The type of the labels.</typeparam>
    public class LabelPipe<TLabel> : FuncPipe<ILabel<TLabel>, TLabel>
        where TLabel : IEquatable<TLabel>, IComparable<TLabel>, IComparable
    {

        #region Constructor(s)

        #region LabelPipe(IEnumerable = null, IEnumerator = null)

        /// <summary>
        /// Emits the labels of the given labeled objects.
        /// </summary>
        /// <param name="IEnumerable">An optional IEnumerable&lt;...&gt; as element source.</param>
        /// <param name="IEnumerator">An optional IEnumerator&lt;...&gt; as element source.</param>
        public LabelPipe(IEnumerable<ILabel<TLabel>> IEnumerable = null, IEnumerator<ILabel<TLabel>> IEnumerator = null)
            : base(Object => Object.Label, IEnumerable, IEnumerator)
        { }

        #endregion

        #endregion

    }

    #endregion

}
