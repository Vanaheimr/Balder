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
    /// StartPipe is a handy was to create a pipe out of the provided object.
    /// The provided object is set as the start of the Pipe that simply returns the object or
    /// If the object is an IEnumerator/IEnumerable, the objects of the object.
    /// </summary>
    /// <typeparam name="S">The type of the consuming object.</typeparam>
    public class StartPipe<S> : AbstractPipe<S, S>
    {

        #region Constructor(s)

        #region StartPipe(Object)

        public StartPipe(Object Object)
        {

            if (Object is IEnumerator<S>)
                _InputEnumerator = (IEnumerator<S>) Object;

            else if (Object is IEnumerable<S>)
                _InputEnumerator = ((IEnumerable<S>) Object).GetEnumerator();

            else if (Object is S)
                _InputEnumerator = (new List<S>() { (S) Object }).GetEnumerator();

            else
                throw new ArgumentException("Could not use the given Object!");

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
                return true;
            }

            return false;

        }

        #endregion

    }

}
