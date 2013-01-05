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
    /// Extension methods for the BackPipe.
    /// </summary>
    public static class BackPipeExtensions
    {

        #region Back<E, S>(this IPipe, Steps = 1)

        /// <summary>
        /// Returns the incomming vertices of the given PropertyEdges.
        /// </summary>
        /// <param name="IEnumerable">An enumeration of PropertyEdges.</param>
        /// <param name="VertexFilter">An optional delegate for vertex filtering.</param>
        /// <returns>The incomming vertices of the given PropertyEdges.</returns>
        public static BackPipe<E, S> Back<E, S>(this IPipe<S, E> IPipe, UInt64 Steps = 1)
        {
            return new BackPipe<E, S>(IPipe, Steps);
        }

        #endregion

    }


    #region BackPipe()

    /// <summary>
    /// BackPipe will fully process the object through its internal pipe.
    /// If the internal pipe yields results, then the original object is emitted
    /// from the BackPipe.
    /// </summary>
    /// <typeparam name="S">The type of the elements within the filter.</typeparam>
    public class BackPipe<E, S> : AbstractPipe<E, S>
    {

        #region Data

        private readonly IPipe<S, E> IPipe;
        private readonly UInt64      Steps = 1;
        private          Int32       _ReturnPosition;

        #endregion

        #region Constructor(s)

        #region BackPipe(IPipe, Steps = 1)

        public BackPipe(IPipe<S, E> IPipe, UInt64 Steps = 1)
        {
            this.IPipe          = IPipe;
            this.Steps          = Steps;
            _InputEnumerator = IPipe.GetEnumerator();
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

                _ReturnPosition = this.Path.Count - 2 - (Int32) Steps;

                if (_ReturnPosition < 0)
                    throw new Exception("The path is shorter than the given number of steps!");

                try
                {
                    _CurrentElement = (S) this.Path[_ReturnPosition];
                }
                catch (Exception)
                {
                    throw new Exception("Invalid type of element within path!");
                }

                return true;

            }

            return false;

        }

        #endregion

    }

    #endregion

}
