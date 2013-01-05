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
    /// The GroupCountPipe will simply emit the incoming object, but generate a map side effect.
    /// The map's keys are the objects that come into the pipe.
    /// The map's values are the number of times that the key object has come into the pipe.
    /// </summary>
    public class GroupCountPipe<S> : AbstractPipe<S, S>, ISideEffectPipe<S, S, IDictionary<S, UInt64>>
    {

        #region Data

        private IDictionary<S, UInt64> _CountMap;

        #endregion

        #region Constructor(s)

        #region GroupCountPipe()

        /// <summary>
        /// Creates a new GroupCountPipe.
        /// </summary>
        public GroupCountPipe()
        {
            _CountMap = new Dictionary<S, UInt64>();
        }

        #endregion

        #region GroupCountPipe(myIDictionary)

        /// <summary>
        /// Creates a new GroupCountPipe using the given IDictionary&lt;S, UInt64&gt;.
        /// </summary>
        public GroupCountPipe(IDictionary<S, UInt64> myIDictionary)
        {
            _CountMap = myIDictionary;
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
                UpdateMap(_CurrentElement);
                return true;
            }

            else
                return false;

        }

        #endregion

        #region SideEffect

        /// <summary>
        /// The sideeffect produced by this pipe.
        /// </summary>
        public IDictionary<S, UInt64> SideEffect
        {
            get
            {
                return _CountMap;
            }
        }

        #endregion


        #region (private) UpdateMap(myElement)

        private void UpdateMap(S myElement)
        {

            UInt64 _Counter;

            if (_CountMap.TryGetValue(myElement, out _Counter))
                _CountMap[myElement] = _Counter + 1;

            else
                _CountMap.Add(myElement, 1);

        }

        #endregion

    }

}
