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
    /// SingleEnumerator is an iterator that only contains one element
    /// of type T. This has applications in various metapipes, where
    /// single objects are manipulated at a time.
    /// </summary>
    /// <typeparam name="T">The type of the stored element.</typeparam>
	public class SingleEnumerator<T> : ISingleEnumerator, IEnumerator<T>
    {

        #region Data

        private readonly T                     _Element;
        private          SingleEnumeratorState _InternalState;
	
		#endregion

        #region Enum SingleEnumeratorState

        /// <summary>
        /// The internal state of the SingleEnumerator&lt;T&gt;.
        /// </summary>
        private enum SingleEnumeratorState
        {

            /// <summary>
            /// Before the element.
            /// </summary>
            BEFORE,

            /// <summary>
            /// At the element.
            /// </summary>
            AT,

            /// <summary>
            /// Behind the element.
            /// </summary>
            BEHIND

        }

        #endregion

		#region Constructor(s)

        #region SingleEnumerator(myElement)

        /// <summary>
        /// Creates a new single element enumerator based on the given element.
        /// </summary>
        /// <param name="myElement">The element within the enumerator.</param>
        public SingleEnumerator(T myElement)
        {
            _Element       = myElement;
            _InternalState = SingleEnumeratorState.BEFORE;
        }

        #endregion

        #endregion


        #region Current

        /// <summary>
        /// Return the current element of the current IEnumertor&lt;T&gt;.
        /// </summary>
		public T Current
		{
			get
			{

                if (_InternalState == SingleEnumeratorState.AT)
                    return _Element;

                throw new InvalidOperationException();
                
			}
		}

        /// <summary>
        /// Return the current element of the internal IEnumertor.
        /// </summary>
		Object System.Collections.IEnumerator.Current
		{	
			get
			{

                if (_InternalState == SingleEnumeratorState.AT)
                    return _Element;

                throw new InvalidOperationException();

			}
		}

        /// <summary>
        /// Return the current element of the internal ISingleEnumerator.
        /// </summary>
        Object ISingleEnumerator.Current
        {
            get
            {

                if (_InternalState == SingleEnumeratorState.AT)
                    return _Element;

                throw new InvalidOperationException();

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

            switch (_InternalState)
            {

                case SingleEnumeratorState.BEFORE:
                    _InternalState = SingleEnumeratorState.AT;
                    return true;

                case SingleEnumeratorState.AT:
                    _InternalState = SingleEnumeratorState.BEHIND;
                    return false;

                case SingleEnumeratorState.BEHIND:
                    return false;

            }

            return false;

		}

        #endregion

        #region Reset()

        /// <summary>
        /// Sets the enumerator to its initial position, which is
        /// before the first element in the collection.
        /// </summary>
        public void Reset()
		{
            _InternalState = SingleEnumeratorState.BEFORE;
		}

        #endregion


        #region Dispose()

        /// <summary>
        /// Dispose this enumerator.
        /// </summary>
        public void Dispose()
        { }

        #endregion


        
    }

}
