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
using System.Collections.Generic;

#endregion

namespace de.ahzf.Vanaheimr.Styx
{

    /// <summary>
    /// Filters the consuming objects by calling a Func&lt;S, Boolean&gt;.
    /// </summary>
    /// <typeparam name="S">The type of the consuming and emitting objects.</typeparam>
    public class FuncFilterPipe<S> : AbstractPipe<S, S>, IFilterPipe<S>
    {

        #region Data

        private readonly Func<S, Boolean> _FilterFunc;

        #endregion

        #region Constructor(s)

        #region FuncFilterPipe(FilterFunc)

        /// <summary>
        /// Creates a new FuncFilterPipe using the given Func&lt;S, E&gt;.
        /// </summary>
        /// <param name="FilterFunc">A Func&lt;S, Boolean&gt; filtering the consuming objects. True means filter (ignore).</param>
        public FuncFilterPipe(Func<S, Boolean> FilterFunc)
        {

            if (FilterFunc == null)
                throw new ArgumentNullException("The given FilterFunc must not be null!");

            _FilterFunc = FilterFunc;

        }

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

                if (!_FilterFunc(_InputEnumerator.Current))
                {
                    _CurrentElement = _InputEnumerator.Current;
                    return true;
                }

            }

            return false;

        }

        #endregion

    }

}
