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
    /// The DuplicateFilterPipe will not allow a duplicate object to pass through it.
    /// This is accomplished by the Pipe maintaining an internal HashSet that is used
    /// to store a history of previously seen objects.
    /// Thus, the more unique objects that pass through this Pipe, the slower it
    /// becomes as a log_2 index is checked for every object.
    /// </summary>
    public static class DuplicateFilterPipeExtensions
    {

        #region DuplicateFilter(this IEnumerable)

        /// <summary>
        /// The DuplicateFilter will filter duplicate entries from the enumeration.
        /// </summary>
        /// <param name="IEnumerable">An enumeration of objects of type S.</param>
        /// <typeparam name="S">The type of the elements within the filter.</typeparam>
        public static DuplicateFilterPipe<S> DuplicateFilter<S>(this IEnumerable<S> IEnumerable)
        {
            return new DuplicateFilterPipe<S>(IEnumerable);
        }

        #endregion

    }
    
    /// <summary>
    /// The DuplicateFilterPipe will not allow a duplicate object to pass through it.
    /// This is accomplished by the Pipe maintaining an internal HashSet that is used
    /// to store a history of previously seen objects.
    /// Thus, the more unique objects that pass through this Pipe, the slower it
    /// becomes as a log_2 index is checked for every object.
    /// </summary>
    /// <typeparam name="S">The type of the elements within the filter.</typeparam>
    public class DuplicateFilterPipe<S> : AbstractPipe<S, S>, IFilterPipe<S>
    {

        #region Data

        /// <summary>
        /// The internal hashset used as a history of values.
        /// </summary>
        private readonly HashSet<S> _HistorySet;

        #endregion

        #region Constructor(s)

        #region DuplicateFilterPipe(IEnumerable = null, IEnumerator = null)

        /// <summary>
        /// Creates a new DuplicateFilterPipe.
        /// </summary>
        /// <param name="IEnumerable">An optional enumation of directories as element source.</param>
        /// <param name="IEnumerator">An optional enumerator of directories as element source.</param>
        public DuplicateFilterPipe(IEnumerable<S> IEnumerable = null, IEnumerator<S> IEnumerator = null)
            : base(IEnumerable, IEnumerator)
        {
            _HistorySet = new HashSet<S>();
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

                _CurrentElement = _InternalEnumerator.Current;

                if (!_HistorySet.Contains(_CurrentElement))
                {
                    _HistorySet.Add(_CurrentElement);
                    return true;
                }

            }

            return false;

        }

        #endregion

    }

}
