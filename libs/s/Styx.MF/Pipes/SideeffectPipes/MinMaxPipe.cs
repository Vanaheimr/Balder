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
    /// The MinMaxPipe produces two side effects which keep
    /// track of the Min and Max values of S.
    /// </summary>
    /// <typeparam name="S">The type of the consuming and emitting objects.</typeparam>
    public class MinMaxPipe<S> : AbstractSideEffectPipe<S, S, S, S>
        where S: IComparable, IComparable<S>, IEquatable<S>
    {

        #region Properties

        #region Min

        /// <summary>
        /// The minimum of the passed values.
        /// </summary>
        public S Min
        {
            get
            {
                return _SideEffect1;
            }
        }

        #endregion

        #region Max

        /// <summary>
        /// The maximum of the passed values.
        /// </summary>
        public S Max
        {
            get
            {
                return _SideEffect2;
            }
        }

        #endregion

        #endregion

        #region Constructor(s)

        #region MinMaxFilter<S>(Min, Max, IEnumerable = null, IEnumerator = null)

        /// <summary>
        /// The MinMaxPipe produces two side effects which keep
        /// track of the Min and Max values of S.
        /// </summary>
        /// <param name="Min">The initial minimum.</param>
        /// <param name="Max">The initial maximum.</param>
        /// <param name="IEnumerable">An optional IEnumerable&lt;Double&gt; as element source.</param>
        /// <param name="IEnumerator">An optional IEnumerator&lt;Double&gt; as element source.</param>
        public MinMaxPipe(S Min, S Max, IEnumerable<S> IEnumerable = null, IEnumerator<S> IEnumerator = null)
            : base(IEnumerable, IEnumerator)
        {
            this.SideEffect1 = Min;
            this.SideEffect2 = Max;
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

            if (_InternalEnumerator.MoveNext())
            {

                _CurrentElement = _InternalEnumerator.Current;

                if (Min.CompareTo(_CurrentElement) > 0)
                    SideEffect1 = _CurrentElement;

                if (Max.CompareTo(_CurrentElement) < 0)
                    SideEffect2 = _CurrentElement;

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
            return base.ToString() + "<Min: " + Min + ", Max: " + Max + ">";
        }

        #endregion

    }

}
