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
    /// A HistoryEnumerator wraps and behaves like a classical IEnumerator.
    /// However, it will remember what was last returned out of the IEnumerator.
    /// </summary>
    /// <typeparam name="T">The type of the stored elements.</typeparam>
	public class HistoryEnumerator<T> : IHistoryEnumerator, IEnumerator<T>
	{
		
		#region Data
		
	    private readonly IEnumerator<T> _InternalEnumerator;
	    private          T              _Last;
        private          Boolean        _FirstMove;
	
		#endregion
		
		#region Constructor(s)

        #region HistoryEnumerator(myIEnumerator)

        /// <summary>
        /// Creates a new HistoryEnumerator based on the given myIEnumerator.
        /// </summary>
        /// <param name="myIEnumerator">The enumerator to be wrapped.</param>
        public HistoryEnumerator(IEnumerator<T> myIEnumerator)
        {
            _InternalEnumerator = myIEnumerator;
            _Last               = default(T);
            _FirstMove          = true;
	    }

        #endregion

        #endregion


        #region Current

        /// <summary>
        /// Return the current element of the internal IEnumertor.
        /// </summary>
		public T Current
		{
			get
			{
				return _InternalEnumerator.Current;
			}
		}

        /// <summary>
        /// Return the current element of the internal IEnumertor.
        /// </summary>
		Object System.Collections.IEnumerator.Current
		{	
			get
			{
                return _InternalEnumerator.Current;
			}
		}

        #endregion

        #region Last

        /// <summary>
        /// Return the last element of the internal IEnumertor&lt;T&gt;.
        /// </summary>
        public T Last
        {
            get
            {
                return _Last;
            }
        }

        /// <summary>
        /// Return the last element of the internal IEnumertor.
        /// </summary>
        Object IHistoryEnumerator.Last
        {
            get
            {
                return _Last;
            }
        }

        #endregion

        #region MoveNext()

        /// <summary>
        /// Advances the enumerator to the next element of the collection.
        /// </summary>
        /// <returns>True if the enumerator was successfully advanced to the next element; false if the enumerator has passed the end of the collection.</returns>
		public Boolean MoveNext()
		{

            if (!_FirstMove)
                _Last = _InternalEnumerator.Current;
            else
                _FirstMove = false;

			return _InternalEnumerator.MoveNext();

		}

        #endregion

        #region Reset()

        /// <summary>
        /// Sets the enumerator to its initial position, which is
        /// before the first element in the collection.
        /// </summary>
        public void Reset()
		{
            //_InternalEnumerator.Reset();
            _Last = default(T);
		}

        #endregion


        #region Dispose()

        /// <summary>
        /// Dispose this enumerator.
        /// </summary>
        public void Dispose()
        {
            _InternalEnumerator.Dispose();
        }

        #endregion
	
	}

}
