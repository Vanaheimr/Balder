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
using System.Threading;
using System.Collections.Generic;

#endregion

namespace de.ahzf.Vanaheimr.Styx
{

    /// <summary>
    /// The CountPipe produces a side effect that is the total
    /// number of objects that have passed through it.
    /// </summary>
    public class CountPipe<S> : AbstractSideEffectPipe<S, S, Int64>
    {

        #region Constructor(s)

        #region CountPipe()

        /// <summary>
        /// Creates a new CountPipe.
        /// </summary>
        /// <param name="InitialValue">An optional initial value.</param>
        /// <param name="IEnumerable">An optional IEnumerable&lt;Double&gt; as element source.</param>
        /// <param name="IEnumerator">An optional IEnumerator&lt;Double&gt; as element source.</param>
        public CountPipe(Int64 InitialValue = 0, IEnumerable<S> IEnumerable = null, IEnumerator<S> IEnumerator = null)
            : base(IEnumerable, IEnumerator)
        {
            _SideEffect = InitialValue;
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

            if (_InputEnumerator.MoveNext())
            {
                _CurrentElement = _InputEnumerator.Current;
                Interlocked.Increment(ref _SideEffect);
                return true;
            }

            return false;

        }

        #endregion


        #region ToString()

        /// <summary>
        /// Returns a string representation of this pipe.
        /// </summary>
        public override String ToString()
        {
            return base.ToString() + "<" + _SideEffect + ">";
        }

        #endregion

    }

}
