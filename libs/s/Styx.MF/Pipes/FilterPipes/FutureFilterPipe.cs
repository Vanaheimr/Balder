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
    /// FutureFilterPipe will allow an object to pass through it if the
    /// object has an output from the pipe provided in the constructor
    /// of the FutureFilterPipe.
    /// </summary>
    /// <typeparam name="S">The type of the elements within the filter.</typeparam>
    public class FutureFilterPipe<S> : AbstractPipe<S, S>, IFilterPipe<S>
    {

        #region Data

        private readonly IPipe _Pipe;

        #endregion

        #region Constructor(s)

        #region FutureFilterPipe(myPipe)

        /// <summary>
        /// Creates a new FutureFilterPipe.
        /// </summary>
        public FutureFilterPipe(IPipe myPipe)
        {
            _Pipe = myPipe;
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

            if (_InternalEnumerator == null)
                return false;

            while (_InternalEnumerator.MoveNext())
            {

                _Pipe.SetSource(new SingleEnumerator<S>(_InternalEnumerator.Current));

                if (_Pipe.MoveNext())
                {

                    while (_Pipe.MoveNext())
                    { }

                    _CurrentElement = _InternalEnumerator.Current;

                    return true;

                }

            }

            return false;

        }

        #endregion


        #region ToString()

        /// <summary>
        /// A string representation of this pipe.
        /// </summary>
        public override String ToString()
        {
            return base.ToString() + "<" + _Pipe + ">";
        }

        #endregion

    }

}
