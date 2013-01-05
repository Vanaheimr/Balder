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
    /// The ScatterPipe will unroll any IEnumerator/IEnumerable that is inputted into it.
    /// This will only occur for one level deep. It will not unroll an IEnumerator emitted by an IEnumerator, etc.
    /// </summary>
    /// <typeparam name="S">The type of the consuming objects.</typeparam>
    /// <typeparam name="E">The type of the emitting objects.</typeparam>
    public class ScatterPipe<S, E> : AbstractPipe<S, E>
    {

        #region Data

        private IEnumerator<S> _TempIterator;

        #endregion

        #region Constructor(s)

        #region ScatterPipe

        /// <summary>
        /// The ScatterPipe will unroll any IEnumerator/IEnumerable that is inputted into it.
        /// </summary>
        /// <param name="IEnumerable"></param>
        /// <param name="IEnumerator"></param>
        public ScatterPipe(IEnumerable<S> IEnumerable = null,
                           IEnumerator<S> IEnumerator = null)

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
                    _CurrentElement = (E) (Object) _TempIterator.Current;
                    return true;
                }
                
                if (_InternalEnumerator.MoveNext())
                {
                    
                    var _S = _InternalEnumerator.Current;
                    
                    if (_S is IEnumerator<S>)
                        _TempIterator = (IEnumerator<S> ) _S;

                    else if (_S is IEnumerable<S>)
                        _TempIterator = ((IEnumerable<S>) _S).GetEnumerator();

                    else
                    {
                        try
                        {
                            _CurrentElement = (E) (Object) _S;
                            return true;
                        }
                        catch (Exception)
                        {
                            return false;
                        }
                    }

                }

                else
                    return false;

            }


        }

        #endregion

    }

}
