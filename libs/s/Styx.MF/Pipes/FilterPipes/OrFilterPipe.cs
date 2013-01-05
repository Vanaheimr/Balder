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
    /// The OrFilterPipe takes a collection of pipes that emit boolean objects.
    /// Each pipe in the collection is fed the same incoming S object. If one
    /// of the internal pipes emits true, then the OrFilterPipe emits the S
    /// object. If not, then the incoming object is not emitted.
    /// </summary>
    /// <typeparam name="S">The type of the elements within the filter.</typeparam>
    public class OrFilterPipe<S> : AbstractPipe<S, S>, IFilterPipe<S>
    {

        #region Data

        private readonly IEnumerable<IPipe<S, Boolean>> _Pipes;

        #endregion

        #region Constructor(s)

        #region OrFilterPipe(myPipes)

        /// <summary>
        /// Creates a new pipe based on the given pipes.
        /// </summary>
        /// <param name="myPipes">Multiple IPipes&lt;S, Boolean&gt;.</param>
        public OrFilterPipe(params IPipe<S, Boolean>[] myPipes)
        {
            _Pipes = new List<IPipe<S, Boolean>>(myPipes);
        }

        #endregion

        #region OrFilterPipe(myPipes)

        /// <summary>
        /// Creates a new pipe based on the given pipes.
        /// </summary>
        /// <param name="myPipes">A collection of IPipes&lt;S, Boolean&gt;.</param>
        public OrFilterPipe(IEnumerable<IPipe<S, Boolean>> myPipes)
        {
            _Pipes = myPipes;
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

                foreach (var _Pipe in _Pipes)
                {

                    _Pipe.SetSource(new SingleEnumerator<S>(_InternalEnumerator.Current));

                    if (_Pipe.MoveNext())
                    {
                        _CurrentElement = _InternalEnumerator.Current;
                        return true;
                    }

                }

            }

            return false;

        }

        #endregion

    }

}
