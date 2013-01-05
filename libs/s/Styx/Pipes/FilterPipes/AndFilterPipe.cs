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
    /// The AndFilterPipe takes a collection of pipes, where E is boolean.
    /// Each provided pipe is fed the same incoming S object. If all the
    /// pipes emit true, then the AndFilterPipe emits the incoming S object.
    /// If not, then the incoming S object is not emitted.
    /// </summary>
    /// <typeparam name="S">The type of the consuming and emitting objects.</typeparam>
    public class AndFilterPipe<S> : AbstractPipe<S, S>, IFilterPipe<S>
    {

        #region Data

        private readonly IEnumerable<IPipe<S, Boolean>> _Pipes;

        #endregion

        #region Constructor(s)

        #region AndFilterPipe(myPipes)

        /// <summary>
        /// Creates a new pipe based on the given pipes.
        /// </summary>
        /// <param name="myPipes">Multiple IPipes&lt;S, Boolean&gt;.</param>
        public AndFilterPipe(params IPipe<S, Boolean>[] myPipes)
        {
            _Pipes = new List<IPipe<S, Boolean>>(myPipes);
        }

        #endregion

        #region AndFilterPipe(myPipes)

        /// <summary>
        /// Creates a new pipe based on the given pipes.
        /// </summary>
        /// <param name="myPipes">A collection of IPipes&lt;S, Boolean&gt;.</param>
        public AndFilterPipe(IEnumerable<IPipe<S, Boolean>> myPipes)
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

            if (_InputEnumerator == null)
                return false;

            while (_InputEnumerator.MoveNext())
            {

                var _And = true;

                foreach (var _Pipe in _Pipes)
                {

                    _Pipe.SetSource(new SingleEnumerator<S>(_InputEnumerator.Current));

                    if (!_Pipe.MoveNext())
                    {
                        _And = false;
                        break;
                    }

                }

                if (_And)
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
