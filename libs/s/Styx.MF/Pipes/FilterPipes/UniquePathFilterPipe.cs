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
    /// UniquePathFilterPipe will only let an object pass if the path up to
    /// this point has no repeated elements. Thus, its a way to filter out
    /// paths that are looping.
    /// </summary>
    /// <typeparam name="S">The type of the elements within the filter.</typeparam>
    public class UniquePathFilterPipe<S> : AbstractPipe<S, S>, IFilterPipe<S>
    {

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

                var _InternalPipe = _InternalEnumerator as IPipe;

                if (_InternalPipe != null)
                {

                    var _TmpSet     = new HashSet<Object>();
                    var _DoReturn   = true;

                    foreach (var _Object in _InternalPipe.Path)
                    {

                        if (_TmpSet.Contains(_Object))
                        {
                            _DoReturn = false;
                            break;
                        }

                        else
                            _TmpSet.Add(_Object);

                    }

                    if (_DoReturn)
                    {
                        _CurrentElement = _InternalEnumerator.Current;
                        return true;
                    }

                }

                else
                {
                    _CurrentElement = _InternalEnumerator.Current;
                    return true;
                }

            }

            return false;

        }

        #endregion

    }

}
