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

#endregion

namespace de.ahzf.Styx
{

    /// <summary>
    /// The UnrollPipe will unroll any IEnumerator/IEnumerable of IEnumerable that is inputted into it.
    /// </summary>
    /// <typeparam name="S">The type of the consuming objects.</typeparam>
    public class UnrollPipe<S> : AbstractPipe<IEnumerable<S>, S>
    {

        #region Data

        private IEnumerator<S> _TempIterator;

        #endregion

        #region Constructor(s)

        #region UnrollPipe

        /// <summary>
        /// The UnrollPipe will unroll any IEnumerator/IEnumerable of IEnumerable that is inputted into it.
        /// </summary>
        /// <param name="IEnumerable"></param>
        /// <param name="IEnumerator"></param>
        public UnrollPipe(IEnumerable<IEnumerable<S>> IEnumerable = null,
                          IEnumerator<IEnumerable<S>> IEnumerator = null)

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

            if (_InternalEnumerator == null)
                return false;

            while (true)
            {

                if (_TempIterator != null && _TempIterator.MoveNext())
                {
                    _CurrentElement = _TempIterator.Current;
                    return true;
                }
                
                if (_InternalEnumerator.MoveNext())
                {
                    _TempIterator = _InternalEnumerator.Current.GetEnumerator();
                }

                else
                    return false;

            }


        }

        #endregion

    }

}
