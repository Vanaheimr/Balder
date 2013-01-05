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
    /// The RangeFilterPipe will only allow a sequential subset of its incoming
    /// objects to be emitted to its output. This pipe can be provided -1 for
    /// both its high and low range to denote a wildcard for high and/or low.
    /// Note that -1 for both high and low is equivalent to the IdentityPipe.
    /// </summary>
    public static class RangeFilterPipeExtensions
    {

        #region RangeFilter(this IEnumerable, Low, High)

        /// <summary>
        /// The RangeFilter will only allow a sequential subset of its incoming
        /// objects to be emitted to its output. This pipe can be provided -1 for
        /// both its high and low range to denote a wildcard for high and/or low.
        /// Note that -1 for both high and low is equivalent to the IdentityPipe.
        /// </summary>
        /// <param name="Low">The minimal value.</param>
        /// <param name="High">The maximum value.</param>
        /// <param name="IEnumerable">An enumeration of objects of type S.</param>
        /// <typeparam name="S">The type of the elements within the filter.</typeparam>
        public static RangeFilterPipe<S> RangeFilter<S>(this IEnumerable<S> IEnumerable, Int32 Low, Int32 High)
        {
            return new RangeFilterPipe<S>(Low, High, IEnumerable);
        }

        #endregion

    }

    
    /// <summary>
    /// The RangeFilterPipe will only allow a sequential subset of its incoming
    /// objects to be emitted to its output. This pipe can be provided -1 for
    /// both its high and low range to denote a wildcard for high and/or low.
    /// Note that -1 for both high and low is equivalent to the IdentityPipe.
    /// </summary>
    /// <typeparam name="S">The type of the elements within the filter.</typeparam>
    public class RangeFilterPipe<S> : AbstractPipe<S, S>, IFilterPipe<S>
    {

        #region Data

        private readonly Int32 Low;
        private readonly Int32 High;
        private          Int32 Counter;

        #endregion

        #region Constructor(s)

        #region RangeFilterPipe(Low, High, IEnumerable = null, IEnumerator = null)

        /// <summary>
        /// Creates a new RangeFilterPipe.
        /// </summary>
        /// <param name="Low">The minimal value.</param>
        /// <param name="High">The maximum value.</param>
        /// <param name="IEnumerable">An optional enumation of directories as element source.</param>
        /// <param name="IEnumerator">An optional enumerator of directories as element source.</param>
        public RangeFilterPipe(Int32 Low, Int32 High, IEnumerable<S> IEnumerable = null, IEnumerator<S> IEnumerator = null)
            : base(IEnumerable, IEnumerator)
        {

            if (Low > -1 && High > -1 && Low >= High)
                throw new ArgumentOutOfRangeException("Low must be smaller than High!");

            this.Low     = Low;
            this.High    = High;
            this.Counter = -1;

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

                Counter++;

                if ((Low  == -1 || Counter >= Low) &&
                    (High == -1 || Counter <= High))
                {
                    _CurrentElement = _InternalEnumerator.Current;
                    return true;
                }

                if (High > 0 && Counter > High)
                    return false;

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
            return base.ToString() + "<" + Low + ", " + High + ">";
        }

        #endregion

    }

}
