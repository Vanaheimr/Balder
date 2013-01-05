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
using System.Linq;
using System.Collections.Generic;

#endregion

namespace de.ahzf.Vanaheimr.Styx
{

    /// <summary>
    /// A CollectionFilterPipe will take a collection of objects and
    /// a Filter.NOT_EQUAL or Filter.EQUAL argument.
    /// If an incoming object is contained (or not contained) in the
    /// provided collection, then it is emitted (or not emitted).
    /// </summary>
    /// <typeparam name="S">The type of the elements within the filter.</typeparam>
    public class CollectionFilterPipe<S> : AbstractPipe<S, S>, IFilterPipe<S>, IComparisonFilterPipe<S, S>
        where S : IComparable
    {

        #region Data

        private readonly IEnumerable<S>   _InternalEnumerable;
        private readonly ComparisonFilter _ComparisonFilter;

        #endregion

        #region Constructor(s)

        #region CollectionFilterPipe(myIEnumerable, myComparisonFilter)

        /// <summary>
        /// A CollectionFilterPipe will take a collection of objects and
        /// a Filter.NOT_EQUAL or Filter.EQUAL argument.
        /// If an incoming object is contained (or not contained) in the
        /// provided collection, then it is emitted (or not emitted).
        /// </summary>
        /// <param name="myIEnumerable">The IEnumerable for filtering.</param>
        /// <param name="myComparisonFilter">The ComparisonFilter used for filtering.</param>
        public CollectionFilterPipe(IEnumerable<S> myIEnumerable, ComparisonFilter myComparisonFilter)
        {

            _InternalEnumerable = myIEnumerable;

            if (myComparisonFilter == ComparisonFilter.NOT_EQUAL ||
                myComparisonFilter == ComparisonFilter.EQUAL)
                _ComparisonFilter = myComparisonFilter;

            else
                throw new ArgumentOutOfRangeException("The only legal filters are equals and not equals");

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

            while (true)
            {

                if (_InputEnumerator.MoveNext())
                {

                    var _S = _InputEnumerator.Current;

                    if (CompareObjects(_S))
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


        #region CompareObjects(myLeftObject, myRightObject)

        /// <summary>
        /// Compares two objects.
        /// </summary>
        /// <param name="myLeftObject">The left object.</param>
        /// <param name="myRightObject">The right object.</param>
        /// <returns>A match based on the defined filter.</returns>
        public Boolean CompareObjects(S myLeftObject, S myRightObject)
        {

            if (_ComparisonFilter == ComparisonFilter.NOT_EQUAL)
                if (_InternalEnumerable.Contains(myRightObject))
                    return true;

            else
                if (!_InternalEnumerable.Contains(myRightObject))
                    return true;

            return false;

        }

        #endregion

        #region CompareObjects(myRightObject)

        private Boolean CompareObjects(S myRightObject)
        {

            // NOT_EQUAL
            if (_ComparisonFilter == ComparisonFilter.NOT_EQUAL)
            {

                if (_InternalEnumerable.Contains(myRightObject))
                    return true;

                return false;

            }

            // EQUAL
            else
            {
                
                if (_InternalEnumerable.Contains(myRightObject))
                    return false;
                
                return true;

            }

        }

        #endregion


    }

}
