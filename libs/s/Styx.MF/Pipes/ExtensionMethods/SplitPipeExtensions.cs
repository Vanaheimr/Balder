/*
 * Copyright (c) 2010-2012, Achim 'ahzf' Friedland <achim@graph-database.org>
 * This file is part of Styx <http://www.github.com/Vanaheimr/Styx>
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
using System.IO;

#endregion

namespace de.ahzf.Styx
{

    public static class SplitPipeExtensions
    {

        #region SplitPipeExtensions(this myIEnumerable, Ids, IEnumerable = null, IEnumerator = null)

        /// <summary>
        /// The SplitPipe either allows or disallows all
        /// Edges that have the provided label.
        /// </summary>
        /// <param name="IEnumerable">A collection of objects implementing IPropertyEdge.</param>
        public static SplitPipe<S> SplitPipe<S>(this IEnumerable<S> IEnumerable, Byte Ids)
        {
            return new SplitPipe<S>(Ids, IEnumerable);
        }

        #endregion

        #region SplitPipeExtensions(this myIEnumerator, myLabel, myComparisonFilter)

        /// <summary>
        /// The LabelFilterPipe either allows or disallows all
        /// Edges that have the provided label.
        /// </summary>
        /// <param name="IEnumerator">A enumerator of objects implementing IPropertyEdge.</param>
        public static SplitPipe<S> SplitPipe<S>(this IEnumerator<S> IEnumerator, Byte Ids)
        {
            return new SplitPipe<S>(Ids, null, IEnumerator);
        }

        #endregion

    }

}
