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
    /// The AbstractComparisonFilterPipe provides the necessary functionality
    /// that is required of most ComparisonFilterPipe implementations.
    /// The compareObjects() implementation is useful for comparing two objects
    /// to determine if the current object in the pipe should be filtered.
    /// Depending on the type of ComparisonFilterPipe.Filter used, different
    /// types of comparisons are evaluated.
    /// </summary>
    public abstract class AbstractComparisonFilterPipe<S, T> : AbstractPipe<S, S>, IComparisonFilterPipe<S, T>
        where S : IComparable
        where T : IComparable

    {

        #region Data

        /// <summary>
        /// The filter used for comparing two objects.
        /// </summary>
        protected readonly ComparisonFilter _Filter;

        #endregion

        #region Constructor(s)

        #region AbstractComparisonFilterPipe(myFilter)

        /// <summary>
        /// Creates a new AbstractComparisonFilterPipe using the given filter.
        /// </summary>
        /// <param name="myFilter">The filter used for comparing two objects.</param>
        public AbstractComparisonFilterPipe(ComparisonFilter myFilter)
        {
            _Filter = myFilter;
        }

        #endregion

        #endregion


        #region CompareObjects(myLeftObject, myRightObject)

        /// <summary>
        /// Compares two objects.
        /// </summary>
        /// <param name="myLeftObject">The left object.</param>
        /// <param name="myRightObject">The right object.</param>
        /// <returns>A match based on the defined filter.</returns>
        public Boolean CompareObjects(T myLeftObject, T myRightObject)
        {

            switch (_Filter)
            {

                case ComparisonFilter.EQUAL:
                    if (null == myLeftObject)
                        return myRightObject == null;
                    return myLeftObject.Equals(myRightObject);

                case ComparisonFilter.NOT_EQUAL:
                    if (null == myLeftObject)
                        return myRightObject != null;
                    return !myLeftObject.Equals(myRightObject);

                case ComparisonFilter.GREATER_THAN:
                    if (null == myLeftObject || myRightObject == null)
                        return true;
                    return myLeftObject.CompareTo(myRightObject) == 1;

                case ComparisonFilter.LESS_THAN:
                    if (null == myLeftObject || myRightObject == null)
                        return true;
                    return myLeftObject.CompareTo(myRightObject) == -1;

                case ComparisonFilter.GREATER_THAN_EQUAL:
                    if (null == myLeftObject || myRightObject == null)
                        return true;
                    return myLeftObject.CompareTo(myRightObject) >= 0;

                case ComparisonFilter.LESS_THAN_EQUAL:
                    if (null == myLeftObject || myRightObject == null)
                        return true;
                    return myLeftObject.CompareTo(myRightObject) <= 0;

                default:
                    throw new Exception("Invalid state as no valid filter had been provided!");
            
            }

        }

        #endregion


        #region ToString()

        /// <summary>
        /// A string representation of this filter pipe.
        /// </summary>
        public override String ToString()
        {
            return base.ToString() + "<" + _Filter + ">";
        }

        #endregion

    }

}
