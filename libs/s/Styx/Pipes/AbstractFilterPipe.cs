/*
 * Copyright (c) 2010-2013, Achim 'ahzf' Friedland <achim@graph-database.org>
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
using System.Collections;
using System.Collections.Generic;

#endregion

namespace de.ahzf.Vanaheimr.Styx
{

    #region AbstractFilterPipe<S>

    /// <summary>
    /// An AbstractFilterPipe provides most of the functionality that is repeated
    /// in every instance of a FilterPipe. Any subclass of AbstractPipe should simply
    /// implement MoveNext().
    /// </summary>
    /// <typeparam name="S">The type of the filtered objects.</typeparam>
    public abstract class AbstractFilterPipe<S> : AbstractPipe<S, S>,
                                                  IFilterPipe<S>
	{

		#region AbstractFilterPipe()
		
        /// <summary>
        /// Creates a AbstractFilterPipe pipe.
        /// </summary>
		public AbstractFilterPipe()
		{ }
		
		#endregion

        #region AbstractFilterPipe(IEnumerator, IEnumerable)

        /// <summary>
        /// Creates a new AbstractFilterPipe using the elements emitted
        /// by the given IEnumerator as input.
        /// </summary>
        /// <param name="IEnumerable">An IEnumerable&lt;S&gt; as element source.</param>
        /// <param name="IEnumerator">An IEnumerator&lt;S&gt; as element source.</param>
        public AbstractFilterPipe(IEnumerable<S> IEnumerable, IEnumerator<S> IEnumerator)
            : base(IEnumerable, IEnumerator)
        { }

        #endregion

    }

    #endregion

}
