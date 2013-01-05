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

#endregion

namespace de.ahzf.Styx
{

    /// <summary>
    /// The ObjectFilterPipe will either allow or disallow all objects that pass
    /// through it depending on the result of the compareObject() method.
    /// </summary>
    /// <typeparam name="S">The type of the elements within the filter.</typeparam>
    public class ObjectFilterPipe<S> : AbstractComparisonFilterPipe<S, S>
        where S : IComparable
    {

        #region Data

        private readonly S _Object;

        #endregion

        #region Constructor(s)

        #region ObjectFilterPipe(myObject, myComparisonFilter)

        /// <summary>
        /// Create a new ObjectFilterPipe.
        /// </summary>
        /// <param name="myObject">The Object.</param>
        /// <param name="myComparisonFilter">The filter to use.</param>
        public ObjectFilterPipe(S myObject, ComparisonFilter myComparisonFilter)
            : base(myComparisonFilter)
        {
            _Object = myObject;
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

            while (true)
            {

                if (_InternalEnumerator.MoveNext())
                {

                    var _S = _InternalEnumerator.Current;

                    if (!CompareObjects(_S, _Object))
                    {
                        _CurrentElement = _S;
                        return true;
                    }

                }

                else
                    return false;

            }

        }

        #endregion


        #region ToString()

        /// <summary>
        /// A string representation of this pipe.
        /// </summary>
        public override String ToString()
        {
            return base.ToString() + "<" + _Object + ">";
        }

        #endregion

    }

}
