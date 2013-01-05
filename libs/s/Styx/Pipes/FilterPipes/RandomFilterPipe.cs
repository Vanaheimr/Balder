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
    /// The RandomFilterPipe filters out objects that pass through it using a biased coin.
    /// For each passing object, a random number generator creates a double value between 0 and 1.
    /// If the randomly generated double is less than or equal the provided bias, then the object is allowed to pass.
    /// If the randomly generated double is greater than the provided bias, then the object is not allowed to pass.
    /// </summary>
    public static class RandomFilterPipeExtensions
    {

        #region RandomFilter(this IEnumerable)

        /// <summary>
        /// The RandomFilterPipe filters out objects that pass through it using a biased coin.
        /// For each passing object, a random number generator creates a double value between 0 and 1.
        /// If the randomly generated double is less than or equal the provided bias, then the object is allowed to pass.
        /// If the randomly generated double is greater than the provided bias, then the object is not allowed to pass.        /// </summary>
        /// <param name="Bias">The bias.</param>
        /// <param name="Random">An optional source of randomness.</param>
        /// <param name="IEnumerable">An enumeration of objects of type S.</param>
        /// <typeparam name="S">The type of the elements within the filter.</typeparam>
        public static RandomFilterPipe<S> RandomFilter<S>(this IEnumerable<S> IEnumerable, Double Bias, Random Random = null)
        {
            return new RandomFilterPipe<S>(Bias, Random, IEnumerable);
        }

        #endregion

    }

    
    /// <summary>
    /// The RandomFilterPipe filters out objects that pass through it using a biased coin.
    /// For each passing object, a random number generator creates a double value between 0 and 1.
    /// If the randomly generated double is less than or equal the provided bias, then the object is allowed to pass.
    /// If the randomly generated double is greater than the provided bias, then the object is not allowed to pass.
    /// </summary>
    /// <typeparam name="S">The type of the elements within the filter.</typeparam>
    public class RandomFilterPipe<S> : AbstractPipe<S, S>, IFilterPipe<S>
    {

        #region Data

        private readonly Random Random;
        private readonly Double Bias;

        #endregion

        #region Constructor(s)

        #region RandomFilterPipe(Bias, Random = null, IEnumerable = null, IEnumerator = null)

        /// <summary>
        /// Creates a new RandomFilterPipe.
        /// </summary>
        /// <param name="Bias">The bias.</param>
        /// <param name="Random">An optional source of randomness.</param>
        /// <param name="IEnumerable">An optional enumation of directories as element source.</param>
        /// <param name="IEnumerator">An optional enumerator of directories as element source.</param>
        public RandomFilterPipe(Double Bias, Random Random = null, IEnumerable<S> IEnumerable = null, IEnumerator<S> IEnumerator = null)
            : base(IEnumerable, IEnumerator)
        {
            this.Bias   = Bias;
            this.Random = (Random == null) ? new Random() : Random;
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

                if (Bias >= Random.NextDouble())
                {
                    _CurrentElement = _InputEnumerator.Current;
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
            return base.ToString() + "<" + Bias + ">";
        }

        #endregion

    }

}
