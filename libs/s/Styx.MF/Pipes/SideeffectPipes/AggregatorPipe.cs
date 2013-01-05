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
    /// The AggregatorPipe produces a side effect that is the provided collection
    /// filled with the contents of all the objects that have passed through it.
    /// Before the first object is emitted from the AggregatorPipe, all of its
    /// incoming objects have been aggregated into the collection.
    /// The collection enumerator is used as the emitting enumerator. Thus, what
    /// goes into AggregatorPipe may not be the same as what comes out of
    /// AggregatorPipe.
    /// For example, duplicates removed, different order to the stream, etc.
    /// Finally, note that different Collections have different behaviors and
    /// write/read times.
    /// </summary>
    public class AggregatorPipe<S> : AbstractSideEffectPipe<S, S, ICollection<S>>
    {

        #region Data

        private IEnumerator<S> _AggregateEnumerator;

        #endregion

        #region Constructor(s)

        #region AggregatorPipe(ICollection = null)

        /// <summary>
        /// Creates a new AggregatorPipe.
        /// </summary>
        /// <param name="ICollection">An optional ICollection to store the passed objects.</param>
        public AggregatorPipe(ICollection<S> ICollection = null)
        {
            if (ICollection == null)
                _SideEffect = new List<S>();
            else
                _SideEffect = ICollection;
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

            // First consume all elements emitted by the _InternalEnumerator
            if (_AggregateEnumerator == null)
            {

                if (_InternalEnumerator == null)
                    return false;

                while (_InternalEnumerator.MoveNext())
                    _SideEffect.Add(_InternalEnumerator.Current);

                _AggregateEnumerator = _SideEffect.GetEnumerator();

            }

            // Second emit the collected elements
            if (_AggregateEnumerator.MoveNext())
            {
                _CurrentElement = _AggregateEnumerator.Current;
                return true;
            }

            else
                return false;

        }

        #endregion

    }

}
