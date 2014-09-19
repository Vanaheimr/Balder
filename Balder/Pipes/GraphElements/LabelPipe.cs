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

    #region LabelPipeExtensions

    /// <summary>
    /// Extension methods for the LabelPipe.
    /// </summary>
    public static class LabelPipeExtensions
    {

        /// <summary>
        /// Emits the labels of the given labeled objects.
        /// </summary>
        /// <typeparam name="TLabel">The type of the labels.</typeparam>
        /// <param name="SourcePipe">An IEndPipe&lt;IIdentifier&lt;TId&gt;&gt; as element source.</param>
        /// <returns>An enumeration of labels.</returns>
        public static LabelPipe<TLabel> Labels<TLabel>(this IEndPipe<ILabel<TLabel>> SourcePipe)
            where TLabel : IEquatable<TLabel>, IComparable<TLabel>, IComparable
        {
            return new LabelPipe<TLabel>(SourcePipe);
        }

    }

    #endregion

    #region LabelPipe<TLabel>

    /// <summary>
    /// Emits the labels of the given labeled objects.
    /// </summary>
    /// <typeparam name="TLabel">The type of the labels.</typeparam>
    public class LabelPipe<TLabel> : SelectPipe<ILabel<TLabel>, TLabel>
        where TLabel : IEquatable<TLabel>, IComparable<TLabel>, IComparable
    {

        /// <summary>
        /// Emits the labels of the given labeled objects.
        /// </summary>
        /// <param name="SourcePipe">An IEndPipe&lt;IIdentifier&lt;TId&gt;&gt; as element source.</param>
        public LabelPipe(IEndPipe<ILabel<TLabel>> SourcePipe)
            : base(SourcePipe, Object => Object.Label)
        { }

    }

    #endregion

}
